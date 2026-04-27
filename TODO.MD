请帮我开发一个专业级 SaaS 全栈 Web 应用，项目名称使用英文：

PortfolioHub

技术栈使用：

- Blazor (.NET 8 或 .NET 10)
- C#
- Entity Framework Core
- SQL Server 或 SQLite
- Bootstrap / modern CSS

整体风格要像真实企业级软件，而不是练习项目。参考风格：

- OnePlan
- Monday.com
- Microsoft internal dashboard
- Asana enterprise version

==================================================
项目定位（重要）
==================================================

PortfolioHub 是一个 Strategic Portfolio Management 平台，用于企业管理：

- Projects（项目）
- Budgets（预算）
- Resources（资源）
- Risks（风险）
- Priorities（优先级）
- Executive Reporting（高层报表）

让整个系统看起来像真实公司内部正在使用的 SaaS 产品。

==================================================
整体 UI Layout
==================================================

左侧 Sidebar 导航：

- Dashboard
- Projects
- Resources
- Financials
- Risks
- Reports
- Settings

顶部 Navbar：

- Logo: PortfolioHub
- Search bar
- Notification icon
- User avatar
- Settings menu

整体风格：

- 专业简洁
- 白色主内容区
- 深蓝侧边栏
- 圆角卡片
- 阴影卡片
- 企业 SaaS 风格
- 数据可视化强
- 高级感

==================================================
核心功能模块
==================================================

1. Dashboard 页面

显示 KPI 卡片：

- Active Projects
- Projects At Risk
- Budget Utilization %
- Resource Capacity %
- On-Time Delivery %

图表：

- Project Status Pie Chart
- Monthly Budget Bar Chart
- Resource Allocation Chart

Recent Projects Table：

Project Name | Owner | Status | Progress | Budget | Risk Level

==================================================

2. Projects 页面（重点）

实现完整 CRUD：

- Add Project
- Edit Project
- Delete Project
- Search
- Filter by status
- Sort by priority

字段：

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

Status:

- On Track
- At Risk
- Delayed
- Completed

==================================================

3. Resources 页面

表格：

Name | Role | Capacity % | Assigned Projects | Availability

功能：

- 分配员工到项目
- 查看 overload 的员工
- 查看可分配员工

Role:

- Developer
- Designer
- PM
- Analyst

==================================================

4. Financials 页面

KPI：

- Planned Budget
- Actual Spend
- Remaining Budget
- Variance %

表格：

Project | Planned | Actual | Variance | Forecast

==================================================

5. Risks 页面

字段：

- Project
- Risk Description
- Severity
- Owner
- Mitigation Plan
- Status

Severity:

- Low
- Medium
- High
- Critical

==================================================
技术要求
==================================================

请使用：

- Blazor Server 或 Blazor Web App
- Entity Framework Core
- Dependency Injection
- Repository Pattern
- Form Validation
- Reusable Components
- Clean Architecture
- Responsive Design

==================================================
高级加分功能（可选）
==================================================

- Dark Mode Toggle
- CSV Export
- Role-based Login
- Notification Center
- Audit Logs
- Global Search

==================================================
数据库模型
==================================================

建立以下 Entity：

- Project
- Employee
- Budget
- Risk
- Department

关系：

- One Project has many Risks
- One Project has many Resources
- One Employee can join many Projects

==================================================
假数据（真实感）
==================================================

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

==================================================
最终目标（最重要）
==================================================

让这个项目看起来像真实企业正在使用的产品级 SaaS 系统。

适合放到软件工程师简历中，申请 OnePlan Solutions / enterprise SaaS / full stack developer 岗位。

请直接生成：

- 完整页面
- Models
- DbContext
- Services
- UI Components
- Sample Seed Data
- Styling
- 可运行代码结构

代码中的项目名、页面名、字段名全部使用英文。