using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO;

public class ReceptionReportDTO
{

    public string FullName { get; set; }
    public int TotalReservations { get; set; }

    public int TotalCanceled { get; set; }
}
