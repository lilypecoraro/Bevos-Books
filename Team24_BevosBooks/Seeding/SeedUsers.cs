using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Seeding
{
    public static class SeedUsers
    {
        public static async Task SeedAllUsers(UserManager<AppUser> userManager)
        {
            int customerNumber = 9010;

            // ---------------------------
            // Helper to create customer
            // ---------------------------
            async Task CreateCustomer(string email, string password, string first, string last,
                                      string address, string city, string state, string zip, string phone)
            {
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = first,
                        LastName = last,
                        Address = address,
                        City = city,
                        State = state,
                        ZipCode = zip,
                        PhoneNumber = phone,
                        Status = AppUser.UserStatus.Customer,
                        CustomerNumber = customerNumber
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Customer");
                    }

                    customerNumber++;
                }
                else
                {
                    customerNumber++; // maintain numbering even if already exists
                }
            }

            // ---------------------------
            // Helper to create employee/admin
            // ---------------------------
            async Task CreateEmployee(string email, string password, string first, string last,
                                      string address, string city, string state, string zip,
                                      string phone, AppUser.UserStatus status, string role)
            {
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new AppUser
                    {
                        UserName = email,
                        Email = email,
                        FirstName = first,
                        LastName = last,
                        Address = address,
                        City = city,
                        State = state,
                        ZipCode = zip,
                        PhoneNumber = phone,
                        Status = status
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }


            // ============================================================
            //                      CUSTOMERS (9010–9060)
            // ============================================================

            await CreateCustomer("cbaker@example.com", "bookworm", "Christopher", "Baker",
                "1898 Schurz Alley", "Austin", "TX", "78705", "5725458641");

            await CreateCustomer("banker@longhorn.net", "potato", "Michelle", "Banks",
                "97 Elmside Pass", "Austin", "TX", "78712", "9867048435");

            await CreateCustomer("franco@example.com", "painting", "Franco", "Broccolo",
                "88 Crowley Circle", "Austin", "TX", "78786", "6836109514");

            await CreateCustomer("wchang@example.com", "texas1", "Wendy", "Chang",
                "56560 Sage Junction", "Eagle Pass", "TX", "78852", "7070911071");

            await CreateCustomer("limchou@gogle.com", "Anchorage", "Lim", "Chou",
                "60 Lunder Point", "Austin", "TX", "78729", "1488907687");

            await CreateCustomer("shdixon@aoll.com", "aggies", "Shan", "Dixon",
                "9448 Pleasure Avenue", "Georgetown", "TX", "78628", "6899701824");

            await CreateCustomer("j.b.evans@aheca.org", "hampton1", "Jim Bob", "Evans",
                "51 Emmet Parkway", "Austin", "TX", "78705", "9986825917");

            await CreateCustomer("feeley@penguin.org", "longhorns", "Lou Ann", "Feeley",
                "65 Darwin Crossing", "Austin", "TX", "78704", "3464121966");

            await CreateCustomer("tfreeley@minnetonka.ci.us", "mustangs", "Tesa", "Freeley",
                "7352 Loftsgordon Court", "College Station", "TX", "77840", "6581357270");

            await CreateCustomer("mgarcia@gogle.com", "onetime", "Margaret", "Garcia",
                "7 International Road", "Austin", "TX", "78756", "3767347949");

            await CreateCustomer("chaley@thug.com", "pepperoni", "Charles", "Haley",
                "8 Warrior Trail", "Austin", "TX", "78746", "2198604221");

            await CreateCustomer("jeffh@sonic.com", "raiders", "Jeffrey", "Hampton",
                "9107 Lighthouse Bay Road", "Austin", "TX", "78756", "1222185888");

            await CreateCustomer("wjhearniii@umich.org", "jhearn22", "John", "Hearn",
                "59784 Pierstorff Center", "Liberty", "TX", "77575", "5123071976");

            await CreateCustomer("ahick@yaho.com", "hickhickup", "Anthony", "Hicks",
                "932 Monica Way", "San Antonio", "TX", "78203", "1211949601");

            await CreateCustomer("ingram@jack.com", "ingram2015", "Brad", "Ingram",
                "4 Lukken Court", "New Braunfels", "TX", "78132", "1372121569");

            await CreateCustomer("toddj@yourmom.com", "toddy25", "Todd", "Jacobs",
                "7 Susan Junction", "New York", "NY", "10101", "8543163836");

            await CreateCustomer("thequeen@aska.net", "something", "Victoria", "Lawrence",
                "669 Oak Junction", "Lockhart", "TX", "78644", "3214163359");

            await CreateCustomer("linebacker@gogle.com", "Password1", "Erik", "Lineback",
                "099 Luster Point", "Kingwood", "TX", "77325", "2505265350");

            await CreateCustomer("elowe@netscare.net", "aclfest2017", "Ernest", "Lowe",
                "35473 Hansons Hill", "Beverly Hills", "CA", "90210", "4070619503");

            await CreateCustomer("cluce@gogle.com", "nothinggood", "Chuck", "Luce",
                "4 Emmet Junction", "Navasota", "TX", "77868", "7358436110");

            await CreateCustomer("mackcloud@george.com", "whatever", "Jennifer", "MacLeod",
                "3 Orin Road", "Austin", "TX", "78712", "7240178229");

            await CreateCustomer("cmartin@beets.com", "snowsnow", "Elizabeth", "Markham",
                "8171 Commercial Crossing", "Austin", "TX", "78712", "2495200223");

            await CreateCustomer("clarence@yoho.com", "whocares", "Clarence", "Martin",
                "96 Anthes Place", "Schenectady", "NY", "12345", "4086179161");

            await CreateCustomer("gregmartinez@drdre.com", "xcellent", "Gregory", "Martinez",
                "10 Northridge Plaza", "Austin", "TX", "78717", "9371927523");

            await CreateCustomer("cmiller@bob.com", "mydogspot", "Charles", "Miller",
                "87683 Schmedeman Circle", "Austin", "TX", "78727", "5954063857");

            await CreateCustomer("knelson@aoll.com", "spotmydog", "Kelly", "Nelson",
                "3244 Ludington Court", "Beaumont", "TX", "77720", "8929209512");

            await CreateCustomer("joewin@xfactor.com", "joejoejoe", "Joe", "Nguyen",
                "4780 Talisman Court", "San Marcos", "TX", "78667", "9226301774");

            await CreateCustomer("orielly@foxnews.cnn", "billyboy", "Bill", "O'Reilly",
                "4154 Delladonna Plaza", "Bergheim", "TX", "78004", "2537646912");

            await CreateCustomer("ankaisrad@gogle.com", "radgirl", "Anka", "Radkovich",
                "72361 Bayside Drive", "Austin", "TX", "78789", "2182889379");

            await CreateCustomer("megrhodes@freserve.co.uk", "meganr34", "Megan", "Rhodes",
                "76875 Hoffman Point", "Orlando", "FL", "32830", "9532396075");

            await CreateCustomer("erynrice@aoll.com", "ricearoni", "Eryn", "Rice",
                "048 Elmside Park", "South Padre Island", "TX", "78597", "7303815953");

            await CreateCustomer("jorge@noclue.com", "alaskaboy", "Jorge", "Rodriguez",
                "01 Browning Pass", "Austin", "TX", "78744", "3677322422");

            await CreateCustomer("mrrogers@lovelyday.com", "bunnyhop", "Allen", "Rogers",
                "844 Anderson Alley", "Canyon Lake", "TX", "78133", "3911705385");

            await CreateCustomer("stjean@athome.com", "dustydusty", "Olivier", "Saint-Jean",
                "1891 Docker Point", "Austin", "TX", "78779", "7351610920");

            await CreateCustomer("saunders@pen.com", "jrod2017", "Sarah", "Saunders",
                "1469 Upham Road", "Austin", "TX", "78720", "5269661692");

            await CreateCustomer("willsheff@email.com", "martin1234", "William", "Sewell",
                "1672 Oak Valley Circle", "Austin", "TX", "78705", "1875727246");

            await CreateCustomer("sheffiled@gogle.com", "penguin12", "Martin", "Sheffield",
                "816 Kennedy Place", "Round Rock", "TX", "78680", "1394323615");

            await CreateCustomer("johnsmith187@aoll.com", "rogerthat", "John", "Smith",
                "0745 Golf Road", "Austin", "TX", "78760", "6645937874");

            await CreateCustomer("dustroud@mail.com", "smitty444", "Dustin", "Stroud",
                "505 Dexter Plaza", "Sweet Home", "TX", "77987", "6470254680");

            await CreateCustomer("estuart@anchor.net", "stewball", "Eric", "Stuart",
                "585 Claremont Drive", "Corpus Christi", "TX", "78412", "7701621022");

            await CreateCustomer("peterstump@noclue.com", "slowwind", "Peter", "Stump",
                "89035 Welch Circle", "Pflugerville", "TX", "78660", "2181960061");

            await CreateCustomer("jtanner@mustang.net", "tanner5454", "Jeremy", "Tanner",
                "4 Stang Trail", "Austin", "TX", "78702", "9908469499");

            await CreateCustomer("taylordjay@aoll.com", "allyrally", "Allison", "Taylor",
                "726 Twin Pines Avenue", "Austin", "TX", "78713", "7011918647");

            await CreateCustomer("rtaylor@gogle.com", "taylorbaylor", "Rachel", "Taylor",
                "06605 Sugar Drive", "Austin", "TX", "78712", "8937910053");

            await CreateCustomer("teefrank@noclue.com", "teeoff22", "Frank", "Tee",
                "3567 Dawn Plaza", "Austin", "TX", "78786", "6394568913");

            await CreateCustomer("ctucker@alphabet.co.uk", "tucksack1", "Clent", "Tucker",
                "704 Northland Alley", "San Antonio", "TX", "78279", "2676838676");

            await CreateCustomer("avelasco@yoho.com", "meow88", "Allen", "Velasco",
                "72 Harbort Point", "Navasota", "TX", "77868", "3452909754");

            await CreateCustomer("vinovino@grapes.com", "vinovino", "Janet", "Vino",
                "1 Oak Valley Place", "Boston", "MA", "02114", "8567089194");

            await CreateCustomer("westj@pioneer.net", "gowest", "Jake", "West",
                "48743 Banding Parkway", "Marble Falls", "TX", "78654", "6260784394");

            await CreateCustomer("winner@hootmail.com", "louielouie", "Louis", "Winthorpe",
                "96850 Summit Crossing", "Austin", "TX", "78730", "3733971174");

            await CreateCustomer("rwood@voyager.net", "woodyman1", "Reagan", "Wood",
                "18354 Bluejay Street", "Austin", "TX", "78712", "8433359800");


            // ============================================================
            //                    EMPLOYEES + ADMINS
            // ============================================================

            await CreateEmployee("c.baker@bevosbooks.com", "dewey4", "Christopher", "Baker",
                "1245 Lake Libris Dr.", "Cedar Park", "TX", "78613", "3395325649",
                AppUser.UserStatus.Admin, "Admin");

            await CreateEmployee("s.barnes@bevosbooks.com", "smitty", "Susan", "Barnes",
                "888 S. Main", "Kyle", "TX", "78640", "9636389416",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("h.garcia@bevosbooks.com", "squirrel", "Hector", "Garcia",
                "777 PBR Drive", "Austin", "TX", "78712", "4547135738",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("b.ingram@bevosbooks.com", "changalang", "Brad", "Ingram",
                "6548 La Posada Ct.", "Austin", "TX", "78705", "5817343315",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("j.jackson@bevosbooks.com", "rhythm", "Jack", "Jackson",
                "222 Main", "Austin", "TX", "78760", "8241915317",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("t.jacobs@bevosbooks.com", "approval", "Todd", "Jacobs",
                "4564 Elm St.", "Georgetown", "TX", "78628", "2477822475",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("l.jones@bevosbooks.com", "society", "Lester", "Jones",
                "999 LeBlat", "Austin", "TX", "78747", "4764966462",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("b.larson@bevosbooks.com", "tanman", "Bill", "Larson",
                "1212 N. First Ave", "Round Rock", "TX", "78665", "3355258855",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("v.lawrence@bevosbooks.com", "longhorns", "Victoria", "Lawrence",
                "6639 Bookworm Ln.", "Austin", "TX", "78712", "7511273054",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("m.lopez@bevosbooks.com", "swansong", "Marshall", "Lopez",
                "90 SW North St", "Austin", "TX", "78729", "7477907070",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("j.macleod@bevosbooks.com", "fungus", "Jennifer", "MacLeod",
                "2504 Far West Blvd.", "Austin", "TX", "78705", "2621216845",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("e.markham@bevosbooks.com", "median", "Elizabeth", "Markham",
                "7861 Chevy Chase", "Austin", "TX", "78785", "5028075807",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("g.martinez@bevosbooks.com", "decorate", "Gregory", "Martinez",
                "8295 Sunset Blvd.", "Austin", "TX", "78712", "1994708542",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("j.mason@bevosbooks.com", "rankmary", "Jack", "Mason",
                "444 45th St", "Austin", "TX", "78701", "1748136441",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("c.miller@bevosbooks.com", "kindly", "Charles", "Miller",
                "8962 Main St.", "Austin", "TX", "78709", "8999319585",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("m.nguyen@bevosbooks.com", "ricearoni", "Mary", "Nguyen",
                "465 N. Bear Cub", "Austin", "TX", "78734", "8716746381",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("s.rankin@bevosbooks.com", "walkamile", "Suzie", "Rankin",
                "23 Dewey Road", "Austin", "TX", "78712", "5239029525",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("m.rhodes@bevosbooks.com", "ingram45", "Megan", "Rhodes",
                "4587 Enfield Rd.", "Austin", "TX", "78729", "1232139514",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("e.rice@bevosbooks.com", "arched", "Eryn", "Rice",
                "3405 Rio Grande", "Austin", "TX", "78746", "2706602803",
                AppUser.UserStatus.Admin, "Admin");

            await CreateEmployee("a.rogers@bevosbooks.com", "lottery", "Allen", "Rogers",
                "4965 Oak Hill", "Austin", "TX", "78705", "4139645586",
                AppUser.UserStatus.Admin, "Admin");

            await CreateEmployee("s.saunders@bevosbooks.com", "nostalgic", "Sarah", "Saunders",
                "332 Avenue C", "Austin", "TX", "78733", "9036349587",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("w.sewell@bevosbooks.com", "offbeat", "William", "Sewell",
                "2365 51st St.", "Austin", "TX", "78755", "7224308314",
                AppUser.UserStatus.Admin, "Admin");

            await CreateEmployee("m.sheffield@bevosbooks.com", "evanescent", "Martin", "Sheffield",
                "3886 Avenue A", "San Marcos", "TX", "78666", "9349192978",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("c.silva@bevosbooks.com", "stewboy", "Cindy", "Silva",
                "900 4th St", "Austin", "TX", "78758", "4874328170",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("e.stuart@bevosbooks.com", "instrument", "Eric", "Stuart",
                "5576 Toro Ring", "Austin", "TX", "78758", "1967846827",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("j.tanner@bevosbooks.com", "hecktour", "Jeremy", "Tanner",
                "4347 Almstead", "Austin", "TX", "78712", "5923026779",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("a.taylor@bevosbooks.com", "countryrhodes", "Allison", "Taylor",
                "467 Nueces St.", "Austin", "TX", "78727", "7246195827",
                AppUser.UserStatus.Employee, "Employee");

            await CreateEmployee("r.taylor@bevosbooks.com", "landus", "Rachel", "Taylor",
                "345 Longview Dr.", "Austin", "TX", "78746", "9071236087",
                AppUser.UserStatus.Admin, "Admin");
        }
    }
}
