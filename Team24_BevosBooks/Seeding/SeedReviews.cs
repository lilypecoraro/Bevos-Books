using System;
using System.Collections.Generic;
using System.Linq;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedReviews
    {
        public static void SeedAllReviews(AppDbContext db)
        {
            int reviewsAdded = 0;
            string current = "NONE";

            // Prevent double seed
            if (db.Reviews.Any()) return;

            // Helper – get user by email
            string GetUserIdByEmail(string email)
            {
                var user = db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (user == null)
                    throw new Exception($"❌ USER NOT FOUND: {email}");
                return user.Id;
            }

            // Helper – get book ID by title
            int GetBookId(string title)
            {
                var book = db.Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());
                if (book == null)
                    throw new Exception($"❌ BOOK NOT FOUND: {title}");
                return book.BookID;
            }

            // REVIEWER EMAILS (From your list)
            string CBaker = GetUserIdByEmail("cbaker@example.com");
            string WChang = GetUserIdByEmail("wchang@example.com");
            string LChou = GetUserIdByEmail("limchou@gogle.com");
            string JHampton = GetUserIdByEmail("jeffh@sonic.com");
            string CMiller = GetUserIdByEmail("cmiller@bob.com");
            string ELowe = GetUserIdByEmail("elowe@netscare.net");

            // APPROVER EMAILS (from original examples)
            string SBarnes = GetUserIdByEmail("s.barnes@bevosbooks.com");
            string JMason = GetUserIdByEmail("j.mason@bevosbooks.com");
            string CSilva = GetUserIdByEmail("c.silva@bevosbooks.com");
            string EStuart = GetUserIdByEmail("e.stuart@bevosbooks.com");
            string ARogers = GetUserIdByEmail("a.rogers@bevosbooks.com");
            string HGarcia = GetUserIdByEmail("h.garcia@bevosbooks.com");

            // ALL REVIEWS
            List<Review> reviews = new List<Review>
            {
                new Review {
                    ReviewerID = CBaker,
                    BookID     = GetBookId("Say Goodbye"),
                    ApproverID = SBarnes,
                    Rating     = 5,
                    ReviewText = "Incredible pacing and tension throughout—couldn’t stop reading.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = CBaker,
                    BookID     = GetBookId("Chasing Darkness"),
                    ApproverID = JMason,
                    Rating     = 4,
                    ReviewText = "Tight mystery with solid twists; a bit slow in the middle.",
                    DisputeStatus     = "Rejected"
                },

                new Review {
                    ReviewerID = WChang,
                    BookID     = GetBookId("The Professional"),
                    ApproverID = CSilva,
                    Rating     = 4,
                    ReviewText = "Classic Spenser. Sharp dialogue and old-school charm.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = LChou,
                    BookID     = GetBookId("The Other Queen"),
                    ApproverID = EStuart,
                    Rating     = 3,
                    ReviewText = "Rich historical detail, but pacing drags at times.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = LChou,
                    BookID     = GetBookId("Wrecked"),
                    ApproverID = ARogers,
                    Rating     = 5,
                    ReviewText = "Fast-moving and witty. Loved the Cape Cod setting.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = LChou,
                    BookID     = GetBookId("Reckless"),
                    ApproverID = HGarcia,
                    Rating     = 4,
                    ReviewText = "Emotional and thrilling. Hauck’s motives feel real.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = JHampton,
                    BookID     = GetBookId("The Professional"),
                    ApproverID = CSilva,
                    Rating     = 5,
                    ReviewText = "Lean, witty Spenser case—couldn’t put it down.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = CMiller,
                    BookID     = GetBookId("Say Goodbye"),
                    ApproverID = ARogers,
                    Rating     = 4,
                    ReviewText = "Creepy, clever, and tightly plotted.",
                    DisputeStatus     = "Rejected"
                },

                new Review {
                    ReviewerID = ELowe,
                    BookID     = GetBookId("Wrecked"),
                    ApproverID = EStuart,
                    Rating     = 4,
                    ReviewText = "Light, fun mystery with brisk pacing.",
                    DisputeStatus     = "Approved"
                },

                new Review {
                    ReviewerID = ELowe,
                    BookID     = GetBookId("Reckless"),
                    ApproverID = EStuart,
                    Rating     = 3,
                    ReviewText = "Gritty and tense, but a bit uneven.",
                    DisputeStatus     = "Approved"
                }
            };

            // SAVE
            try
            {
                foreach (Review r in reviews)
                {
                    current = $"{r.ReviewerID} → {r.BookID}";
                    db.Reviews.Add(r);
                    db.SaveChanges();
                    reviewsAdded++;
                }
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException?.Message ?? "NONE";
                throw new Exception($"❌ ERROR Seeding Reviews\nCurrent: {current}\nAdded: {reviewsAdded}\n{inner}");
            }
        }
    }
}
