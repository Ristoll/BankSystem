using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class PaymentType
{
    public int PaymentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
