using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class ServiceDurationDTO
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
