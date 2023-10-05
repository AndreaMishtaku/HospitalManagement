using Microsoft.AspNetCore.Identity;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IUserService
{
    Task<IdentityResult> AddUser(AddUserDTO addUserDto, int userId);
    Task<UserListDTO> GetUserById(int userId);
    Task<PagedListResponse<IEnumerable<UserDetailsDTO>>> GetAllUsersWithPagination(LookupDTO filter);

    Task<IEnumerable<UserDetailsDTO>> GetAllUsers();

}
