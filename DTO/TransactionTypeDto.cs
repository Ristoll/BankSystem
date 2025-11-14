using System;
using System.Collections.Generic;

namespace DTO;

public partial class TransactionTypeDto
{
    public int TransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}
