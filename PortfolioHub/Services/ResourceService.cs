using PortfolioHub.Models;
using PortfolioHub.Repositories;

namespace PortfolioHub.Services;

public class ResourceService(IResourceRepository resourceRepository) : IResourceService
{
    public async Task<IReadOnlyList<Employee>> GetEmployeesAsync(string? search = null, EmployeeRole? role = null, string? availability = null)
    {
        var employees = await resourceRepository.GetEmployeesAsync();
        var query = employees.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(employee =>
                employee.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                employee.Role.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (employee.Department?.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        if (role.HasValue)
        {
            query = query.Where(employee => employee.Role == role.Value);
        }

        query = availability switch
        {
            "overloaded" => query.Where(employee => employee.CapacityPercent >= 90),
            "available" => query.Where(employee => employee.CapacityPercent < 80),
            "allocated" => query.Where(employee => employee.CapacityPercent is >= 80 and < 90),
            _ => query
        };

        return query.ToList();
    }

    public Task<IReadOnlyList<Project>> GetAssignableProjectsAsync()
    {
        return resourceRepository.GetAssignableProjectsAsync();
    }

    public Task AssignEmployeeToProjectAsync(int employeeId, int projectId)
    {
        return resourceRepository.AssignEmployeeToProjectAsync(employeeId, projectId);
    }

    public Task RemoveEmployeeFromProjectAsync(int employeeId, int projectId)
    {
        return resourceRepository.RemoveEmployeeFromProjectAsync(employeeId, projectId);
    }
}
