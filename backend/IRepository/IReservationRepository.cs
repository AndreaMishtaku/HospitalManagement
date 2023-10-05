using Entities.Models;
using Shared;
using Shared.DTO;
using Shared.DTO.Response;
using Shared.Enums;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IReservationRepository
{
    void CreateRecord(Reservation reservation);
    void UpdateRecord(Reservation reservation); 
    void DeleteRecord(Reservation reservation);
    Task<Reservation> GetRecordByIdAsync(int id);

    Task<IEnumerable<Reservation>> GetAllRecords();

    Task<IEnumerable<Reservation>> GetReservationsByStaffIdAsync(int staffId);

    Task<IEnumerable<Reservation>> GetRecordsByDateAsync(DateTime date,int serviceDoctorId);

    Task<PagedListRequest<ReservationResponseDTO>> GetReservationsWithMetaData(LookupDTO filter, int userId, string role);

    Task<PagedListRequest<ReservationResponseDTO>> GetSuccededReservationsWithMetaDataForStaff(LookupDTO filter, int userId);

    Task<PagedListRequest<ReservationResponseDTO>> GetPendAndPostpReservationsWithMetaDataForStaff(LookupDTO filter, int userId);

    Task<DayInfoDTO> GetScheduleInfo(int doctorId, int serviceId, DateTime date);

    Task<PagedListRequest<ReservationResponseDTO>> GetPendAndPostpReservationsForReception(LookupDTO filter);

    Task<List<StaffReportDTO>> GetStaffReport();
    Task<ReservationReportDTO> GetReservationsReport();
    Task<List<ReceptionReportDTO>> GetReceptionReport();

    Task<ReservationReportDTO> GetReservationsReportForStaff(int staffId);
}
