using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Request;

public class ServiceRequestDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string? Duration { get; set; }
    public List<WorkingHoursDTO>? WorkingHours { get; set; }
    public List<int>? EquipmentIds { get; set; }
}
