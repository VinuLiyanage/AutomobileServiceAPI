﻿using GarageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options) { }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrdersItem> OrdersItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite key for StudentCourse
            modelBuilder.Entity<OrdersItem>()
                .HasKey(oi => new { oi.OrderId, oi.ItemId });

            // Configure the relationships
            modelBuilder.Entity<OrdersItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrdersItem)
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<OrdersItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrdersItem)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}
