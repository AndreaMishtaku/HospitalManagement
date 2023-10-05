using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class Client
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string? Gender { get; set; }    

    public string IdentityNumber { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime? RegisteredAt { get; set; }
}
