using AutoMapper;
using SmartTestTask.Application.MappingProfiles;

namespace SmartTestTask.Application.Tests;

internal static class UnitTestsHelper
{

    public static IMapper GetMapper()
    {
        var profiles = new List<Profile>()
        {
            new ContractProfile(),
            new IndustrialPremiseProfile(),
            new TechnicalEquipmentTypeProfile(),
        };
        var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));

        return new Mapper(configuration);
    }
}