using Microsoft.EntityFrameworkCore;
using PortfolioHub.Data;
using PortfolioHub.Models;

namespace PortfolioHub.Repositories;

public class ResourceRepository(ApplicationDbContext dbContext) : IResourceRepository
{
    public async Task<IReadOnlyList<Employee>> GetEmployeesAsync()
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Include(employee => employee.Department)
            .Include(employee => employee.ProjectEmployees)
                .ThenInclude(projectEmployee => projectEmployee.Project)
            .OrderByDescending(employee => employee.CapacityPercent)
            .ThenBy(employee => employee.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Project>> GetAssignableProjectsAsync()
    {
        return await dbContext.Projects
            .AsNoTracking()
            .Where(project => project.Status != ProjectStatus.Completed)
            .OrderByDescending(project => project.Priority)
            .ThenBy(project => project.Name)
            .ToListAsync();
    }

    public async Task AssignEmployeeToProjectAsync(int employeeId, int projectId)
    {
        var exists = await dbContext.ProjectEmployees
            .AnyAsync(projectEmployee =>
                projectEmployee.EmployeeId == employeeId &&
                projectEmployee.ProjectId == projectId);

        if (exists)
        {
            return;
        }

        dbContext.ProjectEmployees.Add(new ProjectEmployee
        {
            EmployeeId = employeeId,
            ProjectId = projectId
        });

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveEmployeeFromProjectAsync(int employeeId, int projectId)
    {
        var assignment = await dbContext.ProjectEmployees
            .FirstOrDefaultAsync(projectEmployee =>
                projectEmployee.EmployeeId == employeeId &&
                projectEmployee.ProjectId == projectId);

        if (assignment is null)
        {
            return;
        }

        dbContext.ProjectEmployees.Remove(assignment);
        await dbContext.SaveChangesAsync();
    }
}
