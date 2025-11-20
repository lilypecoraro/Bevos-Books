using System;
using System.Collections.Generic;
using System.Linq;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedOrders
    {
        public static void SeedAllOrders(AppDbContext db)
        {
            Int32 ordersAdded = 0;
            Int32 detailsAdded = 0;
            string current = "START";

            if (db.Orders.Any()) return;

            // Helper: get UserID from First + Last name
            string User(string fullName)
            {
                var parts = fullName.Split(' ');
                string first = parts[0].Trim();
                string last = parts[1].Trim();

                var u = db.Users.FirstOrDefault(x =>
                    x.FirstName == first && x.LastName == last);

                if (u == null)
                    throw new Exception($"No AppUser found for name: {fullName}");

                return u.Id;
            }

            // Helper: book lookup
            int Book(string title)
            {
                var b = db.Books.FirstOrDefault(x => x.Title == title);
                if (b == null)
                    throw new Exception($"No Book found for title: {title}");
                return b.BookID;
            }

            // Helper: CardID lookup
            int? Card(int? cardId)
            {
                if (cardId == null) return null;
                return db.Cards.FirstOrDefault(c => c.CardID == cardId)?.CardID;
            }

            // ================================
            // Build your Order + OrderDetail rows
            // Copied EXACTLY from your Excel Screenshot
            // ================================

            var rows = new List<dynamic>
            {
                new { OrderID=10001, Customer="Christopher Baker", CardID=(int?)null, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=23.95m, OrderStatus="InCart", Book="The Art Of Racing In The Rain", Price=23.95m, Cost=10.30m, Qty=3 },
                new { OrderID=10001, Customer="Christopher Baker", CardID=(int?)null, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=25.99m, OrderStatus="InCart", Book="The Host", Price=25.99m, Cost=13.25m, Qty=1 },

                new { OrderID=10002, Customer="Todd Jacobs", CardID=(int?)null, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=24.99m, OrderStatus="InCart", Book="Roses", Price=24.99m, Cost=20.99m, Qty=2 },

                new { OrderID=10003, Customer="Charles Miller", CardID=(int?)null, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=27.99m, OrderStatus="InCart", Book="Alter of Eden", Price=27.99m, Cost=25.75m, Qty=2 },

                new { OrderID=10004, Customer="Wendy Chang", CardID=1004, CouponID=(int?)null, OrderDate=new DateTime(2025,10,31), ShippingFee=3.5m, OrderStatus="Ordered", Book="The Professional", Price=26.95m, Cost=7.01m, Qty=1 },

                new { OrderID=10005, Customer="Christopher Baker", CardID=1002, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=9.5m, OrderStatus="Ordered", Book="Say Goodbye", Price=25.00m, Cost=11.25m, Qty=5 },
                new { OrderID=10005, Customer="Christopher Baker", CardID=1002, CouponID=(int?)null, OrderDate=new DateTime(2025,10,1), ShippingFee=3.5m, OrderStatus="Ordered", Book="Chasing Darkness", Price=25.95m, Cost=9.08m, Qty=1 },

                new { OrderID=10006, Customer="Lim Chou", CardID=1005, CouponID=(int?)null, OrderDate=new DateTime(2025,10,5), ShippingFee=107m, OrderStatus="Ordered", Book="The Other Queen", Price=25.95m, Cost=23.61m, Qty=1 },
                new { OrderID=10006, Customer="Lim Chou", CardID=1005, CouponID=(int?)null, OrderDate=new DateTime(2025,10,5), ShippingFee=25m, OrderStatus="Ordered", Book="Wrecked", Price=25.00m, Cost=18.00m, Qty=1 },
                new { OrderID=10006, Customer="Lim Chou", CardID=1005, CouponID=(int?)null, OrderDate=new DateTime(2025,10,5), ShippingFee=36.5m, OrderStatus="Ordered", Book="Reckless", Price=22.00m, Cost=9.46m, Qty=23 },

                new { OrderID=10007, Customer="Wendy Chang", CardID=1004, CouponID=(int?)null, OrderDate=new DateTime(2025,10,30), ShippingFee=3.5m, OrderStatus="Ordered", Book="The Professional", Price=26.95m, Cost=7.01m, Qty=1 },

                new { OrderID=10008, Customer="Jeffrey Hampton", CardID=1006, CouponID=(int?)null, OrderDate=new DateTime(2025,11,1), ShippingFee=5m, OrderStatus="Ordered", Book="The Professional", Price=26.95m, Cost=7.01m, Qty=1 },

                new { OrderID=10009, Customer="Charles Miller", CardID=1007, CouponID=(int?)null, OrderDate=new DateTime(2025,11,3), ShippingFee=3.5m, OrderStatus="Ordered", Book="Say Goodbye", Price=25.00m, Cost=11.25m, Qty=1 },

                new { OrderID=10010, Customer="Ernest Lowe", CardID=1008, CouponID=(int?)null, OrderDate=new DateTime(2025,11,2), ShippingFee=6.5m, OrderStatus="Ordered", Book="Wrecked", Price=25.00m, Cost=18.00m, Qty=1 },
                new { OrderID=10010, Customer="Ernest Lowe", CardID=1008, CouponID=(int?)null, OrderDate=new DateTime(2025,11,2), ShippingFee=18.5m, OrderStatus="Ordered", Book="Reckless", Price=22.00m, Cost=9.46m, Qty=11 }
            };

            // Group by OrderID
            var orderGroups = rows.GroupBy(r => r.OrderID);

            using var transaction = db.Database.BeginTransaction();

            try
            {
                // Turn ON identity insert
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Orders ON");

                foreach (var group in orderGroups)
                {
                    int orderId = group.Key;
                    current = $"ORDER {orderId}";

                    var first = group.First();

                    Order o = new Order
                    {
                        OrderID = orderId,
                        UserID = User(first.Customer),
                        OrderDate = first.OrderDate,
                        ShippingFee = first.ShippingFee,
                        OrderStatus = first.OrderStatus
                    };

                    db.Orders.Add(o);
                    db.SaveChanges();
                    ordersAdded++;

                    // Add order details
                    foreach (var row in group)
                    {
                        OrderDetail d = new OrderDetail
                        {
                            OrderID = orderId,
                            BookID = Book(row.Book),
                            CardID = row.CardID,
                            CouponID = row.CouponID,
                            Quantity = row.Qty,
                            Price = row.Price,
                            Cost = row.Cost
                        };

                        db.OrderDetails.Add(d);
                        db.SaveChanges();
                        detailsAdded++;
                    }
                }

                // Turn OFF identity insert
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Orders OFF");

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(
                    $"FAILED inserting Orders at {current} | Orders Added: {ordersAdded} | Details Added: {detailsAdded} | {ex.Message}"
                );
            }
        }
    }
}
