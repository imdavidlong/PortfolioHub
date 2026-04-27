using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class ProjectFormModel : IValidatableObject
{
    public int Id { get; set; }

    [Required]
    [StringLength(180)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(140)]
    public string Owner { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Select a department.")]
    public int DepartmentId { get; set; }

    [Required]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required]
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddMonths(3));

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.OnTrack;

    [Required]
    public ProjectPriority Priority { get; set; } = ProjectPriority.Medium;

    [Range(0, 999999999)]
    public decimal BudgetAmount { get; set; }

    [Range(0, 100)]
    public int ProgressPercent { get; set; }

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    public Project ToProject()
    {
        return new Project
        {
            Id = Id,
            Name = Name,
            Owner = Owner,
            DepartmentId = DepartmentId,
            StartDate = StartDate,
            EndDate = EndDate,
            Status = Status,
            Priority = Priority,
            BudgetAmount = BudgetAmount,
            ProgressPercent = ProgressPercent,
            Description = Description,
            Budget = new Budget
            {
                Planned = BudgetAmount,
                Actual = Math.Round(BudgetAmount * ProgressPercent / 100m * 0.92m, 2),
                Forecast = Status is ProjectStatus.AtRisk or ProjectStatus.Delayed
                    ? Math.Round(BudgetAmount * 1.08m, 2)
                    : Math.Round(BudgetAmount * 0.98m, 2)
            }
        };
    }

    public static ProjectFormModel FromProject(Project project)
    {
        return new ProjectFormModel
        {
            Id = project.Id,
            Name = project.Name,
            Owner = project.Owner,
            DepartmentId = project.DepartmentId,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status,
            Priority = project.Priority,
            BudgetAmount = project.BudgetAmount,
            ProgressPercent = project.ProgressPercent,
            Description = project.Description
        };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate < StartDate)
        {
            yield return new ValidationResult("End date must be after start date.", new[] { nameof(EndDate) });
        }
    }
}
