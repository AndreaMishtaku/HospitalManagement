using IService;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;

namespace HospitalManagement.Controllers;


[Route("api/equipments")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly IServiceManager _service;

    public EquipmentController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateEquipment([FromBody]  EquipmentRequestDTO equipmentDTO)
    {
        var result = await _service.EquipmentService.CreateEquipment(equipmentDTO);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEquipments()
    {
        var result = await _service.EquipmentService.GetAllEquipments();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEquipmentById(int id)
    {
        var result = await _service.EquipmentService.GetEquipmentById(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(int id, [FromBody] EquipmentRequestDTO equipmentDTO)
    {
        var result = await _service.EquipmentService.UpdateEquipment(id, equipmentDTO);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipment(int id)
    {
        var result = await _service.EquipmentService.DeleteEquipment(id);
        return Ok(result);
    }

}

