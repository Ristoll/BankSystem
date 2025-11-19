using System;
using System.Collections.Generic;

namespace DTO;

public partial class PaymentDto
{
    public int PaymentId { get; set; }

    public int CreditId { get; set; }

    public int PaymentTypeId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual CreditDto? Credit { get; set; }

    public virtual PaymentTypeDto? PaymentType { get; set; }
}
