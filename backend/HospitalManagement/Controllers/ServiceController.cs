using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;

namespace HospitalManagement.Controllers;

[Route("api/services")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IServiceManager _service;

    public ServiceController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateService([FromBody] ServiceRequestDTO serviceDTO)
    {
        var result = await _service.ServicesService.CreateService(serviceDTO);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var result = await _service.ServicesService.GetAllServices();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetServiceById(int id)
    {
        var result = await _service.ServicesService.GetServiceById(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceRequestDTO serviceDTO)
    {
        var result = await _service.ServicesService.UpdateService(id,serviceDTO);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var result = await _service.ServicesService.DeleteService(id);
        return Ok(result);
    }
}
