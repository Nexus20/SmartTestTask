using SmartTestTask.Domain.Entities.Abstract;

namespace SmartTestTask.Domain.Entities;

public class TechnicalEquipmentType : Entity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Area { get; set; }
    public virtual List<Contract>? Contracts { get; set; }
}