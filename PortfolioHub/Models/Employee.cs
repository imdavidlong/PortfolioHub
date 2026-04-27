using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [StringLength(140)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public EmployeeRole Role { get; set; }

    [Range(0, 150)]
    public int CapacityPercent { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}
