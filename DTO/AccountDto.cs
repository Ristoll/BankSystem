using System;
using System.Collections.Generic;

namespace DTO;

public partial class AccountDto
{
    public int AccountId { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public int AccountTypeId { get; set; }
    public string? AccountTypeName { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public int? BranchId { get; set; }
    public string? BranchName { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public decimal? Balance { get; set; }
    public DateOnly OpenDate { get; set; }
    public DateOnly? CloseDate { get; set; }
}
