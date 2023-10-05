using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class WorkingHoursDTO
{
    public WeekdayEnum Weekday { get; set; }
    public string StartHour { get; set; }

    public string EndHour { get; set; }
}
