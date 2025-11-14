using System;
using System.Collections.Generic;

namespace DTO;

public partial class BranchTypeDto
{
    public int BranchTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BankBranchDto> BankBranches { get; set; } = new List<BankBranchDto>();
}
