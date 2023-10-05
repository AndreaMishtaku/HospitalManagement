using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class CheckIn
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ReservationId")]
    public int ReservationId { get; set; }
    public Reservation Reservation;

    public TimeSpan StartTime { get; set; }

}
