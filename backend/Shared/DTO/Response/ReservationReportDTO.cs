using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class ReservationReportDTO
{
    public int TotalWaitingReservations { get; set; }
    public int TotalSuccessfulReservations { get; set; }

    public int TotalCanceledReservations { get; set; }
}
