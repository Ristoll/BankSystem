using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class EmployeeRole
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
