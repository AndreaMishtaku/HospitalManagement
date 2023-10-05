using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class UserUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Specialization { get; set; }
}
