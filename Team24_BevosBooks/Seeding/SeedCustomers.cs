using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Team24_BevosBooks.Seeding
{
	public static class SeedCustomers
	{
		public static void SeedAllCustomers(AppDbContext db)
		{
			Int32 intCustomersAdded = 0;
			String strCustomerFlag = "Begin";

			List<Customer> Customers = new List<Customer>();

			Customer c1 = new Customer()
			{
				CustomerID = 9010,
				Password = "bookworm",
				LastName = "Baker",
				FirstName = "Christopher",
				Birthday = new DateTime(1949, 11, 23),
				Street = "1898 Schurz Alley",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				SSN = "477-44-9589",
				Email = "cbaker@example.com",
				Phone = "5725458641"
			};
			Customers.Add(c1);

			Customer c2 = new Customer()
			{
				CustomerID = 9011,
				Password = "potato",
				LastName = "Banks",
				FirstName = "Michelle",
				Birthday = new DateTime(1962, 11, 27),
				Street = "97 Elmside Pass",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				SSN = "247-31-0787",
				Email = "banker@longhorn.net",
				Phone = "9867048435"
			};
			Customers.Add(c2);

			Customer c3 = new Customer()
			{
				CustomerID = 9012,
				Password = "painting",
				LastName = "Broccolo",
				FirstName = "Franco",
				Birthday = new DateTime(1992, 10, 11),
				Street = "88 Crowley Circle",
				City = "Austin",
				State = "TX",
				Zip = "78786",
				SSN = "486-47-8748",
				Email = "franco@example.com",
				Phone = "6836109514"
			};
			Customers.Add(c3);

			Customer c4 = new Customer()
			{
				CustomerID = 9013,
				Password = "texas1",
				LastName = "Chang",
				FirstName = "Wendy",
				Birthday = new DateTime(1997, 5, 16),
				Street = "56560 Sage Junction",
				City = "Eagle Pass",
				State = "TX",
				Zip = "78852",
				SSN = "182-52-9193",
				Email = "wchang@example.com",
				Phone = "7070911071"
			};
			Customers.Add(c4);

			Customer c5 = new Customer()
			{
				CustomerID = 9014,
				Password = "Anchorage",
				LastName = "Chou",
				FirstName = "Lim",
				Birthday = new DateTime(1970, 4, 6),
				Street = "60 Lunder Point",
				City = "Austin",
				State = "TX",
				Zip = "78729",
				SSN = "679-75-0653",
				Email = "limchou@gogle.com",
				Phone = "1488907687"
			};
			Customers.Add(c5);

			Customer c6 = new Customer()
			{
				CustomerID = 9015,
				Password = "aggies",
				LastName = "Dixon",
				FirstName = "Shan",
				Birthday = new DateTime(1984, 1, 12),
				Street = "9448 Pleasure Avenue",
				City = "Georgetown",
				State = "TX",
				Zip = "78628",
				SSN = "593-06-9800",
				Email = "shdixon@aoll.com",
				Phone = "6899701824"
			};
			Customers.Add(c6);

			Customer c7 = new Customer()
			{
				CustomerID = 9016,
				Password = "hampton1",
				LastName = "Evans",
				FirstName = "Jim Bob",
				Birthday = new DateTime(1959, 9, 9),
				Street = "51 Emmet Parkway",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				SSN = "647-72-4711",
				Email = "j.b.evans@aheca.org",
				Phone = "9986825917"
			};
			Customers.Add(c7);

			Customer c8 = new Customer()
			{
				CustomerID = 9017,
				Password = "longhorns",
				LastName = "Feeley",
				FirstName = "Lou Ann",
				Birthday = new DateTime(2001, 1, 12),
				Street = "65 Darwin Crossing",
				City = "Austin",
				State = "TX",
				Zip = "78704",
				SSN = "577-89-2279",
				Email = "feeley@penguin.org",
				Phone = "3464121966"
			};
			Customers.Add(c8);

			Customer c9 = new Customer()
			{
				CustomerID = 9018,
				Password = "mustangs",
				LastName = "Freeley",
				FirstName = "Tesa",
				Birthday = new DateTime(1991, 2, 4),
				Street = "7352 Loftsgordon Court",
				City = "College Station",
				State = "TX",
				Zip = "77840",
				SSN = "853-72-9538",
				Email = "tfreeley@minnetonka.ci.us",
				Phone = "6581357270"
			};
			Customers.Add(c9);

			Customer c10 = new Customer()
			{
				CustomerID = 9019,
				Password = "onetime",
				LastName = "Garcia",
				FirstName = "Margaret",
				Birthday = new DateTime(1991, 10, 2),
				Street = "7 International Road",
				City = "Austin",
				State = "TX",
				Zip = "78756",
				SSN = "887-12-0229",
				Email = "mgarcia@gogle.com",
				Phone = "3767347949"
			};
			Customers.Add(c10);

			Customer c11 = new Customer()
			{
				CustomerID = 9020,
				Password = "pepperoni",
				LastName = "Haley",
				FirstName = "Charles",
				Birthday = new DateTime(1974, 7, 10),
				Street = "8 Warrior Trail",
				City = "Austin",
				State = "TX",
				Zip = "78746",
				SSN = "528-58-6911",
				Email = "chaley@thug.com",
				Phone = "2198604221"
			};
			Customers.Add(c11);

			Customer c12 = new Customer()
			{
				CustomerID = 9021,
				Password = "raiders",
				LastName = "Hampton",
				FirstName = "Jeffrey",
				Birthday = new DateTime(2004, 3, 10),
				Street = "9107 Lighthouse Bay Road",
				City = "Austin",
				State = "TX",
				Zip = "78756",
				SSN = "658-45-9102",
				Email = "jeffh@sonic.com",
				Phone = "1222185888"
			};
			Customers.Add(c12);

			Customer c13 = new Customer()
			{
				CustomerID = 9022,
				Password = "jhearn22",
				LastName = "Hearn",
				FirstName = "John",
				Birthday = new DateTime(1950, 8, 5),
				Street = "59784 Pierstorff Center",
				City = "Liberty",
				State = "TX",
				Zip = "77575",
				SSN = "712-69-1666",
				Email = "wjhearniii@umich.org",
				Phone = "5123071976"
			};
			Customers.Add(c13);

			Customer c14 = new Customer()
			{
				CustomerID = 9023,
				Password = "hickhickup",
				LastName = "Hicks",
				FirstName = "Anthony",
				Birthday = new DateTime(2004, 12, 8),
				Street = "932 Monica Way",
				City = "San Antonio",
				State = "TX",
				Zip = "78203",
				SSN = "179-94-2233",
				Email = "ahick@yaho.com",
				Phone = "1211949601"
			};
			Customers.Add(c14);

			Customer c15 = new Customer()
			{
				CustomerID = 9024,
				Password = "ingram2015",
				LastName = "Ingram",
				FirstName = "Brad",
				Birthday = new DateTime(2001, 9, 5),
				Street = "4 Lukken Court",
				City = "New Braunfels",
				State = "TX",
				Zip = "78132",
				SSN = "126-14-4287",
				Email = "ingram@jack.com",
				Phone = "1372121569"
			};
			Customers.Add(c15);

			Customer c16 = new Customer()
			{
				CustomerID = 9025,
				Password = "toddy25",
				LastName = "Jacobs",
				FirstName = "Todd",
				Birthday = new DateTime(1999, 1, 20),
				Street = "7 Susan Junction",
				City = "New York",
				State = "NY",
				Zip = "10101",
				SSN = "355-61-0890",
				Email = "toddj@yourmom.com",
				Phone = "8543163836"
			};
			Customers.Add(c16);

			Customer c17 = new Customer()
			{
				CustomerID = 9026,
				Password = "something",
				LastName = "Lawrence",
				FirstName = "Victoria",
				Birthday = new DateTime(2000, 4, 14),
				Street = "669 Oak Junction",
				City = "Lockhart",
				State = "TX",
				Zip = "78644",
				SSN = "840-91-4997",
				Email = "thequeen@aska.net",
				Phone = "3214163359"
			};
			Customers.Add(c17);

			Customer c18 = new Customer()
			{
				CustomerID = 9027,
				Password = "Password1",
				LastName = "Lineback",
				FirstName = "Erik",
				Birthday = new DateTime(2003, 12, 2),
				Street = "099 Luster Point",
				City = "Kingwood",
				State = "TX",
				Zip = "77325",
				SSN = "303-25-5592",
				Email = "linebacker@gogle.com",
				Phone = "2505265350"
			};
			Customers.Add(c18);

			Customer c19 = new Customer()
			{
				CustomerID = 9028,
				Password = "aclfest2017",
				LastName = "Lowe",
				FirstName = "Ernest",
				Birthday = new DateTime(1977, 12, 7),
				Street = "35473 Hansons Hill",
				City = "Beverly Hills",
				State = "CA",
				Zip = "90210",
				SSN = "547-72-1592",
				Email = "elowe@netscare.net",
				Phone = "4070619503"
			};
			Customers.Add(c19);

			Customer c20 = new Customer()
			{
				CustomerID = 9029,
				Password = "nothinggood",
				LastName = "Luce",
				FirstName = "Chuck",
				Birthday = new DateTime(1949, 3, 16),
				Street = "4 Emmet Junction",
				City = "Navasota",
				State = "TX",
				Zip = "77868",
				SSN = "434-46-8800",
				Email = "cluce@gogle.com",
				Phone = "7358436110"
			};
			Customers.Add(c20);

			Customer c21 = new Customer()
			{
				CustomerID = 9030,
				Password = "whatever",
				LastName = "MacLeod",
				FirstName = "Jennifer",
				Birthday = new DateTime(1947, 2, 21),
				Street = "3 Orin Road",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				SSN = "219-58-3683",
				Email = "mackcloud@george.com",
				Phone = "7240178229"
			};
			Customers.Add(c21);

			Customer c22 = new Customer()
			{
				CustomerID = 9031,
				Password = "snowsnow",
				LastName = "Markham",
				FirstName = "Elizabeth",
				Birthday = new DateTime(1972, 3, 20),
				Street = "8171 Commercial Crossing",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				SSN = "116-38-6529",
				Email = "cmartin@beets.com",
				Phone = "2495200223"
			};
			Customers.Add(c22);

			Customer c23 = new Customer()
			{
				CustomerID = 9032,
				Password = "whocares",
				LastName = "Martin",
				FirstName = "Clarence",
				Birthday = new DateTime(1992, 7, 19),
				Street = "96 Anthes Place",
				City = "Schenectady",
				State = "NY",
				Zip = "12345",
				SSN = "220-24-4049",
				Email = "clarence@yoho.com",
				Phone = "4086179161"
			};
			Customers.Add(c23);

			Customer c24 = new Customer()
			{
				CustomerID = 9033,
				Password = "xcellent",
				LastName = "Martinez",
				FirstName = "Gregory",
				Birthday = new DateTime(1947, 5, 28),
				Street = "10 Northridge Plaza",
				City = "Austin",
				State = "TX",
				Zip = "78717",
				SSN = "559-67-5740",
				Email = "gregmartinez@drdre.com",
				Phone = "9371927523"
			};
			Customers.Add(c24);

			Customer c25 = new Customer()
			{
				CustomerID = 9034,
				Password = "mydogspot",
				LastName = "Miller",
				FirstName = "Charles",
				Birthday = new DateTime(1990, 10, 15),
				Street = "87683 Schmedeman Circle",
				City = "Austin",
				State = "TX",
				Zip = "78727",
				SSN = "209-79-0473",
				Email = "cmiller@bob.com",
				Phone = "5954063857"
			};
			Customers.Add(c25);

			Customer c26 = new Customer()
			{
				CustomerID = 9035,
				Password = "spotmydog",
				LastName = "Nelson",
				FirstName = "Kelly",
				Birthday = new DateTime(1971, 7, 13),
				Street = "3244 Ludington Court",
				City = "Beaumont",
				State = "TX",
				Zip = "77720",
				SSN = "227-64-1445",
				Email = "knelson@aoll.com",
				Phone = "8929209512"
			};
			Customers.Add(c26);

			Customer c27 = new Customer()
			{
				CustomerID = 9036,
				Password = "joejoejoe",
				LastName = "Nguyen",
				FirstName = "Joe",
				Birthday = new DateTime(1984, 3, 17),
				Street = "4780 Talisman Court",
				City = "San Marcos",
				State = "TX",
				Zip = "78667",
				SSN = "480-18-2513",
				Email = "joewin@xfactor.com",
				Phone = "9226301774"
			};
			Customers.Add(c27);

			Customer c28 = new Customer()
			{
				CustomerID = 9037,
				Password = "billyboy",
				LastName = "O'Reilly",
				FirstName = "Bill",
				Birthday = new DateTime(1959, 7, 8),
				Street = "4154 Delladonna Plaza",
				City = "Bergheim",
				State = "TX",
				Zip = "78004",
				SSN = "505-04-1746",
				Email = "orielly@foxnews.cnn",
				Phone = "2537646912"
			};
			Customers.Add(c28);

			Customer c29 = new Customer()
			{
				CustomerID = 9038,
				Password = "radgirl",
				LastName = "Radkovich",
				FirstName = "Anka",
				Birthday = new DateTime(1966, 5, 19),
				Street = "72361 Bayside Drive",
				City = "Austin",
				State = "TX",
				Zip = "78789",
				SSN = "772-85-3188",
				Email = "ankaisrad@gogle.com",
				Phone = "2182889379"
			};
			Customers.Add(c29);

			Customer c30 = new Customer()
			{
				CustomerID = 9039,
				Password = "meganr34",
				LastName = "Rhodes",
				FirstName = "Megan",
				Birthday = new DateTime(1965, 3, 12),
				Street = "76875 Hoffman Point",
				City = "Orlando",
				State = "FL",
				Zip = "32830",
				SSN = "855-90-6552",
				Email = "megrhodes@freserve.co.uk",
				Phone = "9532396075"
			};
			Customers.Add(c30);

			Customer c31 = new Customer()
			{
				CustomerID = 9040,
				Password = "ricearoni",
				LastName = "Rice",
				FirstName = "Eryn",
				Birthday = new DateTime(1975, 4, 28),
				Street = "048 Elmside Park",
				City = "South Padre Island",
				State = "TX",
				Zip = "78597",
				SSN = "208-34-2385",
				Email = "erynrice@aoll.com",
				Phone = "7303815953"
			};
			Customers.Add(c31);

			Customer c32 = new Customer()
			{
				CustomerID = 9041,
				Password = "alaskaboy",
				LastName = "Rodriguez",
				FirstName = "Jorge",
				Birthday = new DateTime(1953, 12, 8),
				Street = "01 Browning Pass",
				City = "Austin",
				State = "TX",
				Zip = "78744",
				SSN = "391-71-4611",
				Email = "jorge@noclue.com",
				Phone = "3677322422"
			};
			Customers.Add(c32);

			Customer c33 = new Customer()
			{
				CustomerID = 9042,
				Password = "bunnyhop",
				LastName = "Rogers",
				FirstName = "Allen",
				Birthday = new DateTime(1973, 4, 22),
				Street = "844 Anderson Alley",
				City = "Canyon Lake",
				State = "TX",
				Zip = "78133",
				SSN = "308-74-1186",
				Email = "mrrogers@lovelyday.com",
				Phone = "3911705385"
			};
			Customers.Add(c33);

			Customer c34 = new Customer()
			{
				CustomerID = 9043,
				Password = "dustydusty",
				LastName = "Saint-Jean",
				FirstName = "Olivier",
				Birthday = new DateTime(1995, 2, 19),
				Street = "1891 Docker Point",
				City = "Austin",
				State = "TX",
				Zip = "78779",
				SSN = "832-08-8657",
				Email = "stjean@athome.com",
				Phone = "7351610920"
			};
			Customers.Add(c34);

			Customer c35 = new Customer()
			{
				CustomerID = 9044,
				Password = "jrod2017",
				LastName = "Saunders",
				FirstName = "Sarah",
				Birthday = new DateTime(1978, 2, 19),
				Street = "1469 Upham Road",
				City = "Austin",
				State = "TX",
				Zip = "78720",
				SSN = "485-81-2960",
				Email = "saunders@pen.com",
				Phone = "5269661692"
			};
			Customers.Add(c35);

			Customer c36 = new Customer()
			{
				CustomerID = 9045,
				Password = "martin1234",
				LastName = "Sewell",
				FirstName = "William",
				Birthday = new DateTime(2004, 12, 23),
				Street = "1672 Oak Valley Circle",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				SSN = "845-76-6886",
				Email = "willsheff@email.com",
				Phone = "1875727246"
			};
			Customers.Add(c36);

			Customer c37 = new Customer()
			{
				CustomerID = 9046,
				Password = "penguin12",
				LastName = "Sheffield",
				FirstName = "Martin",
				Birthday = new DateTime(1960, 5, 8),
				Street = "816 Kennedy Place",
				City = "Round Rock",
				State = "TX",
				Zip = "78680",
				SSN = "786-58-8427",
				Email = "sheffiled@gogle.com",
				Phone = "1394323615"
			};
			Customers.Add(c37);

			Customer c38 = new Customer()
			{
				CustomerID = 9047,
				Password = "rogerthat",
				LastName = "Smith",
				FirstName = "John",
				Birthday = new DateTime(1955, 6, 25),
				Street = "0745 Golf Road",
				City = "Austin",
				State = "TX",
				Zip = "78760",
				SSN = "833-36-3857",
				Email = "johnsmith187@aoll.com",
				Phone = "6645937874"
			};
			Customers.Add(c38);

			Customer c39 = new Customer()
			{
				CustomerID = 9048,
				Password = "smitty444",
				LastName = "Stroud",
				FirstName = "Dustin",
				Birthday = new DateTime(1967, 7, 26),
				Street = "505 Dexter Plaza",
				City = "Sweet Home",
				State = "TX",
				Zip = "77987",
				SSN = "862-95-5935",
				Email = "dustroud@mail.com",
				Phone = "6470254680"
			};
			Customers.Add(c39);

			Customer c40 = new Customer()
			{
				CustomerID = 9049,
				Password = "stewball",
				LastName = "Stuart",
				FirstName = "Eric",
				Birthday = new DateTime(1947, 12, 4),
				Street = "585 Claremont Drive",
				City = "Corpus Christi",
				State = "TX",
				Zip = "78412",
				SSN = "401-87-6781",
				Email = "estuart@anchor.net",
				Phone = "7701621022"
			};
			Customers.Add(c40);

			Customer c41 = new Customer()
			{
				CustomerID = 9050,
				Password = "slowwind",
				LastName = "Stump",
				FirstName = "Peter",
				Birthday = new DateTime(1974, 7, 10),
				Street = "89035 Welch Circle",
				City = "Pflugerville",
				State = "TX",
				Zip = "78660",
				SSN = "414-55-9948",
				Email = "peterstump@noclue.com",
				Phone = "2181960061"
			};
			Customers.Add(c41);

			Customer c42 = new Customer()
			{
				CustomerID = 9051,
				Password = "tanner5454",
				LastName = "Tanner",
				FirstName = "Jeremy",
				Birthday = new DateTime(1944, 1, 11),
				Street = "4 Stang Trail",
				City = "Austin",
				State = "TX",
				Zip = "78702",
				SSN = "215-59-9614",
				Email = "jtanner@mustang.net",
				Phone = "9908469499"
			};
			Customers.Add(c42);

			Customer c43 = new Customer()
			{
				CustomerID = 9052,
				Password = "allyrally",
				LastName = "Taylor",
				FirstName = "Allison",
				Birthday = new DateTime(1990, 11, 14),
				Street = "726 Twin Pines Avenue",
				City = "Austin",
				State = "TX",
				Zip = "78713",
				SSN = "263-91-7172",
				Email = "taylordjay@aoll.com",
				Phone = "7011918647"
			};
			Customers.Add(c43);

			Customer c44 = new Customer()
			{
				CustomerID = 9053,
				Password = "taylorbaylor",
				LastName = "Taylor",
				FirstName = "Rachel",
				Birthday = new DateTime(1976, 1, 18),
				Street = "06605 Sugar Drive",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				SSN = "774-06-1511",
				Email = "rtaylor@gogle.com",
				Phone = "8937910053"
			};
			Customers.Add(c44);

			Customer c45 = new Customer()
			{
				CustomerID = 9054,
				Password = "teeoff22",
				LastName = "Tee",
				FirstName = "Frank",
				Birthday = new DateTime(1998, 9, 6),
				Street = "3567 Dawn Plaza",
				City = "Austin",
				State = "TX",
				Zip = "78786",
				SSN = "522-65-3064",
				Email = "teefrank@noclue.com",
				Phone = "6394568913"
			};
			Customers.Add(c45);

			Customer c46 = new Customer()
			{
				CustomerID = 9055,
				Password = "tucksack1",
				LastName = "Tucker",
				FirstName = "Clent",
				Birthday = new DateTime(1943, 2, 25),
				Street = "704 Northland Alley",
				City = "San Antonio",
				State = "TX",
				Zip = "78279",
				SSN = "876-29-4912",
				Email = "ctucker@alphabet.co.uk",
				Phone = "2676838676"
			};
			Customers.Add(c46);

			Customer c47 = new Customer()
			{
				CustomerID = 9056,
				Password = "meow88",
				LastName = "Velasco",
				FirstName = "Allen",
				Birthday = new DateTime(1985, 9, 10),
				Street = "72 Harbort Point",
				City = "Navasota",
				State = "TX",
				Zip = "77868",
				SSN = "216-67-9243",
				Email = "avelasco@yoho.com",
				Phone = "3452909754"
			};
			Customers.Add(c47);

			Customer c48 = new Customer()
			{
				CustomerID = 9057,
				Password = "vinovino",
				LastName = "Vino",
				FirstName = "Janet",
				Birthday = new DateTime(1985, 2, 7),
				Street = "1 Oak Valley Place",
				City = "Boston",
				State = "MA",
				Zip = "02114",
				SSN = "565-57-4107",
				Email = "vinovino@grapes.com",
				Phone = "8567089194"
			};
			Customers.Add(c48);

			Customer c49 = new Customer()
			{
				CustomerID = 9058,
				Password = "gowest",
				LastName = "West",
				FirstName = "Jake",
				Birthday = new DateTime(1976, 1, 9),
				Street = "48743 Banding Parkway",
				City = "Marble Falls",
				State = "TX",
				Zip = "78654",
				SSN = "390-37-6155",
				Email = "westj@pioneer.net",
				Phone = "6260784394"
			};
			Customers.Add(c49);

			Customer c50 = new Customer()
			{
				CustomerID = 9059,
				Password = "louielouie",
				LastName = "Winthorpe",
				FirstName = "Louis",
				Birthday = new DateTime(1953, 4, 19),
				Street = "96850 Summit Crossing",
				City = "Austin",
				State = "TX",
				Zip = "78730",
				SSN = "445-77-2754",
				Email = "winner@hootmail.com",
				Phone = "3733971174"
			};
			Customers.Add(c50);

			Customer c51 = new Customer()
			{
				CustomerID = 9060,
				Password = "woodyman1",
				LastName = "Wood",
				FirstName = "Reagan",
				Birthday = new DateTime(2002, 12, 28),
				Street = "18354 Bluejay Street",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				SSN = "805-38-7710",
				Email = "rwood@voyager.net",
				Phone = "8433359800"
			};
			Customers.Add(c51);

			try
			{
				foreach (Customer c in Customers)
				{
					strCustomerFlag = c.Email;
					Customer dbCustomer = db.Customers.FirstOrDefault(x => x.CustomerID == c.CustomerID);
					if (dbCustomer == null)
					{
						db.Customers.Add(c);
						db.SaveChanges();
						intCustomersAdded += 1;
					}
					else
					{
						dbCustomer.FirstName = c.FirstName;
						dbCustomer.LastName = c.LastName;
						dbCustomer.Password = c.Password;
						dbCustomer.Birthday = c.Birthday;
						dbCustomer.Street = c.Street;
						dbCustomer.City = c.City;
						dbCustomer.State = c.State;
						dbCustomer.Zip = c.Zip;
						dbCustomer.Phone = c.Phone;
						dbCustomer.Email = c.Email;
						dbCustomer.SSN = c.SSN;
						db.Update(dbCustomer);
						db.SaveChanges();
						intCustomersAdded += 1;
					}
				}
			}

			catch (Exception ex)
			{
				String msg = " Customers added: " + intCustomersAdded + "; Error on " + strCustomerFlag;
				throw new InvalidOperationException(ex.Message + msg);
			}
		}
	}
}
