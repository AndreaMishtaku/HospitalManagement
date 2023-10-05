using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class ReservationRequest1DTO
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string? Gender { get; set; }

    public string IdentityNumber { get; set; }

    [Column(TypeName = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }
    public string? Reason { get; set; }

    [Column(TypeName = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public string StartTime { get; set; }

    public int StaffId { get; set; }
}
