using System.ComponentModel.DataAnnotations;

namespace SmartTestTask.Application.Models.Requests;

public class CreateContractRequest
{
    [Required]
    public string IndustrialPremiseCode { get; set; } = null!;
    [Required]
    public string TechnicalEquipmentTypeCode { get; set; } = null!;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum equipment count is 1")]
    public int Count { get; set; }
}