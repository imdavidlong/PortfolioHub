using PortfolioHub.Models;

namespace PortfolioHub.Services;

public interface IProjectService
{
    Task<IReadOnlyList<Project>> GetProjectsAsync(string? search = null, ProjectStatus? status = null, bool sortByPriority = true);
    Task<IReadOnlyList<Department>> GetDepartmentsAsync();
    Task<Project?> GetProjectAsync(int id);
    Task CreateProjectAsync(Project project);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(int id);
}
