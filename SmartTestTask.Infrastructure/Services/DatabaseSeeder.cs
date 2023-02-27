using Microsoft.EntityFrameworkCore;
using SmartTestTask.Application.Services;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Infrastructure.Services;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedDatabaseAsync()
    {
        var isSaveNeeded = false;

        if (!await _dbContext.IndustrialPremises.AnyAsync())
        {
            await _dbContext.IndustrialPremises.AddRangeAsync(IndustrialPremises);
            isSaveNeeded = true;
        }

        if (!await _dbContext.TechnicalEquipmentTypes.AnyAsync())
        {
            await _dbContext.TechnicalEquipmentTypes.AddRangeAsync(TechnicalEquipmentTypes);
            isSaveNeeded = true;
        }

        if(isSaveNeeded)
            await _dbContext.SaveChangesAsync();
    }

    private static readonly IEnumerable<IndustrialPremise> IndustrialPremises = new List<IndustrialPremise>()
    {
        new()
        {
            Code = "IP-111",
            Area = 120,
            Name = "Warehouse 1"
        },
        new()
        {
            Code = "IP-222",
            Area = 33,
            Name = "Warehouse 2"
        },
        new()
        {
            Code = "IP-333",
            Area = 21,
            Name = "Warehouse 3"
        },
        new()
        {
            Code = "IP-444",
            Area = 44,
            Name = "Warehouse 4"
        },
        new()
        {
            Code = "IP-555",
            Area = 67,
            Name = "Warehouse 5"
        },
    };
    
    private static readonly IEnumerable<TechnicalEquipmentType> TechnicalEquipmentTypes = new List<TechnicalEquipmentType>()
    {
        new()
        {
            Code = "TET-111",
            Area = 7,
            Name = "Lathe"
        },
        new()
        {
            Code = "TET-222",
            Area = 3.5,
            Name = "Milling machine"
        },
        new()
        {
            Code = "TET-333",
            Area = 5,
            Name = "Rolling machine"
        },
        new()
        {
            Code = "TET-444",
            Area = 4,
            Name = "Autoclave"
        },
        new()
        {
            Code = "TET-555",
            Area = 2,
            Name = "Grinding machine"
        },
    };
}