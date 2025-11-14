using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class Credit
{
    public int CreditId { get; set; }

    public int ClientId { get; set; }

    public int AccountId { get; set; }

    public decimal CreditAmount { get; set; }

    public decimal InterestRate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int StatusId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual CreditStatus Status { get; set; } = null!;
}
