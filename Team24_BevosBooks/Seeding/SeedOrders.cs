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
            // Prevent double-seeding
            if (db.Orders.Any()) return;

            // -------- Helper mappers --------
            string UID(string email)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (user == null) throw new Exception($"❌ No AppUser found for: {email}");
                return user.Id;
            }

            int BID(string title)
            {
                var book = db.Books.FirstOrDefault(b => b.Title == title);
                if (book == null) throw new Exception($"❌ No Book found for: {title}");
                return book.BookID;
            }

            // -------- Helper to add Order + its details --------
            void AddOrder(string email, DateTime? date, decimal shipping, string status,
                          List<(string title, int qty, decimal price, decimal cost, int? card)> lines)
            {
                var order = new Order
                {
                    UserID = UID(email),
                    OrderDate = date ?? DateTime.Now,
                    ShippingFee = shipping,
                    OrderStatus = status
                };

                db.Orders.Add(order);
                db.SaveChanges(); // order now has OrderID

                foreach (var item in lines)
                {
                    db.OrderDetails.Add(new OrderDetail
                    {
                        OrderID = order.OrderID,
                        BookID = BID(item.title),
                        Quantity = item.qty,
                        Price = item.price,
                        Cost = item.cost,
                        CardID = item.card  // may be null for carts
                    });
                }

                db.SaveChanges(); // commit all details
            }

            // ======================================================
            //                     ORDERS
            // ======================================================

            // ----- CARTS -----
            AddOrder(
                email: "cbaker@example.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Art Of Racing In The Rain", 2, 23.95m, 10.30m, null),
                    ("The Host", 1, 25.99m, 13.25m, null)
                }
            );

            AddOrder(
                email: "toddj@yourmom.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Roses", 2, 24.99m, 20.99m, null)
                }
            );

            AddOrder(
                email: "cmiller@bob.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Altar of Eden", 1, 27.99m, 25.75m, null)
                }
            );

            AddOrder(
                email: "knelson@aoll.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Cast", 1, 21.95m, 12.95m, null)
                }
            );

            AddOrder(
                email: "cluce@gogle.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Brooklyn", 1, 18.95m, 3.60m, null)
                }
            );

            AddOrder(
                email: "erynrice@aoll.com",
                date: null,
                shipping: 0m,
                status: "InCart",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Dexter By Design", 1, 25.00m, 2.75m, null),
                    ("The Midnight House", 3, 25.95m, 3.11m, null)
                }
            );

            // ----- ACTUAL ORDERS -----
            AddOrder(
                email: "wchang@example.com",
                date: DateTime.Parse("2025-10-31"),
                shipping: 3.50m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Professional", 1, 26.95m, 7.01m, 1004)
                }
            );

            AddOrder(
                email: "cbaker@example.com",
                date: DateTime.Parse("2025-10-01"),
                shipping: 9.50m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Say Goodbye", 5, 25.00m, 11.25m, 1002),
                    ("Chasing Darkness", 1, 25.95m, 9.08m, 1002)
                }
            );

            AddOrder(
                email: "limchou@gogle.com",
                date: DateTime.Parse("2025-10-05"),
                shipping: 107.00m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Other Queen", 70, 25.95m, 23.61m, 1005),
                    ("Wrecked", 10, 25.00m, 18.00m, 1005),
                    ("Reckless", 23, 22.00m, 9.46m, 1005)
                }
            );

            AddOrder(
                email: "wchang@example.com",
                date: DateTime.Parse("2025-10-30"),
                shipping: 3.50m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Professional", 1, 26.95m, 7.01m, 1004)
                }
            );

            AddOrder(
                email: "jeffh@sonic.com",
                date: DateTime.Parse("2025-11-01"),
                shipping: 5.00m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("The Professional", 2, 26.95m, 7.01m, 1006)
                }
            );

            AddOrder(
                email: "cmiller@bob.com",
                date: DateTime.Parse("2025-11-03"),
                shipping: 3.50m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Say Goodbye", 1, 25.00m, 11.25m, 1007)
                }
            );

            AddOrder(
                email: "elowe@netscare.net",
                date: DateTime.Parse("2025-11-02"),
                shipping: 6.50m,
                status: "Ordered",
                new List<(string, int, decimal, decimal, int?)>
                {
                    ("Wrecked", 3, 25.00m, 18.00m, 1008),
                    ("Reckless", 11, 22.00m, 9.46m, 1008)
                }
            );

        } // end SeedAllOrders
    }
}
