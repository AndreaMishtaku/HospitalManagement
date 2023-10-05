using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class CheckInDTO
{
    public int ReservationId { get; set; }
    public string CheckInStartTime { get; set; }
}
