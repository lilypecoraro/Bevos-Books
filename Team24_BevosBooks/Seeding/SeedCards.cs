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

            // -----------------------------------------------
            // Helper: Map email → UserID (string)
            // -----------------------------------------------
            string GetUserId(string email)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

                if (user == null)
                    throw new Exception($"❌ ERROR: No AppUser found with email {email}. Did you seed users first?");

                return user.Id;
            }

            // -----------------------------------------------
            // Card Seed Data (NO CardID — SQL generates it!)
            // -----------------------------------------------
            List<Card> cards = new List<Card>()
            {
                new Card {
                    UserID = GetUserId("cbaker@example.com"),
                    CardNumber = "3517193267072490",
                    CardType = Card.CardTypes.Visa
                },

                new Card {
                    UserID = GetUserId("cbaker@example.com"),
                    CardNumber = "5653264624505624",
                    CardType = Card.CardTypes.MasterCard
                },

                new Card {
                    UserID = GetUserId("franco@example.com"),
                    CardNumber = "2340139018242888",
                    CardType = Card.CardTypes.MasterCard
                },

                new Card {
                    UserID = GetUserId("wchang@example.com"),
                    CardNumber = "4888561830797648",
                    CardType = Card.CardTypes.Visa
                },

                new Card {
                    UserID = GetUserId("limchou@gogle.com"),
                    CardNumber = "7874839329412510",
                    CardType = Card.CardTypes.AmericanExpress
                },

                new Card {
                    UserID = GetUserId("jeffh@sonic.com"),
                    CardNumber = "8882933892564410",
                    CardType = Card.CardTypes.Visa
                },

                new Card {
                    UserID = GetUserId("cmiller@bob.com"),
                    CardNumber = "9577230402048890",
                    CardType = Card.CardTypes.MasterCard
                },

                new Card {
                    UserID = GetUserId("elowe@netscare.net"),
                    CardNumber = "3391194669212420",
                    CardType = Card.CardTypes.AmericanExpress
                },

                new Card {
                    UserID = GetUserId("knelson@aoll.com"),
                    CardNumber = "4186773703003410",
                    CardType = Card.CardTypes.Visa
                }
            };

            // -----------------------------------------------
            // Save Cards
            // -----------------------------------------------
            try
            {
                foreach (Card c in cards)
                {
                    currentCard = c.CardNumber;

                    db.Cards.Add(c);
                    db.SaveChanges();
                    cardsAdded++;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"❌ SeedCards FAILED Card#: {currentCard}  Cards Added: {cardsAdded}  EX: {ex.Message}  INNER: {ex.InnerException?.Message}"
                );
            }
        }
    }
}
