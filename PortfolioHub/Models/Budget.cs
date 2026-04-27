using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioHub.Models;

public class Budget
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999)]
    public decimal Planned { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999)]
    public decimal Actual { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999)]
    public decimal Forecast { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}
