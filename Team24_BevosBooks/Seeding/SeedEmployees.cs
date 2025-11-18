using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Team24_BevosBooks.Seeding
{
	public static class SeedEmployees
	{
		public static void SeedAllEmployees(AppDbContext db)
		{
			Int32 intEmployeesAdded = 0;
			String strEmpFlag = "Begin";

			List<Employee> Employees = new List<Employee>();

			Employee e1 = new Employee()
			{
				LastName = "Baker",
				FirstName = "Christopher",
				Password = "dewey4",
				SSN = "401661146",
				EmployeeType = "Admin",
				Address = "1245 Lake Libris Dr.",
				City = "Cedar Park",
				State = "TX",
				Zip = "78613",
				Phone = "3395325649",
				Email = "c.baker@bevosbooks.com"
			};
			strEmpFlag = "c.baker@bevosbooks.com";
			Employees.Add(e1);

			Employee e2 = new Employee()
			{
				LastName = "Barnes",
				FirstName = "Susan",
				Password = "smitty",
				SSN = "1112221212",
				EmployeeType = "Employee",
				Address = "888 S. Main",
				City = "Kyle",
				State = "TX",
				Zip = "78640",
				Phone = "9636389416",
				Email = "s.barnes@bevosbooks.com"
			};
			strEmpFlag = "s.barnes@bevosbooks.com";
			Employees.Add(e2);

			Employee e3 = new Employee()
			{
				LastName = "Garcia",
				FirstName = "Hector",
				Password = "squirrel",
				SSN = "4445554343",
				EmployeeType = "Employee",
				Address = "777 PBR Drive",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				Phone = "4547135738",
				Email = "h.garcia@bevosbooks.com"
			};
			strEmpFlag = "h.garcia@bevosbooks.com";
			Employees.Add(e3);

			Employee e4 = new Employee()
			{
				LastName = "Ingram",
				FirstName = "Brad",
				Password = "changalang",
				SSN = "797348821",
				EmployeeType = "Employee",
				Address = "6548 La Posada Ct.",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				Phone = "5817343315",
				Email = "b.ingram@bevosbooks.com"
			};
			strEmpFlag = "b.ingram@bevosbooks.com";
			Employees.Add(e4);

			Employee e5 = new Employee()
			{
				LastName = "Jackson",
				FirstName = "Jack",
				Password = "rhythm",
				SSN = "8889993434",
				EmployeeType = "Employee",
				Address = "222 Main",
				City = "Austin",
				State = "TX",
				Zip = "78760",
				Phone = "8241915317",
				Email = "j.jackson@bevosbooks.com"
			};
			strEmpFlag = "j.jackson@bevosbooks.com";
			Employees.Add(e5);

			Employee e6 = new Employee()
			{
				LastName = "Jacobs",
				FirstName = "Todd",
				Password = "approval",
				SSN = "341553365",
				EmployeeType = "Employee",
				Address = "4564 Elm St.",
				City = "Georgetown",
				State = "TX",
				Zip = "78628",
				Phone = "2477822475",
				Email = "t.jacobs@bevosbooks.com"
			};
			strEmpFlag = "t.jacobs@bevosbooks.com";
			Employees.Add(e6);

			Employee e7 = new Employee()
			{
				LastName = "Jones",
				FirstName = "Lester",
				Password = "society",
				SSN = "9099099999",
				EmployeeType = "Employee",
				Address = "999 LeBlat",
				City = "Austin",
				State = "TX",
				Zip = "78747",
				Phone = "4764966462",
				Email = "l.jones@bevosbooks.com"
			};
			strEmpFlag = "l.jones@bevosbooks.com";
			Employees.Add(e7);

			Employee e8 = new Employee()
			{
				LastName = "Larson",
				FirstName = "Bill",
				Password = "tanman",
				SSN = "5554443333",
				EmployeeType = "Employee",
				Address = "1212 N. First Ave",
				City = "Round Rock",
				State = "TX",
				Zip = "78665",
				Phone = "3355258855",
				Email = "b.larson@bevosbooks.com"
			};
			strEmpFlag = "b.larson@bevosbooks.com";
			Employees.Add(e8);

			Employee e9 = new Employee()
			{
				LastName = "Lawrence",
				FirstName = "Victoria",
				Password = "longhorns",
				SSN = "770097399",
				EmployeeType = "Employee",
				Address = "6639 Bookworm Ln.",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				Phone = "7511273054",
				Email = "v.lawrence@bevosbooks.com"
			};
			strEmpFlag = "v.lawrence@bevosbooks.com";
			Employees.Add(e9);

			Employee e10 = new Employee()
			{
				LastName = "Lopez",
				FirstName = "Marshall",
				Password = "swansong",
				SSN = "2223332222",
				EmployeeType = "Employee",
				Address = "90 SW North St",
				City = "Austin",
				State = "TX",
				Zip = "78729",
				Phone = "7477907070",
				Email = "m.lopez@bevosbooks.com"
			};
			strEmpFlag = "m.lopez@bevosbooks.com";
			Employees.Add(e10);

			Employee e11 = new Employee()
			{
				LastName = "MacLeod",
				FirstName = "Jennifer",
				Password = "fungus",
				SSN = "775908138",
				EmployeeType = "Employee",
				Address = "2504 Far West Blvd.",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				Phone = "2621216845",
				Email = "j.macleod@bevosbooks.com"
			};
			strEmpFlag = "j.macleod@bevosbooks.com";
			Employees.Add(e11);

			Employee e12 = new Employee()
			{
				LastName = "Markham",
				FirstName = "Elizabeth",
				Password = "median",
				SSN = "101529845",
				EmployeeType = "Employee",
				Address = "7861 Chevy Chase",
				City = "Austin",
				State = "TX",
				Zip = "78785",
				Phone = "5028075807",
				Email = "e.markham@bevosbooks.com"
			};
			strEmpFlag = "e.markham@bevosbooks.com";
			Employees.Add(e12);

			Employee e13 = new Employee()
			{
				LastName = "Martinez",
				FirstName = "Gregory",
				Password = "decorate",
				SSN = "463566718",
				EmployeeType = "Employee",
				Address = "8295 Sunset Blvd.",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				Phone = "1994708542",
				Email = "g.martinez@bevosbooks.com"
			};
			strEmpFlag = "g.martinez@bevosbooks.com";
			Employees.Add(e13);

			Employee e14 = new Employee()
			{
				LastName = "Mason",
				FirstName = "Jack",
				Password = "rankmary",
				SSN = "1112223232",
				EmployeeType = "Employee",
				Address = "444 45th St",
				City = "Austin",
				State = "TX",
				Zip = "78701",
				Phone = "1748136441",
				Email = "j.mason@bevosbooks.com"
			};
			strEmpFlag = "j.mason@bevosbooks.com";
			Employees.Add(e14);

			Employee e15 = new Employee()
			{
				LastName = "Miller",
				FirstName = "Charles",
				Password = "kindly",
				SSN = "353308615",
				EmployeeType = "Employee",
				Address = "8962 Main St.",
				City = "Austin",
				State = "TX",
				Zip = "78709",
				Phone = "8999319585",
				Email = "c.miller@bevosbooks.com"
			};
			strEmpFlag = "c.miller@bevosbooks.com";
			Employees.Add(e15);

			Employee e16 = new Employee()
			{
				LastName = "Nguyen",
				FirstName = "Mary",
				Password = "ricearoni",
				SSN = "7776665555",
				EmployeeType = "Employee",
				Address = "465 N. Bear Cub",
				City = "Austin",
				State = "TX",
				Zip = "78734",
				Phone = "8716746381",
				Email = "m.nguyen@bevosbooks.com"
			};
			strEmpFlag = "m.nguyen@bevosbooks.com";
			Employees.Add(e16);

			Employee e17 = new Employee()
			{
				LastName = "Rankin",
				FirstName = "Suzie",
				Password = "walkamile",
				SSN = "1911919111",
				EmployeeType = "Employee",
				Address = "23 Dewey Road",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				Phone = "5239029525",
				Email = "s.rankin@bevosbooks.com"
			};
			strEmpFlag = "s.rankin@bevosbooks.com";
			Employees.Add(e17);

			Employee e18 = new Employee()
			{
				LastName = "Rhodes",
				FirstName = "Megan",
				Password = "ingram45",
				SSN = "353904746",
				EmployeeType = "Employee",
				Address = "4587 Enfield Rd.",
				City = "Austin",
				State = "TX",
				Zip = "78729",
				Phone = "1232139514",
				Email = "m.rhodes@bevosbooks.com"
			};
			strEmpFlag = "m.rhodes@bevosbooks.com";
			Employees.Add(e18);

			Employee e19 = new Employee()
			{
				LastName = "Rice",
				FirstName = "Eryn",
				Password = "arched",
				SSN = "454776657",
				EmployeeType = "Admin",
				Address = "3405 Rio Grande",
				City = "Austin",
				State = "TX",
				Zip = "78746",
				Phone = "2706602803",
				Email = "e.rice@bevosbooks.com"
			};
			strEmpFlag = "e.rice@bevosbooks.com";
			Employees.Add(e19);

			Employee e20 = new Employee()
			{
				LastName = "Rogers",
				FirstName = "Allen",
				Password = "lottery",
				SSN = "700002943",
				EmployeeType = "Admin",
				Address = "4965 Oak Hill",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				Phone = "4139645586",
				Email = "a.rogers@bevosbooks.com"
			};
			strEmpFlag = "a.rogers@bevosbooks.com";
			Employees.Add(e20);

			Employee e21 = new Employee()
			{
				LastName = "Saunders",
				FirstName = "Sarah",
				Password = "nostalgic",
				SSN = "500987810",
				EmployeeType = "Employee",
				Address = "332 Avenue C",
				City = "Austin",
				State = "TX",
				Zip = "78733",
				Phone = "9036349587",
				Email = "s.saunders@bevosbooks.com"
			};
			strEmpFlag = "s.saunders@bevosbooks.com";
			Employees.Add(e21);

			Employee e22 = new Employee()
			{
				LastName = "Sewell",
				FirstName = "William",
				Password = "offbeat",
				SSN = "500830084",
				EmployeeType = "Admin",
				Address = "2365 51st St.",
				City = "Austin",
				State = "TX",
				Zip = "78755",
				Phone = "7224308314",
				Email = "w.sewell@bevosbooks.com"
			};
			strEmpFlag = "w.sewell@bevosbooks.com";
			Employees.Add(e22);

			Employee e23 = new Employee()
			{
				LastName = "Sheffield",
				FirstName = "Martin",
				Password = "evanescent",
				SSN = "223449167",
				EmployeeType = "Employee",
				Address = "3886 Avenue A",
				City = "San Marcos",
				State = "TX",
				Zip = "78666",
				Phone = "9349192978",
				Email = "m.sheffield@bevosbooks.com"
			};
			strEmpFlag = "m.sheffield@bevosbooks.com";
			Employees.Add(e23);

			Employee e24 = new Employee()
			{
				LastName = "Silva",
				FirstName = "Cindy",
				Password = "stewboy",
				SSN = "7776661111",
				EmployeeType = "Employee",
				Address = "900 4th St",
				City = "Austin",
				State = "TX",
				Zip = "78758",
				Phone = "4874328170",
				Email = "c.silva@bevosbooks.com"
			};
			strEmpFlag = "c.silva@bevosbooks.com";
			Employees.Add(e24);

			Employee e25 = new Employee()
			{
				LastName = "Stuart",
				FirstName = "Eric",
				Password = "instrument",
				SSN = "363998335",
				EmployeeType = "Employee",
				Address = "5576 Toro Ring",
				City = "Austin",
				State = "TX",
				Zip = "78758",
				Phone = "1967846827",
				Email = "e.stuart@bevosbooks.com"
			};
			strEmpFlag = "e.stuart@bevosbooks.com";
			Employees.Add(e25);

			Employee e26 = new Employee()
			{
				LastName = "Tanner",
				FirstName = "Jeremy",
				Password = "hecktour",
				SSN = "904440929",
				EmployeeType = "Employee",
				Address = "4347 Almstead",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				Phone = "5923026779",
				Email = "j.tanner@bevosbooks.com"
			};
			strEmpFlag = "j.tanner@bevosbooks.com";
			Employees.Add(e26);

			Employee e27 = new Employee()
			{
				LastName = "Taylor",
				FirstName = "Allison",
				Password = "countryrhodes",
				SSN = "934778452",
				EmployeeType = "Employee",
				Address = "467 Nueces St.",
				City = "Austin",
				State = "TX",
				Zip = "78727",
				Phone = "7246195827",
				Email = "a.taylor@bevosbooks.com"
			};
			strEmpFlag = "a.taylor@bevosbooks.com";
			Employees.Add(e27);

			Employee e28 = new Employee()
			{
				LastName = "Taylor",
				FirstName = "Rachel",
				Password = "landus",
				SSN = "393412631",
				EmployeeType = "Admin",
				Address = "345 Longview Dr.",
				City = "Austin",
				State = "TX",
				Zip = "78746",
				Phone = "9071236087",
				Email = "r.taylor@bevosbooks.com"
			};
			strEmpFlag = "r.taylor@bevosbooks.com";
			Employees.Add(e28);

			try
			{
				foreach (Employee e in Employees)
				{
					Employee dbEmp = db.Employees.FirstOrDefault(x => x.Email == e.Email);
					if (dbEmp == null)
					{
						db.Employees.Add(e);
						db.SaveChanges();
						intEmployeesAdded += 1;
					}
					else
					{
						dbEmp.LastName = e.LastName;
						dbEmp.FirstName = e.FirstName;
						dbEmp.Password = e.Password;
						dbEmp.SSN = e.SSN;
						dbEmp.EmployeeType = e.EmployeeType;
						dbEmp.Address = e.Address;
						dbEmp.City = e.City;
						dbEmp.State = e.State;
						dbEmp.Zip = e.Zip;
						dbEmp.Phone = e.Phone;
						dbEmp.Email = e.Email;
						db.Update(dbEmp);
						db.SaveChanges();
					}
				}
			}
			catch (Exception ex)
			{
				String msg = " Employees added: " + intEmployeesAdded + "; Error on " + strEmpFlag;
				throw new InvalidOperationException(ex.Message + msg);
			}
		}
	}
}
