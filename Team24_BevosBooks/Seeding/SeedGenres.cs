using System;
using System.Linq;
using System.Collections.Generic;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedGenres
    {
        public static void SeedAllGenres(AppDbContext db)
        {
            if (db.Genres.Any()) return; // prevents duplicates

            List<Genre> genres = new List<Genre>()
            {
                new Genre { GenreName = "Contemporary Fiction" },
                new Genre { GenreName = "Science Fiction" },
                new Genre { GenreName = "Mystery" },
                new Genre { GenreName = "Suspense" },
                new Genre { GenreName = "Romance" },
                new Genre { GenreName = "Thriller" },
                new Genre { GenreName = "Fantasy" },
                new Genre { GenreName = "Historical Fiction" },
                new Genre { GenreName = "Humor" },
                new Genre { GenreName = "Adventure" },
                new Genre { GenreName = "Horror" },
                new Genre { GenreName = "Poetry" },
                new Genre { GenreName = "Shakespeare" }
            };

            db.Genres.AddRange(genres);
            db.SaveChanges();
        }
    }
}
