# PortfolioHub 中文学习版 README

这份文档是写给 **刚开始学习 .NET / Blazor / 全栈开发的程序员小白** 的。

英文版 `README.md` 更像是给招聘官、面试官看的项目介绍；而这份中文 README 的目标是帮助你真正理解：

- 这个项目是怎么跑起来的
- Blazor 在这里承担了什么角色
- 前端页面是怎么写的
- 后端逻辑在哪里
- 数据库是怎么连接的
- CRUD 是怎么一步一步完成的
- Dashboard、Projects、Resources、Financials、Risks 等页面的数据从哪里来

你可以把它当成一份“项目源码学习导览”。

---

## 1. 项目是什么？

项目名称：

```text
PortfolioHub
```

它是一个企业级风格的 Strategic Portfolio Management 系统，也就是企业用来管理项目组合的平台。

它管理的内容包括：

- Projects：项目
- Resources：员工资源
- Financials：预算和花费
- Risks：风险
- Reports：高层报告
- Settings：系统设置

这个项目不是一个简单的 `Hello World`，而是模拟真实 SaaS 系统的结构。

---

## 2. 用到了哪些技术？

这个项目使用：

```text
Blazor
C#
.NET 10
Entity Framework Core
SQLite
ASP.NET Core Identity
Bootstrap
自定义 CSS
Repository Pattern
Service Layer
Dependency Injection
```

如果你是小白，可以先这样理解：

| 技术 | 作用 |
|---|---|
| Blazor | 用 C# 写网页 UI |
| C# | 项目的主要编程语言 |
| .NET | 运行 C# Web 应用的平台 |
| EF Core | 用 C# 操作数据库的工具 |
| SQLite | 本地轻量数据库 |
| Bootstrap | 基础 UI 样式库 |
| CSS | 自定义页面样式 |
| Repository | 专门负责数据库操作 |
| Service | 专门负责业务逻辑 |
| DI | 自动把需要的对象传给页面或服务 |

---

## 3. 项目目录怎么看？

项目根目录大概长这样：

```text
PortfolioHub/
├── PortfolioHub.sln
├── PortfolioHub/
│   ├── Components/
│   │   ├── Layout/
│   │   └── Pages/
│   ├── Data/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── wwwroot/
│   └── Program.cs
├── PortfolioHub.Client/
├── screenshot/
├── README.md
└── README.zh-CN.md
```

重点看这些目录：

### `Components/Pages`

这里放页面。

例如：

```text
Home.razor          Dashboard 页面
Projects.razor      Projects 页面
Resources.razor     Resources 页面
Financials.razor    Financials 页面
Risks.razor         Risks 页面
Reports.razor       Reports 页面
Settings.razor      Settings 页面
```

你在浏览器访问的页面，大多来自这里。

### `Components/Layout`

这里放整体布局。

例如：

```text
MainLayout.razor
NavMenu.razor
```

`MainLayout.razor` 控制页面整体结构，比如顶部导航栏、主体区域。

`NavMenu.razor` 控制左侧菜单，比如 Dashboard、Projects、Resources。

### `Models`

这里放数据库模型，也叫实体类。

例如：

```text
Project.cs
Employee.cs
Budget.cs
Risk.cs
Department.cs
ProjectEmployee.cs
```

你可以把 Model 理解成“数据库表在 C# 代码里的样子”。

例如 `Project.cs` 就代表数据库里的 `Projects` 表。

### `Data`

这里放数据库上下文和迁移文件。

关键文件：

```text
ApplicationDbContext.cs
Data/Migrations/
```

`ApplicationDbContext` 是 EF Core 操作数据库的入口。

### `Repositories`

这里放数据库操作代码。

例如：

```text
ProjectRepository.cs
ResourceRepository.cs
RiskRepository.cs
```

Repository 负责：

- 查询数据库
- 新增数据
- 修改数据
- 删除数据

### `Services`

这里放业务逻辑。

例如：

```text
ProjectService.cs
ResourceService.cs
RiskService.cs
```

Service 会调用 Repository，然后给页面提供更好用的方法。

---

## 4. Blazor 是什么？

传统 Web 开发通常是：

```text
HTML + CSS + JavaScript 写前端
C# / Java / Node 写后端
```

