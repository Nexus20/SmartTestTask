using AutoMapper;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.MappingProfiles;

public class TechnicalEquipmentTypeProfile : Profile
{
    public TechnicalEquipmentTypeProfile()
    {
        CreateMap<TechnicalEquipmentType, TechnicalEquipmentTypeResult>();
    }
}