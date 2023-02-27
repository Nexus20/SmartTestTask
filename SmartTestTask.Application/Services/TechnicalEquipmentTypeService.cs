using AutoMapper;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Services;

public class TechnicalEquipmentTypeService : ITechnicalEquipmentTypeService
{
    private readonly IRepository<TechnicalEquipmentType> _technicalEquipmentTypeRepository;
    private readonly IMapper _mapper;

    public TechnicalEquipmentTypeService(IRepository<TechnicalEquipmentType> technicalEquipmentTypeRepository, IMapper mapper)
    {
        _technicalEquipmentTypeRepository = technicalEquipmentTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<TechnicalEquipmentTypeResult>> GetTechnicalEquipmentTypesAsync()
    {
        var source = await _technicalEquipmentTypeRepository.GetAsync();
        return _mapper.Map<List<TechnicalEquipmentType>, List<TechnicalEquipmentTypeResult>>(source);
    }
}