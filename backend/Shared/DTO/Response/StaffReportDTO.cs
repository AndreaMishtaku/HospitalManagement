using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

public class StaffReportDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public int SuccessfulReservations { get; set; }

    public int PostponedReservations { get; set; }

    public int UnCompletedReservations { get; set; }

    public int Delays { get; set; }

}
