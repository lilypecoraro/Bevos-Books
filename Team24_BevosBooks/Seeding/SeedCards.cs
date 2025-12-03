using Microsoft.Data.SqlClient;
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

            // --- Exact card list based on your Excel sheet ---
            var cards = new List<Card>()
            {
                new Card {
                    CardID = 1001,
                    UserID = GetUserIdByName("Christopher", "Baker"),
                    CardNumber = "3517193267072490",
                    CardType = Card.CardTypes.Visa
                },
                new Card {
                    CardID = 1002,
                    UserID = GetUserIdByName("Christopher", "Baker"),
                    CardNumber = "5653264624505624",
                    CardType = Card.CardTypes.MasterCard
                },
                new Card {
                    CardID = 1003,
                    UserID = GetUserIdByName("Franco", "Broccolo"),
                    CardNumber = "2340139018242888",
                    CardType = Card.CardTypes.MasterCard
                },
                new Card {
                    CardID = 1004,
                    UserID = GetUserIdByName("Wendy", "Chang"),
                    CardNumber = "4888561830797648",
                    CardType = Card.CardTypes.Visa
                },
                new Card {
                    CardID = 1005,
                    UserID = GetUserIdByName("Lim", "Chou"),
                    CardNumber = "7874839329412510",
                    CardType = Card.CardTypes.AmericanExpress
                },
                new Card {
                    CardID = 1006,
                    UserID = GetUserIdByName("Jeffrey", "Hampton"),
                    CardNumber = "8882933892564410",
                    CardType = Card.CardTypes.Visa
                },
                new Card {
                    CardID = 1007,
                    UserID = GetUserIdByName("Charles", "Miller"),
                    CardNumber = "9577230402048890",
                    CardType = Card.CardTypes.MasterCard
                },
                new Card {
                    CardID = 1008,
                    UserID = GetUserIdByName("Ernest", "Lowe"),
                    CardNumber = "3391194669212420",
                    CardType = Card.CardTypes.AmericanExpress
                },
                new Card {
                    CardID = 1009,
                    UserID = GetUserIdByName("Kelly", "Nelson"),
                    CardNumber = "4186773703003410",
                    CardType = Card.CardTypes.Visa
                }
            };

            try
            {
                // ENABLE identity insert
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cards ON");

                foreach (var card in cards)
                {
                    currentCard = card.CardID.ToString();
                    db.Cards.Add(card);
                    db.SaveChanges();
                    cardsAdded++;
                }

                // DISABLE identity insert
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Cards OFF");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"❌ SeedCards FAILED at CardID {currentCard}. Added {cardsAdded}. EX: {ex.Message} INNER: {ex.InnerException?.Message}"
                );
            }
        }
    }
}
