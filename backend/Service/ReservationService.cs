using AutoMapper;
using EmailService;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Shared;
using Shared.DataTableColumns;
using Shared.DTO;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.Enums;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class ReservationService : IReservationService
{

    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IEmailSender _emailSender;

    public ReservationService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager,
       IEmailSender emailSender
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _emailSender = emailSender;
    }   
    public async Task<ReservationSuccessfulDTO> CreateReservationForFirstTime(ReservationRequest1DTO reservationDTO,int userId)
    {
        try
        {
            var existingServiceStaffActive = await _repositoryManager.ServiceStaffRepository.GetRecordByIdAsync(reservationDTO.StaffId);

            if(existingServiceStaffActive is null)
            {
                throw new BadRequestException("Te dhenat nuk perputhen me staff-sherbim");
            }

            if (existingServiceStaffActive.Service.IsActive == false)
            {
                throw new BadRequestException("Sherbimi i kerkuar nuk eshte aktiv");
            }


            var serviceWorkingHours = await _repositoryManager.WorkingHoursRepository.GetRecordsByServiceId(existingServiceStaffActive.ServiceId);
     

            
            var existingWorkingHour = serviceWorkingHours.FirstOrDefault(x => ((DayOfWeek)(((int)x.WeekDay + 1) % 7)).ToString().Equals(reservationDTO.Date.DayOfWeek.ToString()));

            if (existingWorkingHour is null || TimeSpan.Parse(reservationDTO.StartTime) < existingWorkingHour.StartHour|| TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)) > existingWorkingHour.EndHour)
            {
                throw new BadRequestException("Ju jeni jashte itenerarit te oreve te punes te sherbimit");
            }

            //Duhet bere kontrolli per gjithe 
            var existingReservations = await _repositoryManager.ReservationRepository.GetRecordsByDateAsync(reservationDTO.Date, reservationDTO.StaffId);

            if (existingReservations.FirstOrDefault(x =>
            (x.StartTime < TimeSpan.Parse(reservationDTO.StartTime)&& (x.StartTime.Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1))) > TimeSpan.Parse(reservationDTO.StartTime))
            ||(x.StartTime < TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)) && 
            (x.StartTime.Add( existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1))) > TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)))) is not null)
            {
                throw new BadRequestException("Ky orar eshte i zene");
            }


            var existingClient =await _repositoryManager.ClientRepository.GetRecordByIdentityNumber(reservationDTO.IdentityNumber);
            var client = new Client
                {
                    FirstName = reservationDTO.FirstName,
                    LastName = reservationDTO.LastName,
                    Gender = reservationDTO.Gender,
                    Birthday = reservationDTO.Birthday,
                    Email = reservationDTO.Email,
                    IdentityNumber=reservationDTO.IdentityNumber,
                    RegisteredAt = DateTime.Today
            };
            if(existingClient is null)
            {
                _repositoryManager.ClientRepository.CreateRecord(client);
                await _repositoryManager.SaveAsync();
            }

            var newReservation = new Reservation
            {
                     Reason =reservationDTO.Reason,
                     Date=reservationDTO.Date,
                     StartTime=TimeSpan.Parse(reservationDTO.StartTime),
                     Status=ReservationStatusEnum.Pending,
                     ClientId=existingClient is not null?existingClient.Id:client.Id,
                     ServiceDoctorId=reservationDTO.StaffId,
                     DateCreated=DateTime.Now
            };

            if(userId!=0)
            {
                newReservation.EmployeeId = userId;
            }

            _repositoryManager.ReservationRepository.CreateRecord(newReservation);
            await _repositoryManager.SaveAsync();

            var message = new Message(new string[] {reservationDTO.Email }, "Konfirmimi i rezervimit","Rezervimi u krye me sukses dhe eshte ne gjendje pritje.");
            await _emailSender.SendEmailAsync(message);


            return new ReservationSuccessfulDTO
            {
                Result=true,
                Message="Rezervimi u krye me sukses",
                StatusCode=200,
                ReservationId=newReservation.Id
            };

        }catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateReservationForFirstTime), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }    
    
    public async Task<ReservationSuccessfulDTO> CreateReservationMoreThanOnce(ReservationRequest2DTO reservationDTO,int userId)
    {
        try
        {
            var existingServiceStaffActive = await _repositoryManager.ServiceStaffRepository.GetRecordByIdAsync(reservationDTO.StaffId);

            if (existingServiceStaffActive is null)
            {
                throw new BadRequestException("Te dhenat nuk perputhen me staff-sherbim");
            }

            if (existingServiceStaffActive.Service.IsActive == false)
            {
                throw new BadRequestException("Sherbimi i kerkuar nuk eshte aktiv");
            }


            var serviceWorkingHours = await _repositoryManager.WorkingHoursRepository.GetRecordsByServiceId(existingServiceStaffActive.ServiceId);

            var existingWorkingHour = serviceWorkingHours.FirstOrDefault(x => ((DayOfWeek)(((int)x.WeekDay + 1) % 7)).ToString().Equals(reservationDTO.Date.DayOfWeek.ToString()));

            if (existingWorkingHour is null || TimeSpan.Parse(reservationDTO.StartTime) < existingWorkingHour.StartHour || TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)) > existingWorkingHour.EndHour)
            {
                throw new BadRequestException("Ju jeni jashte itenerarit te oreve te punes te sherbimit");
            }

            //Duhet bere kontrolli per gjithe 
            var existingReservations = await _repositoryManager.ReservationRepository.GetRecordsByDateAsync(reservationDTO.Date, reservationDTO.StaffId);



            if (existingReservations.FirstOrDefault(x =>
            (x.StartTime < TimeSpan.Parse(reservationDTO.StartTime) && (x.StartTime.Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1))) > TimeSpan.Parse(reservationDTO.StartTime))
            || (x.StartTime < TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)) && (x.StartTime.Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1))) > TimeSpan.Parse(reservationDTO.StartTime).Add(existingServiceStaffActive.Service.Duration).Subtract(TimeSpan.FromMinutes(1)))) is not null)
            {
                throw new BadRequestException("Ky orar eshte i zene");
            }


            var existingClient = await _repositoryManager.ClientRepository.GetRecordByIdentityNumber(reservationDTO.IdentityNumber);

            if(existingClient is null)
            {
                throw new NotFoundException("Klienti nuk gjendet");
            }
            if (existingClient is null)
            {
                _repositoryManager.ClientRepository.CreateRecord(existingClient);
                await _repositoryManager.SaveAsync();
            }

            var newReservation = new Reservation
            {
                Reason = reservationDTO.Reason,
                Date = reservationDTO.Date,
                StartTime = TimeSpan.Parse(reservationDTO.StartTime),
                Status = ReservationStatusEnum.Pending,
                ClientId = existingClient.Id,
                ServiceDoctorId = reservationDTO.StaffId,
                DateCreated = DateTime.Now,        
            };

            if(userId!=0)
            {
                newReservation.EmployeeId = userId;
            }

            _repositoryManager.ReservationRepository.CreateRecord(newReservation);
            await _repositoryManager.SaveAsync();


            var message = new Message(new string[] { existingClient.Email }, "Konfirmimi i rezervimit", "Rezervimi u krye me sukses dhe eshte ne gjendje pritje.");
            await _emailSender.SendEmailAsync(message);

            return new ReservationSuccessfulDTO
            {
                Result = true,
                Message = "Rezervimi u krye me sukses",
                StatusCode = 200,
                ReservationId=newReservation.Id
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateReservationForFirstTime), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }
    public async Task<BaseResponse> CancelReservation(int id, int userId)
    {
        try
        {
            var existingReservation =await  _repositoryManager.ReservationRepository.GetRecordByIdAsync(id);

            if(existingReservation is null)
            {
                throw new NotFoundException("Reservimi nuk u gjet");
            }

            existingReservation.Status = ReservationStatusEnum.Canceled;

            if(userId==0) { existingReservation.CanceledByStaffId = null; } 
            else{
                existingReservation.CanceledByStaffId = userId;
            }

            existingReservation.DateCanceled = DateTime.Now;

            _repositoryManager.ReservationRepository.UpdateRecord(existingReservation);

            await _repositoryManager.SaveAsync();

            var message = new Message(new string[] { existingReservation.Client.Email }, "Anullimi i rezervimit", "Rezervimi qe ju keni kryer u anullua me sukses.");
            await _emailSender.SendEmailAsync(message);

            return new BaseResponse
            {
                Result = true,
                Message = "Reservimi u anullua me sukses",
                StatusCode = 200,
            };


        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetAllReservationsWithPagination(LookupDTO filter,int userId,string userRole)
    {
        try
        {
            var reservationsWithMetaData = await _repositoryManager.ReservationRepository.GetReservationsWithMetaData(filter,userId,userRole);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<ReservationResponseDTO>> response = new PagedListResponse<IEnumerable<ReservationResponseDTO>>
            {
                RowCount = reservationsWithMetaData.MetaData.TotalCount,
                Page = reservationsWithMetaData.MetaData.CurrentPage,
                PageSize = reservationsWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = reservationsWithMetaData,
                HasNext = reservationsWithMetaData.MetaData.HasNext,
                HasPrevious = reservationsWithMetaData.MetaData.HasPrevious,
                TotalPages = reservationsWithMetaData.MetaData.TotalPages
            };
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }

    }

    public async Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetSuccededReservationsWithPaginationForStaff(LookupDTO filter, int userId)
    {
        try
        {
            var reservationsWithMetaData = await _repositoryManager.ReservationRepository.GetSuccededReservationsWithMetaDataForStaff(filter, userId);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<ReservationResponseDTO>> response = new PagedListResponse<IEnumerable<ReservationResponseDTO>>
            {
                RowCount = reservationsWithMetaData.MetaData.TotalCount,
                Page = reservationsWithMetaData.MetaData.CurrentPage,
                PageSize = reservationsWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = reservationsWithMetaData,
                HasNext = reservationsWithMetaData.MetaData.HasNext,
                HasPrevious = reservationsWithMetaData.MetaData.HasPrevious,
                TotalPages = reservationsWithMetaData.MetaData.TotalPages
            };
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }

    }

    public async Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetPendAndPostReservationsWithPaginationForStaff(LookupDTO filter, int userId)
    {
        try
        {
            var reservationsWithMetaData = await _repositoryManager.ReservationRepository.GetPendAndPostpReservationsWithMetaDataForStaff(filter, userId);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<ReservationResponseDTO>> response = new PagedListResponse<IEnumerable<ReservationResponseDTO>>
            {
                RowCount = reservationsWithMetaData.MetaData.TotalCount,
                Page = reservationsWithMetaData.MetaData.CurrentPage,
                PageSize = reservationsWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = reservationsWithMetaData,
                HasNext = reservationsWithMetaData.MetaData.HasNext,
                HasPrevious = reservationsWithMetaData.MetaData.HasPrevious,
                TotalPages = reservationsWithMetaData.MetaData.TotalPages
            };
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }

    }

    public async Task<ReservationResponseDTO> GetReservationById(int id)
    {
        try
        {
            var reservation = await _repositoryManager.ReservationRepository.GetRecordByIdAsync(id);

            if (reservation is null)
            {
                throw new NotFoundException("Rezervimi nuk ekziston");
            }

            var reservationResponse = _mapper.Map<ReservationResponseDTO>(reservation);
            return reservationResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<BaseResponse> PostponeReservation(int id,int userId,ReservationPostponeDTO reservationPostponeDTO)
    {
        try
        {
            var reservation = await _repositoryManager.ReservationRepository.GetRecordByIdAsync(id);

            if(reservation is null)
            {
                throw new NotFoundException("Rezervimi nuk ekziston");
            }

            //disa kushte per shtyrjen
            
            reservation.Date = reservationPostponeDTO.Date;
            reservation.StartTime = TimeSpan.Parse(reservationPostponeDTO.StartTime);
            reservation.Status = ReservationStatusEnum.Postponed;
            reservation.PostponedById = userId;


            _repositoryManager.ReservationRepository.UpdateRecord(reservation);

            await _repositoryManager.SaveAsync();

            var reservationResponse = _mapper.Map<ReservationResponseDTO>(reservation);
            return new BaseResponse { 
                Result=true,
                Message="Rezervimi u shty",
                StatusCode=200
            };


            var message = new Message(new string[] { reservation.Client.Email }, "Shtyrje e rezervimit", "Rezervimi qe ju keni kryer u shty " +
                "nga mjeku per ne daten "+reservationPostponeDTO.Date+" dhe ne oren"+reservationPostponeDTO.StartTime+" .");
            await _emailSender.SendEmailAsync(message);

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


    public async Task<PagedListResponse<IEnumerable<ReservationResponseDTO>>> GetPendAndPostReservationsWithPaginationForReception(LookupDTO filter)
    {
        try
        {
            var reservationsWithMetaData = await _repositoryManager.ReservationRepository.GetPendAndPostpReservationsForReception(filter);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<ReservationResponseDTO>> response = new PagedListResponse<IEnumerable<ReservationResponseDTO>>
            {
                RowCount = reservationsWithMetaData.MetaData.TotalCount,
                Page = reservationsWithMetaData.MetaData.CurrentPage,
                PageSize = reservationsWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = reservationsWithMetaData,
                HasNext = reservationsWithMetaData.MetaData.HasNext,
                HasPrevious = reservationsWithMetaData.MetaData.HasPrevious,
                TotalPages = reservationsWithMetaData.MetaData.TotalPages
            };
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }

    }

    public async Task<AvailableHoursDTO> GetScheduleInfo (ScheduleRequestDTO scheduleDTO)
    {
        try
        {
            var serviceStaff = await _repositoryManager.ServiceStaffRepository.GetRecordByIdAsync(scheduleDTO.StaffId);

            if (serviceStaff is null)
            {
                throw new NotFoundException("Rezervimi nuk ekziston");
            }

            var workingHour = await _repositoryManager.WorkingHoursRepository.GetRecordsByServiceId(serviceStaff.ServiceId);

            if(workingHour.FirstOrDefault(x=> ((DayOfWeek)(((int)x.WeekDay + 1) % 7)).ToString().Equals(scheduleDTO.Date.DayOfWeek.ToString())) is null){
                throw new NotFoundException("Ne kete dite jave sherbimi nuk eshte i disponueshem");
            }

            var dayInfo = await _repositoryManager.ReservationRepository.GetScheduleInfo(serviceStaff.StaffId, serviceStaff.ServiceId, scheduleDTO.Date);

            var response = new AvailableHoursDTO() {
                AvailableHours=new List<string>(),
             };

            var temp =dayInfo.StartTime;

            while (temp.Add(TimeSpan.Parse(dayInfo.Interval))<dayInfo.EndTime)
            {

                if (isTimeAvailable(temp,dayInfo.BusyHours,dayInfo.Interval))
                {
                    var string1 = temp.ToString().Substring(0,5);
                    response.AvailableHours.Add(string1);
                }
                temp+=TimeSpan.Parse(dayInfo.Interval);
                
            }


            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<List<WorkingHoursDTO>> GetWorkingDays(int staffId)
    {
        try
        {
            var serviceStaff = await _repositoryManager.ServiceStaffRepository.GetRecordByIdAsync(staffId);

            if (serviceStaff is null)
            {
                throw new NotFoundException("Sherbimi i padisponueshem");
            }

            var workingHours = await _repositoryManager.WorkingHoursRepository.GetRecordsByServiceId(serviceStaff.ServiceId);


            var workingHourResponse = _mapper.Map<List<WorkingHoursDTO>>(workingHours);


            return workingHourResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    
    public async Task<List<StaffReportDTO>> GetStaffReport()
    {
        try
        {
            var result = await _repositoryManager.ReservationRepository.GetStaffReport();


            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }
    public async Task<ReservationReportDTO> GetReservationsReport()
    {
        try
        {
            var result = await _repositoryManager.ReservationRepository.GetReservationsReport();


            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }
    public async Task<List<ReceptionReportDTO>> GetReceptionReport()
    {
        try
        {
            var result = await _repositoryManager.ReservationRepository.GetReceptionReport();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<ReservationReportDTO> GetReservationsReportForStaff(int staffId)
    {
        try
        {
            var result = await _repositoryManager.ReservationRepository.GetReservationsReportForStaff(staffId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CancelReservation), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }
    private Boolean isTimeAvailable(TimeSpan time,List<ServiceDurationDTO> busyHours,string interval)
    {
        var intervalTimeSpan = TimeSpan.Parse(interval);
   

        foreach(var t in busyHours)
        {
            if((t.StartTime <= time && (t.StartTime.Add(intervalTimeSpan).Subtract(TimeSpan.FromMinutes(1))) > time)
            || (t.StartTime <= time.Add(intervalTimeSpan) && (t.StartTime.Add(intervalTimeSpan).Subtract(TimeSpan.FromMinutes(1))) > time.Add(intervalTimeSpan)))
            {
                return false;
            }
        }

        return true;
    }

   

    private List<DataTableColumn> GetDataTableColumns()
    {
        // get the columns
        var columns = GenerateDataTableColumn<ReservationColumns>.GetDataTableColumns();
        // return all columns
        return columns;
    }
}
