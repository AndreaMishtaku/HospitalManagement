using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class ReservationPostponeDTO
{
    public DateTime Date { get; set; }
    public string StartTime { get; set; }
}
