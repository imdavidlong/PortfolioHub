using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class Risk
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public RiskSeverity Severity { get; set; }

    [Required]
    [StringLength(140)]
    public string Owner { get; set; } = string.Empty;

    [Required]
    [StringLength(700)]
    public string MitigationPlan { get; set; } = string.Empty;

    [Required]
    public RiskStatus Status { get; set; }
}
