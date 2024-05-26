using insure_fixlast.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace insure_fixlast.Data
{
    public static class SeedData
    {
        private static bool isInitialized = false; // Flag to check if data has been initialized

        public static void Initialize(IServiceProvider serviceProvider)
        {
            if (isInitialized)
            {
                return; // If data has been initialized, do not seed again
            }

            using (var context = new insure_fixlastContext(
                serviceProvider.GetRequiredService<DbContextOptions<insure_fixlastContext>>()))
            {
                // Check and seed data for various tables
                if (!context.Account.Any() || !context.Company.Any() || !context.Employee.Any())
                {
                    SeedAccounts(context);
                    SeedCompanies(context);
                    SeedEmployees(context);
                }

                isInitialized = true;
            }
        }

        private static void SeedAccounts(insure_fixlastContext context)
        {
            if (!context.Account.Any())
            {
                context.Account.AddRange(
                    new Account { Email = "admin@example.com", Name = "Admin", Password = HashPassword("Admin123"), Role = Role.Admin },
                    new Account { Email = "user1@example.com", Name = "User1", Password = HashPassword("User1234"), Role = Role.User },
                    new Account { Email = "user2@example.com", Name = "User2", Password = HashPassword("User1234"), Role = Role.User }
                );

                context.SaveChanges();
            }
        }

        private static void SeedCompanies(insure_fixlastContext context)
        {
            if (!context.Company.Any())
            {
                var accounts = context.Account.ToList();
                context.Company.AddRange(
                    new Company
                    {
                        Name = "TechCorp",
                        Email = "contact@techcorp.com",
                        Phone = "123-456-7890",
                        Address = "123 Tech Street",
                        AccountId = accounts.FirstOrDefault(a => a.Email == "admin@example.com").Id,
                        isDelete = false
                    },
                    new Company
                    {
                        Name = "HealthInc",
                        Email = "info@healthinc.com",
                        Phone = "098-765-4321",
                        Address = "456 Health Avenue",
                        AccountId = accounts.FirstOrDefault(a => a.Email == "user1@example.com").Id,
                        isDelete = false
                    },
                     new Company
                     {
                         Name = "HealthInc",
                         Email = "info@healthinc.com",
                         Phone = "098-765-4321",
                         Address = "456 Health Avenue",
                         AccountId = accounts.FirstOrDefault(a => a.Email == "user2@example.com").Id,
                         isDelete = false
                     }
                );

                context.SaveChanges();
            }
        }

        private static void SeedEmployees(insure_fixlastContext context)
        {
            if (!context.Employee.Any())
            {
                var companies = context.Company.ToList();
                context.Employee.AddRange(
                    new Employee
                    {
                        Name = "John Doe",
                        Address = "789 Employee Road",
                        Phone = "111-222-3333",
                        Level = Level.manager,
                        CompanyId = companies.FirstOrDefault(c => c.Name == "HealthInc").Id,
                        isDelete = false
                    },

                    new Employee
                    {
                        Name = "Jane Smith",
                        Address = "321 Worker Lane",
                        Phone = "444-555-6666",
                        Level = Level.employee,
                        CompanyId = companies.FirstOrDefault(c => c.Name == "HealthInc").Id,
                        isDelete = false
                    }
                );

                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
