using PortfolioHub.Models;

namespace PortfolioHub.Repositories;

public interface IRiskRepository
{
    Task<IReadOnlyList<Risk>> GetRisksAsync();
    Task<Risk?> GetRiskAsync(int id);
    Task AddRiskAsync(Risk risk);
    Task UpdateRiskAsync(Risk risk);
    Task DeleteRiskAsync(int id);
}
