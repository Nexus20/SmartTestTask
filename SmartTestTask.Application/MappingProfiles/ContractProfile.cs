using AutoMapper;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.MappingProfiles;

public class ContractProfile : Profile
{
    public ContractProfile()
    {
        CreateMap<Contract, ContractResult>()
            .ForMember(d => d.IndustrialPremiseName, o => o.MapFrom(s => s.IndustrialPremise.Name))
            .ForMember(d => d.TechnicalEquipmentTypeName, o => o.MapFrom(s => s.TechnicalEquipmentType.Name));
    }
}