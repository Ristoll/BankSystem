using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public int ClientId { get; set; }

    public int AccountTypeId { get; set; }

    public int CurrencyId { get; set; }

    public int? BranchId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Balance { get; set; }

    public DateOnly OpenDate { get; set; }

    public DateOnly? CloseDate { get; set; }

    public virtual AccountType AccountType { get; set; } = null!;

    public virtual BankBranch? Branch { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual Currency Currency { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
