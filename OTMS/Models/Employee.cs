using System.ComponentModel.DataAnnotations;

namespace OTMS.Models;

public sealed class Employee
{
    [Key]
    public int UserId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string ContactNo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public EmployeeRole Role { get; set; }
}

public enum EmployeeRole
{
    Employee,
    SystemAdmin,
    OperationalAdmin
}