using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedCards
    {
        public static void SeedAllCards(AppDbContext db)
        {
            Int32 cardsAdded = 0;
            String currentCard = "Beginning";

            // Do not double-seed
            if (db.Cards.Any()) return;

            // Helper → get user ID by name
            string GetUserIdByName(string first, string last)
            {
                var user = db.Users
                    .FirstOrDefault(u => u.FirstName == first && u.LastName == last);

                if (user == null)
                    throw new Exception($"User '{first} {last}' not found.");

                return user.Id;
            }

            var cards = new List<Card>()
            {
                new Card { CardID = 1001, UserID = GetUserIdByName("Christopher", "Baker"),
                    CustomerName="Christopher Baker", CardNumber="3517193267072490", CardType = Card.CardTypes.Visa },

                new Card { CardID = 1002, UserID = GetUserIdByName("Christopher", "Baker"),
                    CustomerName="Christopher Baker", CardNumber="5653264624505624", CardType = Card.CardTypes.MasterCard },

                new Card { CardID = 1003, UserID = GetUserIdByName("Franco", "Broccolo"),
                    CustomerName="Franco Broccolo", CardNumber="2340139018242888", CardType = Card.CardTypes.MasterCard },

                new Card { CardID = 1004, UserID = GetUserIdByName("Wendy", "Chang"),
                    CustomerName="Wendy Chang", CardNumber="4888561830797648", CardType = Card.CardTypes.Visa },

                new Card { CardID = 1005, UserID = GetUserIdByName("Lim", "Chou"),
                    CustomerName="Lim Chou", CardNumber="7874839329412510", CardType = Card.CardTypes.AmericanExpress },

                new Card { CardID = 1006, UserID = GetUserIdByName("Jeffrey", "Hampton"),
                    CustomerName="Jeffrey Hampton", CardNumber="8882933892564410", CardType = Card.CardTypes.Visa },

                new Card { CardID = 1007, UserID = GetUserIdByName("Charles", "Miller"),
                    CustomerName="Charles Miller", CardNumber="9577230402048890", CardType = Card.CardTypes.MasterCard },

                new Card { CardID = 1008, UserID = GetUserIdByName("Ernest", "Lowe"),
                    CustomerName="Ernest Lowe", CardNumber="3391194669212420", CardType = Card.CardTypes.AmericanExpress },

                new Card { CardID = 1009, UserID = GetUserIdByName("Kelly", "Nelson"),
                    CustomerName="Kelly Nelson", CardNumber="4186773703003410", CardType = Card.CardTypes.Visa }
            };

            try
            {
                foreach (var card in cards)
                {
                    currentCard = card.CardID.ToString();
                    db.Cards.Add(card);
                    db.SaveChanges();
                    cardsAdded++;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"❌ SeedCards FAILED at CardID {currentCard}. Added {cardsAdded}. " +
                    $"EX: {ex.Message} | INNER: {ex.InnerException?.Message}"
                );
            }
        }
    }
}
