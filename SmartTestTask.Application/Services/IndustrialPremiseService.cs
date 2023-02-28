using AutoMapper;
using SmartTestTask.Application.Interfaces.Repositories;
using SmartTestTask.Application.Interfaces.Services;
using SmartTestTask.Application.Models.Results;
using SmartTestTask.Domain.Entities;

namespace SmartTestTask.Application.Services;

public class IndustrialPremiseService : IIndustrialPremiseService
{
    private readonly IRepository<IndustrialPremise> _industrialPremiseRepository;
    private readonly IMapper _mapper;

    public IndustrialPremiseService(IRepository<IndustrialPremise> industrialPremiseRepository, IMapper mapper)
    {
        _industrialPremiseRepository = industrialPremiseRepository;
        _mapper = mapper;
    }

    public async Task<List<IndustrialPremiseResult>> GetIndustrialPremisesAsync()
    {
        var source = await _industrialPremiseRepository.GetAsync();
        return _mapper.Map<List<IndustrialPremise>, List<IndustrialPremiseResult>>(source);
    }
}