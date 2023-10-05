using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class ServiceStaff
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("DoctorId")]
    public int StaffId { get; set; }
    public User Staff { get; set; }

    [ForeignKey("ServiceId")]
    public int ServiceId { get; set; }
    public Services Service { get; set; }

    public List<Reservation>? Reservations { get; set; }
}
