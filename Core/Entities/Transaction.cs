using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public int TransactionTypeId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual TransactionType TransactionType { get; set; } = null!;
}
