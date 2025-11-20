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

            // Prevent duplicate seeding
            if (db.Reviews.Any()) return;

            // ===============================
            // Helper: Get AppUserID by name
            // ===============================
            string GetUserId(string fullName)
            {
                var parts = fullName.Split(' ');
                string first = parts[0].Trim();
                string last = parts[1].Trim();

                var user = db.Users.FirstOrDefault(u =>
                    u.FirstName.ToLower() == first.ToLower() &&
                    u.LastName.ToLower() == last.ToLower());

                if (user == null)
                    throw new Exception($"❌ No AppUser found for name: {fullName}");

                return user.Id;
            }

            // ===============================
            // Helper: Get AppUserID by email
            // ===============================
            string GetUserIdByEmail(string email)
            {
                var user = db.Users.FirstOrDefault(u =>
                    u.Email.ToLower() == email.ToLower());

                if (user == null)
                    throw new Exception($"❌ No AppUser found for email: {email}");

                return user.Id;
            }

            // ===============================
            // Helper: Get BookID by Title
            // ===============================
            int GetBookId(string title)
            {
                var book = db.Books.FirstOrDefault(b =>
                    b.Title.ToLower() == title.ToLower());

                if (book == null)
                    throw new Exception($"❌ No Book found with title: {title}");

                return book.BookID;
            }

            // ===============================
            // Build the list of reviews
            // ===============================
            List<Review> reviews = new List<Review>
            {
                new Review {
                    ReviewerID = GetUserId("Christopher Baker"),
                    BookID = GetBookId("Say Goodbye"),
                    ApproverID = GetUserIdByEmail("s.barnes@bevosbooks.com"),
                    Rating = 5,
                    ReviewText = "Incredible pacing and tension throughout—couldn’t stop reading.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Christopher Baker"),
                    BookID = GetBookId("Chasing Darkness"),
                    ApproverID = GetUserIdByEmail("j.mason@bevosbooks.com"),
                    Rating = 4,
                    ReviewText = "Tight mystery with solid twists; a bit slow in the middle.",
                    DisputeStatus = "Reject"
                },

                new Review {
                    ReviewerID = GetUserId("Wendy Chang"),
                    BookID = GetBookId("The Professional"),
                    ApproverID = GetUserIdByEmail("c.silva@bevosbooks.com"),
                    Rating = 4,
                    ReviewText = "Classic Spenser. Sharp dialogue and old-school charm.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Lim Chou"),
                    BookID = GetBookId("The Other Queen"),
                    ApproverID = GetUserIdByEmail("e.stuart@bevosbooks.com"),
                    Rating = 3,
                    ReviewText = "Rich historical detail, but pacing drags at times.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Lim Chou"),
                    BookID = GetBookId("Wrecked"),
                    ApproverID = GetUserIdByEmail("a.rogers@bevosbooks.com"),
                    Rating = 5,
                    ReviewText = "Fast-moving and witty. Loved the Cape Cod setting.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Lim Chou"),
                    BookID = GetBookId("Reckless"),
                    ApproverID = GetUserIdByEmail("h.garcia@bevosbooks.com"),
                    Rating = 4,
                    ReviewText = "Emotional and thrilling. Hauck’s motives feel real.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Jeffrey Hampton"),
                    BookID = GetBookId("The Professional"),
                    ApproverID = GetUserIdByEmail("c.silva@bevosbooks.com"),
                    Rating = 5,
                    ReviewText = "Lean, witty Spenser case—couldn’t put it down.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Charles Miller"),
                    BookID = GetBookId("Say Goodbye"),
                    ApproverID = GetUserIdByEmail("a.rogers@bevosbooks.com"),
                    Rating = 4,
                    ReviewText = "Creepy, clever, and tightly plotted.",
                    DisputeStatus = "Reject"
                },

                new Review {
                    ReviewerID = GetUserId("Ernest Lowe"),
                    BookID = GetBookId("Wrecked"),
                    ApproverID = GetUserIdByEmail("e.stuart@bevosbooks.com"),
                    Rating = 4,
                    ReviewText = "Light, fun mystery with brisk pacing.",
                    DisputeStatus = "Approve"
                },

                new Review {
                    ReviewerID = GetUserId("Ernest Lowe"),
                    BookID = GetBookId("Reckless"),
                    ApproverID = GetUserIdByEmail("e.stuart@bevosbooks.com"),
                    Rating = 3,
                    ReviewText = "Gritty and tense, but a bit uneven.",
                    DisputeStatus = "Approve"
                }
            };

            // ===============================
            // Save to DB
            // ===============================
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
                string inner1 = ex.InnerException?.Message ?? "NONE";
                string inner2 = ex.InnerException?.InnerException?.Message ?? "NONE";

                throw new Exception(
                    $"❌ ERROR Seeding Reviews | Current Review: {current} | Added: {reviewsAdded}\n" +
                    $"EX: {ex.Message}\nINNER1: {inner1}\nINNER2: {inner2}"
                );
            }
        }
    }
}
