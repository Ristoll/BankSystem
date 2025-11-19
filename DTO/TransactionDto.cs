using System;
using System.Collections.Generic;

namespace DTO;

public partial class TransactionDto
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public int TransactionTypeId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public int? EmployeeId { get; set; }

    public virtual AccountDto? Account { get; set; }

    public virtual EmployeeDto? Employee { get; set; }

    public virtual TransactionTypeDto? TransactionType { get; set; }
}
