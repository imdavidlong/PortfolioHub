using PortfolioHub.Models;

namespace PortfolioHub.Services;

public interface IResourceService
{
    Task<IReadOnlyList<Employee>> GetEmployeesAsync(string? search = null, EmployeeRole? role = null, string? availability = null);
    Task<IReadOnlyList<Project>> GetAssignableProjectsAsync();
    Task AssignEmployeeToProjectAsync(int employeeId, int projectId);
    Task RemoveEmployeeFromProjectAsync(int employeeId, int projectId);
}
