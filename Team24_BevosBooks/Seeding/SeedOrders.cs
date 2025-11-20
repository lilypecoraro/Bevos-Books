using System;
using System.Collections.Generic;
using System.Linq;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.DAL;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedOrders
    {
        public static void SeedAllOrders(AppDbContext db)
        {
            List<Order> Orders = new List<Order>();
            List<OrderDetail> Details = new List<OrderDetail>();

            Orders.Add(new Order {
                OrderID = 10001,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Christopher" && u.LastName == "Baker").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10001,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Art Of Racing In The Rain").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 3,
                Price = 23.95m,
                Cost = 10.3m
            });

            Details.Add(new OrderDetail {
                OrderID = 10001,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Host").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 1,
                Price = 25.99m,
                Cost = 13.25m
            });

            Orders.Add(new Order {
                OrderID = 10002,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Todd" && u.LastName == "Jacobs").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10002,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Roses").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 2,
                Price = 24.99m,
                Cost = 20.99m
            });

            Orders.Add(new Order {
                OrderID = 10003,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Charles" && u.LastName == "Miller").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10003,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Alter of Eden").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 2,
                Price = 27.99m,
                Cost = 25.75m
            });

            Orders.Add(new Order {
                OrderID = 10004,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Wendy" && u.LastName == "Chang").Id,
                OrderDate = new DateTime(2025, 10, 31),
                ShippingFee = 3.5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10004,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Professional").BookID,
                CardID = 1004,
                CouponID = null,
                Quantity = 1,
                Price = 26.95m,
                Cost = 7.01m
            });

            Orders.Add(new Order {
                OrderID = 10005,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Christopher" && u.LastName == "Baker").Id,
                OrderDate = new DateTime(2025, 10, 1),
                ShippingFee = 9.5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10005,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Say Goodbye").BookID,
                CardID = 1002,
                CouponID = null,
                Quantity = 5,
                Price = 25m,
                Cost = 11.25m
            });

            Details.Add(new OrderDetail {
                OrderID = 10005,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Chasing Darkness").BookID,
                CardID = 1002,
                CouponID = null,
                Quantity = 1,
                Price = 25.95m,
                Cost = 9.08m
            });

            Orders.Add(new Order {
                OrderID = 10006,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Lim" && u.LastName == "Chou").Id,
                OrderDate = new DateTime(2025, 10, 5),
                ShippingFee = 107m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10006,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Other Queen").BookID,
                CardID = 1005,
                CouponID = null,
                Quantity = 70,
                Price = 25.95m,
                Cost = 23.61m
            });

            Details.Add(new OrderDetail {
                OrderID = 10006,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Wrecked").BookID,
                CardID = 1005,
                CouponID = null,
                Quantity = 10,
                Price = 25m,
                Cost = 18m
            });

            Details.Add(new OrderDetail {
                OrderID = 10006,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Reckless").BookID,
                CardID = 1005,
                CouponID = null,
                Quantity = 23,
                Price = 22m,
                Cost = 9.46m
            });

            Orders.Add(new Order {
                OrderID = 10007,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Wendy" && u.LastName == "Chang").Id,
                OrderDate = new DateTime(2025, 10, 30),
                ShippingFee = 3.5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10007,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Professional").BookID,
                CardID = 1004,
                CouponID = null,
                Quantity = 1,
                Price = 26.95m,
                Cost = 7.01m
            });

            Orders.Add(new Order {
                OrderID = 10008,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Jeffrey" && u.LastName == "Hampton").Id,
                OrderDate = new DateTime(2025, 11, 1),
                ShippingFee = 5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10008,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Professional").BookID,
                CardID = 1006,
                CouponID = null,
                Quantity = 2,
                Price = 26.95m,
                Cost = 7.01m
            });

            Orders.Add(new Order {
                OrderID = 10009,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Charles" && u.LastName == "Miller").Id,
                OrderDate = new DateTime(2025, 11, 3),
                ShippingFee = 3.5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10009,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Say Goodbye").BookID,
                CardID = 1007,
                CouponID = null,
                Quantity = 1,
                Price = 25m,
                Cost = 11.25m
            });

            Orders.Add(new Order {
                OrderID = 10010,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Ernest" && u.LastName == "Lowe").Id,
                OrderDate = new DateTime(2025, 11, 2),
                ShippingFee = 6.5m,
                OrderStatus = "Ordered"
            });

            Details.Add(new OrderDetail {
                OrderID = 10010,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Wrecked").BookID,
                CardID = 1008,
                CouponID = null,
                Quantity = 3,
                Price = 25m,
                Cost = 18m
            });

            Details.Add(new OrderDetail {
                OrderID = 10010,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Reckless").BookID,
                CardID = 1008,
                CouponID = null,
                Quantity = 11,
                Price = 22m,
                Cost = 9.46m
            });

            Orders.Add(new Order {
                OrderID = 10011,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Kelly" && u.LastName == "Nelson").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10011,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Cast").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 1,
                Price = 21.95m,
                Cost = 12.95m
            });

            Orders.Add(new Order {
                OrderID = 10012,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Chuck" && u.LastName == "Luce").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10012,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Brooklyn").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 4,
                Price = 18.95m,
                Cost = 3.6m
            });

            Orders.Add(new Order {
                OrderID = 10013,
                UserID = db.Users
                    .FirstOrDefault(u => u.FirstName == "Eryn" && u.LastName == "Rice").Id,
                OrderDate = new DateTime(2025,1,1),
                ShippingFee = 0m,
                OrderStatus = "InCart"
            });

            Details.Add(new OrderDetail {
                OrderID = 10013,
                BookID = db.Books.FirstOrDefault(b => b.Title == "Dexter By Design").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 1,
                Price = 25m,
                Cost = 2.75m
            });

            Details.Add(new OrderDetail {
                OrderID = 10013,
                BookID = db.Books.FirstOrDefault(b => b.Title == "The Midnight House").BookID,
                CardID = 0,
                CouponID = null,
                Quantity = 3,
                Price = 25.95m,
                Cost = 3.11m
            });

            foreach (var o in Orders)
                db.Orders.Add(o);
            foreach (var d in Details)
                db.OrderDetails.Add(d);
            db.SaveChanges();
        }
    }
}

