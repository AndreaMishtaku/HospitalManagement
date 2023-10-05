using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
    public MenuRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<ApplicationMenuDTO>> GetMenuByUserRole(string userRole)
    {
        var applicationMenu = await (from m in RepositoryContext.Menu
                               where m.Role.Name == userRole && m.ParentId==null
                               select new ApplicationMenuDTO
                               {
                                   Id = m.Id,
                                   Title = m.Title,
                                   Path = m.Path,
                                   Icon = m.Icon,
                                   Children = null
                               }).ToListAsync();

        foreach (var a in applicationMenu)
        {
            var childrenMenu = await (from m in RepositoryContext.Menu
                                      where m.ParentId==a.Id
                                      select new ApplicationMenuDTO
                                      {
                                          Id=m.Id,
                                          Title = m.Title,
                                          Path = m.Path,
                                          Icon = m.Icon,
                                          Children = new List<ApplicationMenuDTO>()
                                      }).ToListAsync();

            a.Children = childrenMenu;
            
        }

        return applicationMenu;
    }
}
