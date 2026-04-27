using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(180)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(140)]
    public string Owner { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateOnly EndDate { get; set; }

    [Required]
    public ProjectStatus Status { get; set; }

    [Required]
    public ProjectPriority Priority { get; set; }

    [Range(0, 999999999)]
    public decimal BudgetAmount { get; set; }

    [Range(0, 100)]
    public int ProgressPercent { get; set; }

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    public Budget? Budget { get; set; }
    public ICollection<Risk> Risks { get; set; } = new List<Risk>();
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}
