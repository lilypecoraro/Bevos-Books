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
            var baseCosts = await _context.Books
                .Select(b => new { b.BookID, b.Cost })
                .ToDictionaryAsync(x => x.BookID, x => x.Cost);

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

            var result = new Dictionary<int, decimal>();

            foreach (var book in baseCosts)
            {
                var bookID = book.Key;
                var baseCost = book.Value;

                var supplierRecord = supplierDetails.FirstOrDefault(x => x.BookID == bookID);

                if (supplierRecord == null || supplierRecord.TotalQty == 0)
                {
                    result[bookID] = baseCost;
                }
                else
                {
                    decimal startingCostContribution = baseCost * 1;
                    decimal startingQty = 1;

                    decimal totalCost = startingCostContribution + supplierRecord.TotalCost;
                    decimal qty = startingQty + supplierRecord.TotalQty;

                    result[bookID] = totalCost / qty;
                }
            }

            return result;
        }

        private IQueryable<OrderDetail> SalesQuery()
        {
            return _context.OrderDetails
                .Include(od => od.Order).ThenInclude(o => o.User)
                .Include(od => od.Book)
                .Where(od => od.Order.OrderStatus == "Ordered");
        }

        // ========= A. Books Sold =========
        public async Task<IActionResult> BooksSold(
            DateTime? StartDate,
            DateTime? EndDate,
            decimal? MinPrice,
            decimal? MaxPrice,
            // NEW FILTERS
            decimal? MinAvgCost,
            decimal? MaxAvgCost,
            decimal? MinProfit,
            decimal? MaxProfit,
            int? BookId,
            int? CustomerId,
            string? BookName,
            string? CustomerName,
            string sort = "recent")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = SalesQuery();

            // Date filters (EndDate inclusive)
            if (StartDate.HasValue)
                q = q.Where(od => od.Order.OrderDate >= StartDate.Value);

            if (EndDate.HasValue)
            {
                var endExclusive = EndDate.Value.Date.AddDays(1);
                q = q.Where(od => od.Order.OrderDate < endExclusive);
            }

            // Price filters
            if (MinPrice.HasValue)
                q = q.Where(od => od.Price >= MinPrice.Value);

            if (MaxPrice.HasValue)
                q = q.Where(od => od.Price <= MaxPrice.Value);

            // ID filters
            if (BookId.HasValue)
                q = q.Where(od => od.BookID == BookId.Value);

            if (CustomerId.HasValue)
                q = q.Where(od => od.Order.UserID == CustomerId.Value.ToString());

            // Name-based filters (case-insensitive)
            if (!string.IsNullOrWhiteSpace(BookName))
            {
                var bookNameLower = BookName.Trim().ToLower();
                q = q.Where(od => od.Book.Title.ToLower().Contains(bookNameLower));
            }

            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                var customerNameLower = CustomerName.Trim().ToLower();
                q = q.Where(od =>
                    (od.Order.User.FirstName + " " + od.Order.User.LastName)
                        .ToLower()
                        .Contains(customerNameLower));
            }

            // Project rows (avg cost and profit computed in memory using the dictionary)
            var items = await q.Select(od => new BookSaleRowVM
            {
                BookID = od.BookID,
                Title = od.Book.Title,
                Quantity = od.Quantity,
                OrderID = od.OrderID,
                CustomerId = od.Order.UserID,
                CustomerName = od.Order.User.FirstName + " " + od.Order.User.LastName,
                SellingPrice = od.Price,
                AverageCost = avgCost.ContainsKey(od.BookID) ? avgCost[od.BookID] : 0m,
                ProfitMargin = (od.Price)
                             - ((avgCost.ContainsKey(od.BookID) ? avgCost[od.BookID] : 0m)),
                OrderDate = od.Order.OrderDate
            }).ToListAsync();

            // Apply AvgCost and Profit filters on the materialized list
            if (MinAvgCost.HasValue)
                items = items.Where(i => i.AverageCost >= MinAvgCost.Value).ToList();

            if (MaxAvgCost.HasValue)
                items = items.Where(i => i.AverageCost <= MaxAvgCost.Value).ToList();

            if (MinProfit.HasValue)
                items = items.Where(i => i.ProfitMargin >= MinProfit.Value).ToList();

            if (MaxProfit.HasValue)
                items = items.Where(i => i.ProfitMargin <= MaxProfit.Value).ToList();

            // Sorting
            items = sort switch
            {
                "profitAsc" => items.OrderBy(i => i.ProfitMargin).ToList(),
                "profitDesc" => items.OrderByDescending(i => i.ProfitMargin).ToList(),
                "priceAsc" => items.OrderBy(i => i.SellingPrice).ToList(),
                "priceDesc" => items.OrderByDescending(i => i.SellingPrice).ToList(),
                "popular" => items.OrderByDescending(i => i.Quantity).ToList(),
                _ => items.OrderByDescending(i => i.OrderDate).ToList()
            };

            // Build VM including new filters
            var vm = new BooksSoldReportVM
            {
                Rows = items,
                RecordCount = items.Count(),
                CurrentSort = sort,
                Filter = new ReportFilterVM
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    MinPrice = MinPrice,
                    MaxPrice = MaxPrice,
                    MinAvgCost = MinAvgCost,
                    MaxAvgCost = MaxAvgCost,
                    MinProfit = MinProfit,
                    MaxProfit = MaxProfit,
                    CustomerName = CustomerName,
                    BookName = BookName
                }
            };

            return View(vm);
        }

        // ========= B. Orders Report =========
        [HttpGet]
        public async Task<IActionResult> OrdersReport(
            DateTime? StartDate,
            DateTime? EndDate,
            decimal? MinAvgCost,
            decimal? MaxAvgCost,
            decimal? MinProfit,
            decimal? MaxProfit,
            decimal? MinRevenue,
            decimal? MaxRevenue,
            string? CustomerName,
            string sort = "recent")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = SalesQuery();

            if (StartDate.HasValue)
                q = q.Where(od => od.Order.OrderDate >= StartDate.Value);

            if (EndDate.HasValue)
            {
                var endExclusive = EndDate.Value.Date.AddDays(1);
                q = q.Where(od => od.Order.OrderDate < endExclusive);
            }

            // Name-based filter (case-insensitive), modeled after BooksSold
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                var customerNameLower = CustomerName.Trim().ToLower();
                q = q.Where(od =>
                    (od.Order.User.FirstName + " " + od.Order.User.LastName)
                        .ToLower()
                        .Contains(customerNameLower));
            }

            var list = await q.ToListAsync();

            var grouped = list
                .GroupBy(od => od.OrderID)
                .Select(g =>
                {
                    var revenue = g.Sum(x => x.Price * x.Quantity);
                    var cost = g.Sum(x => (avgCost.ContainsKey(x.BookID) ? avgCost[x.BookID] : 0m) * x.Quantity);
                    var profit = revenue - cost;
                    var margin = revenue > 0 ? profit / revenue : 0m;

                    return new OrderReportRowVM
                    {
                        OrderID = g.Key,
                        OrderDate = g.Max(x => x.Order.OrderDate),
                        CustomerName = g.Max(x => x.Order.User.FirstName + " " + x.Order.User.LastName),
                        TotalQuantity = g.Sum(x => x.Quantity),
                        OrderRevenue = revenue,
                        OrderCost = cost,
                        OrderProfit = profit,
                        OrderMargin = margin
                    };
                })
                .ToList();

            if (MinAvgCost.HasValue)
                grouped = grouped.Where(r => r.OrderCost >= MinAvgCost.Value).ToList();
            if (MaxAvgCost.HasValue)
                grouped = grouped.Where(r => r.OrderCost <= MaxAvgCost.Value).ToList();
            if (MinProfit.HasValue)
                grouped = grouped.Where(r => r.OrderProfit >= MinProfit.Value).ToList();
            if (MaxProfit.HasValue)
                grouped = grouped.Where(r => r.OrderProfit <= MaxProfit.Value).ToList();

            if (MinRevenue.HasValue)
                grouped = grouped.Where(r => r.OrderRevenue >= MinRevenue.Value).ToList();
            if (MaxRevenue.HasValue)
                grouped = grouped.Where(r => r.OrderRevenue <= MaxRevenue.Value).ToList();

            grouped = sort switch
            {
                "marginAsc" => grouped.OrderBy(r => r.OrderMargin).ToList(),
                "marginDesc" => grouped.OrderByDescending(r => r.OrderMargin).ToList(),
                "priceAsc" => grouped.OrderBy(r => r.OrderRevenue).ToList(),
                "priceDesc" => grouped.OrderByDescending(r => r.OrderRevenue).ToList(),
                _ => grouped.OrderByDescending(r => r.OrderDate).ToList()
            };

            var vm = new OrdersReportVM
            {
                Rows = grouped,
                RecordCount = grouped.Count(),
                CurrentSort = sort,
                Filter = new ReportFilterVM
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    MinAvgCost = MinAvgCost,
                    MaxAvgCost = MaxAvgCost,
                    MinProfit = MinProfit,
                    MaxProfit = MaxProfit,
                    MinRevenue = MinRevenue,
                    MaxRevenue = MaxRevenue,
                    CustomerName = CustomerName
                }
            };

            return View(vm);
        }

        // ========= C. Customers Report =========
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CustomersReport(
            DateTime? StartDate,
            DateTime? EndDate,
            decimal? MinAvgCost,
            decimal? MaxAvgCost,
            decimal? MinProfit,
            decimal? MaxProfit,
            decimal? MinRevenue,
            decimal? MaxRevenue,
            string? CustomerName,
            string sort = "profitDesc")
        {
            var avgCost = await GetWeightedAverageCostByBook();
            var q = SalesQuery();

            // Date range (EndDate inclusive)
            if (StartDate.HasValue)
                q = q.Where(od => od.Order.OrderDate >= StartDate.Value);

            if (EndDate.HasValue)
            {
                var endExclusive = EndDate.Value.Date.AddDays(1);
                q = q.Where(od => od.Order.OrderDate < endExclusive);
            }

            // Name-based filter (case-insensitive), like OrdersReport/BooksSold
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                var customerNameLower = CustomerName.Trim().ToLower();
                q = q.Where(od =>
                    (od.Order.User.FirstName + " " + od.Order.User.LastName)
                        .ToLower()
                        .Contains(customerNameLower));
            }

            var list = await q.ToListAsync();

            var grouped = list
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

            // Apply numeric filters on the materialized list (same as other reports)
            if (MinAvgCost.HasValue)
                grouped = grouped.Where(r => r.Cost >= MinAvgCost.Value).ToList();
            if (MaxAvgCost.HasValue)
                grouped = grouped.Where(r => r.Cost <= MaxAvgCost.Value).ToList();
            if (MinProfit.HasValue)
                grouped = grouped.Where(r => r.Profit >= MinProfit.Value).ToList();
            if (MaxProfit.HasValue)
                grouped = grouped.Where(r => r.Profit <= MaxProfit.Value).ToList();
            if (MinRevenue.HasValue)
                grouped = grouped.Where(r => r.Revenue >= MinRevenue.Value).ToList();
            if (MaxRevenue.HasValue)
                grouped = grouped.Where(r => r.Revenue <= MaxRevenue.Value).ToList();

            // Sorting
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
                CurrentSort = sort,
                Filter = new ReportFilterVM
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    MinAvgCost = MinAvgCost,
                    MaxAvgCost = MaxAvgCost,
                    MinProfit = MinProfit,
                    MaxProfit = MaxProfit,
                    MinRevenue = MinRevenue,
                    MaxRevenue = MaxRevenue,
                    CustomerName = CustomerName
                }
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
        [HttpGet] // optional, just explicit
        public async Task<IActionResult> CurrentInventory(
            decimal? MinAvgCost,
            decimal? MaxAvgCost,
            int? MinInventoryQty,
            int? MaxInventoryQty,
            string sort = "title")
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

            // Apply filters
            if (MinAvgCost.HasValue)
                rows = rows.Where(r => r.AverageCost >= MinAvgCost.Value).ToList();
            if (MaxAvgCost.HasValue)
                rows = rows.Where(r => r.AverageCost <= MaxAvgCost.Value).ToList();
            if (MinInventoryQty.HasValue)
                rows = rows.Where(r => r.InventoryQuantity >= MinInventoryQty.Value).ToList();
            if (MaxInventoryQty.HasValue)
                rows = rows.Where(r => r.InventoryQuantity <= MaxInventoryQty.Value).ToList();

            // Sorting
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
                CurrentSort = sort,
                Filter = new ReportFilterVM
                {
                    MinAvgCost = MinAvgCost,
                    MaxAvgCost = MaxAvgCost,
                    MinInventoryQty = MinInventoryQty,
                    MaxInventoryQty = MaxInventoryQty
                }
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
