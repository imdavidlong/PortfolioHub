namespace PortfolioHub.Models;

public enum ProjectStatus
{
    OnTrack = 1,
    AtRisk = 2,
    Delayed = 3,
    Completed = 4
}

public enum ProjectPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum EmployeeRole
{
    Developer = 1,
    Designer = 2,
    PM = 3,
    Analyst = 4
}

public enum RiskSeverity
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum RiskStatus
{
    Open = 1,
    Monitoring = 2,
    Mitigating = 3,
    Closed = 4
}
