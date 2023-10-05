using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;

namespace HospitalManagement.Controllers;

[Route("api/check-ins")]
[Authorize]
[ApiController]
public class CheckInController : ControllerBase
{
    private readonly IServiceManager _service;

    public CheckInController(IServiceManager service)
    {
        _service = service;
    }



    [HttpPost]
    public async Task<IActionResult> CreateCheckIn([FromBody] CheckInDTO checkInDTO)
    {
        var result = await _service.CheckInService.CreateCheckIn(checkInDTO);
        return Ok(result);
    }



}
