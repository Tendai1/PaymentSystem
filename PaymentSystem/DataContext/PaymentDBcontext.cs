using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentSystem.Models;

namespace PaymentSystem.DataContext
{
    public class PaymentDBcontext : DbContext
    {
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentState> PaymentState { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { 
            options.UseSqlServer(ConfigurationManager.AppSettings["dbConString"].ToString().Trim()); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasKey(p => new { p.PaymentId});

            var converter = new EnumToStringConverter<PaymentState.Status>();


            modelBuilder.Entity<PaymentState>()
                .Property(e => e.State)
                .HasConversion(converter)
                //.HasOne<Payment>(p => p.payment)
                //.WithOne(ps => ps.Status)
                ;
        }
    }
}