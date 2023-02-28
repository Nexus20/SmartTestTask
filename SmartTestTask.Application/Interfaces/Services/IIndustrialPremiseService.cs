using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.Application.Interfaces.Services;

public interface IIndustrialPremiseService
{
    public Task<List<IndustrialPremiseResult>> GetIndustrialPremisesAsync();
}