using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;

namespace Team24_BevosBooks.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete cycle between Order and OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            // Prevent cascade on Book → OrderDetail too
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Book)
                .WithMany()
                .HasForeignKey(od => od.BookID)
                .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction

            // Seed default shipping settings
            modelBuilder.Entity<ShippingSetting>().HasData(
                new ShippingSetting
                {
                    SettingID = 1,
                    FirstBookRate = 3.50m,
                    AdditionalBookRate = 1.50m
                }
            );
        }

        // DbSets
        public DbSet<Book> Books { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Reorder> Reorders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingSetting> ShippingSettings { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
    }
}