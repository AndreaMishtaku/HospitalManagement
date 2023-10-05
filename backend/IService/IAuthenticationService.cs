using Microsoft.AspNetCore.Identity;
using Shared.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IAuthenticationService
{
    Task<IdentityResult> CreateUserAsync(UserRegisterDTO userRegister, string role);
    Task<TokenDTO> ValidateUserAndCreateToken(UserLoginDTO userLogin);
    Task<TokenDTO> RefreshToken(TokenDTO tokenDto);
}
