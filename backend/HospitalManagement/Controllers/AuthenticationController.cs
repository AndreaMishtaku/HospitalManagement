using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;
using Shared.ResponseFeatures;
using Shared.Utility;

namespace HospitalManagement.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;
    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegister)
    {
        var result = await _service.AuthenticationService.CreateUserAsync(userRegister, UserRole.Staff);
        var baseResponse = new BaseResponse
        {
            Result = true,
            Message="Perdoruesi u regjistrua me sukese",
            StatusCode = 200
        };
        return Ok(baseResponse);
            

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
    {
         var tokenDto = await _service.AuthenticationService.ValidateUserAndCreateToken(userLogin);
  
         return Ok(tokenDto);
        
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDto)
    {
        var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}
