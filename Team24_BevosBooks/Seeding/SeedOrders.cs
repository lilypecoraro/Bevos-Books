using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Team24_BevosBooks.Seeding
{
	public static class SeedOrders
	{
		public static void SeedAllOrders(AppDbContext db)
		{
			Int32 intOrdersAdded = 0;
			String strOrderFlag = "Begin";

			List<Order> Orders = new List<Order>();

			Order o1 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Art Of Racing In The Rainm,
				Tax = 23.95m,
				ShippingCost = 10.3m,
				Total = 3m
			};
			o1.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o1.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o1.OrderDetails = new List<OrderDetail>();
			Orders.Add(o1);

			OrderDetail od2 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od2.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o1.OrderDetails.Add(od2);

			OrderDetail od3 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od3.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o1.OrderDetails.Add(od3);

			Order o2 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Rosesm,
				Tax = 24.99m,
				ShippingCost = 20.99m,
				Total = 2m
			};
			o2.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o2.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o2.OrderDetails = new List<OrderDetail>();
			Orders.Add(o2);

			OrderDetail od4 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od4.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o2.OrderDetails.Add(od4);

			Order o3 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Alter of Edenm,
				Tax = 27.99m,
				ShippingCost = 25.75m,
				Total = 2m
			};
			o3.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o3.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o3.OrderDetails = new List<OrderDetail>();
			Orders.Add(o3);

			OrderDetail od5 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od5.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o3.OrderDetails.Add(od5);

			Order o4 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Professionalm,
				Tax = 26.95m,
				ShippingCost = 7.01m,
				Total = 1m
			};
			o4.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1004);
			o4.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o4.OrderDetails = new List<OrderDetail>();
			Orders.Add(o4);

			OrderDetail od6 = new OrderDetail()
			{
				Quantity = 3.5,
				BookPrice = Orderedm
			};
			od6.Book = db.Books.FirstOrDefault(b => b.Title == "10/31/2025");
			o4.OrderDetails.Add(od6);

			Order o5 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Say Goodbyem,
				Tax = 25m,
				ShippingCost = 11.25m,
				Total = 5m
			};
			o5.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1002);
			o5.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o5.OrderDetails = new List<OrderDetail>();
			Orders.Add(o5);

			OrderDetail od7 = new OrderDetail()
			{
				Quantity = 9.5,
				BookPrice = Orderedm
			};
			od7.Book = db.Books.FirstOrDefault(b => b.Title == "10/1/2025");
			o5.OrderDetails.Add(od7);

			OrderDetail od8 = new OrderDetail()
			{
				Quantity = 3.5,
				BookPrice = Orderedm
			};
			od8.Book = db.Books.FirstOrDefault(b => b.Title == "10/1/2025");
			o5.OrderDetails.Add(od8);

			Order o6 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Other Queenm,
				Tax = 25.95m,
				ShippingCost = 23.61m,
				Total = 70m
			};
			o6.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1005);
			o6.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o6.OrderDetails = new List<OrderDetail>();
			Orders.Add(o6);

			OrderDetail od9 = new OrderDetail()
			{
				Quantity = 107,
				BookPrice = Orderedm
			};
			od9.Book = db.Books.FirstOrDefault(b => b.Title == "10/5/2025");
			o6.OrderDetails.Add(od9);

			OrderDetail od10 = new OrderDetail()
			{
				Quantity = 17,
				BookPrice = Orderedm
			};
			od10.Book = db.Books.FirstOrDefault(b => b.Title == "10/5/2025");
			o6.OrderDetails.Add(od10);

			OrderDetail od11 = new OrderDetail()
			{
				Quantity = 36.5,
				BookPrice = Orderedm
			};
			od11.Book = db.Books.FirstOrDefault(b => b.Title == "10/5/2025");
			o6.OrderDetails.Add(od11);

			Order o7 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Professionalm,
				Tax = 26.95m,
				ShippingCost = 7.01m,
				Total = 1m
			};
			o7.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1004);
			o7.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o7.OrderDetails = new List<OrderDetail>();
			Orders.Add(o7);

			OrderDetail od12 = new OrderDetail()
			{
				Quantity = 3.5,
				BookPrice = Orderedm
			};
			od12.Book = db.Books.FirstOrDefault(b => b.Title == "10/30/2025");
			o7.OrderDetails.Add(od12);

			Order o8 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Professionalm,
				Tax = 26.95m,
				ShippingCost = 7.01m,
				Total = 2m
			};
			o8.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1006);
			o8.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o8.OrderDetails = new List<OrderDetail>();
			Orders.Add(o8);

			OrderDetail od13 = new OrderDetail()
			{
				Quantity = 5,
				BookPrice = Orderedm
			};
			od13.Book = db.Books.FirstOrDefault(b => b.Title == "11/1/2025");
			o8.OrderDetails.Add(od13);

			Order o9 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Say Goodbyem,
				Tax = 25m,
				ShippingCost = 11.25m,
				Total = 1m
			};
			o9.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1007);
			o9.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o9.OrderDetails = new List<OrderDetail>();
			Orders.Add(o9);

			OrderDetail od14 = new OrderDetail()
			{
				Quantity = 3.5,
				BookPrice = Orderedm
			};
			od14.Book = db.Books.FirstOrDefault(b => b.Title == "11/3/2025");
			o9.OrderDetails.Add(od14);

			Order o10 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Wreckedm,
				Tax = 25m,
				ShippingCost = 18m,
				Total = 3m
			};
			o10.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 1008);
			o10.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o10.OrderDetails = new List<OrderDetail>();
			Orders.Add(o10);

			OrderDetail od15 = new OrderDetail()
			{
				Quantity = 6.5,
				BookPrice = Orderedm
			};
			od15.Book = db.Books.FirstOrDefault(b => b.Title == "11/2/2025");
			o10.OrderDetails.Add(od15);

			OrderDetail od16 = new OrderDetail()
			{
				Quantity = 18.5,
				BookPrice = Orderedm
			};
			od16.Book = db.Books.FirstOrDefault(b => b.Title == "11/2/2025");
			o10.OrderDetails.Add(od16);

			Order o11 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = The Castm,
				Tax = 21.95m,
				ShippingCost = 12.95m,
				Total = 1m
			};
			o11.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o11.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o11.OrderDetails = new List<OrderDetail>();
			Orders.Add(o11);

			OrderDetail od17 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od17.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o11.OrderDetails.Add(od17);

			Order o12 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Brooklynm,
				Tax = 18.95m,
				ShippingCost = 3.6m,
				Total = 4m
			};
			o12.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o12.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o12.OrderDetails = new List<OrderDetail>();
			Orders.Add(o12);

			OrderDetail od18 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od18.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o12.OrderDetails.Add(od18);

			Order o13 = new Order()
			{
				OrderDate = new DateTime(2025, 1, 1),
				Subtotal = Dexter By Designm,
				Tax = 25m,
				ShippingCost = 2.75m,
				Total = 1m
			};
			o13.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == );
			o13.CreditCard = db.CreditCards.FirstOrDefault(cc => cc.CardID == );
			o13.OrderDetails = new List<OrderDetail>();
			Orders.Add(o13);

			OrderDetail od19 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od19.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o13.OrderDetails.Add(od19);

			OrderDetail od20 = new OrderDetail()
			{
				Quantity = ,
				BookPrice = InCartm
			};
			od20.Book = db.Books.FirstOrDefault(b => b.Title == "");
			o13.OrderDetails.Add(od20);

			try
			{
				foreach (Order o in Orders)
				{
				Order dbOrder = db.Orders.FirstOrDefault(x => x.Customer.CustomerID == o.Customer.CustomerID && x.OrderDate == o.OrderDate);
				if (dbOrder == null)
				{
					db.Orders.Add(o);
					db.SaveChanges();
					intOrdersAdded += 1;
				}
				else
				{
					dbOrder.Subtotal = o.Subtotal;
					dbOrder.Tax = o.Tax;
					dbOrder.ShippingCost = o.ShippingCost;
					dbOrder.Total = o.Total;
					db.Update(dbOrder);
					db.SaveChanges();
					intOrdersAdded += 1;
				}
				}
			}
			catch (Exception ex)
			{
				String msg = " Orders added: " + intOrdersAdded + "; Error on " + strOrderFlag;
				throw new InvalidOperationException(ex.Message + msg);
			}
		}
	}
}