Blazor 的特别之处是：

```text
可以用 C# 写前端交互
```

在这个项目里，页面文件是 `.razor`。

例如：

```text
Projects.razor
```

一个 `.razor` 文件通常包含两部分：

### 页面 HTML

类似这样：

```razor
<h1>Projects</h1>

<button class="btn btn-primary" @onclick="StartCreate">
    Add Project
</button>
```

### C# 代码

写在 `@code { }` 里面：

```razor
@code {
    private void StartCreate()
    {
        IsFormOpen = true;
    }
}
```

所以 Blazor 页面可以同时写：

- HTML
- C#
- 表单
- 按钮点击事件
- 数据循环
- 条件显示

---

## 5. Blazor 页面如何对应 URL？

看页面顶部：

```razor
@page "/projects"
```

这代表：

```text
浏览器访问 /projects 时，显示这个页面
```

例如：

| 文件 | URL |
|---|---|
| `Home.razor` | `/` |
| `Projects.razor` | `/projects` |
| `Resources.razor` | `/resources` |
| `Financials.razor` | `/financials` |
| `Risks.razor` | `/risks` |
| `Reports.razor` | `/reports` |
| `Settings.razor` | `/settings` |

---

## 6. 页面如何使用后端服务？

在 Blazor 页面里可以注入服务。

例如 `Projects.razor` 里面有：

```razor
@inject IProjectService ProjectService
```

意思是：

```text
请 Blazor 帮我拿到一个 ProjectService，我要在页面里使用它。
```

然后页面可以调用：

```csharp
ProjectList = await ProjectService.GetProjectsAsync();
```

这就是页面从后端拿数据的方式。

---

## 7. 什么是 Dependency Injection？

Dependency Injection 简称 DI，中文常叫“依赖注入”。

你可以先简单理解成：

```text
我需要什么对象，框架自动帮我准备好。
```

在 `Program.cs` 里面有这样的代码：

```csharp
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
```

意思是：

```text
如果有人需要 IProjectService，就给他 ProjectService。
如果有人需要 IProjectRepository，就给他 ProjectRepository。
```

所以在页面里写：

```razor
@inject IProjectService ProjectService
```

Blazor 就知道要创建 `ProjectService` 给页面用。

---

## 8. 数据从数据库到页面的完整流程

以 Projects 页面为例。

数据流是：

```text
Projects.razor
    -> ProjectService
        -> ProjectRepository
            -> ApplicationDbContext
                -> SQLite 数据库
```

一步一步看：

### 第一步：页面调用 Service

在 `Projects.razor`：

```csharp
ProjectList = await ProjectService.GetProjectsAsync();
```

### 第二步：Service 调用 Repository

在 `ProjectService.cs`：

```csharp
var projects = await projectRepository.GetAllAsync();
```

### 第三步：Repository 查询数据库

在 `ProjectRepository.cs`：

```csharp
return await dbContext.Projects
    .AsNoTracking()
    .Include(project => project.Department)
    .Include(project => project.Budget)
    .Include(project => project.Risks)
    .ToListAsync();
```

### 第四步：EF Core 转成 C# 对象

EF Core 从 SQLite 读取数据，然后转成：

```text
List<Project>
```

### 第五步：页面循环显示

在 Razor 页面：

```razor
@foreach (var project in FilteredProjects)
{
    <tr>
        <td>@project.Name</td>
        <td>@project.Owner</td>
        <td>@project.Status</td>
    </tr>
}
```

最终你就在浏览器里看到项目表格。

---

## 9. EF Core 是怎么连接数据库的？

数据库连接字符串在：

```text
PortfolioHub/appsettings.json
```

