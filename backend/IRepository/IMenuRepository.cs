using Entities.Models;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IMenuRepository
{
   Task<IEnumerable<ApplicationMenuDTO>> GetMenuByUserRole(string userRole);
}