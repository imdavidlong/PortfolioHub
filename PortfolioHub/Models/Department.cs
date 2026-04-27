using System.ComponentModel.DataAnnotations;

namespace PortfolioHub.Models;

public class Department
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
