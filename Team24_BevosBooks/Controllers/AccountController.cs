using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Utilities;
using Team24_BevosBooks.Models.ViewModels;


namespace Team24_BevosBooks.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ------------------------------
        // REGISTER (CUSTOMER)
        // ------------------------------
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {
            if (!ModelState.IsValid)
            {
                return View(rvm);
            }

            AppUser newUser = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                Address = rvm.Address,
                City = rvm.City,
                State = rvm.State,
                ZipCode = rvm.ZipCode,
                PhoneNumber = rvm.PhoneNumber,
                Status = AppUser.UserStatus.Customer

            };

            IdentityResult result = await _userManager.CreateAsync(newUser, rvm.Password);

            if (result.Succeeded == false)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(rvm);
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");

            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        // ------------------------------
        // LOGIN
        // ------------------------------
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (!ModelState.IsValid)
                return View(lvm);

            var result = await _signInManager.PasswordSignInAsync(
                lvm.Email, lvm.Password, lvm.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(lvm);
            }

            AppUser user = await _userManager.Users.FirstAsync(u => u.Email == lvm.Email);

            if (user.Status == AppUser.UserStatus.Disabled)
            {
                ModelState.AddModelError("", "Your account has been disabled.");
                return View(lvm);
            }


            return RedirectToAction("Index", "Home");
        }

        // ------------------------------
        // LOGOUT
        // ------------------------------
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // ------------------------------
        // MANAGE PROFILE
        // ------------------------------
        public async Task<IActionResult> Manage()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(AppUser updatedUser)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Address = updatedUser.Address;
            user.City = updatedUser.City;
            user.State = updatedUser.State;
            user.ZipCode = updatedUser.ZipCode;
            user.PhoneNumber = updatedUser.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Manage");
        }

        // ------------------------------
        // CHANGE PASSWORD
        // ------------------------------
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel cpvm)
        {
            if (!ModelState.IsValid)
                return View(cpvm);

            AppUser user = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(
                user, cpvm.OldPassword, cpvm.NewPassword);

            if (!result.Succeeded)
            {
                foreach (IdentityError e in result.Errors)
                    ModelState.AddModelError("", e.Description);

                return View(cpvm);
            }

            return RedirectToAction("Manage");
        }

        // ------------------------------
        // ADD CREDIT CARD
        // ------------------------------
        public IActionResult AddCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardViewModel cvm)
        {
            if (!ModelState.IsValid)
                return View(cvm);

            // Get currently logged-in user
            AppUser user = await _userManager.GetUserAsync(User);

            // Count how many cards this user already has
            int cardCount = _context.Cards
                                    .Where(c => c.UserID == user.Id)
                                    .Count();

            if (cardCount >= 3)
            {
                ModelState.AddModelError("", "You may only store 3 credit cards.");
                return View(cvm);
            }

            // Convert string "Visa" → enum CardTypes.Visa
            Card.CardTypes parsedType = Enum.Parse<Card.CardTypes>(cvm.CardType);

            // Create new card
            Card card = new Card
            {
                UserID = user.Id,
                User = user,
                CardNumber = cvm.CardNumber,
                CardType = parsedType
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage");
        }

    }
}
