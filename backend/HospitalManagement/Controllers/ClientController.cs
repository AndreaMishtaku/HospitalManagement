using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;
using Shared.RequestFeatures;

namespace HospitalManagement.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IServiceManager _service;

    public ClientController(IServiceManager service)
    {
        _service = service;
    }



    [HttpPost("get-all")]
    public async Task<IActionResult> GetAllClients([FromBody] LookupDTO filter)
    {
        var result = await _service.ClientService.GetAllClientsWithPagination(filter);
        return Ok(result);
    }

    [HttpPost("getBy-identityNumber")]
    public async Task<IActionResult> GetClientByIdentityNumber([FromBody] ClientIdentityNumberDTO nr)
    {
        var result = await _service.ClientService.GetClientByIdentityNumber(nr);
        return Ok(result);
    }

}