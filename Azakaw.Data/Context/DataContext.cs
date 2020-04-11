using Microsoft.EntityFrameworkCore;

namespace Azakaw.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}