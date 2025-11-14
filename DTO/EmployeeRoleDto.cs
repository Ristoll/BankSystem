using System;
using System.Collections.Generic;

namespace DTO;

public partial class EmployeeRoleDto
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
}
