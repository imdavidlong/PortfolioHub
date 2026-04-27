using PortfolioHub.Models;
using PortfolioHub.Repositories;

namespace PortfolioHub.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<IReadOnlyList<Project>> GetProjectsAsync(string? search = null, ProjectStatus? status = null, bool sortByPriority = true)
    {
        var projects = await projectRepository.GetAllAsync();
        var query = projects.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(project =>
                project.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                project.Owner.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (project.Department?.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        if (status.HasValue)
        {
            query = query.Where(project => project.Status == status.Value);
        }

        query = sortByPriority
            ? query.OrderByDescending(project => project.Priority).ThenBy(project => project.EndDate)
            : query.OrderBy(project => project.Name);

        return query.ToList();
    }

    public Task<IReadOnlyList<Department>> GetDepartmentsAsync()
    {
        return projectRepository.GetDepartmentsAsync();
    }

    public Task<Project?> GetProjectAsync(int id)
    {
        return projectRepository.GetByIdAsync(id);
    }

    public Task CreateProjectAsync(Project project)
    {
        return projectRepository.AddAsync(project);
    }

    public Task UpdateProjectAsync(Project project)
    {
        return projectRepository.UpdateAsync(project);
    }

    public Task DeleteProjectAsync(int id)
    {
        return projectRepository.DeleteAsync(id);
    }
}
