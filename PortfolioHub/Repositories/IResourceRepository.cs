using PortfolioHub.Models;

namespace PortfolioHub.Repositories;

public interface IResourceRepository
{
    Task<IReadOnlyList<Employee>> GetEmployeesAsync();
    Task<IReadOnlyList<Project>> GetAssignableProjectsAsync();
    Task AssignEmployeeToProjectAsync(int employeeId, int projectId);
    Task RemoveEmployeeFromProjectAsync(int employeeId, int projectId);
}
