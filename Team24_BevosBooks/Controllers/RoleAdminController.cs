using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Models.ViewModels;

namespace Team24_BevosBooks.Controllers
{
    [Authorize(Roles = "Admin")]
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

        // -------------------------
        // MANAGE EMPLOYEES
        // -------------------------
        public async Task<IActionResult> ManageEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            var allEmployees = employees.Concat(admins).Distinct().OrderBy(e => e.LastName);

            return View(allEmployees);
        }

        // -------------------------
        // CREATE EMPLOYEE
        // -------------------------
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
                return View(rvm);

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

        // -------------------------
        // EDIT EMPLOYEE
        // -------------------------
        public async Task<IActionResult> EditEmployee(string id)
        {
            AppUser employee = await _userManager.FindByIdAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(AppUser edited)
        {
            AppUser employee = await _userManager.FindByIdAsync(edited.Id);

            employee.FirstName = edited.FirstName;
            employee.LastName = edited.LastName;
            employee.PhoneNumber = edited.PhoneNumber;

            await _userManager.UpdateAsync(employee);

            return RedirectToAction("ManageEmployees");
        }

        // -------------------------
        // DISABLE / ENABLE CUSTOMER ACCOUNTS
        // -------------------------
        public async Task<IActionResult> ManageCustomers()
        {
            var customers = await _userManager.GetUsersInRoleAsync("Customer");
            var sorted = customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
            return View(sorted);
        }

        public async Task<IActionResult> Disable(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            // lock user out forever
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageCustomers");
        }

        public async Task<IActionResult> Enable(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            // unlock the user
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("ManageCustomers");
        }


        // -------------------------
        // PROMOTE TO ADMIN
        // -------------------------
        public async Task<IActionResult> Promote(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            // remove Employee role if exists
            if (await _userManager.IsInRoleAsync(user, "Employee"))
                await _userManager.RemoveFromRoleAsync(user, "Employee");

            await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("ManageEmployees");
        }
    }
}
