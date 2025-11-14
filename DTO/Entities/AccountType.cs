using System;
using System.Collections.Generic;

namespace BankSystem.Entities;

public partial class AccountType
{
    public int AccountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
