using System;
using System.Collections.Generic;

namespace DTO;

public partial class CurrencyDto
{
    public int CurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public decimal ExchangeRate { get; set; }
}
