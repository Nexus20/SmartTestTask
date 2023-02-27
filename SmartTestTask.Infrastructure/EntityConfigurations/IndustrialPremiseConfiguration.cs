using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.EntityConfigurations;

internal class IndustrialPremiseConfiguration : IEntityTypeConfiguration<IndustrialPremise>
{
    public void Configure(EntityTypeBuilder<IndustrialPremise> builder)
    {
        builder.HasIndex(x => new {x.Code, x.Name}).IsUnique();

        builder.HasMany(x => x.Contracts)
            .WithOne(x => x.IndustrialPremise)
            .HasForeignKey(x => x.IndustrialPremiseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}