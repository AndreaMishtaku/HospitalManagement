using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;

namespace HospitalManagement.Controllers;

[Route("api/staff")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IServiceManager _service;

    public StaffController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateStaffForService([FromBody] StaffRequestDTO staffDTO)
    {
        var result = await _service.StaffService.CreateStaffForService(staffDTO);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStaffs()
    {
        var result = await _service.StaffService.GetAllStaffs();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStaffById(int id)
    {
        var result = await _service.StaffService.GetStaffById(id);
        return Ok(result);
    }

}
