using Shared;
using Shared.DTO;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IReservationService
{
    Task<ReservationSuccessfulDTO> CreateReservationForFirstTime(ReservationRequest1DTO reservationDTO,int userId);

    Task<ReservationSuccessfulDTO> CreateReservationMoreThanOnce(ReservationRequest2DTO reservationDTO,int userId);

    Task<ReservationResponseDTO> GetReservationById(int id);

    Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetAllReservationsWithPagination(LookupDTO filter,int userId,string userRole);

    Task<BaseResponse> CancelReservation(int id,int userId);

    Task<BaseResponse> PostponeReservation(int id,int userId,ReservationPostponeDTO reservationPostponeDTO);

    Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetSuccededReservationsWithPaginationForStaff(LookupDTO filter, int userId);

    Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetPendAndPostReservationsWithPaginationForStaff(LookupDTO filter, int userId);
    Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetPendAndPostReservationsWithPaginationForReception(LookupDTO filter);

    Task<AvailableHoursDTO> GetScheduleInfo(ScheduleRequestDTO scheduleDTO);

    Task<List<WorkingHoursDTO>> GetWorkingDays(int staffId);
    Task<List<StaffReportDTO>> GetStaffReport();

    Task<ReservationReportDTO> GetReservationsReport();

    Task<List<ReceptionReportDTO>> GetReceptionReport();

    Task<ReservationReportDTO> GetReservationsReportForStaff(int staffId);
}
