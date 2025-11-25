using System;
using System.Collections.Generic;

namespace DTO;

public partial class CreditDto
{
    public int CreditId { get; set; }

    public int ClientId { get; set; }
    public string? ClientName { get; set; }

    public int AccountId { get; set; }

    public decimal CreditAmount { get; set; }

    public decimal InterestRate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int StatusId { get; set; }
    public string? StatusName { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
}
