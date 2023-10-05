using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class Services
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public TimeSpan Duration { get; set; }

    public List <WorkingHours>? WorkingHours { get; set; }


    public List<ServiceStaff>? ServiceDoctor { get; set; }

    public List<ServiceEquipment>? ServiceEquipments { get; set; }
}

