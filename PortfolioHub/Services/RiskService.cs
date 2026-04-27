using PortfolioHub.Models;
using PortfolioHub.Repositories;

namespace PortfolioHub.Services;

public class RiskService(IRiskRepository riskRepository) : IRiskService
{
    public async Task<IReadOnlyList<Risk>> GetRisksAsync(string? search = null, RiskSeverity? severity = null, RiskStatus? status = null)
    {
        var risks = await riskRepository.GetRisksAsync();
        var query = risks.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(risk =>
                risk.Description.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                risk.Owner.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (risk.Project?.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        if (severity.HasValue)
        {
            query = query.Where(risk => risk.Severity == severity.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(risk => risk.Status == status.Value);
        }

        return query
            .OrderByDescending(risk => risk.Severity)
            .ThenBy(risk => risk.Status)
            .ToList();
    }

    public Task<Risk?> GetRiskAsync(int id)
    {
        return riskRepository.GetRiskAsync(id);
    }

    public Task CreateRiskAsync(Risk risk)
    {
        return riskRepository.AddRiskAsync(risk);
    }

    public Task UpdateRiskAsync(Risk risk)
    {
        return riskRepository.UpdateRiskAsync(risk);
    }

    public Task DeleteRiskAsync(int id)
    {
        return riskRepository.DeleteRiskAsync(id);
    }
}
