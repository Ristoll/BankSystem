using System;
using System.Collections.Generic;

namespace DTO;

public partial class CreditStatusDto
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CreditDto> Credits { get; set; } = new List<CreditDto>();
}
