using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IMenuService
{
    Task<IEnumerable<ApplicationMenuDTO>> GetMenuByRole(string userRole);

}
