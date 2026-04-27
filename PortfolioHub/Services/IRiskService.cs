using PortfolioHub.Models;

namespace PortfolioHub.Services;

public interface IRiskService
{
    Task<IReadOnlyList<Risk>> GetRisksAsync(string? search = null, RiskSeverity? severity = null, RiskStatus? status = null);
    Task<Risk?> GetRiskAsync(int id);
    Task CreateRiskAsync(Risk risk);
    Task UpdateRiskAsync(Risk risk);
    Task DeleteRiskAsync(int id);
}
