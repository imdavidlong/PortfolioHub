using Microsoft.EntityFrameworkCore;
using PortfolioHub.Data;
using PortfolioHub.Models;

namespace PortfolioHub.Repositories;

public class RiskRepository(ApplicationDbContext dbContext) : IRiskRepository
{
    public async Task<IReadOnlyList<Risk>> GetRisksAsync()
    {
        return await dbContext.Risks
            .AsNoTracking()
            .Include(risk => risk.Project)
                .ThenInclude(project => project!.Department)
            .OrderByDescending(risk => risk.Severity)
            .ThenBy(risk => risk.Status)
            .ToListAsync();
    }

    public async Task<Risk?> GetRiskAsync(int id)
    {
        return await dbContext.Risks
            .Include(risk => risk.Project)
            .FirstOrDefaultAsync(risk => risk.Id == id);
    }

    public async Task AddRiskAsync(Risk risk)
    {
        dbContext.Risks.Add(risk);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateRiskAsync(Risk risk)
    {
        var existingRisk = await dbContext.Risks.FindAsync(risk.Id);

        if (existingRisk is null)
        {
            return;
        }

        existingRisk.ProjectId = risk.ProjectId;
        existingRisk.Description = risk.Description;
        existingRisk.Severity = risk.Severity;
        existingRisk.Owner = risk.Owner;
        existingRisk.MitigationPlan = risk.MitigationPlan;
        existingRisk.Status = risk.Status;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRiskAsync(int id)
    {
        var risk = await dbContext.Risks.FindAsync(id);

        if (risk is null)
        {
            return;
        }

        dbContext.Risks.Remove(risk);
        await dbContext.SaveChangesAsync();
    }
}
