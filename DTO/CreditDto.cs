using System;
using System.Collections.Generic;

namespace DTO;

public partial class CreditDto
{
    public int CreditId { get; set; }

    public int ClientId { get; set; }

    public int AccountId { get; set; }

    public decimal CreditAmount { get; set; }

    public decimal InterestRate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int StatusId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual AccountDto Account { get; set; } = null!;

    public virtual ClientDto Client { get; set; } = null!;

    public virtual EmployeeDto? Employee { get; set; }

    public virtual ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();

    public virtual CreditStatusDto Status { get; set; } = null!;
}
