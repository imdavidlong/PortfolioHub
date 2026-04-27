# PortfolioHub

PortfolioHub is an enterprise-style Strategic Portfolio Management SaaS application built with Blazor, C#, Entity Framework Core, SQLite, and Bootstrap/CSS. It is designed to look and behave like a realistic internal portfolio management platform used by product, finance, security, and operations leadership teams.

The application focuses on the core workflows of enterprise portfolio governance:

- Project portfolio tracking
- Budget and forecast visibility
- Resource capacity management
- Risk ownership and mitigation
- Executive reporting
- SaaS-style navigation and dashboarding

This project was built as a full-stack portfolio project for enterprise SaaS / full-stack developer roles.

## Screenshots

### Dashboard

![PortfolioHub Dashboard](screenshot/Dashboard.png)

### Projects

![PortfolioHub Projects](screenshot/Projects.png)

### Add Project

![PortfolioHub Add Project](screenshot/AddProject.png)

## What PortfolioHub Does

PortfolioHub gives an executive or portfolio manager a centralized view of enterprise initiatives. The application models projects, departments, employees, budgets, risks, and project assignments, then surfaces those records through dashboard pages, CRUD workflows, and executive reporting screens.

The goal is not to be a small tutorial app. The structure intentionally resembles a real business application:

- Domain models are separated from UI pages.
- Data access is encapsulated behind repositories.
- Business access is exposed through services.
- EF Core migrations define the database schema.
- Seed data creates a realistic portfolio immediately after startup.
- UI pages use a consistent enterprise SaaS layout.

## Tech Stack

- **Blazor Web App / Blazor Server interactivity**
- **.NET 10**
- **C#**
- **Entity Framework Core**
- **SQLite**
- **ASP.NET Core Identity scaffold**
- **Bootstrap**
- **Modern custom CSS**
- **Repository pattern**
- **Dependency injection**
- **Data annotations validation**

## Current Feature Set

### Dashboard

The dashboard gives a high-level portfolio health view.

It includes:

- Active Projects
- Projects At Risk
- Budget Utilization
- Resource Capacity
- On-Time Delivery
- Project status distribution
- Monthly budget visualization
- Recent projects table

The dashboard is data-driven. It reads projects, budgets, risks, and departments from the EF Core-backed service layer rather than displaying static placeholder values.

### Projects

The Projects module is the most complete CRUD workflow in the application.

Features:

- Project list
- Add project
- Edit project
- Delete project
- Search by project, owner, or department
- Filter by status
- Sort by priority or name
- Form validation
- Budget creation/update tied to project save
- Progress tracking
- Status and priority badges

Project fields:

- Project Name
- Owner
- Department
- Start Date
- End Date
- Status
- Priority
- Budget
- Progress %
- Description

Supported project statuses:

- On Track
- At Risk
- Delayed
- Completed

Supported priorities:

- Low
- Medium
- High
- Critical

### Resources

The Resources module manages employee capacity and project assignments.

Features:

- Employee list
- Role filter
- Availability filter
- Capacity visualization
- Overload detection
- Assign employee to project
- Remove project assignment
- Assigned projects displayed as pills

Supported roles:

- Developer
- Designer
- PM
- Analyst

Availability logic:

- **Available**: under 80% capacity
- **Allocated**: 80-89% capacity
- **Overloaded**: 90% or higher capacity

### Financials

The Financials page gives an enterprise reporting view over project budgets.

Features:

- Planned Budget
- Actual Spend
- Remaining Budget
- Variance %
- Budget utilization visual
- Forecast exposure chart
- Project financial table

Financial table columns:

- Project
- Planned
- Actual
- Remaining
- Variance
- Forecast
- Status

Projects forecasted above plan are flagged as **Over Plan**.

### Risks

The Risks module tracks portfolio-level risks with CRUD functionality.

Features:

- Risk list
- Add risk
- Edit risk
- Delete risk
- Search by risk, owner, or project
- Filter by severity
- Filter by status
- Severity badges
- Mitigation ownership

Risk fields:

- Project
- Risk Description
- Severity
- Owner
- Mitigation Plan
- Status

Supported severities:

- Low
- Medium
- High
- Critical

Supported statuses:

