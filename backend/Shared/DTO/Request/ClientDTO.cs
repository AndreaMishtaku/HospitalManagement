using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class ClientDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string? Gender { get; set; }

    public string IdentityNumber { get; set; }

    public string? Birthday { get; set; }

    public string? RegisteredAt { get; set; }
}
