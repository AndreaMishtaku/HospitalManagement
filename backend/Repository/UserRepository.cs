using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Response;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(User user) => Create(user);

    public void DeleteRecord(User user) => Delete(user);

    public async Task<IEnumerable<User>> GetAllRecords()=>  await FindAll().ToListAsync();


    public async Task<User> GetRecordByIdAsync(int id) =>
        await FindByCondition(c => c.Id.Equals(id)).SingleOrDefaultAsync();

    public void UpdateRecord(User user) => Update(user);

    public async Task<PagedListRequest<UserDetailsDTO>> GetUsersWithMetaData(LookupDTO filter)
    {
        var query =  (from u in RepositoryContext.Users
                      join ur in RepositoryContext.UserRoles on u.Id equals ur.UserId
                      join r in RepositoryContext.Roles.Where(r => r.Name != "Manager") on ur.RoleId equals r.Id
                       select new UserDetailsDTO
                       {
                               Id = u.Id,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               BirthDay = u.BirthDay.ToString(),
                               Gender = u.Gender,
                               Email=u.Email,
                               Address = u.Address,
                               PhoneNumber = u.PhoneNumber,
                               Specialization = u.Specialization,
                               Role = r.Name
                       });

        
        if(filter.SearchValue is not null && filter.SearchValue.Length>0)
        {
            var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
            return PagedListRequest<UserDetailsDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
        }
    
        var result=await query.ToListAsync();
        return PagedListRequest<UserDetailsDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
        
    }
}
