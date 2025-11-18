using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Team24_BevosBooks.Seeding
{
	public static class SeedCards
	{
		public static void SeedAllCards(AppDbContext db)
		{
			Int32 intCardsAdded = 0;
			String strCardFlag = "Begin";

			List<Card> Cards = new List<Card>();

			Card c1 = new Card()
			{
				CardID = 1001,
				CardNumber = "3517193267072490",
				CardType = "Visa"
			};
			c1.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9010);
			Cards.Add(c1);

			Card c2 = new Card()
			{
				CardID = 1002,
				CardNumber = "5653264624505624",
				CardType = "Mastercard"
			};
			c2.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9010);
			Cards.Add(c2);

			Card c3 = new Card()
			{
				CardID = 1003,
				CardNumber = "2340139018242888",
				CardType = "Mastercard"
			};
			c3.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9012);
			Cards.Add(c3);

			Card c4 = new Card()
			{
				CardID = 1004,
				CardNumber = "4888561830797648",
				CardType = "Visa"
			};
			c4.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9013);
			Cards.Add(c4);

			Card c5 = new Card()
			{
				CardID = 1005,
				CardNumber = "7874839329412510",
				CardType = "AmericanExpress"
			};
			c5.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9014);
			Cards.Add(c5);

			Card c6 = new Card()
			{
				CardID = 1006,
				CardNumber = "8882933892564410",
				CardType = "Visa"
			};
			c6.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9021);
			Cards.Add(c6);

			Card c7 = new Card()
			{
				CardID = 1007,
				CardNumber = "9577230402048890",
				CardType = "Mastercard"
			};
			c7.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9034);
			Cards.Add(c7);

			Card c8 = new Card()
			{
				CardID = 1008,
				CardNumber = "3391194669212420",
				CardType = "AmericanExpress"
			};
			c8.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9028);
			Cards.Add(c8);

			Card c9 = new Card()
			{
				CardID = 1009,
				CardNumber = "4186773703003410",
				CardType = "Visa"
			};
			c9.Customer = db.Customers.FirstOrDefault(c => c.CustomerID == 9035);
			Cards.Add(c9);

			try
			{
				foreach (Card c in Cards)
				{
					strCardFlag = c.CardNumber;
					Card dbCard = db.Cards.FirstOrDefault(x => x.CardID == c.CardID);
					if (dbCard == null)
					{
						db.Cards.Add(c);
						db.SaveChanges();
						intCardsAdded += 1;
					}
				else
					{
						dbCard.CardNumber = c.CardNumber;
						dbCard.CardType = c.CardType;
						dbCard.Customer = c.Customer;
						db.Update(dbCard);
						db.SaveChanges();
						intCardsAdded += 1;
					}
				}
			}
			catch (Exception ex)
			{
				String msg = " Cards added: " + intCardsAdded + "; Error on " + strCardFlag;
				throw new InvalidOperationException(ex.Message + msg);
			}
		}
	}
}
