using System;
using System.Collections.Generic;

namespace DTO;

public partial class AccountDto
{
    public int AccountId { get; set; }

    public int ClientId { get; set; }

    public int AccountTypeId { get; set; }

    public int CurrencyId { get; set; }

    public int? BranchId { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Balance { get; set; }

    public DateOnly OpenDate { get; set; }

    public DateOnly? CloseDate { get; set; }
}
