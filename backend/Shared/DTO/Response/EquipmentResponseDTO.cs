using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response;

public class EquipmentResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateTime? ProducedAt { get; set; }
}
