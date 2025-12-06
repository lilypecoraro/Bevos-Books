using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//TODO: Update this using statement to include your project name
using Team24_BevosBooks.Models;
using System;
using System.Collections.Generic;

//TODO: Make this namespace match your project name
namespace Team24_BevosBooks.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

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

            modelBuilder.Entity<ShippingSetting>().HasData(
                new ShippingSetting
                {
                    SettingID = 1,
                    FirstBookRate = 3.50m,
                    AdditionalBookRate = 1.50m
                }
            );

        }


        //TODO: Add Dbsets here.  Products is included as an example.  
        public DbSet<Book> Books { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Reorder> Reorders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShippingSetting> ShippingSettings { get; set; }



    }
}
