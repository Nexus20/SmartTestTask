using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Domain.Entities;
using SmartTestTask.Infrastructure.Repositories;
using SmartTestTask.Infrastructure.Services;

namespace SmartTestTask.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IRepository<IndustrialPremise>, IndustrialPremiseRepository>();

        return services;
    }
}