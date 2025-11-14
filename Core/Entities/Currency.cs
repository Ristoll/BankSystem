using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public decimal ExchangeRate { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
