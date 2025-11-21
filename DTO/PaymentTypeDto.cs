using System;
using System.Collections.Generic;

namespace DTO;

public partial class PaymentTypeDto
{
    public int PaymentTypeId { get; set; }

    public string Name { get; set; } = null!;
}