内容类似：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=Data/app.db;Cache=Shared"
  }
}
```

这代表数据库文件在：

```text
PortfolioHub/Data/app.db
```

不过这个 `.db` 文件不会提交到 GitHub，因为它是本地生成的。

---

## 10. ApplicationDbContext 是什么？

文件：

```text
PortfolioHub/Data/ApplicationDbContext.cs
```

它里面有：

```csharp
public DbSet<Project> Projects => Set<Project>();
public DbSet<Employee> Employees => Set<Employee>();
public DbSet<Department> Departments => Set<Department>();
public DbSet<Budget> Budgets => Set<Budget>();
public DbSet<Risk> Risks => Set<Risk>();
public DbSet<ProjectEmployee> ProjectEmployees => Set<ProjectEmployee>();
```

你可以理解成：

```text
DbSet<Project> Projects 就是数据库里的 Projects 表。
DbSet<Employee> Employees 就是数据库里的 Employees 表。
```

如果你想查项目，就用：

```csharp
dbContext.Projects
```

如果你想查员工，就用：

```csharp
dbContext.Employees
```

---

## 11. 数据表之间的关系

这个项目有这些关系：

```text
Department 1 -> 多个 Project
Department 1 -> 多个 Employee
Project 1 -> 1 个 Budget
Project 1 -> 多个 Risk
Project 多个 -> 多个 Employee
```

### Project 和 Budget

一个项目有一个预算。

```csharp
builder.Entity<Project>()
    .HasOne(project => project.Budget)
    .WithOne(budget => budget.Project)
    .HasForeignKey<Budget>(budget => budget.ProjectId);
```

意思是：

```text
Project 和 Budget 是一对一关系。
Budget 通过 ProjectId 找到对应项目。
```

### Project 和 Employee

一个项目可以有多个员工，一个员工也可以参与多个项目。

这叫多对多关系。

中间表是：

```text
ProjectEmployee
```

代码里：

```csharp
builder.Entity<ProjectEmployee>()
    .HasKey(projectEmployee => new
    {
        projectEmployee.ProjectId,
        projectEmployee.EmployeeId
    });
```

这代表 `ProjectEmployee` 用两个字段一起作为主键：

```text
ProjectId + EmployeeId
```

---

## 12. 数据库是怎么自动创建的？

在 `Program.cs` 里有：

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
```

意思是：

```text
应用启动时，自动检查数据库。
如果数据库没有表，就根据 migrations 创建表。
```

所以你第一次运行：

```bash
dotnet run --project PortfolioHub/PortfolioHub.csproj
```

它会自动创建 SQLite 数据库，并插入 seed data。

---

## 13. 什么是 Migration？

Migration 是 EF Core 用来记录数据库结构变化的文件。

目录：

```text
PortfolioHub/Data/Migrations
```

例如：

```text
20260426000100_AddPortfolioDomain.cs
```

这个 migration 负责创建：

```text
Departments
Employees
Projects
Budgets
Risks
ProjectEmployees
```

它还插入了一些假数据，比如：

- AI Security Rollout
- ERP Migration
- Customer Portal Redesign
- SOC Automation Upgrade
- HR Platform Modernization

---

## 14. Dashboard 是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Home.razor
```

页面顶部：

```razor
@page "/"
@inject IProjectService ProjectService
```

说明：

- `/` 是首页
- 首页需要使用 `ProjectService`

页面初始化时：

```csharp
protected override async Task OnInitializedAsync()
{
    Projects = await ProjectService.GetProjectsAsync();
}
```

这会从数据库读取项目。

然后 Dashboard 计算 KPI。

例如：

### Active Projects

```csharp
private int ActiveProjects =>
    Projects.Count(project => project.Status != ProjectStatus.Completed);
```

意思是：

```text
只要项目不是 Completed，就算 active project。
```

### Projects At Risk

```csharp
private int ProjectsAtRisk =>
    Projects.Count(project =>
        project.Status is ProjectStatus.AtRisk or ProjectStatus.Delayed);
```

意思是：

```text
状态是 AtRisk 或 Delayed 的项目，就是有风险项目。
```

### Budget Utilization

```csharp
private decimal BudgetUtilization =>
    PlannedBudget == 0 ? 0 : ActualSpend / PlannedBudget * 100;
```

意思是：

```text
预算使用率 = 实际花费 / 计划预算 * 100
```

所以 Dashboard 不是纯静态页面，而是根据数据库里的项目和预算算出来的。

---

## 15. Projects CRUD 是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Projects.razor
```

这个页面最重要，因为它包含完整 CRUD。

CRUD 是：

```text
Create 新增
Read 查询
Update 修改
Delete 删除
```

### 15.1 查询项目

页面初始化：

