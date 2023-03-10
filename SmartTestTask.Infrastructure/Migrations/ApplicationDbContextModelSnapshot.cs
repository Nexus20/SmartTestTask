// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartTestTask.Infrastructure;

#nullable disable

namespace SmartTestTask.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SmartTestTask.Domain.Entities.Contract", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IndustrialPremiseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TechnicalEquipmentTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IndustrialPremiseId");

                    b.HasIndex("TechnicalEquipmentTypeId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("SmartTestTask.Domain.Entities.IndustrialPremise", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code", "Name")
                        .IsUnique();

                    b.ToTable("IndustrialPremises");
                });

            modelBuilder.Entity("SmartTestTask.Domain.Entities.TechnicalEquipmentType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Code", "Name")
                        .IsUnique();

                    b.ToTable("TechnicalEquipmentTypes");
                });

            modelBuilder.Entity("SmartTestTask.Domain.Entities.Contract", b =>
                {
                    b.HasOne("SmartTestTask.Domain.Entities.IndustrialPremise", "IndustrialPremise")
                        .WithMany("Contracts")
                        .HasForeignKey("IndustrialPremiseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartTestTask.Domain.Entities.TechnicalEquipmentType", "TechnicalEquipmentType")
                        .WithMany("Contracts")
                        .HasForeignKey("TechnicalEquipmentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("IndustrialPremise");

                    b.Navigation("TechnicalEquipmentType");
                });

            modelBuilder.Entity("SmartTestTask.Domain.Entities.IndustrialPremise", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("SmartTestTask.Domain.Entities.TechnicalEquipmentType", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
