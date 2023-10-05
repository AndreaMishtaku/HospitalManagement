using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;

namespace HospitalManagement.Controllers;


[Route("api/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IServiceManager _service;

    public ScheduleController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetWprkingHours([FromBody] ScheduleRequestDTO scheduleDTO)
    {

        var result = await _service.ReservationService.GetScheduleInfo(scheduleDTO);
        return Ok(result);
    }


    [HttpGet("workingDays/{staffId}")]
    public async Task<IActionResult> GetWprkingDays(int staffId)
    {

        var result = await _service.ReservationService.GetWorkingDays(staffId);
        return Ok(result);
    }

}
