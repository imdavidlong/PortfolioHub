using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioHub.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolioDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 140, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CapacityPercent = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                    Owner = table.Column<string>(type: "TEXT", maxLength: 140, nullable: false),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    BudgetAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ProgressPercent = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Planned = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Actual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Forecast = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => new { x.ProjectId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Severity = table.Column<int>(type: "INTEGER", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", maxLength: 140, nullable: false),
                    MitigationPlan = table.Column<string>(type: "TEXT", maxLength: 700, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("""
                INSERT INTO Departments (Id, Name) VALUES
                (1, 'IT'),
                (2, 'Finance'),
                (3, 'Product'),
                (4, 'Security'),
                (5, 'Operations');
                """);

            migrationBuilder.Sql("""
                INSERT INTO Employees (Id, CapacityPercent, DepartmentId, Name, Role) VALUES
                (1, 82, 3, 'Maya Chen', 3),
                (2, 96, 1, 'Ethan Brooks', 1),
                (3, 74, 2, 'Sofia Patel', 4),
                (4, 88, 4, 'Lucas Nguyen', 1),
                (5, 67, 3, 'Ava Thompson', 2),
                (6, 91, 5, 'Noah Rivera', 4);
                """);

            migrationBuilder.Sql("""
                INSERT INTO Projects (Id, BudgetAmount, DepartmentId, Description, EndDate, Name, Owner, Priority, ProgressPercent, StartDate, Status) VALUES
                (1, 850000, 4, 'Enterprise rollout of AI security controls across engineering and operations workflows.', '2026-08-30', 'AI Security Rollout', 'Maya Chen', 4, 48, '2026-01-08', 2),
                (2, 1200000, 2, 'Finance and operations ERP migration with staged data reconciliation.', '2026-10-15', 'ERP Migration', 'Sofia Patel', 3, 62, '2025-11-03', 1),
                (3, 420000, 3, 'Modernized self-service experience for enterprise customers and partner success teams.', '2026-07-01', 'Customer Portal Redesign', 'Ava Thompson', 2, 55, '2026-02-12', 1),
                (4, 690000, 4, 'Automated response playbooks and signal triage for the security operations center.', '2026-09-25', 'SOC Automation Upgrade', 'Lucas Nguyen', 3, 31, '2026-03-01', 3),
                (5, 310000, 5, 'Modernization of employee lifecycle tooling and reporting workflows.', '2026-06-18', 'HR Platform Modernization', 'Noah Rivera', 2, 100, '2026-01-20', 4);
                """);

            migrationBuilder.Sql("""
                INSERT INTO Budgets (Id, Actual, Forecast, Planned, ProjectId) VALUES
                (1, 492000, 910000, 850000, 1),
                (2, 704500, 1185000, 1200000, 2),
                (3, 228000, 402000, 420000, 3),
                (4, 301000, 760000, 690000, 4),
                (5, 298500, 305000, 310000, 5);
                """);

            migrationBuilder.Sql("""
                INSERT INTO ProjectEmployees (EmployeeId, ProjectId) VALUES
                (1, 1),
                (4, 1),
                (3, 2),
                (6, 2),
                (1, 3),
                (5, 3),
                (2, 4),
                (4, 4),
                (3, 5),
                (6, 5);
                """);

            migrationBuilder.Sql("""
                INSERT INTO Risks (Id, Description, MitigationPlan, Owner, ProjectId, Severity, Status) VALUES
                (1, 'Security policy exceptions require additional review cycles.', 'Create weekly exception review board with Legal and Security.', 'Lucas Nguyen', 1, 3, 3),
                (2, 'Legacy data mapping could delay finance close validation.', 'Run parallel reconciliation during the next two close cycles.', 'Sofia Patel', 2, 2, 2),
                (3, 'Automation connector vendor has slipped delivery milestones.', 'Prepare fallback integration path and escalate vendor governance.', 'Lucas Nguyen', 4, 4, 1),
                (4, 'Design approvals may compete with launch campaign timelines.', 'Batch stakeholder reviews and lock content before sprint planning.', 'Ava Thompson', 3, 1, 2);
                """);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_ProjectId",
                table: "Budgets",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentId",
                table: "Projects",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_ProjectId",
                table: "Risks",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Budgets");
            migrationBuilder.DropTable(name: "ProjectEmployees");
            migrationBuilder.DropTable(name: "Risks");
            migrationBuilder.DropTable(name: "Employees");
            migrationBuilder.DropTable(name: "Projects");
            migrationBuilder.DropTable(name: "Departments");
        }
    }
}
