using AutoMapper;
using SmartTestTask.Application.Exceptions;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Requests;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly IRepository<IndustrialPremise> _industrialPremiseRepository;
    private readonly IRepository<TechnicalEquipmentType> _technicalEquipmentTypeRepository;
    private readonly IMapper _mapper;

    public ContractService(IContractRepository contractRepository, IRepository<IndustrialPremise> industrialPremiseRepository, IRepository<TechnicalEquipmentType> technicalEquipmentTypeRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _industrialPremiseRepository = industrialPremiseRepository;
        _technicalEquipmentTypeRepository = technicalEquipmentTypeRepository;
        _mapper = mapper;
    }

    public async Task<ContractResult> CreateContractAsync(CreateContractRequest request)
    {
        var technicalEquipmentType =
            await _technicalEquipmentTypeRepository.GetSingleByExpressionAsync(x =>
                x.Code == request.TechnicalEquipmentTypeCode);

        if (technicalEquipmentType == null)
            throw new ValidationException(
                $"Invalid technical equipment type code {request.TechnicalEquipmentTypeCode}. Technical equipment type with such code doesn't exist");

        var industrialPremise =
            await _industrialPremiseRepository.GetSingleByExpressionAsync(x =>
                x.Code == request.IndustrialPremiseCode);
        
        if(industrialPremise == null)
            throw new ValidationException(
                $"Invalid industrial premise code {request.IndustrialPremiseCode}. Industrial premise with such code doesn't exist");

        var occupiedArea = industrialPremise.Contracts?.Sum(x => x.TechnicalEquipmentType.Area * x.Count) ?? 0;
        var freeArea = industrialPremise.Area - occupiedArea;
        var requestedArea = technicalEquipmentType.Area * request.Count;

        if (requestedArea > freeArea)
            throw new ValidationException(
                $"Requested area of {requestedArea} can't be allocated. Area available is: {freeArea}");

        var newContract = new Contract()
        {
            Count = request.Count,
            IndustrialPremiseId = industrialPremise.Id,
            TechnicalEquipmentTypeId = technicalEquipmentType.Id
        };

        await _contractRepository.CreateContractAsync(newContract);

        newContract = await _contractRepository.GetSingleByExpressionAsync(x => x.Id == newContract.Id);
        
        return _mapper.Map<Contract, ContractResult>(newContract!);
    }

    public async Task<List<ContractResult>> GetContractsAsync()
    {
        var source = await _contractRepository.GetAsync();
        return _mapper.Map<List<Contract>, List<ContractResult>>(source);
    }
}