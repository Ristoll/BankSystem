using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class BranchType
{
    public int BranchTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BankBranch> BankBranches { get; set; } = new List<BankBranch>();
}
