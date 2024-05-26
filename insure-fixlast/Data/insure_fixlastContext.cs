using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using insure_fixlast.Models;

namespace insure_fixlast.Data
{
    public class insure_fixlastContext : DbContext
    {
        public insure_fixlastContext (DbContextOptions<insure_fixlastContext> options)
            : base(options)
        {
        }
        public DbSet<insure_fixlast.Models.Account> Account { get; set; } = default!;
        public DbSet<insure_fixlast.Models.Claim> Claim { get; set; } = default!;
        public DbSet<insure_fixlast.Models.Company> Company { get; set; } = default!;
        public DbSet<insure_fixlast.Models.Employee> Employee { get; set; } = default!;
        public DbSet<insure_fixlast.Models.EmployeeSubService> EmployeeSubService { get; set; } = default!;
        public DbSet<insure_fixlast.Models.Order> Order { get; set; } = default!;
        public DbSet<insure_fixlast.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<insure_fixlast.Models.Service> Service { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>().ToTable("Claim");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmployeeSubService>().ToTable("EmployeeSubService");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<Service>().ToTable("Service");
        }
    }
}
