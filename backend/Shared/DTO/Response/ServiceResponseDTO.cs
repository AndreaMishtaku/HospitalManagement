using Shared.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Response
{
    public class ServiceResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Duration { get; set; }
        public List<WorkingHoursDTO> WorkingHours { get; set; }
        public List<EquipmentResponseDTO> Equipments { get; set; }
    } 
}
