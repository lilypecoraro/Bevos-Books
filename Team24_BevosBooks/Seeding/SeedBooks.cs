using System;
using System.Collections.Generic;
using System.Linq;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedBooks
    {
        public static void SeedAllBooks(AppDbContext db)
        {
            List<Book> Books = new List<Book>();

            Books.Add(new Book {
                BookNumber = 222001,
                Title = "The Art Of Racing In The Rain",
                Authors = "Garth Stein",
                Description = "A Lab-terrier mix with great insight into the human condition helps his owner, a struggling race car driver.",
                Price = 23.95m,
                Cost = 10.30m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2008, 5, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222002,
                Title = "The Host",
                Authors = "Stephenie Meyer",
                Description = "Aliens have taken control of the minds and bodies of most humans, but one woman won’t surrender.",
                Price = 25.99m,
                Cost = 13.25m,
                InventoryQuantity = 8,
                ReorderPoint = 7,
                PublishDate = new DateTime(2008, 5, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222003,
                Title = "Chasing Darkness",
                Authors = "Robert Crais",
                Description = "The Los Angeles private eye Elvis Cole responsible for the release of a serial killer?",
                Price = 25.95m,
                Cost = 9.08m,
                InventoryQuantity = 10,
                ReorderPoint = 7,
                PublishDate = new DateTime(2008, 7, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222004,
                Title = "Say Goodbye",
                Authors = "Lisa Gardner",
                Description = "An F.B.I. agent tracks a serial killer who uses spiders as a weapon.",
                Price = 25.00m,
                Cost = 11.25m,
                InventoryQuantity = 5,
                ReorderPoint = 2,
                PublishDate = new DateTime(2008, 7, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222005,
                Title = "The Gargoyle",
                Authors = "Andrew Davidson",
                Description = "A hideously burned man is cared for by a sculptress who claims they were lovers seven centuries ago.",
                Price = 25.95m,
                Cost = 16.09m,
                InventoryQuantity = 5,
                ReorderPoint = 3,
                PublishDate = new DateTime(2008, 8, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222006,
                Title = "Foreign Body",
                Authors = "Robin Cook",
                Description = "A medical student investigates a rising number of deaths among medical tourists at foreign hospitals.",
                Price = 25.95m,
                Cost = 24.65m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2008, 8, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222007,
                Title = "Acheron",
                Authors = "Sherrilyn Kenyon",
                Description = "Book 12 of the Dark-Hunter paranormal series.",
                Price = 24.95m,
                Cost = 13.72m,
                InventoryQuantity = 2,
                ReorderPoint = 2,
                PublishDate = new DateTime(2008, 8, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222008,
                Title = "Being Elizabeth",
                Authors = "Barbara Taylor Bradford",
                Description = "A 25-year-old newly in control of her family’s corporate empire faces tough choices in business and in love.",
                Price = 27.95m,
                Cost = 21.80m,
                InventoryQuantity = 9,
                ReorderPoint = 5,
                PublishDate = new DateTime(2008, 8, 23),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222009,
                Title = "Just Breathe",
                Authors = "Susan Wiggs",
                Description = "Her marriage broken, the author of a syndicated comic strip flees to California, where romance and surprise await.",
                Price = 25.95m,
                Cost = 5.45m,
                InventoryQuantity = 8,
                ReorderPoint = 8,
                PublishDate = new DateTime(2008, 8, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222010,
                Title = "The Gypsy Morph",
                Authors = "Terry Brooks",
                Description = "In the third volume of the Genesis of Shannara series, champions of the Word and the Void clash.",
                Price = 27.00m,
                Cost = 6.75m,
                InventoryQuantity = 6,
                ReorderPoint = 6,
                PublishDate = new DateTime(2008, 8, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222011,
                Title = "The Other Queen",
                Authors = "Philippa Gregory",
                Description = "The story of Mary, Queen of Scots, in captivity under Queen Elizabeth.",
                Price = 25.95m,
                Cost = 23.61m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2008, 9, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222012,
                Title = "One Fifth Avenue",
                Authors = "Candace Bushnell",
                Description = "The worlds of gossip, theater and hedge funds have one address in common.",
                Price = 25.95m,
                Cost = 17.65m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2008, 9, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222013,
                Title = "The Given Day",
                Authors = "Dennis Lehane",
                Description = "A policman, a fugitive and their families persevere in the turbulence of Boston at the end of World War I.",
                Price = 27.95m,
                Cost = 6.99m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2008, 9, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222014,
                Title = "A Cedar Cove Christmas",
                Authors = "Debbie Macomber",
                Description = "A pregnant woman shows up in Cedar Cove on Christmas Eve and goes into labor in a room above a stable.",
                Price = 16.95m,
                Cost = 4.75m,
                InventoryQuantity = 6,
                ReorderPoint = 4,
                PublishDate = new DateTime(2008, 10, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222015,
                Title = "The Pirate King",
                Authors = "R A Salvatore",
                Description = "In Book 2 of the Transitions fantasy series, Drizzt returns to Luskan, a city dominated by dangerous pirates.",
                Price = 27.95m,
                Cost = 14.25m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2008, 10, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222016,
                Title = "Bones",
                Authors = "Jonathan Kellerman",
                Description = "The psychologist-detective Alex Delaware is called in when women’s bodies keep turning up in a Los Angeles marsh.",
                Price = 27.00m,
                Cost = 14.85m,
                InventoryQuantity = 7,
                ReorderPoint = 2,
                PublishDate = new DateTime(2008, 10, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222017,
                Title = "Rough Weather",
                Authors = "Robert B Parker",
                Description = "The Boston private eye Spenser gets involved when a gunman kidnaps the bride from her wedding on a private island.",
                Price = 26.95m,
                Cost = 20.75m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2008, 10, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222018,
                Title = "Extreme Measures",
                Authors = "Vince Flynn",
                Description = "Mitch Rapp teams up with a C.I.A. colleague to fight a terrorist cell — and the politicians who would rein them in.",
                Price = 27.95m,
                Cost = 15.09m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2008, 10, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222019,
                Title = "A Good Woman",
                Authors = "Danielle Steel",
                Description = "An American society girl who made a new life as a doctor in World War I France returns to New York.",
                Price = 27.00m,
                Cost = 10.53m,
                InventoryQuantity = 6,
                ReorderPoint = 1,
                PublishDate = new DateTime(2008, 11, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222020,
                Title = "Midnight",
                Authors = "Sister Souljah",
                Description = "A boy from Sudan struggles to protect his mother and sister and remain true to his Islamic principles in a Brooklyn housing project.",
                Price = 26.95m,
                Cost = 21.29m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2008, 11, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222021,
                Title = "Scarpetta",
                Authors = "Patricia Cornwell",
                Description = "The forensic pathologist Kay Scarpetta takes an assignment in New York City.",
                Price = 27.95m,
                Cost = 13.14m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2008, 12, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222022,
                Title = "A Darker Place",
                Authors = "Jack Higgins",
                Description = "A Russian defector becomes a counterspy.",
                Price = 26.95m,
                Cost = 11.86m,
                InventoryQuantity = 11,
                ReorderPoint = 7,
                PublishDate = new DateTime(2009, 1, 31),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222023,
                Title = "Fatally Flaky",
                Authors = "Diane Mott Davidson",
                Description = "The caterer Goldy Schulz tries to outwit a killer on the grounds of an Aspen spa.",
                Price = 25.99m,
                Cost = 22.09m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2009, 4, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222024,
                Title = "Turn Coat",
                Authors = "Jim Butcher",
                Description = "Book 11 of the Dresden Files series about a wizard detective in Chicago.",
                Price = 25.95m,
                Cost = 9.34m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2009, 4, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222025,
                Title = "Borderline",
                Authors = "Nevada Barr",
                Description = "Off duty and on vacation in Big Bend National Park, Anna Pigeon rescues a baby and is drawn into cross-border intrigue.",
                Price = 25.95m,
                Cost = 3.11m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2009, 4, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222026,
                Title = "Summer On Blossom Street",
                Authors = "Debbie Macomber",
                Description = "More stories of life and love from a Seattle knitting class.",
                Price = 24.95m,
                Cost = 7.24m,
                InventoryQuantity = 7,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 5, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222027,
                Title = "Dead And Gone",
                Authors = "Charlaine Harris",
                Description = "Sookie Stackhouse searches for the killer of a werepanther.",
                Price = 25.95m,
                Cost = 24.65m,
                InventoryQuantity = 10,
                ReorderPoint = 5,
                PublishDate = new DateTime(2009, 5, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222028,
                Title = "Brooklyn",
                Authors = "Colm Toibin",
                Description = "An unsophisticated young Irishwoman leaves her home for New York in the 1950s. Originally published in 2009 and the basis of the 2015 movie.",
                Price = 18.95m,
                Cost = 3.60m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2009, 5, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222029,
                Title = "The Last Child",
                Authors = "John Hart",
                Description = "A teenager searches for his inexplicably vanished twin sister.",
                Price = 24.95m,
                Cost = 15.72m,
                InventoryQuantity = 5,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 5, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222030,
                Title = "Heartless",
                Authors = "Diana Palmer",
                Description = "A woman‘s secret makes it hard for her to accept her stepbrother‘s love.",
                Price = 24.95m,
                Cost = 21.46m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2009, 5, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222031,
                Title = "Shanghai Girls",
                Authors = "Lisa See",
                Description = "Two Chinese sisters in the 1930s are sold as wives to men from California, and leave their war-torn country to join them.",
                Price = 25.00m,
                Cost = 2.50m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2009, 5, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222032,
                Title = "Skin Trade",
                Authors = "Laurell K Hamilton",
                Description = "Investigating some killings in Las Vegas, the vampire hunter Anita Blake must contend with the power of the weretigers.",
                Price = 26.95m,
                Cost = 2.70m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2009, 6, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222033,
                Title = "Roadside Crosses",
                Authors = "Jeffery Deaver",
                Description = "A California kinesics expert pursues a killer who stalks victims using information they’ve posted online.",
                Price = 26.95m,
                Cost = 7.82m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2009, 6, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222034,
                Title = "Finger Lickin’ Fifteen",
                Authors = "Janet Evanovich",
                Description = "The bounty hunter Stephanie Plum hunts a celebrity chef’s killer.",
                Price = 27.95m,
                Cost = 3.63m,
                InventoryQuantity = 7,
                ReorderPoint = 4,
                PublishDate = new DateTime(2009, 6, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222035,
                Title = "Return To Sullivans Island",
                Authors = "Dorothea Benton Frank",
                Description = "A recent college graduate returns to her family’s home on an island in the South Carolina Lowcountry and wrestles with tragedy and betrayal in the company of her appealing relatives.",
                Price = 25.99m,
                Cost = 13.25m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2009, 7, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222036,
                Title = "The Castaways",
                Authors = "Elin Hilderbrand",
                Description = "A Nantucket couple drowns, raising questions and precipitating conflicts among their group of friends.",
                Price = 24.99m,
                Cost = 16.99m,
                InventoryQuantity = 7,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 7, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222037,
                Title = "Rain Gods",
                Authors = "James Lee Burke",
                Description = "A Texas sheriff investigates a mass murder of illegal aliens and tries to find the young Iraq war veteran who may have been involved — before the F.B.I. can.",
                Price = 25.99m,
                Cost = 21.05m,
                InventoryQuantity = 6,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 7, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222038,
                Title = "Undone",
                Authors = "Karin Slaughter",
                Description = "Dr. Sara Linton works with agents of the Georgia Bureau of Investigation to stop a killer who tortures his victims.",
                Price = 26.00m,
                Cost = 7.28m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 7, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222039,
                Title = "Guardian Of Lies",
                Authors = "Steve Martini",
                Description = "The lawyer Paul Madriani unravels a mystery involving gold coins, the C.I.A., and a weapon forgotten since the Cuban missile crisis.",
                Price = 26.99m,
                Cost = 18.62m,
                InventoryQuantity = 6,
                ReorderPoint = 2,
                PublishDate = new DateTime(2009, 7, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222040,
                Title = "Dreamfever",
                Authors = "Karen Marie Moning",
                Description = "MacKlaya finds herself under the erotic spell of a Fae master.",
                Price = 26.00m,
                Cost = 21.06m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2009, 8, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222041,
                Title = "Resurrecting Midnight",
                Authors = "Eric Jerome Dickey",
                Description = "Gideon, an international assassin, travels to Argentina in pursuit of a dangerous assignment.",
                Price = 26.95m,
                Cost = 14.55m,
                InventoryQuantity = 3,
                ReorderPoint = 3,
                PublishDate = new DateTime(2009, 8, 29),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222042,
                Title = "Dexter By Design",
                Authors = "Jeff Lindsay",
                Description = "A serial killer who arranges victims in artful poses challenges the Miami Police Department and its blood splatter analyst, Dexter.",
                Price = 25.00m,
                Cost = 2.75m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2009, 9, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222043,
                Title = "The Professional",
                Authors = "Robert B Parker",
                Description = "Rich women are turning up dead, and the Boston P.I. Spenser investigates.",
                Price = 26.95m,
                Cost = 7.01m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2009, 10, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222044,
                Title = "The Unseen Academicals",
                Authors = "Terry Pratchett",
                Description = "In this Discworld fantasy, the benevolent tyrant of Ankh-Morpork suggests that Unseen University put together a football team.",
                Price = 25.99m,
                Cost = 3.12m,
                InventoryQuantity = 14,
                ReorderPoint = 9,
                PublishDate = new DateTime(2009, 10, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222045,
                Title = "Pursuit Of Honor",
                Authors = "Vince Flynn",
                Description = "The counterterrorism operative Mitch Rapp must teach politicians about national security following a new Qaeda attack.",
                Price = 27.99m,
                Cost = 5.04m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2009, 10, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222046,
                Title = "No Less Than Victory",
                Authors = "Jeff Shaara",
                Description = "The final volume of a trilogy of novels about World War II focuses on the final years of the war, including the Battle of the Bulge and the American sweep through Germany.",
                Price = 28.00m,
                Cost = 20.72m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2009, 11, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222047,
                Title = "Ford County",
                Authors = "John Grisham",
                Description = "Stories set in rural Mississippi.",
                Price = 24.00m,
                Cost = 14.88m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2009, 11, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222048,
                Title = "Wishin' And Hopin'",
                Authors = "Wally Lamb",
                Description = "A fifth-grader in 1964 gets ready for Christmas.",
                Price = 15.00m,
                Cost = 13.95m,
                InventoryQuantity = 3,
                ReorderPoint = 3,
                PublishDate = new DateTime(2009, 11, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Humor")
            });

            Books.Add(new Book {
                BookNumber = 222049,
                Title = "First Lord’S Fury",
                Authors = "Jim Butcher",
                Description = "With their survival at stake, Alerans prepare for a final battle in the sixth book of the Alera fantasy cycle.",
                Price = 25.95m,
                Cost = 13.23m,
                InventoryQuantity = 4,
                ReorderPoint = 1,
                PublishDate = new DateTime(2009, 11, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222050,
                Title = "Altar Of Eden",
                Authors = "James Rollins",
                Description = "A Louisiana veterinarian discovers a wrecked fishing trawler filled with genetically altered animals.",
                Price = 27.99m,
                Cost = 25.75m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 1, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222051,
                Title = "Deeper Than The Dead",
                Authors = "Tami Hoag",
                Description = "An F.B.I. investigator and a teacher track a series of murders in California in 1985.",
                Price = 26.95m,
                Cost = 9.70m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2010, 1, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222052,
                Title = "Roses",
                Authors = "Leila Meacham",
                Description = "Three generations in a small East Texas town.",
                Price = 24.99m,
                Cost = 20.99m,
                InventoryQuantity = 12,
                ReorderPoint = 8,
                PublishDate = new DateTime(2010, 1, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222053,
                Title = "Blood Ties",
                Authors = "Kay Hooper",
                Description = "The F.B.I. agent Noah Bishop and his special crimes unit  pursue a brutal enemy.",
                Price = 26.00m,
                Cost = 5.20m,
                InventoryQuantity = 7,
                ReorderPoint = 7,
                PublishDate = new DateTime(2010, 1, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222054,
                Title = "The Midnight House",
                Authors = "Alex Berenson",
                Description = "Who is killing members of a secret unit that interrogated terrorists? The C.I.A. agent John Wells is on the case.",
                Price = 25.95m,
                Cost = 3.11m,
                InventoryQuantity = 5,
                ReorderPoint = 5,
                PublishDate = new DateTime(2010, 2, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222055,
                Title = "Poor Little Bitch Girl",
                Authors = "Jackie Collins",
                Description = "Hollywood murder, three beautiful 20-something high school friends, a hot New York club owner.",
                Price = 26.99m,
                Cost = 17.54m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 2, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222056,
                Title = "Deep Shadow",
                Authors = "Randy Wayne White",
                Description = "Murderers want Doc Ford to help them dive for the remains of a wrecked plane supposedly laden with Cuban gold.",
                Price = 25.95m,
                Cost = 5.45m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 3, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222057,
                Title = "Think Twice",
                Authors = "Lisa Scottoline",
                Description = "A woman takes over her twin sister’s life.",
                Price = 26.99m,
                Cost = 21.86m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2010, 3, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222058,
                Title = "The Girl Who Chased The Moon",
                Authors = "Sarah Addison Allen",
                Description = "Mysteries and magic in a quirky North Carolina town.",
                Price = 25.00m,
                Cost = 11.25m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2010, 3, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222059,
                Title = "Without Mercy",
                Authors = "Lisa Jackson",
                Description = "Students are dying at an Oregon boarding school for wayward kids, and the concerned new teacher may be the next target.",
                Price = 25.00m,
                Cost = 4.25m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2010, 4, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222060,
                Title = "Wrecked",
                Authors = "Carol Higgins Clark",
                Description = "In the 13th mystery in this series, the suspicious disappearance of a neighbor interrupts a romantic weekend on Cape Cod for the P.I. Regan Reilly and her husband.",
                Price = 25.00m,
                Cost = 18.00m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2010, 4, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222061,
                Title = "Reckless",
                Authors = "Andrew Gross",
                Description = "A close friend from the investigator Ty Hauck's past has been brutally murdered, and he will risk everything he loves to avenge her death.",
                Price = 22.00m,
                Cost = 9.46m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2010, 5, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222062,
                Title = "Executive Intent",
                Authors = "Dale Brown",
                Description = "With the United States unleashing a missile-launching satellite that can strike anywhere in seconds, China and Russia respond swiftly and brutally.",
                Price = 27.95m,
                Cost = 22.64m,
                InventoryQuantity = 7,
                ReorderPoint = 7,
                PublishDate = new DateTime(2010, 5, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222063,
                Title = "Heart Of The Matter",
                Authors = "Emily Giffin",
                Description = "The lives of two women — one married to a pediatric plastic surgeon, the other a lawyer and single mother — converge after an accident involving the lawyer’s son.",
                Price = 26.99m,
                Cost = 6.21m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2010, 5, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222064,
                Title = "That Perfect Someone",
                Authors = "Johanna Lindsey",
                Description = "To avoid falling into a ruthless nobleman's trap, an heiress enters into a risky, intimate charade with a man she was once bound to wed.",
                Price = 25.00m,
                Cost = 18.25m,
                InventoryQuantity = 9,
                ReorderPoint = 9,
                PublishDate = new DateTime(2010, 6, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222065,
                Title = "Mission Of Honor",
                Authors = "David Weber",
                Description = "Honor Harrington defends the Star Kingdom of Manticore as it is besieged by many enemies.",
                Price = 27.00m,
                Cost = 6.75m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 6, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222066,
                Title = "Sizzling Sixteen",
                Authors = "Janet Evanovich",
                Description = "The bounty hunter Stephanie Plum comes to the aid of a cousin with gambling debts.",
                Price = 27.99m,
                Cost = 12.32m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 6, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222067,
                Title = "The Thousand Autumns Of Jacob De Zoet",
                Authors = "David Mitchell",
                Description = "Forbidden love in Edo-era Japan.",
                Price = 26.00m,
                Cost = 9.62m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2010, 7, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222068,
                Title = "The Search",
                Authors = "Nora Roberts",
                Description = "The only survivor of a serial killer has found peace in the Pacific Northwest, but her life is shaken by the appearance of a new man and a copycat murderer.",
                Price = 26.95m,
                Cost = 8.62m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2010, 7, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222069,
                Title = "Death On The D-List",
                Authors = "Nancy Grace",
                Description = "Fading celebrities who appear on Hailey Dean’s TV show are being murdered.",
                Price = 25.99m,
                Cost = 5.98m,
                InventoryQuantity = 6,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 8, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222070,
                Title = "No Mercy",
                Authors = "Sherrilyn Kenyon",
                Description = "Book 19 of the Dark-Hunter paranormal series.",
                Price = 24.99m,
                Cost = 5.25m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2010, 9, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222071,
                Title = "The Fall",
                Authors = "Guillermo del Toro and Chuck Hogan",
                Description = "A war erupts between Old and New World vampires. Book 2 of the Strain trilogy.",
                Price = 26.99m,
                Cost = 13.23m,
                InventoryQuantity = 7,
                ReorderPoint = 7,
                PublishDate = new DateTime(2010, 9, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222072,
                Title = "Legacy",
                Authors = "Danielle Steel",
                Description = "A writer’s stunning family discovery leads to Paris, the French aristocracy and a mysterious Sioux ancestor.",
                Price = 28.00m,
                Cost = 6.44m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 10, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222073,
                Title = "Call Me Mrs. Miracle",
                Authors = "Debbie Macomber",
                Description = "Working in the toy section of a department store, Emily Merkle is called upon to engineer some Christmas miracles.",
                Price = 16.95m,
                Cost = 8.31m,
                InventoryQuantity = 6,
                ReorderPoint = 4,
                PublishDate = new DateTime(2010, 10, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222074,
                Title = "Promise Me",
                Authors = "Richard Paul Evans",
                Description = "On Christmas Day, a woman with family problems meets a handsome, mysterious stranger.",
                Price = 19.99m,
                Cost = 10.79m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2010, 10, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222075,
                Title = "Crescent Dawn",
                Authors = "Clive Cussler and Dirk Cussler",
                Description = "Dirk Pitt seeks a tie between a trove of ancient Roman artifacts and a series of mosque explosions.",
                Price = 27.95m,
                Cost = 20.12m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2010, 11, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Adventure")
            });

            Books.Add(new Book {
                BookNumber = 222076,
                Title = "An Object Of Beauty",
                Authors = "Steve Martin",
                Description = "A young, beautiful and ambitious woman ruthlessly ascends the heights of the Manhattan art world.",
                Price = 26.99m,
                Cost = 8.91m,
                InventoryQuantity = 6,
                ReorderPoint = 6,
                PublishDate = new DateTime(2010, 11, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222077,
                Title = "Dead Or Alive",
                Authors = "Tom Clancy with Grant Blackwood",
                Description = "Many characters from Clancy’s previous novels make an appearance as an intelligence group tracks a vicious terrorist called the Emir.",
                Price = 28.95m,
                Cost = 24.03m,
                InventoryQuantity = 8,
                ReorderPoint = 8,
                PublishDate = new DateTime(2010, 12, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222078,
                Title = "Damage",
                Authors = "John Lescroart",
                Description = "The San Francisco detective Abe Glitsky faces a scion of wealth who’s seeking revenge against those who sent him to prison a decade ago.",
                Price = 26.95m,
                Cost = 24.26m,
                InventoryQuantity = 8,
                ReorderPoint = 7,
                PublishDate = new DateTime(2011, 1, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222079,
                Title = "The Inner Circle",
                Authors = "Brad Meltzer",
                Description = "An archivist discovers a book that once belonged to George Washington and conceals a deadly secret.",
                Price = 26.99m,
                Cost = 11.61m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2011, 1, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222080,
                Title = "Shadowfever",
                Authors = "Karen Marie Moning",
                Description = "Hunting for her sister’s murderer, MacKayla Lane is caught up in the struggle between humans and the Fae.",
                Price = 26.00m,
                Cost = 13.78m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 1, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222081,
                Title = "Call Me Irresistible",
                Authors = "Susan Elizabeth Phillips",
                Description = "In a small town in Texas, characters from Phillips’s earlier novels reappear as a woman persuades a friend to call off her wedding to the town’s popular mayor.",
                Price = 25.99m,
                Cost = 11.44m,
                InventoryQuantity = 4,
                ReorderPoint = 3,
                PublishDate = new DateTime(2011, 1, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222082,
                Title = "A Discovery Of Witches",
                Authors = "Deborah Harkness",
                Description = "The recovery of a lost ancient manuscript in a library at Oxford sets a fantastical underworld stirring.",
                Price = 28.95m,
                Cost = 3.76m,
                InventoryQuantity = 8,
                ReorderPoint = 7,
                PublishDate = new DateTime(2011, 2, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222083,
                Title = "Gideon’s Sword",
                Authors = "Douglas Preston and Lincoln Child",
                Description = "Gideon Crew avenges his father’s death and is sent on a mission by a government contractor.",
                Price = 26.99m,
                Cost = 19.70m,
                InventoryQuantity = 11,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 2, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222084,
                Title = "Treachery In Death",
                Authors = "J D Robb",
                Description = "Eve Dallas and her partner, Peabody, investigate a grocer’s murder.",
                Price = 26.95m,
                Cost = 5.93m,
                InventoryQuantity = 8,
                ReorderPoint = 5,
                PublishDate = new DateTime(2011, 2, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222085,
                Title = "Live Wire",
                Authors = "Harlan Coben",
                Description = "Myron Bolitar’s search for a missing rock star leads to questions about his own missing brother.",
                Price = 27.95m,
                Cost = 13.98m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2011, 3, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222086,
                Title = "A Lesson In Secrets",
                Authors = "Jacqueline Winspear",
                Description = "In the summer of 1932, Maisie Dobbs’s first assignment for the British secret service takes her undercover to Cambridge as a professor.",
                Price = 25.99m,
                Cost = 12.22m,
                InventoryQuantity = 7,
                ReorderPoint = 7,
                PublishDate = new DateTime(2011, 3, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222087,
                Title = "Crunch Time",
                Authors = "Diane Mott Davidson",
                Description = "The caterer and sleuth Goldy Schulz tries to help a friend whose rental house has been destroyed by arson.",
                Price = 26.99m,
                Cost = 3.78m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2011, 4, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222088,
                Title = "I’Ll Walk Alone",
                Authors = "Mary Higgins Clark",
                Description = "A woman haunted by the disappearance of her young son discovers that someone has stolen her identity.",
                Price = 25.99m,
                Cost = 3.90m,
                InventoryQuantity = 14,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 4, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222089,
                Title = "The Fifth Witness",
                Authors = "Michael Connelly",
                Description = "The defense lawyer Mickey Haller represents a woman facing home foreclosure who is accused of killing a banker.",
                Price = 27.99m,
                Cost = 6.16m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2011, 4, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222090,
                Title = "Save Me",
                Authors = "Lisa Scottoline",
                Description = "A mother’s action during a school emergency causes an uproar in a Philadelphia suburb.",
                Price = 27.99m,
                Cost = 11.20m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2011, 4, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222091,
                Title = "Quicksilver",
                Authors = "Amanda Quick",
                Description = "In this Arcane Society novel set in Victorian London, two paranormal talents must find a murderer before they become the next victims.",
                Price = 25.95m,
                Cost = 23.10m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2011, 4, 23),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222092,
                Title = "The Sixth Man",
                Authors = "David Baldacci",
                Description = "The lawyer for an alleged serial killer is murdered, and two former Secret Service agents are on the case.",
                Price = 27.99m,
                Cost = 20.15m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2011, 4, 23),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222093,
                Title = "Those In Peril",
                Authors = "Wilbur Smith",
                Description = "A private security agent battles pirates who have kidnapped a woman from a yacht in the Indian Ocean.",
                Price = 27.99m,
                Cost = 16.23m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2011, 5, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222094,
                Title = "The Jefferson Key",
                Authors = "Steve Berry",
                Description = "The former government operative Cotton Malone foils an assassination attempt on the president and finds himself at dangerous odds with a secret society.",
                Price = 26.00m,
                Cost = 18.20m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2011, 5, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222095,
                Title = "Summer Rental",
                Authors = "Mary Kay Andrews",
                Description = "Three friends in their mid-30s spend a month on North Carolina’s Outer Banks.",
                Price = 25.99m,
                Cost = 9.62m,
                InventoryQuantity = 11,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 6, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222096,
                Title = "One Summer",
                Authors = "David Baldacci",
                Description = "As Christmas nears, a terminally ill man is preparing his family for his death when another tragedy strikes.",
                Price = 25.99m,
                Cost = 20.01m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2011, 6, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222097,
                Title = "Before I Go To Sleep",
                Authors = "S J Watson",
                Description = "After a mysterious accident, an amnesiac cannot remember her past or form new memories.",
                Price = 14.99m,
                Cost = 6.00m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2011, 6, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222098,
                Title = "Now You See Her",
                Authors = "James Patterson and Michael Ledwidge",
                Description = "Nina Bloom, who years ago changed her identity to save her life, is forced to confront the past and the killer she thought she had escaped.",
                Price = 27.99m,
                Cost = 8.40m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2011, 7, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222099,
                Title = "Full Black",
                Authors = "Brad Thor",
                Description = "The covert counterterrorism operative Scot Harvath has a plan to stop a terrorist group that wants to take down the United States.",
                Price = 26.99m,
                Cost = 5.67m,
                InventoryQuantity = 8,
                ReorderPoint = 4,
                PublishDate = new DateTime(2011, 7, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222100,
                Title = "Ghost Story",
                Authors = "Jim Butcher",
                Description = "Harry Dresden, the wizard detective in Chicago, has been murdered. But that doesn’t stop him when his friends are in danger.",
                Price = 27.95m,
                Cost = 12.02m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 7, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222101,
                Title = "Back Of Beyond",
                Authors = "C J Box",
                Description = "Cody Hoyt, a brilliant cop and an alcoholic struggling with two months of sobriety, is determined to find his mentor’s killer.",
                Price = 25.99m,
                Cost = 24.69m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2011, 8, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222102,
                Title = "The Omen Machine",
                Authors = "Terry Goodkind",
                Description = "A return to the lives of Richard Rahl and Kahlan Amnell, in a tale of a new threat to their world.",
                Price = 29.99m,
                Cost = 17.69m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2011, 8, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222103,
                Title = "The Measure Of The Magic",
                Authors = "Terry Brooks",
                Description = "With the land on edge, Panterra is destined to confront a menace who seeks to claim the last black staff, and the life of the one who wields it.",
                Price = 27.00m,
                Cost = 15.39m,
                InventoryQuantity = 7,
                ReorderPoint = 4,
                PublishDate = new DateTime(2011, 8, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222104,
                Title = "How Firm A Foundation",
                Authors = "David Weber",
                Description = "The island empire of Charis fights to survive.",
                Price = 27.99m,
                Cost = 23.79m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2011, 9, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222105,
                Title = "Reamde",
                Authors = "Neal Stephenson",
                Description = "A virus invades a multiplayer online role-playing game and sets off a violent struggle.",
                Price = 35.00m,
                Cost = 14.70m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2011, 9, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222106,
                Title = "Nightwoods",
                Authors = "Charles Frazier",
                Description = "When a young woman inherits her murdered sister’s troubled twins, her solitary life becomes filled with mystery and action.",
                Price = 26.00m,
                Cost = 10.92m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2011, 10, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222107,
                Title = "The Affair",
                Authors = "Lee Child",
                Description = "For Jack Reacher, an elite military police officer, it all started in 1997. A lonely railroad track. A crime scene. A cover-up.",
                Price = 28.00m,
                Cost = 8.68m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2011, 10, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222108,
                Title = "A Lawman's Christmas",
                Authors = "Linda Lael Miller",
                Description = "The death of the town marshal leaves Blue River, Texas, without a lawman, and Dara Rose Nolan without a husband. Clay McKettrick steps in, and when he and Dara Rose agree to a marriage of convenience, the temporary lawman’s Christmas wish is to make her his permanent wife.",
                Price = 28.00m,
                Cost = 15.96m,
                InventoryQuantity = 10,
                ReorderPoint = 5,
                PublishDate = new DateTime(2011, 10, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222109,
                Title = "Bonnie",
                Authors = "Iris Johansen",
                Description = "The forensic sculptor Eve Duncan learns more about her daughter’s disappearance and the girl’s father‘s possible involvement.",
                Price = 27.99m,
                Cost = 24.07m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 10, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222110,
                Title = "The Christmas Wedding",
                Authors = "James Patterson and Richard DiLallo",
                Description = "A widow keeps the identity of the new man she is about to marry a secret as her children gather for Christmas.",
                Price = 25.99m,
                Cost = 23.65m,
                InventoryQuantity = 3,
                ReorderPoint = 2,
                PublishDate = new DateTime(2011, 10, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222111,
                Title = "Zero Day",
                Authors = "David Baldacci",
                Description = "A military investigator uncovers a conspiracy.",
                Price = 27.99m,
                Cost = 18.47m,
                InventoryQuantity = 12,
                ReorderPoint = 9,
                PublishDate = new DateTime(2011, 11, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222112,
                Title = "The Scottish Prisoner",
                Authors = "Diana Gabaldon",
                Description = "Jamie Fraser, a paroled Jacobite prisoner, and Lord John Grey collaborate uneasily on a mission to Ireland.",
                Price = 28.00m,
                Cost = 24.92m,
                InventoryQuantity = 6,
                ReorderPoint = 2,
                PublishDate = new DateTime(2011, 12, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222113,
                Title = "77 Shadow Street",
                Authors = "Dean Koontz",
                Description = "A 19th-century tycoon’s mansion has been turned into luxury apartments, but it remains in the grip of evil forces.",
                Price = 28.00m,
                Cost = 14.56m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2011, 12, 31),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Horror")
            });

            Books.Add(new Book {
                BookNumber = 222114,
                Title = "Love In A Nutshell",
                Authors = "Janet Evanovich and Dorien Kelly",
                Description = "A former magazine editor attempts to turn her parents’ summer house into a bed-and-breakfast.",
                Price = 27.99m,
                Cost = 22.95m,
                InventoryQuantity = 3,
                ReorderPoint = 3,
                PublishDate = new DateTime(2012, 1, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222115,
                Title = "The Hunter",
                Authors = "John Lescroart",
                Description = "A San Francisco private investigator discovers chilling facts about his birth family.",
                Price = 26.95m,
                Cost = 5.66m,
                InventoryQuantity = 7,
                ReorderPoint = 6,
                PublishDate = new DateTime(2012, 1, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222116,
                Title = "Copper Beach",
                Authors = "Jayne Ann Krentz",
                Description = "Amy Radwell, whose psychic talent enables her to understand the paranormal secrets in rare books, becomes the target of a blackmailer. The first book in a new series about rare books and psychic codes.",
                Price = 25.95m,
                Cost = 16.09m,
                InventoryQuantity = 5,
                ReorderPoint = 5,
                PublishDate = new DateTime(2012, 1, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222117,
                Title = "Left For Dead",
                Authors = "J A Jance",
                Description = "Ali Reynolds seeks justice for an old friend and an unidentified woman, both victims of brutal attacks.",
                Price = 25.99m,
                Cost = 20.01m,
                InventoryQuantity = 13,
                ReorderPoint = 10,
                PublishDate = new DateTime(2012, 2, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222118,
                Title = "Robert Ludlum’S The Janson Command",
                Authors = "Paul Garrison",
                Description = "A former American operative builds a network to help him resolve crises without torture or civilian casualties.",
                Price = 27.99m,
                Cost = 7.28m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2012, 2, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222119,
                Title = "Victims",
                Authors = "Jonathan Kellerman",
                Description = "The Los Angeles psychologist-detective Alex Delaware and the detective Milo Sturgis track down a homicidal maniac.",
                Price = 28.00m,
                Cost = 16.52m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2012, 3, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222120,
                Title = "Another Piece Of My Heart",
                Authors = "Jane Green",
                Description = "A woman in her late 30s marries the man of her dreams and reaches out to his daughters by his previous marriage, but one of them is determined to destroy her.",
                Price = 25.99m,
                Cost = 20.27m,
                InventoryQuantity = 8,
                ReorderPoint = 4,
                PublishDate = new DateTime(2012, 3, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222121,
                Title = "Unnatural Acts",
                Authors = "Stuart Woods",
                Description = "The New York lawyer Stone Barrington becomes involved in the family problems of a billionaire hedge fund manager.",
                Price = 26.95m,
                Cost = 16.71m,
                InventoryQuantity = 8,
                ReorderPoint = 6,
                PublishDate = new DateTime(2012, 4, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222122,
                Title = "Mission To Paris",
                Authors = "Alan Furst",
                Description = "In Paris in 1938, an actor stumbles into the clutches of Nazi conspirators who want to exploit his celebrity.",
                Price = 27.00m,
                Cost = 19.17m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2012, 6, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222123,
                Title = "Shadow Of Night",
                Authors = "Deborah Harkness",
                Description = "An Oxford scholar/witch and a vampire geneticist pursue history, secrets and each other in Elizabethan London.",
                Price = 28.95m,
                Cost = 21.13m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2012, 7, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222124,
                Title = "Where We Belong",
                Authors = "Emily Giffin",
                Description = "A woman’s successful life is disrupted by the appearance of an 18-year-old girl with a link to her past.",
                Price = 27.99m,
                Cost = 8.12m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2012, 7, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222125,
                Title = "Judgment Call",
                Authors = "J A Jance",
                Description = "Joanna Brady, an Arizona sheriff, must function as both a law officer and a mother when her daughter’s high school principal is murdered.",
                Price = 25.99m,
                Cost = 8.84m,
                InventoryQuantity = 11,
                ReorderPoint = 10,
                PublishDate = new DateTime(2012, 7, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222126,
                Title = "Broken Harbor",
                Authors = "Tana French",
                Description = "In French’s fourth Dublin murder squad novel, a detective’s investigation of a crime in a seaside town evokes memories of his disturbing childhood there.",
                Price = 27.95m,
                Cost = 24.04m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2012, 7, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222127,
                Title = "Odd Apocalypse",
                Authors = "Dean Koontz",
                Description = "Odd Thomas, who can communicate with the dead, explores the mysteries of an old estate now owned by a billionaire.",
                Price = 28.00m,
                Cost = 14.28m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2012, 8, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Horror")
            });

            Books.Add(new Book {
                BookNumber = 222128,
                Title = "Haven",
                Authors = "Kay Hooper",
                Description = "The F.B.I. agent Noah Bishop and his special crimes unit help two sisters probe the secrets of a North Carolina town.",
                Price = 26.95m,
                Cost = 14.82m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2012, 8, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222129,
                Title = "The Inn At Rose Harbor",
                Authors = "Debbie Macomber",
                Description = "A young widow buys a bed-and-breakfast.",
                Price = 26.00m,
                Cost = 24.18m,
                InventoryQuantity = 8,
                ReorderPoint = 5,
                PublishDate = new DateTime(2012, 8, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222130,
                Title = "Wards Of Faerie",
                Authors = "Terry Brooks",
                Description = "In the first book of a new fantasy series, the Dark Legacy of Shannara, Druids, Elves and humans unite to try to capture the Elfstones and rescue the troubled Four Lands.",
                Price = 28.00m,
                Cost = 4.20m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2012, 8, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222131,
                Title = "A Sunless Sea",
                Authors = "Anne Perry",
                Description = "A murder case for William Monk of the Thames River Police culminates in a government minister’s corruption trial.",
                Price = 26.00m,
                Cost = 22.62m,
                InventoryQuantity = 2,
                ReorderPoint = 2,
                PublishDate = new DateTime(2012, 9, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222132,
                Title = "Last To Die",
                Authors = "Tess Gerritsen",
                Description = "The detective Jane Rizzoli and the medical examiner Maura Isles protect a boy whose family and foster family have all been murdered.",
                Price = 27.00m,
                Cost = 9.99m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2012, 9, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222133,
                Title = "Telegraph Avenue",
                Authors = "Michael Chabon",
                Description = "Fathers and sons in Berkeley and Oakland, Calif.",
                Price = 27.99m,
                Cost = 11.20m,
                InventoryQuantity = 13,
                ReorderPoint = 10,
                PublishDate = new DateTime(2012, 9, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222134,
                Title = "Midst Toil And Tribulation",
                Authors = "David Weber",
                Description = "In Book 6 of the Safehold science fiction series, the republic of Siddamark descends into chaos.",
                Price = 27.99m,
                Cost = 10.08m,
                InventoryQuantity = 8,
                ReorderPoint = 8,
                PublishDate = new DateTime(2012, 9, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222135,
                Title = "Sleep No More",
                Authors = "Iris Johansen",
                Description = "The forensic sculptor Eve Duncan investigates when her mother’s friend disappears from a mental hospital.",
                Price = 27.99m,
                Cost = 4.48m,
                InventoryQuantity = 4,
                ReorderPoint = 3,
                PublishDate = new DateTime(2012, 10, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222136,
                Title = "Sweet Tooth",
                Authors = "Ian McEwan",
                Description = "A British woman working for MI5 in 1972 falls in love with a writer the service is clandestinely supporting.",
                Price = 26.95m,
                Cost = 16.17m,
                InventoryQuantity = 8,
                ReorderPoint = 4,
                PublishDate = new DateTime(2012, 11, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222137,
                Title = "Merry Christmas, Alex Cross",
                Authors = "James Patterson",
                Description = "Detective Alex Cross confronts both a hostage situation and a terrorist act at Christmas.",
                Price = 28.99m,
                Cost = 8.99m,
                InventoryQuantity = 10,
                ReorderPoint = 7,
                PublishDate = new DateTime(2012, 11, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222138,
                Title = "Threat Vector",
                Authors = "Tom Clancy with Mark Greaney",
                Description = "As China threatens to invade Taiwan, the covert intelligence expert Jack Ryan Jr. aids his father’s administration  — but his agency is no longer secret.",
                Price = 28.95m,
                Cost = 10.71m,
                InventoryQuantity = 12,
                ReorderPoint = 9,
                PublishDate = new DateTime(2012, 12, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222139,
                Title = "Two Graves",
                Authors = "Douglas Preston and Lincoln Child",
                Description = "Special Agent Aloysius Pendergast pursues a serial killer as well as his abducted wife.",
                Price = 26.99m,
                Cost = 13.23m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2012, 12, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222140,
                Title = "The Husband List",
                Authors = "Janet Evanovich and Dorien Kelly",
                Description = "In New York City in 1894, a wealthy young woman yearns for adventure and the love of an Irish-American with new money, rather than the titled Britons to whom her mother hopes to marry her off.",
                Price = 27.99m,
                Cost = 23.51m,
                InventoryQuantity = 11,
                ReorderPoint = 9,
                PublishDate = new DateTime(2013, 1, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222141,
                Title = "Collateral Damage",
                Authors = "Stuart Woods",
                Description = "Back in New York, the lawyer Stone Barrington joins his former partner Holly Barker in pursuing a dangerous case.",
                Price = 26.95m,
                Cost = 19.40m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 1, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222142,
                Title = "Kinsey And Me",
                Authors = "Sue Grafton",
                Description = "Stories about Grafton’s character Kinsey Millhone as well as explorations of Grafton’s own past.",
                Price = 27.95m,
                Cost = 25.43m,
                InventoryQuantity = 10,
                ReorderPoint = 7,
                PublishDate = new DateTime(2013, 1, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222143,
                Title = "The Third Bullet",
                Authors = "Stephen Hunter",
                Description = "The veteran sniper Bob Lee Swagger investigates the assassination of John F. Kennedy. ",
                Price = 26.99m,
                Cost = 15.11m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 1, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222144,
                Title = "The Night Ranger",
                Authors = "Alex Berenson",
                Description = "The former C.I.A. operative John Wells pitches in when four young Americans who work at a refugee camp in Somalia are hijacked by bandits. ",
                Price = 27.95m,
                Cost = 6.71m,
                InventoryQuantity = 4,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 2, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222145,
                Title = "Sweet Tea Revenge",
                Authors = "Laura Childs",
                Description = "Theodosia Browning, owner of Indigo Tea Shop, is a bridesmaid in her friend's wedding. But the bridegroom is found dead on the big day.",
                Price = 29.00m,
                Cost = 13.92m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2013, 3, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222146,
                Title = "The Last Threshold",
                Authors = "R A Salvatore",
                Description = "Book 4 of the fantasy Neverwinter Saga.",
                Price = 27.95m,
                Cost = 2.80m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 3, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222147,
                Title = "The Supremes At Earl's All-You-Can-Eat",
                Authors = "Edward Kelsey Moore",
                Description = "Four decades in the friendship of three middle-class black women in a small  southern Indiana town.",
                Price = 24.95m,
                Cost = 7.49m,
                InventoryQuantity = 11,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 3, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Humor")
            });

            Books.Add(new Book {
                BookNumber = 222148,
                Title = "Lover At Last",
                Authors = "J R Ward",
                Description = "Book 11 of the Black Dagger Brotherhood series.",
                Price = 27.95m,
                Cost = 12.86m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2013, 3, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222149,
                Title = "Leaving Everything Most Loved",
                Authors = "Jacqueline Winspear",
                Description = "In 1933, the private investigator Maisie Dobbs helps an Indian man whose sister’s murder has been ignored by Scotland Yard.",
                Price = 26.99m,
                Cost = 2.97m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 3, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222150,
                Title = "All That Is",
                Authors = "James Salter",
                Description = "A veteran makes a career in publishing in postwar New York and struggles to achieve romantic success.",
                Price = 26.95m,
                Cost = 14.01m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2013, 4, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222151,
                Title = "Unintended Consequences",
                Authors = "Stuart Woods",
                Description = "The New York lawyer Stone Barrington discovers a shadowy network beneath the world of European wealth.",
                Price = 26.95m,
                Cost = 11.32m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2013, 4, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222152,
                Title = "Nos4A2",
                Authors = "Joe Hill",
                Description = "In a creepy battle between real and imaginary worlds, a brave biker chick is pitted against a ghoulish villain who lures children to a place where it is always Christmas.",
                Price = 34.00m,
                Cost = 28.56m,
                InventoryQuantity = 14,
                ReorderPoint = 9,
                PublishDate = new DateTime(2013, 5, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222153,
                Title = "Zero Hour",
                Authors = "Clive Cussler and Graham Brown",
                Description = "Kurt Austin, Joe Zavala and the rest of the Numa team search for a physicist's machine, buried in an ocean trench, that can cause deadly earthquakes in the 11th Numa Files novel.",
                Price = 28.95m,
                Cost = 25.19m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2013, 6, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222154,
                Title = "The Son",
                Authors = "Philipp Meyer",
                Description = "More than 150 years in a Texas family, from Comanche raids to the present, and its rise to money and power in the cattle and oil industries.",
                Price = 22.00m,
                Cost = 19.58m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2013, 6, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222155,
                Title = "Red Sparrow",
                Authors = "Jason Matthews",
                Description = "A Russian intelligence officer trained in the art of seduction becomes entangled with a young C.I.A. officer.",
                Price = 26.95m,
                Cost = 11.59m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2013, 6, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222156,
                Title = "The Silver Star",
                Authors = "Jeannette Walls",
                Description = "When their irresponsible mother takes off, a 12-year-old California girl and her sister join the rest of their family in Virginia. ",
                Price = 23.95m,
                Cost = 8.38m,
                InventoryQuantity = 12,
                ReorderPoint = 8,
                PublishDate = new DateTime(2013, 6, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222157,
                Title = "Sisterland",
                Authors = "Curtis Sittenfeld",
                Description = "Twins with psychic abilities share a devastating secret.",
                Price = 27.99m,
                Cost = 20.71m,
                InventoryQuantity = 10,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 6, 29),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222158,
                Title = "The English Girl",
                Authors = "Daniel Silva",
                Description = "Gabriel Allon, an art restorer and occasional spy for the Israeli secret service, steps in to help the British prime minister, whose lover has been kidnapped. ",
                Price = 17.95m,
                Cost = 2.33m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2013, 7, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222159,
                Title = "Hunting Eve",
                Authors = "Iris Johansen",
                Description = "In the second book of a trilogy, the forensic sculptor Eve Duncan struggles to outwit a kidnapper in the Colorado wilderness.",
                Price = 35.99m,
                Cost = 28.07m,
                InventoryQuantity = 5,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 7, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222160,
                Title = "Light Of The World",
                Authors = "James Lee Burke",
                Description = "A savage killer follows the detective Dave Robicheaux and his family to a Montana ranch. ",
                Price = 32.00m,
                Cost = 10.88m,
                InventoryQuantity = 7,
                ReorderPoint = 5,
                PublishDate = new DateTime(2013, 7, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222161,
                Title = "The Kill List",
                Authors = "Frederick Forsyth",
                Description = "An Arabic-speaking Marine major known as the Tracker pursues a terrorist who radicalizes young Muslims living abroad.",
                Price = 23.95m,
                Cost = 10.78m,
                InventoryQuantity = 7,
                ReorderPoint = 2,
                PublishDate = new DateTime(2013, 8, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222162,
                Title = "Songs Of Willow Frost",
                Authors = "Jamie Ford",
                Description = "A 12-year-old Seattle orphan during the Depression becomes convinced that a movie star is really his vanished mother. ",
                Price = 18.00m,
                Cost = 6.30m,
                InventoryQuantity = 10,
                ReorderPoint = 9,
                PublishDate = new DateTime(2013, 9, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222163,
                Title = "W Is For Wasted",
                Authors = "Sue Grafton",
                Description = "A homeless man inexplicably leaves $600,000 to Kinsey Millhone.",
                Price = 16.00m,
                Cost = 7.04m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 9, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222164,
                Title = "Deadly Heat",
                Authors = "Richard Castle",
                Description = "The N.Y.P.D. homicide detective Nikki Heat and the journalist Jameson Rook search for the former C.I.A. station chief who ordered her mother’s execution.\nThe N.Y.P.D. homicide detective Nikki Heat and the journalist Jameson Rook search for the former C.I.A. station chief who ordered her mother’s execution.",
                Price = 16.00m,
                Cost = 3.36m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2013, 9, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222165,
                Title = "Deadline",
                Authors = "Sandra Brown",
                Description = "A journalist who pursues a story about a former marine, the son of terrorists from 40 years ago,becomes a suspect in the death of his ex-wife.",
                Price = 16.00m,
                Cost = 4.48m,
                InventoryQuantity = 7,
                ReorderPoint = 6,
                PublishDate = new DateTime(2013, 9, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222166,
                Title = "Silencing Eve",
                Authors = "Iris Johansen",
                Description = "The final book of a trilogy about the forensic sculptor Eve Duncan. ",
                Price = 19.00m,
                Cost = 7.03m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2013, 10, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222167,
                Title = "Starry Night",
                Authors = "Debbie Macomber",
                Description = "At Christmastime, a big-city columnist sets out to interview a reclusive author in Alaska. ",
                Price = 24.95m,
                Cost = 17.96m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2013, 10, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222168,
                Title = "Bridget Jones: Mad About The Boy",
                Authors = "Helen Fielding",
                Description = "Bridget, now 51 and a mother and widow, is once again looking for love.",
                Price = 29.95m,
                Cost = 18.57m,
                InventoryQuantity = 6,
                ReorderPoint = 1,
                PublishDate = new DateTime(2013, 10, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222169,
                Title = "The Last Dark",
                Authors = "Stephen R Donaldson",
                Description = "The 10th and final installment of the sprawling fantasy series about Thomas Covenant the Unbeliever.",
                Price = 16.00m,
                Cost = 10.88m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 10, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222170,
                Title = "Aimless Love",
                Authors = "Billy Collins",
                Description = "More than 50 new poems as well as selections from previous books from the two-term poet laureate of the Untied States.",
                Price = 30.99m,
                Cost = 25.41m,
                InventoryQuantity = 11,
                ReorderPoint = 7,
                PublishDate = new DateTime(2013, 10, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Poetry")
            });

            Books.Add(new Book {
                BookNumber = 222171,
                Title = "Tatiana",
                Authors = "Martin Cruz Smith",
                Description = "A dead translator’s coded notebook may hold the key to the murders of a muckraking journalist and an oligarch in the former Soviet Union. Arkady Renko, a senior investigator in the Moscow prosecutor’s office, is on the case.",
                Price = 20.99m,
                Cost = 18.68m,
                InventoryQuantity = 2,
                ReorderPoint = 2,
                PublishDate = new DateTime(2013, 11, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222172,
                Title = "Dust",
                Authors = "Patricia Cornwell",
                Description = "The murder of a computer engineer at M.I.T. leads the Massachusetts Chief Medical Examiner Kay Scarpetta in unexpected directions.",
                Price = 23.99m,
                Cost = 18.23m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2013, 11, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222173,
                Title = "The Supreme Macaroni Company",
                Authors = "Adriana Trigiani",
                Description = "Tensions arise when Valentine Roncalli, a Greenwich Village shoe designer, marries a handsome Italian, and the two must balance careers, countries and families. The final book in the Valentine trilogy. ",
                Price = 34.99m,
                Cost = 7.00m,
                InventoryQuantity = 6,
                ReorderPoint = 4,
                PublishDate = new DateTime(2013, 11, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222174,
                Title = "Innocence",
                Authors = "Dean Koontz",
                Description = "A grotesque man living in exile beneath the city encounters a teenage girl hiding from dangerous enemies.",
                Price = 21.00m,
                Cost = 13.44m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2013, 12, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Horror")
            });

            Books.Add(new Book {
                BookNumber = 222175,
                Title = "Hunting Shadows",
                Authors = "Charles Todd",
                Description = "In the aftermath of World War I, a Scotland Yard detective with a heavy burden of guilt, investigates two murders in Cambridgeshire that may be linked.",
                Price = 32.99m,
                Cost = 18.47m,
                InventoryQuantity = 7,
                ReorderPoint = 4,
                PublishDate = new DateTime(2014, 1, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222176,
                Title = "Confessions Of A Wild Child",
                Authors = "Jackie Collins",
                Description = "The early years of Collins’s recurring character Lucky Santangelo.",
                Price = 30.95m,
                Cost = 17.95m,
                InventoryQuantity = 10,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 2, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222177,
                Title = "The Counterfeit Agent",
                Authors = "Alex Berenson",
                Description = "John Wells is sent on a mission to find the truth about a mysterious Iranian package said to be bound for the United States.",
                Price = 16.99m,
                Cost = 9.68m,
                InventoryQuantity = 8,
                ReorderPoint = 8,
                PublishDate = new DateTime(2014, 2, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222178,
                Title = "Like A Mighty Army",
                Authors = "David Weber",
                Description = "In Book 7 of the Safehold science fiction series, the empire of Charis fights for self-determination. ",
                Price = 22.00m,
                Cost = 17.60m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2014, 2, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222179,
                Title = "Cavendon Hall",
                Authors = "Barbara Taylor Bradford",
                Description = "In Edwardian England, an aristocratic family and the family who serve them share an ancestral home. ",
                Price = 26.00m,
                Cost = 8.06m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2014, 4, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222180,
                Title = "Frog Music",
                Authors = "Emma Donoghue",
                Description = "A murder mystery set in San Francisco in 1876, when the city is in the grip of a smallpox epidemic and a heat wave.",
                Price = 16.95m,
                Cost = 4.92m,
                InventoryQuantity = 10,
                ReorderPoint = 5,
                PublishDate = new DateTime(2014, 4, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222181,
                Title = "Destroyer Angel",
                Authors = "Nevada Barr",
                Description = "The National Park Service Ranger Anna Pigeon must rescue friends who are kidnapped while camping in Minnesota.",
                Price = 32.00m,
                Cost = 3.52m,
                InventoryQuantity = 5,
                ReorderPoint = 3,
                PublishDate = new DateTime(2014, 4, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222182,
                Title = "Warriors",
                Authors = "Ted Bell",
                Description = "The counterspy Alex Hawke must rescue a kidnapped scientist. ",
                Price = 33.99m,
                Cost = 5.44m,
                InventoryQuantity = 12,
                ReorderPoint = 8,
                PublishDate = new DateTime(2014, 4, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222183,
                Title = "Live To See Tomorrow",
                Authors = "Iris Johansen",
                Description = "The C.I.A. operative Catherine Ling must spearhead the rescue of an American journalist kidnapped in Tibet. ",
                Price = 34.00m,
                Cost = 6.46m,
                InventoryQuantity = 9,
                ReorderPoint = 5,
                PublishDate = new DateTime(2014, 5, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222184,
                Title = "All The Light We Cannot See",
                Authors = "Anthony Doerr",
                Description = "The lives of a blind French girl and a gadget-obsessed German boy before and during World War II, when their paths eventually cross. ",
                Price = 23.95m,
                Cost = 10.30m,
                InventoryQuantity = 8,
                ReorderPoint = 6,
                PublishDate = new DateTime(2014, 5, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222185,
                Title = "The Kraken Project",
                Authors = "Douglas Preston",
                Description = "The former C.I.A. agent Wyman Ford must stop Dorothy, a powerful artificial intelligence that has gone rogue.",
                Price = 35.00m,
                Cost = 28.00m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 5, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222186,
                Title = "The Vacationers",
                Authors = "Emma Straub",
                Description = "Well-heeled New Yorkers and their friends spend two weeks in Majorca, a time when rivalries and secrets come to light.",
                Price = 33.00m,
                Cost = 5.94m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 5, 31),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222187,
                Title = "The Hurricane Sisters",
                Authors = "Dorothea Benton Frank",
                Description = "Three generations of women endure a stormy summer in South Carolina's Lowcountry.",
                Price = 16.00m,
                Cost = 9.60m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2014, 6, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222188,
                Title = "The Matchmaker",
                Authors = "Elin Hilderbrand",
                Description = "A Nantucket resident’s life is shaken by a diagnosis and the return to the island of her high school sweetheart. ",
                Price = 25.00m,
                Cost = 11.00m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2014, 6, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222189,
                Title = "Terminal City",
                Authors = "Linda Fairstein",
                Description = "Alexandra Cooper, a Manhattan assistant district attorney, hunts for a killer in Grand Central’s underground tunnels.",
                Price = 32.95m,
                Cost = 16.48m,
                InventoryQuantity = 12,
                ReorderPoint = 8,
                PublishDate = new DateTime(2014, 6, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222190,
                Title = "Landline",
                Authors = "Rainbow Rowell",
                Description = "A woman in a troubled marriage finds a way to communicate with her husband in the past. ",
                Price = 29.00m,
                Cost = 5.80m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2014, 7, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222191,
                Title = "The Book Of Life",
                Authors = "Deborah Harkness",
                Description = "In the conclusion to the All Souls trilogy, the Oxford scholar/witch Diana Bishop and the vampire geneticist Matthew Clairmont return from Elizabethan London to the present.",
                Price = 27.95m,
                Cost = 8.66m,
                InventoryQuantity = 9,
                ReorderPoint = 9,
                PublishDate = new DateTime(2014, 7, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222192,
                Title = "Magic Breaks",
                Authors = "Ilona Andrews",
                Description = "In the seventh Kate Daniels novel, Kate deals with paranormal politics in Atlanta as she prepares the Pack for an attack.",
                Price = 32.00m,
                Cost = 16.96m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2014, 8, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222193,
                Title = "Big Little Lies",
                Authors = "Liane Moriarty",
                Description = "Who will end up dead, and how, when three mothers with children in the same school become friends?",
                Price = 17.00m,
                Cost = 5.10m,
                InventoryQuantity = 11,
                ReorderPoint = 7,
                PublishDate = new DateTime(2014, 8, 2),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222194,
                Title = "Dark Skye",
                Authors = "Kresley Cole",
                Description = "Will a scarred warrior and the beautiful sorceress with the power to heal him overcome the challenges of their warring families and the chaotic battles around them? Book 15 in the Immortals After Dark series.",
                Price = 20.00m,
                Cost = 4.00m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2014, 8, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222195,
                Title = "The Magician's Land",
                Authors = "Lev Grossman",
                Description = "Quentin, an exiled magician, tries a risky heist in the final installment of a trilogy.",
                Price = 28.95m,
                Cost = 5.50m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2014, 8, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222196,
                Title = "Mean Streak",
                Authors = "Sandra Brown",
                Description = "A North Carolina pediatrician is held captive by a mysterious man who forces her to question her life. ",
                Price = 29.95m,
                Cost = 26.66m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2014, 8, 23),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222197,
                Title = "The King's Curse",
                Authors = "Philippa Gregory",
                Description = "As chief lady-in-waiting to Katherine of Aragon, Margaret Pole is torn between the queen and her husband, Henry VIII.",
                Price = 18.99m,
                Cost = 3.99m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 9, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222198,
                Title = "Bones Never Lie",
                Authors = "Kathy Reichs",
                Description = "A child murderer who eluded capture years ago has resurfaced, giving the forensic anthropologist Temperance Brennan another chance to stop her.",
                Price = 14.95m,
                Cost = 9.42m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 9, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222199,
                Title = "Nora Webster",
                Authors = "Colm Toibin",
                Description = "In the 1970s, an Irish widow struggles to find her identity.",
                Price = 30.99m,
                Cost = 4.65m,
                InventoryQuantity = 3,
                ReorderPoint = 2,
                PublishDate = new DateTime(2014, 10, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222200,
                Title = "Winter Street",
                Authors = "Elin Hilderbrand",
                Description = "Complications ensue when the owner of Nantucket’s Winter Street Inn gathers his four children and their families for Christmas.",
                Price = 20.95m,
                Cost = 2.30m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2014, 10, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222201,
                Title = "The Narrow Road To The Deep North",
                Authors = "Richard Flanagan",
                Description = "The hero of the Man Booker Prize-winning novel about love, good and evil is a P.O.W. working on the Thai-Burma Death Railway during World War II.",
                Price = 34.00m,
                Cost = 20.74m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2014, 10, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222202,
                Title = "The Handsome Man's De Luxe Café",
                Authors = "Alexander McCall Smith",
                Description = "The 15th book in the No. 1 Ladies’ Detective Agency series.",
                Price = 25.95m,
                Cost = 17.91m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2014, 11, 1),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222203,
                Title = "The Burning Room",
                Authors = "Michael Connelly",
                Description = "The Los Angeles detective Harry Bosch and his new partner investigate two long-unsolved cases.",
                Price = 36.99m,
                Cost = 17.39m,
                InventoryQuantity = 9,
                ReorderPoint = 7,
                PublishDate = new DateTime(2014, 11, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222204,
                Title = "The Job",
                Authors = "Janet Evanovich and Lee Goldberg",
                Description = "The F.B.I. special agent Kate O’Hare works with Nicolas Fox, a handsome con man, to pursue a drug kingpin.",
                Price = 28.95m,
                Cost = 20.27m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 11, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222205,
                Title = "The Cinderella Murder",
                Authors = "Mary Higgins Clark and Alafair Burke",
                Description = "A  TV producer plans a show about a cold case — the murder of a U.C.L.A. student who was found with one shoe missing.",
                Price = 25.95m,
                Cost = 5.97m,
                InventoryQuantity = 4,
                ReorderPoint = 1,
                PublishDate = new DateTime(2014, 11, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222206,
                Title = "The Mistletoe Promise",
                Authors = "Richard Paul Evans",
                Description = "A divorced woman enters into a contract with a strange man to pretend to be a couple until Christmas.",
                Price = 23.99m,
                Cost = 12.23m,
                InventoryQuantity = 11,
                ReorderPoint = 10,
                PublishDate = new DateTime(2014, 11, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222207,
                Title = "Hope To Die",
                Authors = "James Patterson",
                Description = "Detective Alex Cross’s family is kidnapped by a madman who wants to turn Cross into a perfect killer.",
                Price = 31.95m,
                Cost = 3.51m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2014, 11, 29),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222208,
                Title = "Trust No One",
                Authors = "Jayne Ann Krentz",
                Description = "A woman who is being stalked is aided by an unlikely ally.",
                Price = 17.95m,
                Cost = 12.39m,
                InventoryQuantity = 10,
                ReorderPoint = 9,
                PublishDate = new DateTime(2015, 1, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222209,
                Title = "Private Vegas",
                Authors = "James Patterson and Maxine Paetro",
                Description = "Jack Morgan, the head of an investigative firm, uncovers a murder ring in Las Vegas.",
                Price = 32.00m,
                Cost = 25.92m,
                InventoryQuantity = 11,
                ReorderPoint = 8,
                PublishDate = new DateTime(2015, 1, 31),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222210,
                Title = "Trigger Warning",
                Authors = "Neil Gaiman",
                Description = "Stories and poems about the power of imagination.",
                Price = 17.95m,
                Cost = 10.41m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2015, 2, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Poetry")
            });

            Books.Add(new Book {
                BookNumber = 222211,
                Title = "Twelve Days",
                Authors = "Alex Berenson",
                Description = "The former C.I.A. operative John Wells discovers a plot to trick the president into invading Iran.",
                Price = 25.00m,
                Cost = 18.50m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2015, 2, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222212,
                Title = "A Spool Of Blue Thread",
                Authors = "Anne Tyler",
                Description = "Four generations of a family are drawn to a house in the Baltimore suburbs.",
                Price = 15.95m,
                Cost = 1.75m,
                InventoryQuantity = 10,
                ReorderPoint = 10,
                PublishDate = new DateTime(2015, 2, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222213,
                Title = "Holy Cow",
                Authors = "David Duchovny",
                Description = "A light-hearted fable by the actor.",
                Price = 19.99m,
                Cost = 11.79m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2015, 2, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Humor")
            });

            Books.Add(new Book {
                BookNumber = 222214,
                Title = "Prodigal Son",
                Authors = "Danielle Steel",
                Description = "Twins, one good and one bad, reunite after 20 years when one of them returns to their hometown. But it is no longer clear who the good and who the bad one is.",
                Price = 18.95m,
                Cost = 9.10m,
                InventoryQuantity = 3,
                ReorderPoint = 2,
                PublishDate = new DateTime(2015, 2, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222215,
                Title = "Last One Home",
                Authors = "Debbie Macomber",
                Description = "Three estranged sisters work to resolve their differences",
                Price = 20.00m,
                Cost = 15.60m,
                InventoryQuantity = 5,
                ReorderPoint = 5,
                PublishDate = new DateTime(2015, 3, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222216,
                Title = "The Lady From Zagreb",
                Authors = "Philip Kerr",
                Description = "The former Berlin homicide detective Bernie Gunther is sent to Croatia by Joseph Goebbels to persuade a film star to appear in a movie.",
                Price = 20.95m,
                Cost = 19.27m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2015, 4, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222217,
                Title = "14Th Deadly Sin",
                Authors = "James Patterson and Maxine Paetro",
                Description = "A video of a shocking crime surfaces, casting suspicion on a San Francisco detective's colleagues.",
                Price = 24.99m,
                Cost = 6.25m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2015, 5, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222218,
                Title = "The Fateful Lightning",
                Authors = "Jeff Shaara",
                Description = "The fourth and final volume in a series of Civil War novels describes the war's last months through multiple perspectives.",
                Price = 28.95m,
                Cost = 5.50m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2015, 6, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222219,
                Title = "In The Unlikely Event",
                Authors = "Judy Blume",
                Description = "Secrets are revealed and love stories play out against the backdrop of a series of panic-inducing plane crashes in 1950s New Jersey.",
                Price = 18.95m,
                Cost = 16.87m,
                InventoryQuantity = 12,
                ReorderPoint = 9,
                PublishDate = new DateTime(2015, 6, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222220,
                Title = "The Little Paris Bookshop",
                Authors = "Nina George",
                Description = "A bookseller with a knack for finding just the right book for making others feel better embarks on a journey in pursuit of his own happiness.",
                Price = 34.00m,
                Cost = 5.78m,
                InventoryQuantity = 11,
                ReorderPoint = 6,
                PublishDate = new DateTime(2015, 7, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222221,
                Title = "Friction",
                Authors = "Sandra Brown",
                Description = "A Texas Ranger fights for custody of his daughter amid complications stemming from his attraction to the judge.",
                Price = 18.99m,
                Cost = 2.85m,
                InventoryQuantity = 8,
                ReorderPoint = 7,
                PublishDate = new DateTime(2015, 8, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222222,
                Title = "The Solomon Curse",
                Authors = "Clive Cussler and Russell Blake",
                Description = "The wealthy couple Sam and Remi Fargo investigate a dangerous legend in the Solomon Islands.",
                Price = 26.95m,
                Cost = 18.06m,
                InventoryQuantity = 11,
                ReorderPoint = 9,
                PublishDate = new DateTime(2015, 9, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222223,
                Title = "One Year After",
                Authors = "William R Forstchen",
                Description = "A New Regime imposes tyranny in the aftermath of a nuclear attack.",
                Price = 23.95m,
                Cost = 3.59m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2015, 9, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222224,
                Title = "The Murder House",
                Authors = "James Patterson and David Ellis",
                Description = "When bodies are found at a Hamptons estate where a series of grisly murders once occurred, a local detective and former New York City cop investigates.",
                Price = 23.95m,
                Cost = 17.72m,
                InventoryQuantity = 6,
                ReorderPoint = 6,
                PublishDate = new DateTime(2015, 10, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222225,
                Title = "All The Stars In The Heavens",
                Authors = "Adriana Trigiani",
                Description = "A fictional treatment of the life of the actress Loretta Young focuses on her rumored affair with the married Clark Gable and her subsequent pregnancy.",
                Price = 26.95m,
                Cost = 19.94m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2015, 10, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222226,
                Title = "The Lake House",
                Authors = "Kate Morton",
                Description = "A London detective investigating a missing-persons case becomes curious about an unsolved 1933 kidnapping in Cornwall.",
                Price = 16.95m,
                Cost = 10.17m,
                InventoryQuantity = 12,
                ReorderPoint = 8,
                PublishDate = new DateTime(2015, 10, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222227,
                Title = "The Japanese Lover",
                Authors = "Isabel Allende",
                Description = "A young refugee from the Nazis and the son of her family’s Japanese gardener must hide their love, although it lasts a lifetime.",
                Price = 33.99m,
                Cost = 8.16m,
                InventoryQuantity = 7,
                ReorderPoint = 3,
                PublishDate = new DateTime(2015, 11, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222228,
                Title = "The Promise",
                Authors = "Robert Crais",
                Description = "The Los Angeles P.I. Elvis Cole joins forces with the K-9 officer Scott James of the L.A.P.D. and his German shepherd, Maggie, as well as his partner, Joe Pike, to foil a criminal mastermind.",
                Price = 27.95m,
                Cost = 18.73m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2015, 11, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222229,
                Title = "The Pharaoh's Secret",
                Authors = "Clive Cussler and Graham Brown",
                Description = "Kurt Austin and Joe Zavala must save the NUMA crew from a mysterious toxin deployed by a villain who wants to build a new Egyptian empire.",
                Price = 31.95m,
                Cost = 12.46m,
                InventoryQuantity = 3,
                ReorderPoint = 2,
                PublishDate = new DateTime(2015, 11, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222230,
                Title = "The Guilty",
                Authors = "David Baldacci",
                Description = "The government hit man Will Robie investigates murder charges against his estranged father in their Mississippi hometown.",
                Price = 19.95m,
                Cost = 17.16m,
                InventoryQuantity = 3,
                ReorderPoint = 3,
                PublishDate = new DateTime(2015, 11, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222231,
                Title = "The Mistletoe Inn",
                Authors = "Richard Paul Evans",
                Description = "An aspiring romance writer with a broken heart meets a complicated man at a Christmas writers’ retreat.",
                Price = 36.95m,
                Cost = 11.09m,
                InventoryQuantity = 10,
                ReorderPoint = 10,
                PublishDate = new DateTime(2015, 11, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222232,
                Title = "Find Her",
                Authors = "Lisa Gardner",
                Description = "The Boston detective D.D. Warren hunts for a missing woman who was kidnapped and abused as a college student and may have become a vigilante.",
                Price = 28.95m,
                Cost = 10.71m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 2, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222233,
                Title = "Wedding Cake Murder",
                Authors = "Joanne Fluke",
                Description = "The Lake Eden, Minn., baker Hannah Swensen is about to get married, but first she must solve the murder of a visiting celebrity chef. Recipes included.",
                Price = 23.95m,
                Cost = 17.72m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 2, 27),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222234,
                Title = "The Gangster",
                Authors = "Clive Cussler and Justin Scott",
                Description = "n the ninth book in this series, set in 1906, the New York detective Isaac Bell contends with a crime boss passing as a respectable businessman and a tycoon’s plot against President Theodore Roosevelt.",
                Price = 30.95m,
                Cost = 7.12m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2016, 3, 5),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222235,
                Title = "Fool Me Once",
                Authors = "Harlan Coben",
                Description = "A retired Army helicopter pilot faces combat-related nightmares and mysteries concerning the deaths of her husband and sister.",
                Price = 24.95m,
                Cost = 12.72m,
                InventoryQuantity = 11,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 3, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222236,
                Title = "Robert B. Parker's Slow Burn",
                Authors = "Ace Atkins",
                Description = "Spenser is back, leaving a trail of antagonism as he investigates a series of suspicious Boston fires.",
                Price = 22.99m,
                Cost = 10.58m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2016, 5, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222237,
                Title = "Zero K",
                Authors = "Don DeLillo",
                Description = "A billionaire and his son meet at a remote desert compound dedicated to preserving bodies until they can be returned to life in the future.",
                Price = 20.00m,
                Cost = 15.20m,
                InventoryQuantity = 6,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 5, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222238,
                Title = "The Second Life Of Nick Mason",
                Authors = "Steve Hamilton",
                Description = "A deal with a fellow inmate, a crime boss, springs Nick Mason from prison, but he must become an assassin.",
                Price = 19.99m,
                Cost = 3.20m,
                InventoryQuantity = 5,
                ReorderPoint = 2,
                PublishDate = new DateTime(2016, 5, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222239,
                Title = "The Weekenders",
                Authors = "Mary Kay Andrews",
                Description = "On the North Carolina island of Belle Isle, a woman investigates her husband’s shady financial affairs after his mysterious death.",
                Price = 20.95m,
                Cost = 12.78m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2016, 5, 21),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222240,
                Title = "The Emperor's Revenge",
                Authors = "Clive Cussler and Boyd Morrison",
                Description = "Juan Cabrillo teams up with a former C.I.A. colleague to thwart a plan involving the death of millions and international economic meltdown.",
                Price = 27.99m,
                Cost = 20.43m,
                InventoryQuantity = 3,
                ReorderPoint = 1,
                PublishDate = new DateTime(2016, 6, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222241,
                Title = "Homegoing",
                Authors = "Yaa Gyasi",
                Description = "This Ghanaian-American writer’s first novel traces the lives in West Africa and America of seven generations of the descendants of two half sisters.",
                Price = 26.95m,
                Cost = 2.70m,
                InventoryQuantity = 8,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 6, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222242,
                Title = "Here's To Us",
                Authors = "Elin Hilderbrand",
                Description = "Sparks fly as a celebrity chef’s ex-wives pile into a small cabin in Nantucket to join his widow for the reading of his will.",
                Price = 26.95m,
                Cost = 8.62m,
                InventoryQuantity = 13,
                ReorderPoint = 9,
                PublishDate = new DateTime(2016, 6, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222243,
                Title = "The Pursuit",
                Authors = "Janet Evanovich and Lee Goldberg",
                Description = "The F.B.I. agent Kate O’Hare and her con man partner, Nick Fox, face off against a dangerous ex-Serbian military officer.",
                Price = 25.99m,
                Cost = 18.45m,
                InventoryQuantity = 8,
                ReorderPoint = 5,
                PublishDate = new DateTime(2016, 6, 25),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222244,
                Title = "Among The Wicked",
                Authors = "Linda Castillo",
                Description = "Chief of Police Kate Burkholder goes undercover as a widow in a reclusive Amish community to investigate a girl's death.",
                Price = 24.00m,
                Cost = 6.72m,
                InventoryQuantity = 10,
                ReorderPoint = 9,
                PublishDate = new DateTime(2016, 7, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222245,
                Title = "The Woman In Cabin 10",
                Authors = "Ruth Ware",
                Description = "A travel writer on a cruise is certain she has heard a body thrown overboard, but no one believes her.",
                Price = 32.00m,
                Cost = 3.52m,
                InventoryQuantity = 9,
                ReorderPoint = 7,
                PublishDate = new DateTime(2016, 7, 23),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222246,
                Title = "Truly Madly Guilty",
                Authors = "Liane Moriarty",
                Description = "Tense turning points for three couples at a backyard barbecue gone wrong.",
                Price = 14.99m,
                Cost = 10.04m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2016, 7, 30),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222247,
                Title = "The Underground Railroad",
                Authors = "Colson Whitehead",
                Description = "A slave girl heads toward freedom on the network, envisioned as actual tracks and tunnels.",
                Price = 32.00m,
                Cost = 3.20m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 8, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222248,
                Title = "Dragonmark",
                Authors = "Sherrilyn Kenyon",
                Description = "The first book of a new trilogy featuring Illarion, a dragon made into a human  against his will. A Dark-Hunter novel.",
                Price = 29.95m,
                Cost = 3.29m,
                InventoryQuantity = 10,
                ReorderPoint = 7,
                PublishDate = new DateTime(2016, 8, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222249,
                Title = "Another Brooklyn",
                Authors = "Jacqueline Woodson",
                Description = "An adult novel by an award-winning children's book author centers on memories of growing up and the close friendship of four girls.",
                Price = 36.00m,
                Cost = 9.72m,
                InventoryQuantity = 12,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 8, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222250,
                Title = "Sting",
                Authors = "Sandra Brown",
                Description = "A hired killer and a woman he kidnapped join forces to elude the F.B.I. agents and others who are searching for her corrupt brother.",
                Price = 27.00m,
                Cost = 8.91m,
                InventoryQuantity = 9,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 8, 20),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222251,
                Title = "The Kept Woman",
                Authors = "Karin Slaughter",
                Description = "Will Trent of the Georgia Bureau of Investigation and his lover, the medical examiner Sara Linton, pursue a case involving a dirty Atlanta cop.",
                Price = 16.95m,
                Cost = 15.26m,
                InventoryQuantity = 2,
                ReorderPoint = 2,
                PublishDate = new DateTime(2016, 9, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222252,
                Title = "Twelve Days Of Christmas",
                Authors = "Debbie Macomber",
                Description = "A woman starts a blog about her attempt to reach out to a grumpy neighbor at Christmastime, and finds herself falling for him.",
                Price = 25.99m,
                Cost = 23.13m,
                InventoryQuantity = 11,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 10, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222253,
                Title = "Winter Storms",
                Authors = "Elin Hilderbrand",
                Description = "In the final book of the Winter Street trilogy, a huge snowstorm bearing down on Nantucket threatens the Quinn family’s Christmas, after a year of significant events.",
                Price = 29.00m,
                Cost = 24.94m,
                InventoryQuantity = 7,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 10, 8),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222254,
                Title = "Vince Flynn: Order To Kill",
                Authors = "Kyle Mills",
                Description = "Flynn’s character, the C.I.A. operative Mitch Rapp, uncovers a dangerous Russian plot. Flynn died in 2013.",
                Price = 16.95m,
                Cost = 13.73m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2016, 10, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222255,
                Title = "Crimson Death",
                Authors = "Laurell K Hamilton",
                Description = "The vampire hunter Anita Blake, her friend Edward and her servant Damian travel to Ireland to battle an unusual vampire infestation.",
                Price = 16.99m,
                Cost = 7.65m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 10, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222256,
                Title = "The Obsidian Chamber",
                Authors = "Douglas Preston and Lincoln Child",
                Description = "While the F.B.I. agent Aloysius Pendergast is believed dead, his ward is kidnapped.",
                Price = 17.00m,
                Cost = 3.57m,
                InventoryQuantity = 13,
                ReorderPoint = 10,
                PublishDate = new DateTime(2016, 10, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222257,
                Title = "Escape Clause",
                Authors = "John Sandford",
                Description = "Virgil Flowers of the Minnesota Bureau of Criminal Apprehension must deal with the theft of tigers from the local zoo.",
                Price = 35.95m,
                Cost = 7.19m,
                InventoryQuantity = 5,
                ReorderPoint = 4,
                PublishDate = new DateTime(2016, 10, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222258,
                Title = "The Whistler",
                Authors = "John Grisham",
                Description = "A whistleblower alerts a Florida investigator to judicial corruption involving the Mob and Indian casinos.",
                Price = 26.95m,
                Cost = 13.48m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2016, 10, 29),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222259,
                Title = "The Whole Town's Talking",
                Authors = "Fannie Flagg",
                Description = "A century of life in small-town Elmwood Springs, Mo.",
                Price = 31.99m,
                Cost = 21.11m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2016, 12, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222260,
                Title = "Rogue One: A Star Wars Story",
                Authors = "Alexander Freed",
                Description = "A novelization of the new movie, including additional scenes.",
                Price = 33.95m,
                Cost = 23.77m,
                InventoryQuantity = 7,
                ReorderPoint = 6,
                PublishDate = new DateTime(2016, 12, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222261,
                Title = "The Mistress",
                Authors = "Danielle Steel",
                Description = "The beautiful mistress of a Russian oligarch falls in love with an artist and yearns for freedom.",
                Price = 36.95m,
                Cost = 15.52m,
                InventoryQuantity = 8,
                ReorderPoint = 4,
                PublishDate = new DateTime(2017, 1, 7),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222262,
                Title = "Ring Of Fire",
                Authors = "Brad Taylor",
                Description = "Pike Logan, a member of a secret counterterrorist unit called the Taskforce, investigates a Saudi-backed Moroccan terrorist cell.",
                Price = 22.00m,
                Cost = 19.58m,
                InventoryQuantity = 6,
                ReorderPoint = 4,
                PublishDate = new DateTime(2017, 1, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222263,
                Title = "Death's Mistress",
                Authors = "Terry Goodkind",
                Description = "The first book of a new series, the Nicci Chronicles, centers on a character from the Sword of Truth fantasy series.",
                Price = 20.00m,
                Cost = 12.00m,
                InventoryQuantity = 8,
                ReorderPoint = 7,
                PublishDate = new DateTime(2017, 1, 28),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222264,
                Title = "4 3 2 1",
                Authors = "Paul Auster",
                Description = "Four versions of the formative years of a Jewish boy born in Newark in 1947.",
                Price = 20.95m,
                Cost = 5.87m,
                InventoryQuantity = 9,
                ReorderPoint = 8,
                PublishDate = new DateTime(2017, 2, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222265,
                Title = "Gunmetal Gray",
                Authors = "Mark Greaney",
                Description = "Court Gentry, now working for the C.I.A., pursues a Chinese hacker who is on the run.",
                Price = 21.95m,
                Cost = 16.24m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2017, 2, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222266,
                Title = "Banana Cream Pie Murder",
                Authors = "Joanne Fluke",
                Description = "Hannah Swensen, the bakery owner and amateur sleuth of Lake Eden, Minn., returns from her honeymoon to confront an actress’s mysterious death.",
                Price = 36.95m,
                Cost = 14.41m,
                InventoryQuantity = 7,
                ReorderPoint = 5,
                PublishDate = new DateTime(2017, 3, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222267,
                Title = "Silence Fallen",
                Authors = "Patricia Briggs",
                Description = "The shape-shifter Mercy Thompson finds herself in the clutches of the world’s most powerful vampire.",
                Price = 36.00m,
                Cost = 10.08m,
                InventoryQuantity = 7,
                ReorderPoint = 7,
                PublishDate = new DateTime(2017, 3, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222268,
                Title = "Without Warning",
                Authors = "Joel C Rosenberg",
                Description = "A journalist pursues the head of ISIS after an attack on the Capitol when the administration fails to take action.",
                Price = 27.95m,
                Cost = 12.02m,
                InventoryQuantity = 11,
                ReorderPoint = 7,
                PublishDate = new DateTime(2017, 3, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222269,
                Title = "Song Of The Lion",
                Authors = "Anne Hillerman",
                Description = "The third Southwestern mystery featuring the Navajo police officer Bernadette Manuelito and her husband, Jim Chee.",
                Price = 31.99m,
                Cost = 24.63m,
                InventoryQuantity = 8,
                ReorderPoint = 6,
                PublishDate = new DateTime(2017, 4, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222270,
                Title = "The Burial Hour",
                Authors = "Jeffery Deaver",
                Description = "Lincoln Rhyme travels to Italy to investigate a case.",
                Price = 16.00m,
                Cost = 1.60m,
                InventoryQuantity = 5,
                ReorderPoint = 5,
                PublishDate = new DateTime(2017, 4, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222271,
                Title = "Nighthawk",
                Authors = "Clive Cussler and Graham Brown",
                Description = "The NUMA crew races the Russians and Chinese in a hunt for a missing American aircraft.",
                Price = 30.00m,
                Cost = 21.60m,
                InventoryQuantity = 6,
                ReorderPoint = 1,
                PublishDate = new DateTime(2017, 6, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222272,
                Title = "The Identicals",
                Authors = "Elin Hilderbrand",
                Description = "Complications in the lives of identical twins who were raised by their divorced parents, one on Nantucket, one on Martha’s Vineyard.",
                Price = 33.95m,
                Cost = 4.41m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2017, 6, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222273,
                Title = "House Of Spies",
                Authors = "Daniel Silva",
                Description = "Gabriel Allon, the Israeli art restorer and spy, now the head of Israel’s secret intelligence service, pursues an ISIS mastermind.",
                Price = 33.95m,
                Cost = 17.31m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2017, 7, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222274,
                Title = "Two Nights",
                Authors = "Kathy Reichs",
                Description = "Sunday Night, the heroine of a new series from the creator of Temperance Brennan, searches for a girl who may have been kidnapped by a cult.",
                Price = 17.95m,
                Cost = 15.98m,
                InventoryQuantity = 10,
                ReorderPoint = 9,
                PublishDate = new DateTime(2017, 7, 15),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222275,
                Title = "Meddling Kids",
                Authors = "Edgar Cantero",
                Description = "Four old friends return to the site of their teenage adventures.",
                Price = 30.95m,
                Cost = 3.71m,
                InventoryQuantity = 4,
                ReorderPoint = 3,
                PublishDate = new DateTime(2017, 7, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222276,
                Title = "Watch Me Disappear",
                Authors = "Janelle Brown",
                Description = "When a Berkeley woman vanishes after a hiking trip, her husband and daughter discover disturbing secrets.",
                Price = 32.95m,
                Cost = 30.64m,
                InventoryQuantity = 4,
                ReorderPoint = 4,
                PublishDate = new DateTime(2017, 7, 22),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222277,
                Title = "The Store",
                Authors = "James Patterson and Richard DiLallo",
                Description = "Two New York writers go undercover to expose the secrets of a powerful retailer.",
                Price = 31.00m,
                Cost = 15.19m,
                InventoryQuantity = 2,
                ReorderPoint = 2,
                PublishDate = new DateTime(2017, 8, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222278,
                Title = "I Know A Secret",
                Authors = "Tess Gerritsen",
                Description = "Rizzoli and Isles investigate two separate homicides and uncover other dangerous mysteries.",
                Price = 32.95m,
                Cost = 26.36m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2017, 8, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222279,
                Title = "Sulfur Springs",
                Authors = "William Kent Krueger",
                Description = "A newly married couple search for the wife's missing son, which leads them to a border town in the middle of a drug war.",
                Price = 32.95m,
                Cost = 17.79m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2017, 8, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222280,
                Title = "Enemy Of The State",
                Authors = "Kyle Mills",
                Description = "Vince Flynn's character Mitch Rapp leaves the C.I.A. to go on a manhunt when the nephew of a Saudi King finances a terrorist group.",
                Price = 20.95m,
                Cost = 4.19m,
                InventoryQuantity = 13,
                ReorderPoint = 8,
                PublishDate = new DateTime(2017, 9, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222281,
                Title = "Little Fires Everywhere",
                Authors = "Celeste Ng",
                Description = "An artist with a mysterious past and a disregard for the status quo upends a quiet town outside Cleveland.",
                Price = 16.00m,
                Cost = 12.00m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2017, 9, 16),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222282,
                Title = "Twin Peaks: The Final Dossier",
                Authors = "Mark Frost",
                Description = "Updated profiles on the residents of Twin Peaks are assembled by special agent Tamara Preston.",
                Price = 27.95m,
                Cost = 8.66m,
                InventoryQuantity = 2,
                ReorderPoint = 1,
                PublishDate = new DateTime(2017, 11, 4),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222283,
                Title = "The House Of Unexpected Sisters",
                Authors = "Alexander McCall Smith",
                Description = "During an investigation, Precious Ramotswe encounters a man from her past and a nurse who has her last name.",
                Price = 29.95m,
                Cost = 16.47m,
                InventoryQuantity = 1,
                ReorderPoint = 1,
                PublishDate = new DateTime(2017, 11, 11),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222284,
                Title = "Artemis",
                Authors = "Andy Weir",
                Description = "A small-time smuggler living in a lunar colony schemes to pay off an old debt by pulling off a challenging heist.",
                Price = 31.00m,
                Cost = 18.91m,
                InventoryQuantity = 8,
                ReorderPoint = 6,
                PublishDate = new DateTime(2017, 11, 18),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222285,
                Title = "Robicheaux",
                Authors = "James Lee Burke",
                Description = "A bereaved detective confronts his past and works to clear his name when he becomes a suspect during an investigation into the murder of the man who killed his wife.",
                Price = 17.95m,
                Cost = 15.98m,
                InventoryQuantity = 6,
                ReorderPoint = 5,
                PublishDate = new DateTime(2018, 1, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Thriller")
            });

            Books.Add(new Book {
                BookNumber = 222286,
                Title = "Unbound",
                Authors = "Stuart Woods",
                Description = "The 44th book in the Stone Barrington series.",
                Price = 16.00m,
                Cost = 11.36m,
                InventoryQuantity = 8,
                ReorderPoint = 3,
                PublishDate = new DateTime(2018, 1, 6),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222287,
                Title = "The Immortalists",
                Authors = "Chloe Benjamin",
                Description = "Four adolescents learn the dates of their deaths from a psychic and their lives go on different courses.",
                Price = 31.00m,
                Cost = 22.32m,
                InventoryQuantity = 9,
                ReorderPoint = 6,
                PublishDate = new DateTime(2018, 1, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222288,
                Title = "Blood Fury",
                Authors = "JR Ward",
                Description = "The third book in the Black Dagger Legacy series.",
                Price = 30.95m,
                Cost = 21.05m,
                InventoryQuantity = 13,
                ReorderPoint = 10,
                PublishDate = new DateTime(2018, 1, 13),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Fantasy")
            });

            Books.Add(new Book {
                BookNumber = 222289,
                Title = "The Grave's A Fine And Private Place",
                Authors = "Alan Bradley",
                Description = "Flavia de Luce, a young British sleuth, gets involved in solving a murder after experiencing a family tragedy.",
                Price = 26.95m,
                Cost = 21.83m,
                InventoryQuantity = 7,
                ReorderPoint = 5,
                PublishDate = new DateTime(2018, 2, 3),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222290,
                Title = "An American Marriage",
                Authors = "Tayari Jones",
                Description = "A newlywed couple's relationship is tested when the husband is sentenced to 12 years in prison.",
                Price = 22.95m,
                Cost = 12.16m,
                InventoryQuantity = 11,
                ReorderPoint = 9,
                PublishDate = new DateTime(2018, 2, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222291,
                Title = "Fifty Fifty",
                Authors = "James Patterson and Candice Fox",
                Description = "Detective Harriet Blue tries to clear her brother's name and save a small Australian town from being massacred.",
                Price = 35.99m,
                Cost = 28.07m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2018, 2, 24),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222292,
                Title = "Star Wars: The Last Jedi",
                Authors = "Jason Fry",
                Description = "An adaptation of the film, written with input from its director, Rian Johnson, which includes scenes from alternate versions of the script.",
                Price = 28.99m,
                Cost = 24.64m,
                InventoryQuantity = 4,
                ReorderPoint = 2,
                PublishDate = new DateTime(2018, 3, 10),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Science Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222293,
                Title = "Caribbean Rim",
                Authors = "Randy Wayne White",
                Description = "The 25th book in the Doc Ford series. The marine biologist searches for a state agency official and rare Spanish coins.",
                Price = 15.00m,
                Cost = 9.45m,
                InventoryQuantity = 10,
                ReorderPoint = 8,
                PublishDate = new DateTime(2018, 3, 17),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222294,
                Title = "To Die But Once",
                Authors = "Jacqueline Winspear",
                Description = "In 1940, months after Britain declared war on Germany, Maisie Dobbs investigates the disappearance of an apprentice working on a government contract.",
                Price = 24.95m,
                Cost = 19.21m,
                InventoryQuantity = 5,
                ReorderPoint = 1,
                PublishDate = new DateTime(2018, 3, 31),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Suspense")
            });

            Books.Add(new Book {
                BookNumber = 222295,
                Title = "Macbeth",
                Authors = "Jo Nesbo",
                Description = "In this adaptation of Shakespeare's tragedy, police in a 1970s industrial town take on a pair of drug lords.",
                Price = 28.95m,
                Cost = 7.53m,
                InventoryQuantity = 12,
                ReorderPoint = 7,
                PublishDate = new DateTime(2018, 4, 14),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Shakespeare")
            });

            Books.Add(new Book {
                BookNumber = 222296,
                Title = "The High Tide Club",
                Authors = "Mary Kay Andrews",
                Description = "An eccentric millionaire enlists the attorney Brooke Trappnell to fix old wrongs, which sets up a potential scandal and murder.",
                Price = 23.95m,
                Cost = 14.13m,
                InventoryQuantity = 14,
                ReorderPoint = 10,
                PublishDate = new DateTime(2018, 5, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            Books.Add(new Book {
                BookNumber = 222297,
                Title = "Warlight",
                Authors = "Michael Ondaatje",
                Description = "In Britain after World War II, a pair of teenage siblings are taken under the tutelage of a mysterious man and his cronies who served during the war.",
                Price = 26.00m,
                Cost = 20.28m,
                InventoryQuantity = 10,
                ReorderPoint = 6,
                PublishDate = new DateTime(2018, 5, 12),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Historical Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222298,
                Title = "The Cast",
                Authors = "Danielle Steel",
                Description = "A magazine columnist meets an array of Hollywood professionals when a producer turns a story about her grandmother into a TV series.",
                Price = 21.95m,
                Cost = 12.95m,
                InventoryQuantity = 15,
                ReorderPoint = 10,
                PublishDate = new DateTime(2018, 5, 19),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Romance")
            });

            Books.Add(new Book {
                BookNumber = 222299,
                Title = "Beach House Reunion",
                Authors = "Mary Alice Monroe",
                Description = "Three generations of a family gather one summer in South Carolina.",
                Price = 32.95m,
                Cost = 6.59m,
                InventoryQuantity = 6,
                ReorderPoint = 3,
                PublishDate = new DateTime(2018, 5, 26),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Contemporary Fiction")
            });

            Books.Add(new Book {
                BookNumber = 222300,
                Title = "Turbulence",
                Authors = "Stuart Woods",
                Description = "The 46th book in the Stone Barrington series.",
                Price = 15.95m,
                Cost = 6.06m,
                InventoryQuantity = 13,
                ReorderPoint = 10,
                PublishDate = new DateTime(2018, 6, 9),
                BookStatus = "Active",
                Genre = db.Genres.FirstOrDefault(g => g.GenreName == "Mystery")
            });

            foreach (var b in Books)
            {
                var existing = db.Books.FirstOrDefault(x => x.Title == b.Title);
                if (existing == null)
                {
                    db.Books.Add(b);
                }
                else
                {
                    existing.Title = b.Title;
                    existing.Authors = b.Authors;
                    existing.Description = b.Description;
                    existing.Price = b.Price;
                    existing.Cost = b.Cost;
                    existing.InventoryQuantity = b.InventoryQuantity;
                    existing.ReorderPoint = b.ReorderPoint;
                    existing.PublishDate = b.PublishDate;
                    existing.BookStatus = b.BookStatus;
                    existing.Genre = b.Genre;
                }
            }
            db.SaveChanges();
        }
    }
}

