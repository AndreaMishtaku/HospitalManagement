using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class ReservationSuccessfulDTO
{
    public bool Result { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public int ReservationId { get; set; }
}
