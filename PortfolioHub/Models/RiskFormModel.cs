using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class RiskFormModel
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Select a project.")]
    public int ProjectId { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public RiskSeverity Severity { get; set; } = RiskSeverity.Medium;

    [Required]
    [StringLength(140)]
    public string Owner { get; set; } = string.Empty;

    [Required]
    [StringLength(700)]
    public string MitigationPlan { get; set; } = string.Empty;

    [Required]
    public RiskStatus Status { get; set; } = RiskStatus.Open;

    public Risk ToRisk()
    {
        return new Risk
        {
            Id = Id,
            ProjectId = ProjectId,
            Description = Description,
            Severity = Severity,
            Owner = Owner,
            MitigationPlan = MitigationPlan,
            Status = Status
        };
    }

    public static RiskFormModel FromRisk(Risk risk)
    {
        return new RiskFormModel
        {
            Id = risk.Id,
            ProjectId = risk.ProjectId,
            Description = risk.Description,
            Severity = risk.Severity,
            Owner = risk.Owner,
            MitigationPlan = risk.MitigationPlan,
            Status = risk.Status
        };
    }
}
