using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class Menu
{

    public int Id { get; set; }
    public string Title { get; set; }

    public string Path { get; set; }

    public string Icon { get; set; }

    public int? ParentId {get; set; }

    public int Order {get;set;}


    [ForeignKey("RoleId")]
    public int RoleId { get; set; }
    public ApplicationRole Role { get; set; }
}