```csharp
protected override async Task OnInitializedAsync()
{
    Departments = await ProjectService.GetDepartmentsAsync();
    await LoadProjects();
}
```

`LoadProjects`：

```csharp
private async Task LoadProjects()
{
    ProjectList = await ProjectService.GetProjectsAsync(sortByPriority: SortMode != "name");
}
```

这会把数据库项目读出来，放到 `ProjectList`。

### 15.2 搜索、筛选、排序

页面里有一个计算属性：

```csharp
private IEnumerable<Project> FilteredProjects
```

它会基于 `ProjectList` 做：

- 搜索
- 状态筛选
- 排序

例如搜索：

```csharp
query = query.Where(project =>
    project.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
    project.Owner.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
    (project.Department?.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
```

意思是：

```text
只要项目名、负责人、部门里包含搜索关键词，就显示。
```

### 15.3 新增项目

点击按钮：

```razor
<button @onclick="StartCreate">Add Project</button>
```

会调用：

```csharp
private void StartCreate()
{
    FormModel = new ProjectFormModel
    {
        DepartmentId = Departments.FirstOrDefault()?.Id ?? 0,
        BudgetAmount = 250000
    };

    IsFormOpen = true;
}
```

意思是：

```text
创建一个空表单模型，然后打开表单弹窗。
```

### 15.4 表单

Blazor 表单使用：

```razor
<EditForm Model="FormModel" OnValidSubmit="SaveProject">
    <DataAnnotationsValidator />
    <ValidationSummary />
</EditForm>
```

意思是：

```text
这个表单绑定到 FormModel。
如果验证通过，就调用 SaveProject。
```

### 15.5 保存项目

```csharp
private async Task SaveProject()
{
    var project = FormModel.ToProject();

    if (FormModel.Id == 0)
    {
        await ProjectService.CreateProjectAsync(project);
    }
    else
    {
        await ProjectService.UpdateProjectAsync(project);
    }

    IsFormOpen = false;
    await LoadProjects();
}
```

逻辑是：

```text
如果 Id 是 0，说明是新增。
如果 Id 不是 0，说明是编辑。
保存后关闭表单，重新加载项目列表。
```

### 15.6 删除项目

点击 Delete：

```razor
<button @onclick="() => ConfirmDelete(project)">Delete</button>
```

先弹确认框：

```csharp
private void ConfirmDelete(Project project)
{
    ProjectToDelete = project;
}
```

确认删除：

```csharp
private async Task DeleteProject()
{
    await ProjectService.DeleteProjectAsync(ProjectToDelete.Id);
    ProjectToDelete = null;
    await LoadProjects();
}
```

---

## 16. 为什么要有 ProjectFormModel？

文件：

```text
PortfolioHub/Models/ProjectFormModel.cs
```

你可能会问：

```text
已经有 Project.cs 了，为什么还要 ProjectFormModel.cs？
```

原因是：

```text
Project 是数据库实体。
ProjectFormModel 是页面表单专用模型。
```

这样做更清晰：

- 数据库实体负责存数据
- 表单模型负责验证用户输入

例如：

```csharp
[Required]
[StringLength(180)]
public string Name { get; set; } = string.Empty;
```

表示：

```text
项目名称不能为空，最长 180 个字符。
```

还有自定义验证：

```csharp
if (EndDate < StartDate)
{
    yield return new ValidationResult("End date must be after start date.", new[] { nameof(EndDate) });
}
```

意思是：

```text
结束日期不能早于开始日期。
```

---

## 17. Resources 页面是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Resources.razor
```

它使用：

```razor
@inject IResourceService ResourceService
```

页面初始化：

```csharp
protected override async Task OnInitializedAsync()
{
    await LoadResources();
}
```

`LoadResources` 会加载：

```csharp
Employees = await ResourceService.GetEmployeesAsync();
AssignableProjects = await ResourceService.GetAssignableProjectsAsync();
```

也就是：

- 员工列表
- 可以分配的项目列表

### 分配员工到项目

点击 Assign 后，会调用：

```csharp
await ResourceService.AssignEmployeeToProjectAsync(SelectedEmployeeId, SelectedProjectId);
```

Service 再调用 Repository：

```csharp
dbContext.ProjectEmployees.Add(new ProjectEmployee
{
    EmployeeId = employeeId,
    ProjectId = projectId
});
```

这其实就是往中间表 `ProjectEmployees` 里插入一条数据。

---

## 18. Financials 页面是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Financials.razor
```

