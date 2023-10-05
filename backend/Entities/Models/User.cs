using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class User:IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Column(TypeName = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? BirthDay { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Specialization { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string? TokenHash { get; set; }


}

