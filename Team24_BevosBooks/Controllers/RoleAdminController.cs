using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;

namespace Team24_BevosBooks.Controllers
{
    // Allow authenticated users; restrict actions individually
    [Authorize]
    public class RoleAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public RoleAdminController(UserManager<AppUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }



        // ============================================================
        // STATES
        // ============================================================
        private List<string> GetStates() => new()
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // ============================================================
        // MANAGE EMPLOYEES / ADMINS (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageEmployees()
        {
            var users = await _userManager.Users
                .Where(u => u.Status == AppUser.UserStatus.Employee
                         || u.Status == AppUser.UserStatus.Admin
                         || u.Status == AppUser.UserStatus.Fired)   // include fired employees
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();

            foreach (var user in users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
            }

            return View(users);
        }

        // ============================================================
        // CREATE EMPLOYEE (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public IActionResult CreateEmployee()
        {
            ViewBag.States = new SelectList(GetStates());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate states before returning the view
                ViewBag.States = new SelectList(GetStates());
                return View(rvm);
            }

            AppUser employee = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                PhoneNumber = rvm.PhoneNumber,
                Address = rvm.Address,   // ✅ use values from the form
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                Status = AppUser.UserStatus.Employee
            };

            IdentityResult ir = await _userManager.CreateAsync(employee, rvm.Password);

            if (!ir.Succeeded)
            {
                foreach (var error in ir.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewBag.States = new SelectList(GetStates(), rvm.State);
                return View(rvm);
            }

            await _userManager.AddToRoleAsync(employee, "Employee");

            return RedirectToAction("ManageEmployees");
        }


        // ============================================================
        // EDIT EMPLOYEE (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployee(string id)
        {

            AppUser employee = await _userManager.FindByIdAsync(id);
            if (employee == null) return NotFound();
            ViewBag.States = new SelectList(GetStates());
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(AppUser edited)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.States = new SelectList(GetStates());
                return View(edited);
            }

            var employee = await _userManager.FindByIdAsync(edited.Id);
            if (employee == null) return NotFound();

            // Copy editable fields
            employee.FirstName = edited.FirstName;
            employee.LastName = edited.LastName;
            employee.PhoneNumber = edited.PhoneNumber;
            employee.Address = edited.Address;
            employee.City = edited.City;
            employee.State = edited.State;
            employee.ZipCode = edited.ZipCode;

            // Force status to remain Employee
            employee.Status = AppUser.UserStatus.Employee;

            var result = await _userManager.UpdateAsync(employee);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(edited);
            }

            TempData["Message"] = $"Employee {employee.FirstName} {employee.LastName} updated successfully.";
            return RedirectToAction("ManageEmployees");
        }



        // ============================================================
        // MANAGE CUSTOMERS (Admin + Employee)
        // ============================================================
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> ManageCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            var sorted = customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
            return View(sorted);
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Disable(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.Status = AppUser.UserStatus.Disabled;
            user.LockoutEnabled = true;

            // Use DateTimeOffset.MaxValue for indefinite lockout
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageCustomers");
        }

        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Enable(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.Status = AppUser.UserStatus.Customer;

            // Clear lockout
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageCustomers");
        }

        // ============================================================
        // PROMOTE TO ADMIN (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Promote(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (await _userManager.IsInRoleAsync(user, "Employee"))
                await _userManager.RemoveFromRoleAsync(user, "Employee");

            await _userManager.AddToRoleAsync(user, "Admin");

            user.Status = AppUser.UserStatus.Admin;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageEmployees");
        }

        // ============================================================
        // DEMOTE FROM ADMIN (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Demote(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            if (admins.Count == 1 && admins.First().Id == id)
            {
                TempData["Error"] = "You cannot demote the only remaining administrator.";
                return RedirectToAction("ManageEmployees");
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            user.Status = AppUser.UserStatus.Employee;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageEmployees");
        }

        // ============================================================
        // FIRE EMPLOYEE (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Fire(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Mark status as Fired but keep them in the employees list
            user.Status = AppUser.UserStatus.Fired;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageEmployees");
        }

        // ============================================================
        // REHIRE EMPLOYEE (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Rehire(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Restore status to Employee
            user.Status = AppUser.UserStatus.Employee;
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageEmployees");
        }


        // ============================================================
        // Create Cusomter (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult CreateCustomer()
        {
            // Populate dropdowns if needed (e.g., states)
            ViewBag.States = new SelectList(GetStates());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns if needed
                ViewBag.States = new SelectList(GetStates(), rvm.State);
                return View(rvm);
            }

            // ⭐ Determine the next available customer number ⭐
            int nextCustomerNumber;
            if (_context.Users.Any(u => u.CustomerNumber.HasValue))
            {
                nextCustomerNumber = _context.Users
                    .Where(u => u.CustomerNumber.HasValue)
                    .Max(u => u.CustomerNumber.Value) + 1;
            }
            else
            {
                // First customer after reseed should start at 9010
                nextCustomerNumber = 9010;
            }

            AppUser customer = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                PhoneNumber = rvm.PhoneNumber,
                Address = rvm.Address,
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                Status = AppUser.UserStatus.Customer,

                // ⭐ Assign the next sequential customer number ⭐
                CustomerNumber = nextCustomerNumber
            };

            IdentityResult ir = await _userManager.CreateAsync(customer, rvm.Password);

            if (!ir.Succeeded)
            {
                foreach (var error in ir.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewBag.States = new SelectList(GetStates(), rvm.State);
                return View(rvm);
            }

            await _userManager.AddToRoleAsync(customer, "Customer");

            TempData["Message"] = $"Customer #{customer.CustomerNumber}: {customer.FirstName} {customer.LastName} created successfully.";

            return RedirectToAction("ManageCustomers");
        }



        // ============================================================
        // EDIT CUSTOMER (Admin + Employee)
        // ============================================================
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> EditCustomer(string id)
        {
            AppUser customer = await _userManager.FindByIdAsync(id);
            if (customer == null) return NotFound();
            ViewBag.States = new SelectList(GetStates(), customer.State);
            return View(customer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(AppUser edited)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.States = new SelectList(GetStates(), edited.State);
                return View(edited);
            }

            var customer = await _userManager.FindByIdAsync(edited.Id);
            if (customer == null) return NotFound();

            // Copy editable fields
            customer.FirstName = edited.FirstName;
            customer.LastName = edited.LastName;
            customer.PhoneNumber = edited.PhoneNumber;
            customer.Address = edited.Address;
            customer.City = edited.City;
            customer.State = edited.State;
            customer.ZipCode = edited.ZipCode;

            // Force status to remain Customer
            customer.Status = AppUser.UserStatus.Customer;

            var result = await _userManager.UpdateAsync(customer);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(edited);
            }

            TempData["Message"] = $"Customer {customer.FirstName} {customer.LastName} updated successfully.";
            return RedirectToAction("ManageCustomers");
        }


    }
}
