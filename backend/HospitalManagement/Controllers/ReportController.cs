using HospitalManagement.Utility;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/reports")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IServiceManager _service;

    public ReportController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("staff")]
    [Authorize]
    public async Task<IActionResult> GetStaffReport()
    {

        var result = await _service.ReservationService.GetStaffReport();
        return Ok(result);
    }

    [HttpGet("reservations")]
    [Authorize]
    public async Task<IActionResult> GetReservationsReport()
    {

        var result = await _service.ReservationService.GetReservationsReport();
        return Ok(result);
    }


    [HttpGet("reception")]
    [Authorize]
    public async Task<IActionResult> GetReceptionReport()
    {

        var result = await _service.ReservationService.GetReceptionReport();
        return Ok(result);
    }

    [HttpGet("reservations/staff")]
    [Authorize]
    public async Task<IActionResult> GetReservationsReportForStaff()
    {
        var userId = ClaimsUtility.ReadCurrentUserId(User.Claims);
        var result = await _service.ReservationService.GetReservationsReportForStaff(userId);
        return Ok(result);
    }
}
