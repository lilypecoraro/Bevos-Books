using System;
using System.Linq;
using System.Collections.Generic;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedOrders
    {
        public static void SeedAllOrders(AppDbContext db)
        {
            // Prevent duplicate seeding
            if (db.Orders.Any()) return;

            // -------- Helper: get UserID from email --------
            string UID(string email)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (user == null) throw new Exception($"❌ No AppUser found for: {email}");
                return user.Id;
            }

            // -------- Helper: get BookID from title --------
            int BID(string title)
            {
                var book = db.Books.FirstOrDefault(b => b.Title == title);
                if (book == null) throw new Exception($"❌ No Book found for: {title}");
                return book.BookID;
            }

            // -------- Helper to add an order --------
            void AddOrder(
                int orderId,
                string email,
                DateTime date,
                decimal shipping,
                string status,
                List<(string title, int qty, decimal price, decimal cost, int? card)> lines)
            {
                var order = new Order
                {
                    OrderID = orderId,            // OPTION B: YOU supply the ID
                    UserID = UID(email),
                    OrderDate = date,
                    ShippingFee = shipping,
                    OrderStatus = status
                };

                db.Orders.Add(order);
                db.SaveChanges(); // commit order BEFORE adding details

                foreach (var item in lines)
                {
                    db.OrderDetails.Add(new OrderDetail
                    {
                        OrderID = orderId,
                        BookID = BID(item.title),
                        Quantity = item.qty,
                        Price = item.price,
                        Cost = item.cost,
                        CardID = item.card
                    });
                }

                db.SaveChanges();
            }

            // ======================================================
            //                       SEEDING
            // ======================================================

            // ----- IN CART ORDERS -----
            AddOrder(
                10001,
                "cbaker@example.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("The Art Of Racing In The Rain", 2, 23.95m, 10.30m, null),
                    ("The Host", 1, 25.99m, 13.25m, null)
                }
            );

            AddOrder(
                10002,
                "toddj@yourmom.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("Roses", 2, 24.99m, 20.99m, null)
                }
            );

            AddOrder(
                10003,
                "cmiller@bob.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("Altar of Eden", 1, 27.99m, 25.75m, null)
                }
            );

            AddOrder(
                10011,
                "knelson@aoll.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("The Cast", 1, 21.95m, 12.95m, null)
                }
            );

            AddOrder(
                10012,
                "cluce@gogle.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("Brooklyn", 1, 18.95m, 3.60m, null)
                }
            );

            AddOrder(
                10013,
                "erynrice@aoll.com",
                DateTime.Now,
                0m,
                "InCart",
                new()
                {
                    ("Dexter By Design", 1, 25.00m, 2.75m, null),
                    ("The Midnight House", 3, 25.95m, 3.11m, null)
                }
            );

            // ----- ACTUAL ORDERS -----
            AddOrder(
                10004,
                "wchang@example.com",
                DateTime.Parse("2025-10-31"),
                3.50m,
                "Ordered",
                new()
                {
                    ("The Professional", 1, 26.95m, 7.01m, 1004)
                }
            );

            AddOrder(
                10005,
                "cbaker@example.com",
                DateTime.Parse("2025-10-01"),
                9.50m,
                "Ordered",
                new()
                {
                    ("Say Goodbye", 5, 25.00m, 11.25m, 1002),
                    ("Chasing Darkness", 1, 25.95m, 9.08m, 1002)
                }
            );

            AddOrder(
                10006,
                "limchou@gogle.com",
                DateTime.Parse("2025-10-05"),
                107m,
                "Ordered",
                new()
                {
                    ("The Other Queen", 70, 25.95m, 23.61m, 1005),
                    ("Wrecked", 10, 25.00m, 18.00m, 1005),
                    ("Reckless", 23, 22.00m, 9.46m, 1005)
                }
            );

            AddOrder(
                10007,
                "wchang@example.com",
                DateTime.Parse("2025-10-30"),
                3.50m,
                "Ordered",
                new()
                {
                    ("The Professional", 1, 26.95m, 7.01m, 1004)
                }
            );

            AddOrder(
                10008,
                "jeffh@sonic.com",
                DateTime.Parse("2025-11-01"),
                5m,
                "Ordered",
                new()
                {
                    ("The Professional", 2, 26.95m, 7.01m, 1006)
                }
            );

            AddOrder(
                10009,
                "cmiller@bob.com",
                DateTime.Parse("2025-11-03"),
                3.50m,
                "Ordered",
                new()
                {
                    ("Say Goodbye", 1, 25.00m, 11.25m, 1007)
                }
            );

            AddOrder(
                10010,
                "elowe@netscare.net",
                DateTime.Parse("2025-11-02"),
                6.50m,
                "Ordered",
                new()
                {
                    ("Wrecked", 3, 25.00m, 18.00m, 1008),
                    ("Reckless", 11, 22.00m, 9.46m, 1008)
                }
            );
        }
    }
}
