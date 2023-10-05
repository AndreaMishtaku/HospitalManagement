using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class StaffResponseDTO
{
    public int Id { get; set; }
    public string DoctorFirstName { get; set; }

    public string DoctorLastName { get; set; }

    public string StaffSpecialization { get; set; }

    public int ServiceId { get; set; }
    public string ServiceName { get; set; }

}
