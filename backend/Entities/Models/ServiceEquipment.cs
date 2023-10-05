using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class ServiceEquipment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ServiceId")]
    public int ServiceId { get; set; }
    public Services Service { get; set; }


    [ForeignKey("EquipmentId")]
    public int EquipmentId {get; set; }

    public Equipment Equipment { get; set; }
}
