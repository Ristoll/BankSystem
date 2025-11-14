using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class BankBranch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public int BranchTypeId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual BranchType BranchType { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
