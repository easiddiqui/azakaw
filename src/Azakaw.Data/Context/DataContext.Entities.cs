using Azakaw.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Azakaw.Data.Context
{
    partial class DataContext
    {
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<User> Users { get; set; }
    }
}