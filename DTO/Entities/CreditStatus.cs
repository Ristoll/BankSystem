using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class CreditStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
