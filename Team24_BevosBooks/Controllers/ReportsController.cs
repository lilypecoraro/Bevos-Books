using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.ViewModels;

namespace Team24_BevosBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // Weighted average cost per book from supplier orders (OrderStatus == "Received")
        private async Task<Dictionary<int, decimal>> GetWeightedAverageCostByBook()
        {
            // 1. Load base cost from the Book model
            var baseCosts = await _context.Books
                .Select(b => new { b.BookID, b.Cost })
                .ToDictionaryAsync(x => x.BookID, x => x.Cost);

            // 2. Load all received supplier orders
            var supplierDetails = await _context.OrderDetails
                .Include(od => od.Order)
                .Where(od => od.Order.OrderStatus == "Received")
                .GroupBy(od => od.BookID)
                .Select(g => new
                {
                    BookID = g.Key,
                    TotalCost = g.Sum(x => x.Cost * x.Quantity),
                    TotalQty = g.Sum(x => x.Quantity)
                })
                .ToListAsync();

            // 3. Build weighted averages  
            var result = new Dictionary<int, decimal>();

            foreach (var book in baseCosts)
            {
                var bookID = book.Key;
                var baseCost = book.Value;

                var supplierRecord = supplierDetails.FirstOrDefault(x => x.BookID == bookID);

                if (supplierRecord == null || supplierRecord.TotalQty == 0)
                {
                    // No supplier orders: use seeded cost
                    result[bookID] = baseCost;
                }
                else
                {
                    // Weighted average = (baseCost + supplierCosts) / totalQty
                    // Assume starting inventory = 1 unit of base cost unless you want to track differently
                    decimal startingCostContribution = baseCost * 1;
                    decimal startingQty = 1;

                    decimal totalCost = startingCostContribution + supplierRecord.TotalCost;
                    decimal qty = startingQty + supplierRecord.TotalQty;

                    result[bookID] = totalCost / qty;
                }
            }

            return result;
        }


        // Common sales query (customer orders only)
        private IQueryable<OrderDetail> SalesQuery()
        {
            return _context.OrderDetails
                .Include(od => od.Order).ThenInclude(o => o.User)
                .Include(od => od.Book)
                .Where(od => od.Order.OrderStatus == "Completed");
        }

        // ========= A. Books Sold =========
        public async Task<IActionResult> BooksSold(string sort = "recent")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = SalesQuery();

            var items = await q.Select(od => new BookSaleRowVM
            {
                BookID = od.BookID,
                Title = od.Book.Title,
                Quantity = od.Quantity,
                OrderID = od.OrderID,
                CustomerName = od.Order.User.FirstName + " " + od.Order.User.LastName,
                SellingPrice = od.Price,
                AverageCost = avgCost.ContainsKey(od.BookID) ? avgCost[od.BookID] : 0m,
                ProfitMargin = (od.Price * od.Quantity) - ((avgCost.ContainsKey(od.BookID) ? avgCost[od.BookID] : 0m) * od.Quantity),
                OrderDate = od.Order.OrderDate
            }).ToListAsync();

            items = sort switch
            {
                "profitAsc" => items.OrderBy(i => i.ProfitMargin).ToList(),
                "profitDesc" => items.OrderByDescending(i => i.ProfitMargin).ToList(),
                "priceAsc" => items.OrderBy(i => i.SellingPrice).ToList(),
                "priceDesc" => items.OrderByDescending(i => i.SellingPrice).ToList(),
                "popular" => items.OrderByDescending(i => i.Quantity).ToList(),
                _ => items.OrderByDescending(i => i.OrderDate).ToList()
            };

            var vm = new BooksSoldReportVM
            {
                Rows = items,
                RecordCount = items.Count(),
                CurrentSort = sort
            };

            return View(vm);
        }

        // ========= B. Orders Report =========
        public async Task<IActionResult> OrdersReport(string sort = "recent")
        {
            var avgCost = await GetWeightedAverageCostByBook();

            var q = await SalesQuery().ToListAsync();

            var grouped = q
                .GroupBy(od => od.OrderID)
                .Select(g => new OrderReportRowVM
                {
                    OrderID = g.Key,
                    OrderDate = g.Max(x => x.Order.OrderDate),
                    CustomerName = g.Max(x => x.Order.User.FirstName + " " + x.Order.User.LastName),
                    TotalQuantity = g.Sum(x => x.Quantity),
                    OrderRevenue = g.Sum(x => x.Price * x.Quantity),
                    OrderCost = g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity),
                    OrderProfit = g.Sum(x => x.Price * x.Quantity) -
                                  g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity),
                    OrderMargin = g.Sum(x => x.Price * x.Quantity) > 0
                        ? (g.Sum(x => x.Price * x.Quantity) - g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity))
                          / g.Sum(x => x.Price * x.Quantity)
                        : 0m
                })
                .ToList();

            grouped = sort switch
            {
                "profitAsc" => grouped.OrderBy(r => r.OrderProfit).ToList(),
                "profitDesc" => grouped.OrderByDescending(r => r.OrderProfit).ToList(),
                "priceAsc" => grouped.OrderBy(r => r.OrderRevenue).ToList(),
                "priceDesc" => grouped.OrderByDescending(r => r.OrderRevenue).ToList(),
                _ => grouped.OrderByDescending(r => r.OrderDate).ToList()
            };

            var vm = new OrdersReportVM
            {
                Rows = grouped,
                RecordCount = grouped.Count(),
                CurrentSort = sort
            };

            return View(vm);
        }

        // ========= C. Customers Report =========
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CustomersReport(string sort = "profitDesc")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = await SalesQuery().ToListAsync();

            var grouped = q
                .GroupBy(od => od.Order.UserID)
                .Select(g => new CustomerReportRowVM
                {
                    CustomerId = g.Key,
                    CustomerName = g.Max(x => x.Order.User.FirstName + " " + x.Order.User.LastName),
                    TotalQuantity = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Price * x.Quantity),
                    Cost = g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity),
                    Profit = g.Sum(x => x.Price * x.Quantity) -
                             g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity),
                    Margin = g.Sum(x => x.Price * x.Quantity) > 0
                        ? (g.Sum(x => x.Price * x.Quantity) - g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity))
                          / g.Sum(x => x.Price * x.Quantity)
                        : 0m
                })
                .ToList();

            grouped = sort switch
            {
                "profitAsc" => grouped.OrderBy(r => r.Profit).ToList(),
                "profitDesc" => grouped.OrderByDescending(r => r.Profit).ToList(),
                "revenueAsc" => grouped.OrderBy(r => r.Revenue).ToList(),
                "revenueDesc" => grouped.OrderByDescending(r => r.Revenue).ToList(),
                _ => grouped.OrderByDescending(r => r.Profit).ToList()
            };

            var vm = new CustomersReportVM
            {
                Rows = grouped,
                RecordCount = grouped.Count(),
                CurrentSort = sort
            };

            return View(vm);
        }

        // ========= D. Totals =========
        public async Task<IActionResult> Totals()
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = SalesQuery();

            var rows = await q.Select(od => new
            {
                Revenue = od.Price * od.Quantity,
                Cost = (avgCost.ContainsKey(od.BookID) ? avgCost[od.BookID] : 0m) * od.Quantity
            }).ToListAsync();

            var totalRevenue = rows.Sum(x => x.Revenue);
            var totalCost = rows.Sum(x => x.Cost);
            var totalProfit = totalRevenue - totalCost;
            var totalMargin = totalRevenue > 0 ? totalProfit / totalRevenue : 0m;

            var vm = new TotalsReportVM
            {
                TotalRevenue = totalRevenue,
                TotalCost = totalCost,
                TotalProfit = totalProfit,
                TotalMargin = totalMargin,
                RecordCount = rows.Count()
            };
            return View(vm);
        }

        // ========= E. Current Inventory =========
        public async Task<IActionResult> CurrentInventory(string sort = "title")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var books = await _context.Books.ToListAsync();

            var rows = books.Select(b => new InventoryRowVM
            {
                BookID = b.BookID,
                Title = b.Title,
                InventoryQuantity = b.InventoryQuantity,
                AverageCost = avgCost.ContainsKey(b.BookID) ? avgCost[b.BookID] : 0m
            }).ToList();

            // Sorting options
            rows = sort switch
            {
                "qtyAsc" => rows.OrderBy(r => r.InventoryQuantity).ToList(),
                "qtyDesc" => rows.OrderByDescending(r => r.InventoryQuantity).ToList(),
                "costAsc" => rows.OrderBy(r => r.AverageCost).ToList(),
                "costDesc" => rows.OrderByDescending(r => r.AverageCost).ToList(),
                _ => rows.OrderBy(r => r.Title).ToList()
            };

            var vm = new InventoryReportVM
            {
                Rows = rows,
                TotalInventoryValue = rows.Sum(r => r.AverageCost * r.InventoryQuantity),
                RecordCount = rows.Count(),
                CurrentSort = sort
            };

            return View(vm);
        }
        // ========= F. Reviews Report =========
        public async Task<IActionResult> ReviewsReport(string sort = "empAsc")
        {
            var rows = await _context.Reviews
                .Where(r => !string.IsNullOrEmpty(r.ApproverID)) // filter out null/empty IDs
                .GroupBy(r => r.ApproverID)
                .Select(g => new ReviewsRowVM
                {
                    EmployeeId = g.Key,
                    EmployeeName = _context.Users
                        .Where(u => u.Id == g.Key)
                        .Select(u => u.FirstName + " " + u.LastName)
                        .FirstOrDefault() ?? "Unknown",
                    ApprovedCount = g.Count(x => x.DisputeStatus == "Approved"),
                    RejectedCount = g.Count(x => x.DisputeStatus == "Rejected")
                })
                .ToListAsync();

            // Sorting options
            rows = sort switch
            {
                "approvedAsc" => rows.OrderBy(r => r.ApprovedCount).ToList(),
                "approvedDesc" => rows.OrderByDescending(r => r.ApprovedCount).ToList(),
                "rejectedAsc" => rows.OrderBy(r => r.RejectedCount).ToList(),
                "rejectedDesc" => rows.OrderByDescending(r => r.RejectedCount).ToList(),
                _ => rows.OrderBy(r => r.EmployeeId).ToList()
            };

            var vm = new ReviewsReportVM
            {
                Rows = rows,
                RecordCount = rows.Count(),
                CurrentSort = sort
            };

            return View(vm);
        }
    }
}