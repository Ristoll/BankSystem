using System;
using System.Collections.Generic;

namespace DTO;

public partial class EmployeeDto
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;
    public string? Password { get; set; }
    public string? PasswordHash { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public int BranchId { get; set; }
    public string? BranchName { get; set; }
    public string? Phone { get; set; }

    public string? Email { get; set; }
}
