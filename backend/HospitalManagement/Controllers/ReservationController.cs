using HospitalManagement.Utility;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.RequestFeatures;

namespace HospitalManagement.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IServiceManager _service;

    public ReservationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost("first-time")]
    public async Task<IActionResult> CreateReservationForFirstTime([FromBody] ReservationRequest1DTO reservationDTO)
    {

        var userId = User.Identity.IsAuthenticated ? ClaimsUtility.ReadCurrentUserId(User.Claims) : 0;
    
        var result = await _service.ReservationService.CreateReservationForFirstTime(reservationDTO,userId);
        return Ok(result);
    }


    [HttpPost("more-than-once")]
    public async Task<IActionResult> CreateReservationMoreThanOnce([FromBody] ReservationRequest2DTO reservationDTO)
    {
         var userId = User.Identity.IsAuthenticated ? ClaimsUtility.ReadCurrentUserId(User.Claims) : 0;
        var result = await _service.ReservationService.CreateReservationMoreThanOnce(reservationDTO,userId);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelReservation(int id)
    {
        var userId = User.Identity.IsAuthenticated ? ClaimsUtility.ReadCurrentUserId(User.Claims) : 0;
        var result = await _service.ReservationService.CancelReservation(id,userId);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("postpone/{id}")]
    public async Task<IActionResult> PostponeReservation(int id,[FromBody]ReservationPostponeDTO reseservationPostponeDTO)
    {
        var userId = ClaimsUtility.ReadCurrentUserId(User.Claims);
        var result = await _service.ReservationService.PostponeReservation(id, userId,reseservationPostponeDTO);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("get-all")]
    public async Task<IActionResult> GetAllReservationsWithPagination([FromBody]LookupDTO filter)
    {

        var userId = ClaimsUtility.ReadCurrentUserId(User.Claims);
        var userRole=ClaimsUtility.ReadCurrentUserRole(User.Claims);
        var result = await _service.ReservationService.GetAllReservationsWithPagination(filter,userId,userRole);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("reception/get-all")]
    public async Task<IActionResult> GetAllReservationsWithPaginationForReception([FromBody] LookupDTO filter)
    {

        var result = await _service.ReservationService.GetPendAndPostReservationsWithPaginationForReception(filter);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("staff-succeded/get-all")]
    public async Task<IActionResult> GetSuccededReservationsWithPaginationForStaff([FromBody] LookupDTO filter)
    {

        var userId = ClaimsUtility.ReadCurrentUserId(User.Claims);
      
        var result = await _service.ReservationService.GetSuccededReservationsWithPaginationForStaff(filter, userId);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("staff-pp/get-all")]
    public async Task<IActionResult> GetPendAndPostWithPaginationForStaff([FromBody] LookupDTO filter)
    {

        var userId = ClaimsUtility.ReadCurrentUserId(User.Claims);

        var result = await _service.ReservationService.GetPendAndPostReservationsWithPaginationForStaff(filter, userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        var result = await _service.ReservationService.GetReservationById(id);
        return Ok(result);
    }
}