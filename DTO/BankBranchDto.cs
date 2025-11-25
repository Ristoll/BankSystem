using System;
using System.Collections.Generic;

namespace DTO;

public partial class BankBranchDto
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public string? BranchTypeName { get; set; }

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public int BranchTypeId { get; set; }
}
