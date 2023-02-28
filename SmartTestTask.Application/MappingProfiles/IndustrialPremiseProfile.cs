using AutoMapper;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.MappingProfiles;

public class IndustrialPremiseProfile : Profile
{
    public IndustrialPremiseProfile()
    {
        CreateMap<IndustrialPremise, IndustrialPremiseResult>();
    }
}