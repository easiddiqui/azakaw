using System;
using System.Collections.Generic;
using Azakaw.Data.Configurations;
using Azakaw.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Azakaw.Data.Context
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ComplaintConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // seed complaints data
            var complaints = new List<Complaint>();
            for (var i = 1; i < 5; i++)
            {
                complaints.Add(new Complaint
                {
                    Id = i,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    ComplaintStatus = ComplaintStatus.InProgress,
                    Message = $"complaint message {i}",
                    UserId = i
                });
            }

            // seed user data
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    Email = "admin1@admin.com",
                    Password = "admin1",
                    FirstName = "Admin 1",
                    Role = Role.Admin
                }
            };

            for (var i = 2; i < 10; i++)
            {
                users.Add(new User
                {
                    Id = i,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    Email = $"user{i}@user.com",
                    Password = $"user{i}",
                    FirstName = $"User {i}",
                    Role = Role.User
                });
            }

            modelBuilder.Entity<Complaint>().HasData(complaints);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}