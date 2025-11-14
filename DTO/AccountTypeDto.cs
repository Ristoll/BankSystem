using System;
using System.Collections.Generic;

namespace DTO;

public partial class AccountTypeDto
{
    public int AccountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AccountDto> Accounts { get; set; } = new List<AccountDto>();
}
