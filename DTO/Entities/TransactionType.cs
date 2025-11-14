using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
