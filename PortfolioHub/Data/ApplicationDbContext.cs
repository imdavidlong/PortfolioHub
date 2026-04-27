using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioHub.Models;

namespace PortfolioHub.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<Risk> Risks => Set<Risk>();
    public DbSet<ProjectEmployee> ProjectEmployees => Set<ProjectEmployee>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProjectEmployee>()
            .HasKey(projectEmployee => new { projectEmployee.ProjectId, projectEmployee.EmployeeId });

        builder.Entity<ProjectEmployee>()
            .HasOne(projectEmployee => projectEmployee.Project)
            .WithMany(project => project.ProjectEmployees)
            .HasForeignKey(projectEmployee => projectEmployee.ProjectId);

        builder.Entity<ProjectEmployee>()
            .HasOne(projectEmployee => projectEmployee.Employee)
            .WithMany(employee => employee.ProjectEmployees)
            .HasForeignKey(projectEmployee => projectEmployee.EmployeeId);

        builder.Entity<Project>()
            .HasOne(project => project.Budget)
            .WithOne(budget => budget.Project)
            .HasForeignKey<Budget>(budget => budget.ProjectId);

        builder.Entity<Project>()
            .Property(project => project.BudgetAmount)
            .HasPrecision(18, 2);

        builder.Entity<Budget>()
            .Property(budget => budget.Planned)
            .HasPrecision(18, 2);

        builder.Entity<Budget>()
            .Property(budget => budget.Actual)
            .HasPrecision(18, 2);

        builder.Entity<Budget>()
            .Property(budget => budget.Forecast)
            .HasPrecision(18, 2);

        SeedPortfolioData(builder);
    }

    private static void SeedPortfolioData(ModelBuilder builder)
    {
        builder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "IT" },
            new Department { Id = 2, Name = "Finance" },
            new Department { Id = 3, Name = "Product" },
            new Department { Id = 4, Name = "Security" },
            new Department { Id = 5, Name = "Operations" });

        builder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "Maya Chen", Role = EmployeeRole.PM, CapacityPercent = 82, DepartmentId = 3 },
            new Employee { Id = 2, Name = "Ethan Brooks", Role = EmployeeRole.Developer, CapacityPercent = 96, DepartmentId = 1 },
            new Employee { Id = 3, Name = "Sofia Patel", Role = EmployeeRole.Analyst, CapacityPercent = 74, DepartmentId = 2 },
            new Employee { Id = 4, Name = "Lucas Nguyen", Role = EmployeeRole.Developer, CapacityPercent = 88, DepartmentId = 4 },
            new Employee { Id = 5, Name = "Ava Thompson", Role = EmployeeRole.Designer, CapacityPercent = 67, DepartmentId = 3 },
            new Employee { Id = 6, Name = "Noah Rivera", Role = EmployeeRole.Analyst, CapacityPercent = 91, DepartmentId = 5 });

        builder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Name = "AI Security Rollout",
                Owner = "Maya Chen",
                DepartmentId = 4,
                StartDate = new DateOnly(2026, 1, 8),
                EndDate = new DateOnly(2026, 8, 30),
                Status = ProjectStatus.AtRisk,
                Priority = ProjectPriority.Critical,
                BudgetAmount = 850000,
                ProgressPercent = 48,
                Description = "Enterprise rollout of AI security controls across engineering and operations workflows."
            },
            new Project
            {
                Id = 2,
                Name = "ERP Migration",
                Owner = "Sofia Patel",
                DepartmentId = 2,
                StartDate = new DateOnly(2025, 11, 3),
                EndDate = new DateOnly(2026, 10, 15),
                Status = ProjectStatus.OnTrack,
                Priority = ProjectPriority.High,
                BudgetAmount = 1200000,
                ProgressPercent = 62,
                Description = "Finance and operations ERP migration with staged data reconciliation."
            },
            new Project
            {
                Id = 3,
                Name = "Customer Portal Redesign",
                Owner = "Ava Thompson",
                DepartmentId = 3,
                StartDate = new DateOnly(2026, 2, 12),
                EndDate = new DateOnly(2026, 7, 1),
                Status = ProjectStatus.OnTrack,
                Priority = ProjectPriority.Medium,
                BudgetAmount = 420000,
                ProgressPercent = 55,
                Description = "Modernized self-service experience for enterprise customers and partner success teams."
            },
            new Project
            {
                Id = 4,
                Name = "SOC Automation Upgrade",
                Owner = "Lucas Nguyen",
                DepartmentId = 4,
                StartDate = new DateOnly(2026, 3, 1),
                EndDate = new DateOnly(2026, 9, 25),
                Status = ProjectStatus.Delayed,
                Priority = ProjectPriority.High,
                BudgetAmount = 690000,
                ProgressPercent = 31,
                Description = "Automated response playbooks and signal triage for the security operations center."
            },
            new Project
            {
                Id = 5,
                Name = "HR Platform Modernization",
                Owner = "Noah Rivera",
                DepartmentId = 5,
                StartDate = new DateOnly(2026, 1, 20),
                EndDate = new DateOnly(2026, 6, 18),
                Status = ProjectStatus.Completed,
                Priority = ProjectPriority.Medium,
                BudgetAmount = 310000,
                ProgressPercent = 100,
                Description = "Modernization of employee lifecycle tooling and reporting workflows."
            });

        builder.Entity<Budget>().HasData(
            new Budget { Id = 1, ProjectId = 1, Planned = 850000, Actual = 492000, Forecast = 910000 },
            new Budget { Id = 2, ProjectId = 2, Planned = 1200000, Actual = 704500, Forecast = 1185000 },
            new Budget { Id = 3, ProjectId = 3, Planned = 420000, Actual = 228000, Forecast = 402000 },
            new Budget { Id = 4, ProjectId = 4, Planned = 690000, Actual = 301000, Forecast = 760000 },
            new Budget { Id = 5, ProjectId = 5, Planned = 310000, Actual = 298500, Forecast = 305000 });

        builder.Entity<Risk>().HasData(
            new Risk { Id = 1, ProjectId = 1, Description = "Security policy exceptions require additional review cycles.", Severity = RiskSeverity.High, Owner = "Lucas Nguyen", MitigationPlan = "Create weekly exception review board with Legal and Security.", Status = RiskStatus.Mitigating },
            new Risk { Id = 2, ProjectId = 2, Description = "Legacy data mapping could delay finance close validation.", Severity = RiskSeverity.Medium, Owner = "Sofia Patel", MitigationPlan = "Run parallel reconciliation during the next two close cycles.", Status = RiskStatus.Monitoring },
            new Risk { Id = 3, ProjectId = 4, Description = "Automation connector vendor has slipped delivery milestones.", Severity = RiskSeverity.Critical, Owner = "Lucas Nguyen", MitigationPlan = "Prepare fallback integration path and escalate vendor governance.", Status = RiskStatus.Open },
            new Risk { Id = 4, ProjectId = 3, Description = "Design approvals may compete with launch campaign timelines.", Severity = RiskSeverity.Low, Owner = "Ava Thompson", MitigationPlan = "Batch stakeholder reviews and lock content before sprint planning.", Status = RiskStatus.Monitoring });

        builder.Entity<ProjectEmployee>().HasData(
            new ProjectEmployee { ProjectId = 1, EmployeeId = 1 },
            new ProjectEmployee { ProjectId = 1, EmployeeId = 4 },
            new ProjectEmployee { ProjectId = 2, EmployeeId = 3 },
            new ProjectEmployee { ProjectId = 2, EmployeeId = 6 },
            new ProjectEmployee { ProjectId = 3, EmployeeId = 1 },
            new ProjectEmployee { ProjectId = 3, EmployeeId = 5 },
            new ProjectEmployee { ProjectId = 4, EmployeeId = 2 },
            new ProjectEmployee { ProjectId = 4, EmployeeId = 4 },
            new ProjectEmployee { ProjectId = 5, EmployeeId = 3 },
            new ProjectEmployee { ProjectId = 5, EmployeeId = 6 });
    }
}
