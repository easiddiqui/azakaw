using Azakaw.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azakaw.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder
                .HasMany(x => x.Complaints)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}