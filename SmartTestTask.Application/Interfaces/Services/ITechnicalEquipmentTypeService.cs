using SmartTestTask.Application.Models.Results;

namespace SmartTestTask.Application.Interfaces.Services;

public interface ITechnicalEquipmentTypeService
{
    public Task<List<TechnicalEquipmentTypeResult>> GetTechnicalEquipmentTypesAsync();
}