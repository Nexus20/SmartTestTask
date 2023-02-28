using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.Tests;

internal static class UnitTestsHelper
{
    public static DbContextOptions<ApplicationDbContext> GetUnitTestDbOptions()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        using var context = new ApplicationDbContext(options);
        SeedData(context);

        return options;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        // Seed the needed data here into inMemory Db context

        if (!context.IndustrialPremises.Any())
        {
            context.IndustrialPremises.AddRange(
                new IndustrialPremise
                {
                    Id = "1",
                    Area = 100,
                    Code = "TEP-1",
                    Name = "Test premise 1"
                },
                new IndustrialPremise
                {
                    Id = "2",
                    Area = 50,
                    Code = "TEP-2",
                    Name = "Test premise 2"
                },
                new IndustrialPremise
                {
                    Id = "3",
                    Area = 96,
                    Code = "TEP-3",
                    Name = "Test premise 3"
                }
                );
        }

        if (!context.TechnicalEquipmentTypes.Any())
        {
            context.TechnicalEquipmentTypes.AddRange(new TechnicalEquipmentType
                {
                    Id = "1",
                    Area = 10,
                    Code = "TTET-1",
                    Name = "Test equipment type 1"
                },
                new TechnicalEquipmentType
                {
                    Id = "2",
                    Area = 12,
                    Code = "TTET-2",
                    Name = "Test equipment type 2"
                },
                new TechnicalEquipmentType
                {
                    Id = "3",
                    Area = 8,
                    Code = "TTET-3",
                    Name = "Test equipment type 3"
                });
        }

        if (!context.Contracts.Any())
        {
            context.Contracts.AddRange(new Contract
                {
                    Id = "1",
                    Count = 10,
                    IndustrialPremiseId = "1",
                    TechnicalEquipmentTypeId = "3",
                },
                new Contract
                {
                    Id = "2",
                    Count = 2,
                    IndustrialPremiseId = "1",
                    TechnicalEquipmentTypeId = "2",
                },
                new Contract
                {
                    Id = "3",
                    Count = 5,
                    IndustrialPremiseId = "2",
                    TechnicalEquipmentTypeId = "1",
                },
                new Contract
                {
                    Id = "4",
                    Count = 8,
                    IndustrialPremiseId = "3",
                    TechnicalEquipmentTypeId = "2",
                },
                new Contract
                {
                    Id = "5",
                    Count = 1,
                    IndustrialPremiseId = "3",
                    TechnicalEquipmentTypeId = "1",
                });
        }

        context.SaveChanges();
    }
}