它也使用：

```razor
@inject IProjectService ProjectService
```

因为预算数据挂在 Project 下面。

页面会计算：

```csharp
private decimal PlannedBudget => Projects.Sum(Planned);
private decimal ActualSpend => Projects.Sum(Actual);
private decimal ForecastBudget => Projects.Sum(Forecast);
private decimal RemainingBudget => PlannedBudget - ActualSpend;
```

意思是：

```text
PlannedBudget = 所有项目计划预算之和
ActualSpend = 所有项目实际花费之和
ForecastBudget = 所有项目预测花费之和
RemainingBudget = 计划预算 - 实际花费
```

所以 Financials 页面本质是：

```text
读取项目和预算数据，然后做汇总计算。
```

---

## 19. Risks CRUD 是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Risks.razor
```

它和 Projects 页面很像，也有：

- 查询
- 搜索
- 筛选
- 新增
- 编辑
- 删除

使用：

```razor
@inject IRiskService RiskService
@inject IProjectService ProjectService
```

为什么还需要 `ProjectService`？

因为新增风险时，需要选择这个风险属于哪个项目。

保存风险：

```csharp
private async Task SaveRisk()
{
    var risk = FormModel.ToRisk();

    if (risk.Id == 0)
    {
        await RiskService.CreateRiskAsync(risk);
    }
    else
    {
        await RiskService.UpdateRiskAsync(risk);
    }

    IsFormOpen = false;
    await LoadRisks();
}
```

逻辑和 Project CRUD 基本一样。

---

## 20. Reports 页面是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Reports.razor
```

它会读取：

```csharp
Projects = await ProjectService.GetProjectsAsync();
Employees = await ResourceService.GetEmployeesAsync();
Risks = await RiskService.GetRisksAsync();
```

也就是从三个模块拿数据：

- Projects
- Resources
- Risks

然后计算高层报告指标。

例如：

```csharp
private int OverloadedResources =>
    Employees.Count(employee => employee.CapacityPercent >= 90);
```

意思是：

```text
容量大于等于 90% 的员工数量。
```

Portfolio Health Score：

```csharp
private decimal HealthScore =>
    Projects.Count == 0 ? 0 :
    Math.Max(0, 100 - (HighRisks * 8) - (OverloadedResources * 5) - (DelayedProjects * 10));
```

这个是一个简单的健康分计算：

```text
初始 100 分。
高风险越多扣分。
资源过载越多扣分。
延期项目越多扣分。
```

---

## 21. Settings 页面是怎么实现的？

文件：

```text
PortfolioHub/Components/Pages/Settings.razor
```

这个页面目前主要是 UI 展示，不会写数据库。

它包括：

- Workspace Name
- Fiscal Year Start
- Default Currency
- Governance Thresholds
- Notifications
- Reporting Cadence

你可以把它理解成：

```text
产品级界面占位，为后续持久化设置做准备。
```

---

## 22. CSS 样式在哪里？

全局样式文件：

```text
PortfolioHub/wwwroot/app.css
```

布局样式：

```text
PortfolioHub/Components/Layout/MainLayout.razor.css
PortfolioHub/Components/Layout/NavMenu.razor.css
```

### `app.css`

控制：

- KPI 卡片
- 表格
- badge
- progress bar
- form
- modal overlay
- dashboard layout
- financial chart
- settings card

### `MainLayout.razor.css`

控制：

- 页面整体背景
- 顶部搜索栏
- 顶部按钮
- 用户头像

### `NavMenu.razor.css`

控制：

- 左侧 sidebar
- PortfolioHub logo
- 导航链接
- active 状态

---

## 23. 前端和后端在 Blazor 里怎么区分？

在传统项目里，前端和后端可能是两个项目：

```text
React 前端
ASP.NET Core 后端 API
```

但这个项目是 Blazor Server 风格，所以它更像：

```text
页面和后端运行在同一个 .NET 应用里
```

你可以这样理解：

### 前端部分

