using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class WorkingHours
{
    [Key]
    public int Id { get; set; }
    public WeekdayEnum WeekDay { get; set; }

    public TimeSpan StartHour { get; set; }

    public TimeSpan EndHour { get; set; }

    [ForeignKey("ServiceId")]
    public int? ServiceId { get; set; }
    public Services? Service { get; set; }
}