- Open
- Monitoring
- Mitigating
- Closed

### Reports

The Reports page is an executive summary view intended for leadership reviews.

It includes:

- Portfolio Health Score
- Active Projects
- Budget Variance
- Overloaded Resources
- Risk Exposure
- Delivery Summary
- Leadership Actions
- Executive Project Report

This page demonstrates how operational data can be converted into a management-facing report.

### Settings

The Settings page provides a product-like administration experience.

It includes:

- Workspace configuration
- Fiscal year settings
- Currency settings
- Governance thresholds
- Notification preferences
- Reporting cadence

Some settings controls are currently UI-level placeholders and are not yet persisted to the database.

## Architecture Overview

PortfolioHub follows a layered architecture:

```text
Blazor Razor Pages
    -> Services
        -> Repositories
            -> ApplicationDbContext
                -> SQLite
```

### UI Layer

The UI lives under:

```text
PortfolioHub/Components/Pages
PortfolioHub/Components/Layout
PortfolioHub/wwwroot/app.css
```

Important pages:

```text
Home.razor          Dashboard
Projects.razor      Project CRUD
Resources.razor     Resource assignment and capacity
Financials.razor    Budget reporting
Risks.razor         Risk CRUD
Reports.razor       Executive reporting
Settings.razor      Admin/settings experience
```

The layout uses:

- Left navigation sidebar
- Top search/action bar
- Shared enterprise visual styling
- Responsive cards and tables

### Service Layer

Services live under:

```text
PortfolioHub/Services
```

Current services:

```text
IProjectService / ProjectService
IResourceService / ResourceService
IRiskService / RiskService
```

Responsibilities:

- Provide page-friendly operations
- Apply search/filter/sort behavior
- Keep Razor components from directly querying EF Core
- Act as a boundary between UI and persistence

### Repository Layer

Repositories live under:

```text
PortfolioHub/Repositories
```

Current repositories:

```text
IProjectRepository / ProjectRepository
IResourceRepository / ResourceRepository
IRiskRepository / RiskRepository
```

Responsibilities:

- Encapsulate EF Core queries
- Load related entities using `Include`
- Perform create/update/delete operations
- Keep database details out of UI components

### Data Layer

The EF Core context is:

```text
PortfolioHub/Data/ApplicationDbContext.cs
```

It extends Identity's `IdentityDbContext<ApplicationUser>`, which means the application already has a foundation for authentication and account management.

The app uses SQLite through:

```json
"DefaultConnection": "DataSource=Data/app.db;Cache=Shared"
```

The local database file is intentionally ignored by git. The schema and sample data are created from migrations when the app starts.

## Domain Model

The main domain entities are:

```text
Project
Employee
Department
Budget
Risk
ProjectEmployee
```

### Relationships

```text
Department 1 -> many Projects
Department 1 -> many Employees
Project 1 -> 1 Budget
Project 1 -> many Risks
Project many -> many Employees through ProjectEmployee
Employee many -> many Projects through ProjectEmployee
```

### Project

Represents an enterprise initiative.

Important fields:

```text
Name
Owner
DepartmentId
StartDate
EndDate
Status
Priority
BudgetAmount
ProgressPercent
Description
```

### Employee

Represents a resource who can be assigned to projects.

Important fields:

```text
Name
Role
CapacityPercent
DepartmentId
```

### Budget

Represents planned and actual project financials.

Important fields:

```text
Planned
Actual
Forecast
ProjectId
```

### Risk

Represents delivery, financial, operational, security, or governance risk.

Important fields:

```text
ProjectId
Description
Severity
Owner
MitigationPlan
Status
```

## Database and Seed Data

PortfolioHub uses EF Core migrations.

Migration files live under:

```text
PortfolioHub/Data/Migrations
```

The portfolio domain migration creates:

```text
Departments
Employees
Projects
Budgets
Risks
ProjectEmployees
```

Seed data includes realistic enterprise examples:

Departments:

- IT
- Finance
- Product
- Security
- Operations

Projects:

- AI Security Rollout
- ERP Migration
- Customer Portal Redesign
- SOC Automation Upgrade
- HR Platform Modernization

Employees:

- Maya Chen
- Ethan Brooks
- Sofia Patel
- Lucas Nguyen
- Ava Thompson
- Noah Rivera

