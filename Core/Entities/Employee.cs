using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int RoleId { get; set; }

    public int BranchId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual BankBranch Branch { get; set; } = null!;

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual EmployeeRole Role { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
