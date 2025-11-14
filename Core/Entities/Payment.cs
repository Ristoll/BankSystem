using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int CreditId { get; set; }

    public int PaymentTypeId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Credit Credit { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;
}
