using System.ComponentModel.DataAnnotations;

namespace SmartTestTask.Application.Models.Requests;

public class CreateContractRequest
{
    [Required]
    public string IndustrialPremiseId { get; set; } = null!;
    [Required]
    public string TechnicalEquipmentTypeId { get; set; } = null!;
    [Required]
    public int Count { get; set; }
}