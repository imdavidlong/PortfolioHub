using Microsoft.EntityFrameworkCore;
using PortfolioHub.Data;
using PortfolioHub.Models;

namespace PortfolioHub.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext) : IProjectRepository
{
    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        return await dbContext.Projects
            .AsNoTracking()
            .Include(project => project.Department)
            .Include(project => project.Budget)
            .Include(project => project.Risks)
            .Include(project => project.ProjectEmployees)
                .ThenInclude(projectEmployee => projectEmployee.Employee)
            .OrderByDescending(project => project.Priority)
            .ThenBy(project => project.EndDate)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Department>> GetDepartmentsAsync()
    {
        return await dbContext.Departments
            .AsNoTracking()
            .OrderBy(department => department.Name)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await dbContext.Projects
            .Include(project => project.Department)
            .Include(project => project.Budget)
            .Include(project => project.Risks)
            .Include(project => project.ProjectEmployees)
            .FirstOrDefaultAsync(project => project.Id == id);
    }

    public async Task AddAsync(Project project)
    {
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        var existingProject = await dbContext.Projects
            .Include(existing => existing.Budget)
            .FirstOrDefaultAsync(existing => existing.Id == project.Id);

        if (existingProject is null)
        {
            return;
        }

        existingProject.Name = project.Name;
        existingProject.Owner = project.Owner;
        existingProject.DepartmentId = project.DepartmentId;
        existingProject.StartDate = project.StartDate;
        existingProject.EndDate = project.EndDate;
        existingProject.Status = project.Status;
        existingProject.Priority = project.Priority;
        existingProject.BudgetAmount = project.BudgetAmount;
        existingProject.ProgressPercent = project.ProgressPercent;
        existingProject.Description = project.Description;

        if (existingProject.Budget is null)
        {
            existingProject.Budget = project.Budget;
        }
        else if (project.Budget is not null)
        {
            existingProject.Budget.Planned = project.Budget.Planned;
            existingProject.Budget.Actual = project.Budget.Actual;
            existingProject.Budget.Forecast = project.Budget.Forecast;
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var project = await dbContext.Projects.FindAsync(id);

        if (project is null)
        {
            return;
        }

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
    }
}
