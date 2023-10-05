using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class DayInfoDTO
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public string Interval { get; set; }

    public List<ServiceDurationDTO> BusyHours { get; set; }

}
