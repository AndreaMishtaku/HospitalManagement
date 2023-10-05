using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;
public class Reservation
{
    [Key]
    public int Id { get; set; }
    public string? Reason { get; set; }

    [Column(TypeName = "Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public ReservationStatusEnum Status { get; set; }

    [ForeignKey("ClientId")]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [ForeignKey("EmployeeId")]
    public int? EmployeeId { get; set; }
    public User? Employee { get; set; }
    
    
    [ForeignKey("ServiceDoctorId")]
    public int? ServiceDoctorId { get; set; }
    public ServiceStaff? ServiceDoctor { get; set; }

    public DateTime? DateCreated { get; set; }
    public DateTime? DateCanceled { get; set; }

    public int? CanceledByStaffId { get; set; }
    [NotMapped]
    public User? CanceledByStaff { get; set; }

    public int? CanceledByClientId { get; set; }
    [NotMapped]
    public Client? CanceledByClient { get; set; }

    public int? PostponedById { get; set; }
    [NotMapped]
    public User? PostponedBy { get; set; }

    public CheckIn? CheckIn { get; set; }
}
