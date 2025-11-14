using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
