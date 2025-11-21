using System;
using System.Collections.Generic;

namespace DTO;

public partial class ClientDto
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? PassportNumber { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateOnly RegistrationDate { get; set; }
}
