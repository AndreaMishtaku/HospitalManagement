using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class UserDetailsDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDay { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    public string Specialization { get; set; }

    public string Role {get; set; }
}
