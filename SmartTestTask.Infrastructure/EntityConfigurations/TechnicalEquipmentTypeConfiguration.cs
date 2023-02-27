using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.EntityConfigurations;

internal class TechnicalEquipmentTypeConfiguration : IEntityTypeConfiguration<TechnicalEquipmentType>
{
    public void Configure(EntityTypeBuilder<TechnicalEquipmentType> builder)
    {
        builder.HasIndex(x => new {x.Code, x.Name}).IsUnique();

        builder.HasMany(x => x.Contracts)
            .WithOne(x => x.TechnicalEquipmentType)
            .HasForeignKey(x => x.TechnicalEquipmentTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}