## Validation

The application uses data annotations for form validation.

Examples:

- Project name is required.
- Owner is required.
- Department must be selected.
- Budget must be non-negative.
- Progress must be between 0 and 100.
- End date must be after start date.
- Risk description is required.
- Mitigation plan is required.

For project forms, `ProjectFormModel` implements custom validation for date consistency.

## How To Run Locally

### Prerequisites

Install:

- .NET 10 SDK

No separate SQL Server installation is required because the app uses SQLite.

### Run

From the repository root:

```bash
dotnet run --project PortfolioHub/PortfolioHub.csproj
```

Open:

```text
http://localhost:5170
```

If port `5170` is already in use, stop the existing process:

```bash
killall dotnet
```

Then run the app again.

### Build

```bash
dotnet build PortfolioHub.sln
```

### Database Creation

On startup, the application runs:

```csharp
dbContext.Database.Migrate();
```

This applies migrations automatically and creates the local SQLite database if it does not already exist.

The database file is ignored by git:

```text
*.db
*.db-shm
*.db-wal
```

## Repository Structure

```text
PortfolioHub/
├── PortfolioHub.sln
├── PortfolioHub/
│   ├── Components/
│   │   ├── Layout/
│   │   └── Pages/
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── Migrations/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── wwwroot/
│   │   └── app.css
│   └── Program.cs
├── PortfolioHub.Client/
├── screenshot/
└── README.md
```

## Engineering Decisions

### Why Blazor?

Blazor allows the app to use C# across the UI and backend, which fits well for enterprise .NET environments. It also makes it natural to integrate EF Core, Identity, dependency injection, and strongly typed models.

### Why SQLite?

SQLite keeps the project easy to run locally and easy to review from GitHub. There is no external database setup, but EF Core keeps the design portable enough to move to SQL Server later.

### Why Repository and Service Layers?

The project avoids placing EF Core queries directly inside Razor pages. This makes the codebase closer to a production application:

- UI handles rendering and interactions.
- Services provide business operations.
- Repositories handle persistence.
- DbContext remains the EF Core boundary.

### Why Keep Identity?

The app was created with ASP.NET Core Identity scaffolded in. Even though the main portfolio pages are not currently locked behind role-based authorization, the authentication foundation exists and can support future role-based workflows.

### Why Ignore the SQLite DB?

The database is generated from migrations and seed data. Committing local `.db` files can accidentally include local accounts, test data, and environment-specific state. Migrations are the source of truth.

## Current Limitations

This is a portfolio-grade MVP, not a production SaaS yet.

Known limitations:

- Global top search is currently visual only.
- Financials `Export CSV` button is a placeholder.
- Reports `Download PDF` button is a placeholder.
- Settings controls are not persisted.
- Role-based authorization is scaffolded but not fully configured.
- Audit logs are not implemented yet.
- Dark mode is not implemented yet.
- Automated tests are not yet included.

## Recommended Next Improvements

High-value next steps:

1. Add a real global search service.
2. Implement CSV export for Projects and Financials.
3. Persist Settings values in the database.
4. Add audit logging for create/update/delete actions.
5. Add role-based access control.
6. Add automated tests for services and repositories.
7. Add SQL Server configuration option.
8. Add CI build validation with GitHub Actions.
9. Add dark mode.
10. Add charts using Chart.js or another charting library.

## Suggested Resume Description

**PortfolioHub** - Enterprise Strategic Portfolio Management SaaS built with Blazor, C#, EF Core, SQLite, and Bootstrap. Implemented portfolio dashboarding, project CRUD, resource capacity management, budget reporting, risk management, executive reporting, repository/service architecture, EF Core migrations, seed data, and responsive SaaS-style UI.

## Suggested Interview Talking Points

- Built a layered Blazor application using services and repositories.
- Modeled portfolio management entities and relationships with EF Core.
- Implemented many-to-many project/resource assignments.
- Used migrations and seed data to create a repeatable local demo.
- Added interactive CRUD workflows with form validation.
- Designed a SaaS-style UI with dashboard metrics, badges, tables, and responsive layout.
- Preserved future extensibility for Identity, role-based access, audit logs, CSV export, and reporting.

