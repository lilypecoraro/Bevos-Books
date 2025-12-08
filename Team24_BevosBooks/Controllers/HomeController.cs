using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Team24_BevosBooks.DAL;
using Team24_BevosBooks.Models;
using Team24_BevosBooks.Services;

namespace Team24_BevosBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public HomeController(AppDbContext context,
                              IConfiguration config,
                              IEmailSender emailSender)
        {
            _context = context;
            _config = config;
            _emailSender = emailSender;
        }

        // =====================================
        // HOME PAGE
        // =====================================
        public async Task<IActionResult> Index(string? searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index", "Books", new { searchString });
            }

            var books = await _context.Books
                .OrderBy(b => b.Title)
                .Take(6)
                .ToListAsync();

            var promos = await _context.Coupons
                .Where(c => c.Status == "Enabled")
                .ToListAsync();

            ViewBag.Promos = promos;

            int homeId = _config.GetValue<int>("HomeCouponId");
            var homeCoupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.CouponID == homeId && c.Status == "Enabled");

            ViewBag.HomeCoupon = homeCoupon;

            return View(books);
        }

        // =====================================
        // ABOUT US
        // =====================================
        public IActionResult AboutUs()
        {
            return View();
        }

        // =====================================
        // CONTACT US — GET
        // =====================================
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // =====================================
        // CONTACT US — POST
        // =====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(
            string Name,
            string Email,
            string Category,
            string OrderNumber,
            string Urgency,
            string Message)
        {
            // ================================
            // EMAIL → TEAM 24
            // ================================
            string teamBody = EmailTemplate.Wrap($@"
                <h2>New Contact Form Submission</h2>

                <p><strong>Name:</strong> {Name}</p>
                <p><strong>Email:</strong> {Email}</p>
                <p><strong>Category:</strong> {Category}</p>
                {(Category == "Order Issue" ? $"<p><strong>Order Number:</strong> {OrderNumber}</p>" : "")}
                <p><strong>Urgency:</strong> {Urgency}</p>

                <h3 style='margin-top:25px;'>Message:</h3>
                <p>{Message}</p>
            ");

            await _emailSender.SendEmailAsync(
                "team24.bevobooks@gmail.com",
                $"New Contact Submission — {Category}",
                teamBody
            );

            // ================================
            // AUTO-REPLY → CUSTOMER
            // ================================
            string replyBody = EmailTemplate.Wrap($@"
                <h2>We've Received Your Message</h2>

                <p>Hi {Name},</p>

                <p>Thank you for reaching out to Bevo's Books! Our team has received your message and 
                will respond within <strong>1–2 business days</strong>.</p>

                <p><strong>Your Message:</strong></p>
                <p>{Message}</p>

                <p>If you need to add anything, simply reply to this email.</p>
            ");

            await _emailSender.SendEmailAsync(
                Email,
                "We received your message — Bevo's Books",
                replyBody
            );

            ViewBag.StatusMessage = "Your message has been sent! Our team will get back to you soon.";
            return View();
        }
    }
}