using System;
using System.Collections.Generic;

namespace DTO;

public partial class EmployeeDto
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public int RoleId { get; set; }

    public int BranchId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<AccountDto> Accounts { get; set; } = new List<AccountDto>();

    public virtual BankBranchDto Branch { get; set; } = null!;

    public virtual ICollection<CreditDto> Credits { get; set; } = new List<CreditDto>();

    public virtual EmployeeRoleDto Role { get; set; } = null!;

    public virtual ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}
