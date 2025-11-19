using System;
using System.Collections.Generic;

namespace DTO;

public partial class BankBranchDto
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public int BranchTypeId { get; set; }

    public virtual ICollection<AccountDto> Accounts { get; set; } = new List<AccountDto>();

    public virtual BranchTypeDto? BranchType { get; set; }

    public virtual ICollection<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
}
