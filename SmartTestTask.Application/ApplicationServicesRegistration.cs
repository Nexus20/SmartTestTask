using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Services;

namespace SmartTestTask.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IIndustrialPremiseService, IndustrialPremiseService>();
        services.AddScoped<ITechnicalEquipmentTypeService, TechnicalEquipmentTypeService>();

        return services;
    }
}