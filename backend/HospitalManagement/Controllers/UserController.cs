using HospitalManagement.Authorization;
using HospitalManagement.Utility;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;
using Shared.RequestFeatures;
using Shared.Utility;

namespace HospitalManagement.Controllers;


[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IServiceManager _service;

    public UserController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUser([FromBody] AddUserDTO addUserDto)
    {
   
        var result = await _service.UserService.AddUser(addUserDto, ClaimsUtility.ReadCurrentUserId(User.Claims));
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _service.UserService.GetAllUsers();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _service.UserService.GetUserById(id);
        return Ok(result);
    }

    [HttpGet("details")]
    [Authorize(Roles = UserRole.Staff)]
    public async Task<IActionResult> GetLoggedUser()
    {
        var result = await _service.UserService.GetUserById(ClaimsUtility.ReadCurrentUserId(User.Claims));
        return Ok(result);
    }



    [HttpPost("get-all")]
    public async Task<IActionResult> GetAllUsersWithPagination([FromBody] LookupDTO filter)
    {
        var result = await _service.UserService.GetAllUsersWithPagination(filter);
        return Ok(result);
    }
}

