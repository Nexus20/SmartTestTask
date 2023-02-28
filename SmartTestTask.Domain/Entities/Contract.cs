using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Domain.Entities;

public class Contract : Entity
{
    public string IndustrialPremiseId { get; set; } = null!;
    public IndustrialPremise IndustrialPremise { get; set; } = null!;
    public string TechnicalEquipmentTypeId { get; set; } = null!;
    public TechnicalEquipmentType TechnicalEquipmentType { get; set; } = null!;
    public int Count { get; set; }
}