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

            // CUSTOMER: Christopher Baker
            if (await userManager.FindByEmailAsync("cbaker@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "cbaker@example.com",
                    Email = "cbaker@example.com",
                    FirstName = "Christopher",
                    LastName = "Baker",
                    Address = "1898 Schurz Alley, Austin, TX 78705",
                    PhoneNumber = "5725458641",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "bookworm");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Michelle Banks
            if (await userManager.FindByEmailAsync("banker@longhorn.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "banker@longhorn.net",
                    Email = "banker@longhorn.net",
                    FirstName = "Michelle",
                    LastName = "Banks",
                    Address = "97 Elmside Pass, Austin, TX 78712",
                    PhoneNumber = "9867048435",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "potato");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Franco Broccolo
            if (await userManager.FindByEmailAsync("franco@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "franco@example.com",
                    Email = "franco@example.com",
                    FirstName = "Franco",
                    LastName = "Broccolo",
                    Address = "88 Crowley Circle, Austin, TX 78786",
                    PhoneNumber = "6836109514",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "painting");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Wendy Chang
            if (await userManager.FindByEmailAsync("wchang@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "wchang@example.com",
                    Email = "wchang@example.com",
                    FirstName = "Wendy",
                    LastName = "Chang",
                    Address = "56560 Sage Junction, Eagle Pass, TX 78852",
                    PhoneNumber = "7070911071",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "texas1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Lim Chou
            if (await userManager.FindByEmailAsync("limchou@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "limchou@gogle.com",
                    Email = "limchou@gogle.com",
                    FirstName = "Lim",
                    LastName = "Chou",
                    Address = "60 Lunder Point, Austin, TX 78729",
                    PhoneNumber = "1488907687",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "Anchorage");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Shan Dixon
            if (await userManager.FindByEmailAsync("shdixon@aoll.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "shdixon@aoll.com",
                    Email = "shdixon@aoll.com",
                    FirstName = "Shan",
                    LastName = "Dixon",
                    Address = "9448 Pleasure Avenue, Georgetown, TX 78628",
                    PhoneNumber = "6899701824",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "aggies");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jim Bob Evans
            if (await userManager.FindByEmailAsync("j.b.evans@aheca.org") == null)
            {
                var user = new AppUser
                {
                    UserName = "j.b.evans@aheca.org",
                    Email = "j.b.evans@aheca.org",
                    FirstName = "Jim Bob",
                    LastName = "Evans",
                    Address = "51 Emmet Parkway, Austin, TX 78705",
                    PhoneNumber = "9986825917",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "hampton1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Lou Ann Feeley
            if (await userManager.FindByEmailAsync("feeley@penguin.org") == null)
            {
                var user = new AppUser
                {
                    UserName = "feeley@penguin.org",
                    Email = "feeley@penguin.org",
                    FirstName = "Lou Ann",
                    LastName = "Feeley",
                    Address = "65 Darwin Crossing, Austin, TX 78704",
                    PhoneNumber = "3464121966",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "longhorns");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Tesa Freeley
            if (await userManager.FindByEmailAsync("tfreeley@minnetonka.ci.us") == null)
            {
                var user = new AppUser
                {
                    UserName = "tfreeley@minnetonka.ci.us",
                    Email = "tfreeley@minnetonka.ci.us",
                    FirstName = "Tesa",
                    LastName = "Freeley",
                    Address = "7352 Loftsgordon Court, College Station, TX 77840",
                    PhoneNumber = "6581357270",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "mustangs");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Margaret Garcia
            if (await userManager.FindByEmailAsync("mgarcia@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "mgarcia@gogle.com",
                    Email = "mgarcia@gogle.com",
                    FirstName = "Margaret",
                    LastName = "Garcia",
                    Address = "7 International Road, Austin, TX 78756",
                    PhoneNumber = "3767347949",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "onetime");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Charles Haley
            if (await userManager.FindByEmailAsync("chaley@thug.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "chaley@thug.com",
                    Email = "chaley@thug.com",
                    FirstName = "Charles",
                    LastName = "Haley",
                    Address = "8 Warrior Trail, Austin, TX 78746",
                    PhoneNumber = "2198604221",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "pepperoni");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jeffrey Hampton
            if (await userManager.FindByEmailAsync("jeffh@sonic.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "jeffh@sonic.com",
                    Email = "jeffh@sonic.com",
                    FirstName = "Jeffrey",
                    LastName = "Hampton",
                    Address = "9107 Lighthouse Bay Road, Austin, TX 78756",
                    PhoneNumber = "1222185888",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "raiders");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: John Hearn
            if (await userManager.FindByEmailAsync("wjhearniii@umich.org") == null)
            {
                var user = new AppUser
                {
                    UserName = "wjhearniii@umich.org",
                    Email = "wjhearniii@umich.org",
                    FirstName = "John",
                    LastName = "Hearn",
                    Address = "59784 Pierstorff Center, Liberty, TX 77575",
                    PhoneNumber = "5123071976",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "jhearn22");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Anthony Hicks
            if (await userManager.FindByEmailAsync("ahick@yaho.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "ahick@yaho.com",
                    Email = "ahick@yaho.com",
                    FirstName = "Anthony",
                    LastName = "Hicks",
                    Address = "932 Monica Way, San Antonio, TX 78203",
                    PhoneNumber = "1211949601",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "hickhickup");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Brad Ingram
            if (await userManager.FindByEmailAsync("ingram@jack.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "ingram@jack.com",
                    Email = "ingram@jack.com",
                    FirstName = "Brad",
                    LastName = "Ingram",
                    Address = "4 Lukken Court, New Braunfels, TX 78132",
                    PhoneNumber = "1372121569",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "ingram2015");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Todd Jacobs
            if (await userManager.FindByEmailAsync("toddj@yourmom.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "toddj@yourmom.com",
                    Email = "toddj@yourmom.com",
                    FirstName = "Todd",
                    LastName = "Jacobs",
                    Address = "7 Susan Junction, New York, NY 10101",
                    PhoneNumber = "8543163836",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "toddy25");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Victoria Lawrence
            if (await userManager.FindByEmailAsync("thequeen@aska.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "thequeen@aska.net",
                    Email = "thequeen@aska.net",
                    FirstName = "Victoria",
                    LastName = "Lawrence",
                    Address = "669 Oak Junction, Lockhart, TX 78644",
                    PhoneNumber = "3214163359",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "something");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Erik Lineback
            if (await userManager.FindByEmailAsync("linebacker@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "linebacker@gogle.com",
                    Email = "linebacker@gogle.com",
                    FirstName = "Erik",
                    LastName = "Lineback",
                    Address = "099 Luster Point, Kingwood, TX 77325",
                    PhoneNumber = "2505265350",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "Password1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Ernest Lowe
            if (await userManager.FindByEmailAsync("elowe@netscare.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "elowe@netscare.net",
                    Email = "elowe@netscare.net",
                    FirstName = "Ernest",
                    LastName = "Lowe",
                    Address = "35473 Hansons Hill, Beverly Hills, CA 90210",
                    PhoneNumber = "4070619503",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "aclfest2017");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Chuck Luce
            if (await userManager.FindByEmailAsync("cluce@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "cluce@gogle.com",
                    Email = "cluce@gogle.com",
                    FirstName = "Chuck",
                    LastName = "Luce",
                    Address = "4 Emmet Junction, Navasota, TX 77868",
                    PhoneNumber = "7358436110",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "nothinggood");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jennifer MacLeod
            if (await userManager.FindByEmailAsync("mackcloud@george.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "mackcloud@george.com",
                    Email = "mackcloud@george.com",
                    FirstName = "Jennifer",
                    LastName = "MacLeod",
                    Address = "3 Orin Road, Austin, TX 78712",
                    PhoneNumber = "7240178229",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "whatever");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Elizabeth Markham
            if (await userManager.FindByEmailAsync("cmartin@beets.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "cmartin@beets.com",
                    Email = "cmartin@beets.com",
                    FirstName = "Elizabeth",
                    LastName = "Markham",
                    Address = "8171 Commercial Crossing, Austin, TX 78712",
                    PhoneNumber = "2495200223",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "snowsnow");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Clarence Martin
            if (await userManager.FindByEmailAsync("clarence@yoho.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "clarence@yoho.com",
                    Email = "clarence@yoho.com",
                    FirstName = "Clarence",
                    LastName = "Martin",
                    Address = "96 Anthes Place, Schenectady, NY 12345",
                    PhoneNumber = "4086179161",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "whocares");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Gregory Martinez
            if (await userManager.FindByEmailAsync("gregmartinez@drdre.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "gregmartinez@drdre.com",
                    Email = "gregmartinez@drdre.com",
                    FirstName = "Gregory",
                    LastName = "Martinez",
                    Address = "10 Northridge Plaza, Austin, TX 78717",
                    PhoneNumber = "9371927523",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "xcellent");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Charles Miller
            if (await userManager.FindByEmailAsync("cmiller@bob.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "cmiller@bob.com",
                    Email = "cmiller@bob.com",
                    FirstName = "Charles",
                    LastName = "Miller",
                    Address = "87683 Schmedeman Circle, Austin, TX 78727",
                    PhoneNumber = "5954063857",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "mydogspot");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Kelly Nelson
            if (await userManager.FindByEmailAsync("knelson@aoll.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "knelson@aoll.com",
                    Email = "knelson@aoll.com",
                    FirstName = "Kelly",
                    LastName = "Nelson",
                    Address = "3244 Ludington Court, Beaumont, TX 77720",
                    PhoneNumber = "8929209512",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "spotmydog");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Joe Nguyen
            if (await userManager.FindByEmailAsync("joewin@xfactor.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "joewin@xfactor.com",
                    Email = "joewin@xfactor.com",
                    FirstName = "Joe",
                    LastName = "Nguyen",
                    Address = "4780 Talisman Court, San Marcos, TX 78667",
                    PhoneNumber = "9226301774",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "joejoejoe");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Bill O'Reilly
            if (await userManager.FindByEmailAsync("orielly@foxnews.cnn") == null)
            {
                var user = new AppUser
                {
                    UserName = "orielly@foxnews.cnn",
                    Email = "orielly@foxnews.cnn",
                    FirstName = "Bill",
                    LastName = "O'Reilly",
                    Address = "4154 Delladonna Plaza, Bergheim, TX 78004",
                    PhoneNumber = "2537646912",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "billyboy");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Anka Radkovich
            if (await userManager.FindByEmailAsync("ankaisrad@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "ankaisrad@gogle.com",
                    Email = "ankaisrad@gogle.com",
                    FirstName = "Anka",
                    LastName = "Radkovich",
                    Address = "72361 Bayside Drive, Austin, TX 78789",
                    PhoneNumber = "2182889379",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "radgirl");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Megan Rhodes
            if (await userManager.FindByEmailAsync("megrhodes@freserve.co.uk") == null)
            {
                var user = new AppUser
                {
                    UserName = "megrhodes@freserve.co.uk",
                    Email = "megrhodes@freserve.co.uk",
                    FirstName = "Megan",
                    LastName = "Rhodes",
                    Address = "76875 Hoffman Point, Orlando, FL 32830",
                    PhoneNumber = "9532396075",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "meganr34");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Eryn Rice
            if (await userManager.FindByEmailAsync("erynrice@aoll.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "erynrice@aoll.com",
                    Email = "erynrice@aoll.com",
                    FirstName = "Eryn",
                    LastName = "Rice",
                    Address = "048 Elmside Park, South Padre Island, TX 78597",
                    PhoneNumber = "7303815953",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "ricearoni");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jorge Rodriguez
            if (await userManager.FindByEmailAsync("jorge@noclue.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "jorge@noclue.com",
                    Email = "jorge@noclue.com",
                    FirstName = "Jorge",
                    LastName = "Rodriguez",
                    Address = "01 Browning Pass, Austin, TX 78744",
                    PhoneNumber = "3677322422",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "alaskaboy");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Allen Rogers
            if (await userManager.FindByEmailAsync("mrrogers@lovelyday.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "mrrogers@lovelyday.com",
                    Email = "mrrogers@lovelyday.com",
                    FirstName = "Allen",
                    LastName = "Rogers",
                    Address = "844 Anderson Alley, Canyon Lake, TX 78133",
                    PhoneNumber = "3911705385",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "bunnyhop");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Olivier Saint-Jean
            if (await userManager.FindByEmailAsync("stjean@athome.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "stjean@athome.com",
                    Email = "stjean@athome.com",
                    FirstName = "Olivier",
                    LastName = "Saint-Jean",
                    Address = "1891 Docker Point, Austin, TX 78779",
                    PhoneNumber = "7351610920",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "dustydusty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Sarah Saunders
            if (await userManager.FindByEmailAsync("saunders@pen.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "saunders@pen.com",
                    Email = "saunders@pen.com",
                    FirstName = "Sarah",
                    LastName = "Saunders",
                    Address = "1469 Upham Road, Austin, TX 78720",
                    PhoneNumber = "5269661692",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "jrod2017");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: William Sewell
            if (await userManager.FindByEmailAsync("willsheff@email.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "willsheff@email.com",
                    Email = "willsheff@email.com",
                    FirstName = "William",
                    LastName = "Sewell",
                    Address = "1672 Oak Valley Circle, Austin, TX 78705",
                    PhoneNumber = "1875727246",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "martin1234");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Martin Sheffield
            if (await userManager.FindByEmailAsync("sheffiled@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "sheffiled@gogle.com",
                    Email = "sheffiled@gogle.com",
                    FirstName = "Martin",
                    LastName = "Sheffield",
                    Address = "816 Kennedy Place, Round Rock, TX 78680",
                    PhoneNumber = "1394323615",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "penguin12");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: John Smith
            if (await userManager.FindByEmailAsync("johnsmith187@aoll.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "johnsmith187@aoll.com",
                    Email = "johnsmith187@aoll.com",
                    FirstName = "John",
                    LastName = "Smith",
                    Address = "0745 Golf Road, Austin, TX 78760",
                    PhoneNumber = "6645937874",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "rogerthat");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Dustin Stroud
            if (await userManager.FindByEmailAsync("dustroud@mail.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "dustroud@mail.com",
                    Email = "dustroud@mail.com",
                    FirstName = "Dustin",
                    LastName = "Stroud",
                    Address = "505 Dexter Plaza, Sweet Home, TX 77987",
                    PhoneNumber = "6470254680",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "smitty444");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Eric Stuart
            if (await userManager.FindByEmailAsync("estuart@anchor.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "estuart@anchor.net",
                    Email = "estuart@anchor.net",
                    FirstName = "Eric",
                    LastName = "Stuart",
                    Address = "585 Claremont Drive, Corpus Christi, TX 78412",
                    PhoneNumber = "7701621022",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "stewball");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Peter Stump
            if (await userManager.FindByEmailAsync("peterstump@noclue.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "peterstump@noclue.com",
                    Email = "peterstump@noclue.com",
                    FirstName = "Peter",
                    LastName = "Stump",
                    Address = "89035 Welch Circle, Pflugerville, TX 78660",
                    PhoneNumber = "2181960061",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "slowwind");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jeremy Tanner
            if (await userManager.FindByEmailAsync("jtanner@mustang.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "jtanner@mustang.net",
                    Email = "jtanner@mustang.net",
                    FirstName = "Jeremy",
                    LastName = "Tanner",
                    Address = "4 Stang Trail, Austin, TX 78702",
                    PhoneNumber = "9908469499",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "tanner5454");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Allison Taylor
            if (await userManager.FindByEmailAsync("taylordjay@aoll.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "taylordjay@aoll.com",
                    Email = "taylordjay@aoll.com",
                    FirstName = "Allison",
                    LastName = "Taylor",
                    Address = "726 Twin Pines Avenue, Austin, TX 78713",
                    PhoneNumber = "7011918647",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "allyrally");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Rachel Taylor
            if (await userManager.FindByEmailAsync("rtaylor@gogle.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "rtaylor@gogle.com",
                    Email = "rtaylor@gogle.com",
                    FirstName = "Rachel",
                    LastName = "Taylor",
                    Address = "06605 Sugar Drive, Austin, TX 78712",
                    PhoneNumber = "8937910053",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "taylorbaylor");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Frank Tee
            if (await userManager.FindByEmailAsync("teefrank@noclue.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "teefrank@noclue.com",
                    Email = "teefrank@noclue.com",
                    FirstName = "Frank",
                    LastName = "Tee",
                    Address = "3567 Dawn Plaza, Austin, TX 78786",
                    PhoneNumber = "6394568913",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "teeoff22");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Clent Tucker
            if (await userManager.FindByEmailAsync("ctucker@alphabet.co.uk") == null)
            {
                var user = new AppUser
                {
                    UserName = "ctucker@alphabet.co.uk",
                    Email = "ctucker@alphabet.co.uk",
                    FirstName = "Clent",
                    LastName = "Tucker",
                    Address = "704 Northland Alley, San Antonio, TX 78279",
                    PhoneNumber = "2676838676",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "tucksack1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Allen Velasco
            if (await userManager.FindByEmailAsync("avelasco@yoho.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "avelasco@yoho.com",
                    Email = "avelasco@yoho.com",
                    FirstName = "Allen",
                    LastName = "Velasco",
                    Address = "72 Harbort Point, Navasota, TX 77868",
                    PhoneNumber = "3452909754",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "meow88");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Janet Vino
            if (await userManager.FindByEmailAsync("vinovino@grapes.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "vinovino@grapes.com",
                    Email = "vinovino@grapes.com",
                    FirstName = "Janet",
                    LastName = "Vino",
                    Address = "1 Oak Valley Place, Boston, MA 02114",
                    PhoneNumber = "8567089194",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "vinovino");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Jake West
            if (await userManager.FindByEmailAsync("westj@pioneer.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "westj@pioneer.net",
                    Email = "westj@pioneer.net",
                    FirstName = "Jake",
                    LastName = "West",
                    Address = "48743 Banding Parkway, Marble Falls, TX 78654",
                    PhoneNumber = "6260784394",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "gowest");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Louis Winthorpe
            if (await userManager.FindByEmailAsync("winner@hootmail.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "winner@hootmail.com",
                    Email = "winner@hootmail.com",
                    FirstName = "Louis",
                    LastName = "Winthorpe",
                    Address = "96850 Summit Crossing, Austin, TX 78730",
                    PhoneNumber = "3733971174",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "louielouie");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // CUSTOMER: Reagan Wood
            if (await userManager.FindByEmailAsync("rwood@voyager.net") == null)
            {
                var user = new AppUser
                {
                    UserName = "rwood@voyager.net",
                    Email = "rwood@voyager.net",
                    FirstName = "Reagan",
                    LastName = "Wood",
                    Address = "18354 Bluejay Street, Austin, TX 78712",
                    PhoneNumber = "8433359800",
                    Status = AppUser.UserStatus.Customer
                };

                var result = await userManager.CreateAsync(user, "woodyman1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            // EMPLOYEE: Christopher Baker (ADMIN)
            if (await userManager.FindByEmailAsync("c.baker@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "c.baker@bevosbooks.com",
                    Email = "c.baker@bevosbooks.com",
                    FirstName = "Christopher",
                    LastName = "Baker",
                    Address = "1245 Lake Libris Dr., Cedar Park, TX 78613",
                    PhoneNumber = "3395325649",
                    Status = AppUser.UserStatus.Admin
                };

                var result = await userManager.CreateAsync(user, "dewey4");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // EMPLOYEE: Susan Barnes (EMPLOYEE)
            if (await userManager.FindByEmailAsync("s.barnes@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "s.barnes@bevosbooks.com",
                    Email = "s.barnes@bevosbooks.com",
                    FirstName = "Susan",
                    LastName = "Barnes",
                    Address = "888 S. Main, Kyle, TX 78640",
                    PhoneNumber = "9636389416",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "smitty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Hector Garcia (EMPLOYEE)
            if (await userManager.FindByEmailAsync("h.garcia@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "h.garcia@bevosbooks.com",
                    Email = "h.garcia@bevosbooks.com",
                    FirstName = "Hector",
                    LastName = "Garcia",
                    Address = "777 PBR Drive, Austin, TX 78712",
                    PhoneNumber = "4547135738",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "squirrel");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Brad Ingram (EMPLOYEE)
            if (await userManager.FindByEmailAsync("b.ingram@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "b.ingram@bevosbooks.com",
                    Email = "b.ingram@bevosbooks.com",
                    FirstName = "Brad",
                    LastName = "Ingram",
                    Address = "6548 La Posada Ct., Austin, TX 78705",
                    PhoneNumber = "5817343315",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "changalang");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Jack Jackson (EMPLOYEE)
            if (await userManager.FindByEmailAsync("j.jackson@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "j.jackson@bevosbooks.com",
                    Email = "j.jackson@bevosbooks.com",
                    FirstName = "Jack",
                    LastName = "Jackson",
                    Address = "222 Main, Austin, TX 78760",
                    PhoneNumber = "8241915317",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "rhythm");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Todd Jacobs (EMPLOYEE)
            if (await userManager.FindByEmailAsync("t.jacobs@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "t.jacobs@bevosbooks.com",
                    Email = "t.jacobs@bevosbooks.com",
                    FirstName = "Todd",
                    LastName = "Jacobs",
                    Address = "4564 Elm St., Georgetown, TX 78628",
                    PhoneNumber = "2477822475",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "approval");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Lester Jones (EMPLOYEE)
            if (await userManager.FindByEmailAsync("l.jones@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "l.jones@bevosbooks.com",
                    Email = "l.jones@bevosbooks.com",
                    FirstName = "Lester",
                    LastName = "Jones",
                    Address = "999 LeBlat, Austin, TX 78747",
                    PhoneNumber = "4764966462",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "society");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Bill Larson (EMPLOYEE)
            if (await userManager.FindByEmailAsync("b.larson@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "b.larson@bevosbooks.com",
                    Email = "b.larson@bevosbooks.com",
                    FirstName = "Bill",
                    LastName = "Larson",
                    Address = "1212 N. First Ave, Round Rock, TX 78665",
                    PhoneNumber = "3355258855",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "tanman");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Victoria Lawrence (EMPLOYEE)
            if (await userManager.FindByEmailAsync("v.lawrence@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "v.lawrence@bevosbooks.com",
                    Email = "v.lawrence@bevosbooks.com",
                    FirstName = "Victoria",
                    LastName = "Lawrence",
                    Address = "6639 Bookworm Ln., Austin, TX 78712",
                    PhoneNumber = "7511273054",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "longhorns");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Marshall Lopez (EMPLOYEE)
            if (await userManager.FindByEmailAsync("m.lopez@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "m.lopez@bevosbooks.com",
                    Email = "m.lopez@bevosbooks.com",
                    FirstName = "Marshall",
                    LastName = "Lopez",
                    Address = "90 SW North St, Austin, TX 78729",
                    PhoneNumber = "7477907070",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "swansong");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Jennifer MacLeod (EMPLOYEE)
            if (await userManager.FindByEmailAsync("j.macleod@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "j.macleod@bevosbooks.com",
                    Email = "j.macleod@bevosbooks.com",
                    FirstName = "Jennifer",
                    LastName = "MacLeod",
                    Address = "2504 Far West Blvd., Austin, TX 78705",
                    PhoneNumber = "2621216845",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "fungus");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Elizabeth Markham (EMPLOYEE)
            if (await userManager.FindByEmailAsync("e.markham@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "e.markham@bevosbooks.com",
                    Email = "e.markham@bevosbooks.com",
                    FirstName = "Elizabeth",
                    LastName = "Markham",
                    Address = "7861 Chevy Chase, Austin, TX 78785",
                    PhoneNumber = "5028075807",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "median");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Gregory Martinez (EMPLOYEE)
            if (await userManager.FindByEmailAsync("g.martinez@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "g.martinez@bevosbooks.com",
                    Email = "g.martinez@bevosbooks.com",
                    FirstName = "Gregory",
                    LastName = "Martinez",
                    Address = "8295 Sunset Blvd., Austin, TX 78712",
                    PhoneNumber = "1994708542",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "decorate");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Jack Mason (EMPLOYEE)
            if (await userManager.FindByEmailAsync("j.mason@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "j.mason@bevosbooks.com",
                    Email = "j.mason@bevosbooks.com",
                    FirstName = "Jack",
                    LastName = "Mason",
                    Address = "444 45th St, Austin, TX 78701",
                    PhoneNumber = "1748136441",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "rankmary");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Charles Miller (EMPLOYEE)
            if (await userManager.FindByEmailAsync("c.miller@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "c.miller@bevosbooks.com",
                    Email = "c.miller@bevosbooks.com",
                    FirstName = "Charles",
                    LastName = "Miller",
                    Address = "8962 Main St., Austin, TX 78709",
                    PhoneNumber = "8999319585",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "kindly");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Mary Nguyen (EMPLOYEE)
            if (await userManager.FindByEmailAsync("m.nguyen@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "m.nguyen@bevosbooks.com",
                    Email = "m.nguyen@bevosbooks.com",
                    FirstName = "Mary",
                    LastName = "Nguyen",
                    Address = "465 N. Bear Cub, Austin, TX 78734",
                    PhoneNumber = "8716746381",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "ricearoni");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Suzie Rankin (EMPLOYEE)
            if (await userManager.FindByEmailAsync("s.rankin@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "s.rankin@bevosbooks.com",
                    Email = "s.rankin@bevosbooks.com",
                    FirstName = "Suzie",
                    LastName = "Rankin",
                    Address = "23 Dewey Road, Austin, TX 78712",
                    PhoneNumber = "5239029525",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "walkamile");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Megan Rhodes (EMPLOYEE)
            if (await userManager.FindByEmailAsync("m.rhodes@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "m.rhodes@bevosbooks.com",
                    Email = "m.rhodes@bevosbooks.com",
                    FirstName = "Megan",
                    LastName = "Rhodes",
                    Address = "4587 Enfield Rd., Austin, TX 78729",
                    PhoneNumber = "1232139514",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "ingram45");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Eryn Rice (ADMIN)
            if (await userManager.FindByEmailAsync("e.rice@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "e.rice@bevosbooks.com",
                    Email = "e.rice@bevosbooks.com",
                    FirstName = "Eryn",
                    LastName = "Rice",
                    Address = "3405 Rio Grande, Austin, TX 78746",
                    PhoneNumber = "2706602803",
                    Status = AppUser.UserStatus.Admin
                };

                var result = await userManager.CreateAsync(user, "arched");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // EMPLOYEE: Allen Rogers (ADMIN)
            if (await userManager.FindByEmailAsync("a.rogers@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "a.rogers@bevosbooks.com",
                    Email = "a.rogers@bevosbooks.com",
                    FirstName = "Allen",
                    LastName = "Rogers",
                    Address = "4965 Oak Hill, Austin, TX 78705",
                    PhoneNumber = "4139645586",
                    Status = AppUser.UserStatus.Admin
                };

                var result = await userManager.CreateAsync(user, "lottery");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // EMPLOYEE: Sarah Saunders (EMPLOYEE)
            if (await userManager.FindByEmailAsync("s.saunders@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "s.saunders@bevosbooks.com",
                    Email = "s.saunders@bevosbooks.com",
                    FirstName = "Sarah",
                    LastName = "Saunders",
                    Address = "332 Avenue C, Austin, TX 78733",
                    PhoneNumber = "9036349587",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "nostalgic");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: William Sewell (ADMIN)
            if (await userManager.FindByEmailAsync("w.sewell@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "w.sewell@bevosbooks.com",
                    Email = "w.sewell@bevosbooks.com",
                    FirstName = "William",
                    LastName = "Sewell",
                    Address = "2365 51st St., Austin, TX 78755",
                    PhoneNumber = "7224308314",
                    Status = AppUser.UserStatus.Admin
                };

                var result = await userManager.CreateAsync(user, "offbeat");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // EMPLOYEE: Martin Sheffield (EMPLOYEE)
            if (await userManager.FindByEmailAsync("m.sheffield@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "m.sheffield@bevosbooks.com",
                    Email = "m.sheffield@bevosbooks.com",
                    FirstName = "Martin",
                    LastName = "Sheffield",
                    Address = "3886 Avenue A, San Marcos, TX 78666",
                    PhoneNumber = "9349192978",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "evanescent");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Cindy Silva (EMPLOYEE)
            if (await userManager.FindByEmailAsync("c.silva@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "c.silva@bevosbooks.com",
                    Email = "c.silva@bevosbooks.com",
                    FirstName = "Cindy",
                    LastName = "Silva",
                    Address = "900 4th St, Austin, TX 78758",
                    PhoneNumber = "4874328170",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "stewboy");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Eric Stuart (EMPLOYEE)
            if (await userManager.FindByEmailAsync("e.stuart@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "e.stuart@bevosbooks.com",
                    Email = "e.stuart@bevosbooks.com",
                    FirstName = "Eric",
                    LastName = "Stuart",
                    Address = "5576 Toro Ring, Austin, TX 78758",
                    PhoneNumber = "1967846827",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "instrument");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Jeremy Tanner (EMPLOYEE)
            if (await userManager.FindByEmailAsync("j.tanner@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "j.tanner@bevosbooks.com",
                    Email = "j.tanner@bevosbooks.com",
                    FirstName = "Jeremy",
                    LastName = "Tanner",
                    Address = "4347 Almstead, Austin, TX 78712",
                    PhoneNumber = "5923026779",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "hecktour");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Allison Taylor (EMPLOYEE)
            if (await userManager.FindByEmailAsync("a.taylor@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "a.taylor@bevosbooks.com",
                    Email = "a.taylor@bevosbooks.com",
                    FirstName = "Allison",
                    LastName = "Taylor",
                    Address = "467 Nueces St., Austin, TX 78727",
                    PhoneNumber = "7246195827",
                    Status = AppUser.UserStatus.Employee
                };

                var result = await userManager.CreateAsync(user, "countryrhodes");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            // EMPLOYEE: Rachel Taylor (ADMIN)
            if (await userManager.FindByEmailAsync("r.taylor@bevosbooks.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "r.taylor@bevosbooks.com",
                    Email = "r.taylor@bevosbooks.com",
                    FirstName = "Rachel",
                    LastName = "Taylor",
                    Address = "345 Longview Dr., Austin, TX 78746",
                    PhoneNumber = "9071236087",
                    Status = AppUser.UserStatus.Admin
                };

                var result = await userManager.CreateAsync(user, "landus");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }
    }
}