```text
.razor 页面
CSS
HTML markup
按钮点击
表单绑定
页面状态
```

### 后端部分

```text
Services
Repositories
DbContext
Models
EF Core
SQLite
```

虽然都在同一个项目里，但职责是分开的。

---

## 24. 一个按钮点击后发生了什么？

以 Add Project 为例。

用户点击：

```razor
<button @onclick="StartCreate">Add Project</button>
```

然后：

```text
1. Blazor 捕捉点击事件
2. 调用 C# 方法 StartCreate
3. StartCreate 创建 FormModel
4. IsFormOpen = true
5. 页面重新渲染
6. 表单弹窗显示出来
```

用户填写表单后点击保存：

```text
1. EditForm 验证 FormModel
2. 验证通过后调用 SaveProject
3. SaveProject 把 FormModel 转成 Project
4. 调用 ProjectService
5. ProjectService 调用 ProjectRepository
6. ProjectRepository 调用 EF Core
7. EF Core 写入 SQLite
8. 页面重新加载项目列表
```

这就是一个完整的前后端流程。

---

## 25. 如何运行项目？

进入项目根目录：

```bash
cd /Users/user/PortfolioHub
```

运行：

```bash
dotnet run --project PortfolioHub/PortfolioHub.csproj
```

打开浏览器：

```text
http://localhost:5170
```

如果端口被占用：

```bash
killall dotnet
```

然后重新运行。

---

## 26. 如何编译项目？

```bash
dotnet build PortfolioHub.sln
```

如果看到：

```text
Build succeeded.
0 Warning(s)
0 Error(s)
```

说明项目编译成功。

---

## 27. 如果你想继续学习，建议按这个顺序看代码

建议不要一上来就看所有文件，会很乱。

推荐顺序：

### 第一步：看布局

```text
Components/Layout/MainLayout.razor
Components/Layout/NavMenu.razor
```

理解页面大结构。

### 第二步：看 Dashboard

```text
Components/Pages/Home.razor
```

理解页面如何读取数据、计算 KPI、展示表格。

### 第三步：看 Project Model

```text
Models/Project.cs
Models/ProjectFormModel.cs
```

理解数据结构和表单验证。

### 第四步：看 Project CRUD

```text
Components/Pages/Projects.razor
Services/ProjectService.cs
Repositories/ProjectRepository.cs
```

这是最重要的一组文件。

### 第五步：看 DbContext

```text
Data/ApplicationDbContext.cs
```

理解数据库表和关系。

### 第六步：看 Resources 和 Risks

```text
Components/Pages/Resources.razor
Components/Pages/Risks.razor
```

理解其他模块如何复用同样的模式。

---

## 28. 这个项目目前还有哪些可以继续做？

项目的核心功能已经完成，但如果你想继续练习，可以做这些：

### 1. Global Search

让顶部搜索框真的能搜索：

- Projects
- Employees
- Risks

### 2. CSV Export

让 Financials 页面导出 CSV。

### 3. Settings 持久化

把 Settings 页面里的配置保存到数据库。

### 4. Dark Mode

加一个深色模式切换。

### 5. Audit Logs

记录谁创建、修改、删除了项目或风险。

### 6. Role-based Login

使用 Identity 做角色权限：

- Admin
- Portfolio Manager
- Executive Viewer

### 7. 自动化测试

给 Service 和 Repository 写单元测试。

---

## 29. 你应该从这个项目学到什么？

如果你认真看完这个项目，应该能学到：

- Blazor 页面怎么写
- Razor 语法怎么用
- 按钮点击事件怎么绑定
- 表单怎么验证
- 页面怎么调用 Service
- Service 怎么调用 Repository
- Repository 怎么用 EF Core 查数据库
- EF Core 怎么定义表和关系
- Migration 是什么
- SQLite 怎么作为本地数据库
- CRUD 怎么完整实现
- SaaS 页面布局怎么组织
- 一个全栈项目如何分层

---

## 30. 最重要的一句话

这个项目的核心思想是：

```text
页面只负责显示和交互。
Service 负责业务逻辑。
Repository 负责数据库操作。
DbContext 负责连接数据库。
Model 负责描述数据结构。
```

只要你理解这句话，就已经理解了这个项目的大部分架构。

