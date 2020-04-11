using Azakaw.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Azakaw.Data.Configurations
{
    public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Complaints)
                .HasForeignKey(x => x.UserId);
        }
    }
}