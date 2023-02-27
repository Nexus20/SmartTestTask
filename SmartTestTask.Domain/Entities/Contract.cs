using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Domain.Entities;

public class Contract : Entity
{
    public string IndustrialPremiseId { get; set; } = null!;
    public virtual IndustrialPremise IndustrialPremise { get; set; } = null!;
    public string TechnicalEquipmentTypeId { get; set; } = null!;
    public virtual TechnicalEquipmentType TechnicalEquipmentType { get; set; } = null!;
    public int Count { get; set; }
}