using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Team24_BevosBooks.Seeding
{
	public static class SeedReviews
	{
		public static void SeedAllReviews(AppDbContext db)
		{
			Int32 intReviewsAdded = 0;
			String strReviewFlag = "Begin";

			List<Review> Reviews = new List<Review>();

			Review r1 = new Review()
			{
				Rating = 5,
				ReviewText = "“Incredible pacing and tension throughout—couldn’t stop reading!”",
				DisputeStatus = "Approve"
			};
			r1.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Christopher" && c.LastName == "Baker");
			r1.Book = db.Books.FirstOrDefault(b => b.Title == "Say Goodbye");
			r1.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Susan" && e.LastName == "Barnes");
			Reviews.Add(r1);

			Review r2 = new Review()
			{
				Rating = 4,
				ReviewText = "“Tight mystery with solid twists; a bit slow in the middle.”",
				DisputeStatus = "Reject"
			};
			r2.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Christopher" && c.LastName == "Baker");
			r2.Book = db.Books.FirstOrDefault(b => b.Title == "Chasing Darkness");
			r2.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Jack" && e.LastName == "Mason");
			Reviews.Add(r2);

			Review r3 = new Review()
			{
				Rating = 4,
				ReviewText = "“Classic Spenser. Sharp dialogue and old-school charm.”",
				DisputeStatus = "Approve"
			};
			r3.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Wendy" && c.LastName == "Chang");
			r3.Book = db.Books.FirstOrDefault(b => b.Title == "The Professional");
			r3.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Cindy" && e.LastName == "Silva");
			Reviews.Add(r3);

			Review r4 = new Review()
			{
				Rating = 3,
				ReviewText = "“Rich historical detail, but pacing drags at times.”",
				DisputeStatus = "Approve"
			};
			r4.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Lim" && c.LastName == "Chou");
			r4.Book = db.Books.FirstOrDefault(b => b.Title == "The Other Queen");
			r4.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Eric" && e.LastName == "Stuart");
			Reviews.Add(r4);

			Review r5 = new Review()
			{
				Rating = 5,
				ReviewText = "“Fast-moving and witty. Loved the Cape Cod setting.”",
				DisputeStatus = "Approve"
			};
			r5.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Lim" && c.LastName == "Chou");
			r5.Book = db.Books.FirstOrDefault(b => b.Title == "Wrecked");
			r5.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Allen" && e.LastName == "Rogers");
			Reviews.Add(r5);

			Review r6 = new Review()
			{
				Rating = 4,
				ReviewText = "“Emotional and thrilling. Hauck’s motives feel real.”",
				DisputeStatus = "Approve"
			};
			r6.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Lim" && c.LastName == "Chou");
			r6.Book = db.Books.FirstOrDefault(b => b.Title == "Reckless");
			r6.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Hector" && e.LastName == "Garcia");
			Reviews.Add(r6);

			Review r7 = new Review()
			{
				Rating = 5,
				ReviewText = "“Lean, witty Spenser case—couldn’t put it down.”",
				DisputeStatus = "Approve"
			};
			r7.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Jeffery" && c.LastName == "Hampton");
			r7.Book = db.Books.FirstOrDefault(b => b.Title == "The Professional");
			r7.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Cindy" && e.LastName == "Silva");
			Reviews.Add(r7);

			Review r8 = new Review()
			{
				Rating = 4,
				ReviewText = "“Creepy, clever, and tightly plotted.”",
				DisputeStatus = "Reject"
			};
			r8.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Charles" && c.LastName == "Miller");
			r8.Book = db.Books.FirstOrDefault(b => b.Title == "Say Goodbye");
			r8.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Allen" && e.LastName == "Rogers");
			Reviews.Add(r8);

			Review r9 = new Review()
			{
				Rating = 4,
				ReviewText = "“Light, fun mystery with brisk pacing.”",
				DisputeStatus = "Approve"
			};
			r9.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Ernest" && c.LastName == "Lowe");
			r9.Book = db.Books.FirstOrDefault(b => b.Title == "Wrecked");
			r9.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Eric" && e.LastName == "Stuart");
			Reviews.Add(r9);

			Review r10 = new Review()
			{
				Rating = 3,
				ReviewText = "“Gritty and tense, but a bit uneven.”",
				DisputeStatus = "Approve"
			};
			r10.Reviewer = db.Customers.FirstOrDefault(c => c.FirstName == "Ernest" && c.LastName == "Lowe");
			r10.Book = db.Books.FirstOrDefault(b => b.Title == "Reckless");
			r10.Approver = db.Employees.FirstOrDefault(e => e.FirstName == "Eric" && e.LastName == "Stuart");
			Reviews.Add(r10);

			try
			{
				foreach (Review reviewToAdd in Reviews)
				{
					strReviewFlag = reviewToAdd.ReviewText;

					Review dbReview = db.Reviews.FirstOrDefault(r => 
					    r.Reviewer.CustomerID == reviewToAdd.Reviewer.CustomerID &&
					    r.Book.BookID == reviewToAdd.Book.BookID);
					if (dbReview == null)
					{
						db.Reviews.Add(reviewToAdd);
						db.SaveChanges();
						intReviewsAdded += 1;
					}

					else
					{
						dbReview.Rating = reviewToAdd.Rating;
						dbReview.ReviewText = reviewToAdd.ReviewText;
						dbReview.DisputeStatus = reviewToAdd.DisputeStatus;
						dbReview.Approver = reviewToAdd.Approver;
						db.Update(dbReview);
						db.SaveChanges();
						intReviewsAdded += 1;
					}
				}
			}

			catch (Exception ex)
			{
				String msg = " Reviews added: " + intReviewsAdded + "; Error on " + strReviewFlag;
				throw new InvalidOperationException(ex.Message + msg);
			}
		}
	}
}
