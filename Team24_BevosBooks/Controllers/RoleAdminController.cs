using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        // MANAGE EMPLOYEES / ADMINS (Admin only)
        // ============================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageEmployees()
        {
            var users = await _userManager.Users
                .Where(u => u.Status == AppUser.UserStatus.Employee
                         || u.Status == AppUser.UserStatus.Admin)
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid) return View(rvm);

            AppUser employee = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                PhoneNumber = rvm.PhoneNumber,
                Address = "",
                City = "",
                State = "",
                ZipCode = "",
                Status = AppUser.UserStatus.Employee
            };

            IdentityResult ir = await _userManager.CreateAsync(employee, rvm.Password);

            if (!ir.Succeeded)
            {
                foreach (var error in ir.Errors)
                    ModelState.AddModelError("", error.Description);
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

            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployee(AppUser edited)
        {
            var employee = await _userManager.FindByIdAsync(edited.Id);
            if (employee == null) return NotFound();

            employee.FirstName = edited.FirstName;
            employee.LastName = edited.LastName;
            employee.PhoneNumber = edited.PhoneNumber;

            await _userManager.UpdateAsync(employee);

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
    }
}
