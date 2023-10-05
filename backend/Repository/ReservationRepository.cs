using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.DTO;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.Enums;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
{
    public ReservationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(Reservation reservation) => Create(reservation);

    public void DeleteRecord(Reservation reservation) => Delete(reservation);

    public async Task<Reservation> GetRecordByIdAsync(int id) =>
        await FindByCondition(c => c.Id.Equals(id)).Include(r => r.Client).Include(r=>r.ServiceDoctor).ThenInclude(r=>r.Service)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Reservation>> GetRecordsByDateAsync(DateTime date, int serviceDoctorId)  {
        var query = from r in RepositoryContext.Reservation
                    where r.Date.Day==date.Day && r.ServiceDoctorId.Equals(serviceDoctorId) && r.Status.Equals(ReservationStatusEnum.Pending)
                    select r;
        
        return await query.ToListAsync();
        }

    public void UpdateRecord(Reservation reservation) => Update(reservation);

    public async Task<IEnumerable<Reservation>> GetAllRecords() => await FindAll().Include(x=>x.Client).ToListAsync();


    public async Task<IEnumerable<Reservation>> GetReservationsByStaffIdAsync(int staffId) => await FindByCondition(c => c.ServiceDoctorId.Equals(staffId))
        .ToListAsync();


    public async Task<PagedListRequest<ReservationResponseDTO>> GetReservationsWithMetaData(LookupDTO filter,int userId,string role)
    {

        if (role == "Manager") {

            var query = from r in RepositoryContext.Reservation
                        select new ReservationResponseDTO
                        {
                            Id = r.Id,
                            FirstName = r.Client.FirstName,
                            LastName = r.Client.LastName,
                            Email = r.Client.Email,
                            Gender = r.Client.Gender,
                            IdentityNumber = r.Client.IdentityNumber,
                            Birthday = r.Client.Birthday,
                            Reason = r.Reason,
                            Date = r.Date,
                            StartTime = r.StartTime.ToString(),
                            EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration).ToString()
                    };

        

        if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
        {
            var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
            return PagedListRequest<ReservationResponseDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
        }

        var result = await query.ToListAsync();
        return PagedListRequest<ReservationResponseDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
        }
        else
        {
            var query = from r in RepositoryContext.Reservation
                        where r.ServiceDoctor.StaffId==userId
                        select new ReservationResponseDTO
                        {
                            Id = r.Id,
                            FirstName = r.Client.FirstName,
                            LastName = r.Client.LastName,
                            Email = r.Client.Email,
                            Gender = r.Client.Gender,
                            IdentityNumber = r.Client.IdentityNumber,
                            Birthday = r.Client.Birthday,
                            Reason = r.Reason,
                            Date = r.Date,
                            StartTime = r.StartTime.ToString(),
                            EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration).ToString()
                        };



            if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
            {
                var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
                return PagedListRequest<ReservationResponseDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
            }

            var result = await query.ToListAsync();
            return PagedListRequest<ReservationResponseDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
        }

    }

    public async Task<PagedListRequest<ReservationResponseDTO>> GetSuccededReservationsWithMetaDataForStaff(LookupDTO filter, int userId)
    {

            var query = from r in RepositoryContext.Reservation
                        where r.Status==ReservationStatusEnum.Succeded && r.ServiceDoctor.StaffId==userId
                        select new ReservationResponseDTO
                        {
                            Id = r.Id,
                            FirstName = r.Client.FirstName,
                            LastName = r.Client.LastName,
                            Email = r.Client.Email,
                            Gender = r.Client.Gender,
                            IdentityNumber = r.Client.IdentityNumber,
                            Birthday = r.Client.Birthday,
                            Reason = r.Reason,
                            Date = r.Date,
                            StartTime = r.StartTime.ToString(),
                            EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration).ToString()
                        };


            if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
            {
                var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
                return PagedListRequest<ReservationResponseDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
            }

            var result = await query.ToListAsync();
            return PagedListRequest<ReservationResponseDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
    }

    public async Task<PagedListRequest<ReservationResponseDTO>> GetPendAndPostpReservationsWithMetaDataForStaff(LookupDTO filter, int userId)
    {

        var query = from r in RepositoryContext.Reservation
                    where (r.Status == ReservationStatusEnum.Pending || r.Status==ReservationStatusEnum.Postponed) && r.ServiceDoctor.StaffId==userId
                    select new ReservationResponseDTO
                    {
                        Id = r.Id,
                        FirstName = r.Client.FirstName,
                        LastName = r.Client.LastName,
                        Email = r.Client.Email,
                        Gender = r.Client.Gender,
                        IdentityNumber = r.Client.IdentityNumber,
                        Birthday = r.Client.Birthday,
                        Reason = r.Reason,
                        Date = r.Date,
                        StartTime = r.StartTime.ToString(),
                        EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration).ToString()
                    };


        if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
        {
            var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
            return PagedListRequest<ReservationResponseDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
        }

        var result = await query.ToListAsync();
        return PagedListRequest<ReservationResponseDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
    }

    public async Task<PagedListRequest<ReservationResponseDTO>> GetPendAndPostpReservationsForReception(LookupDTO filter)
    {

        var query = from r in RepositoryContext.Reservation
                    where (r.Status == ReservationStatusEnum.Pending || r.Status == ReservationStatusEnum.Postponed) && r.Date> DateTime.Now
                    select new ReservationResponseDTO
                    {
                        Id = r.Id,
                        FirstName = r.Client.FirstName,
                        LastName = r.Client.LastName,
                        Email = r.Client.Email,
                        Gender = r.Client.Gender,
                        IdentityNumber = r.Client.IdentityNumber,
                        Birthday = r.Client.Birthday,
                        Reason = r.Reason,
                        Date = r.Date,
                        StartTime = r.StartTime.ToString(),
                        EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration).ToString()
                    };


        if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
        {
            var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
            return PagedListRequest<ReservationResponseDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
        }

        var result = await query.ToListAsync();
        return PagedListRequest<ReservationResponseDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);
    }

    public async Task<DayInfoDTO> GetScheduleInfo(int doctorId,int serviceId ,DateTime date)
    {
        var dayOfWeek = date.DayOfWeek.ToString();

        var res =await (from  s in RepositoryContext.Services
                            join wh in RepositoryContext.WorkingHours on s.Id equals wh.ServiceId
                            where s.Id==serviceId  && wh.WeekDay.Equals((WeekdayEnum)Enum.Parse(typeof(WeekdayEnum), dayOfWeek, true))
                            select new DayInfoDTO
                            {
                            StartTime=wh.StartHour,
                            EndTime=wh.EndHour,
                            Interval=s.Duration.ToString(),
                            }).FirstOrDefaultAsync();

        if (res is not null)
        {
            var bH = await (from r in RepositoryContext.Reservation
                            where r.ServiceDoctor.Staff.Id == doctorId && r.Date.Equals(date)
                            select new ServiceDurationDTO
                            {
                                StartTime = r.StartTime,
                                EndTime = r.StartTime.Add(r.ServiceDoctor.Service.Duration)
                            }).ToListAsync();

            res.BusyHours = bH;
        }



        return res;
    }


    public async Task<List<StaffReportDTO>> GetStaffReport()
    {
        var res = await (from u in RepositoryContext.Users
                   join s in RepositoryContext.ServiceStaff on u.Id equals s.StaffId
                   where u.Specialization!=null
                   select new StaffReportDTO
                   {
                       Id=u.Id,
                       FullName=u.FirstName+" "+u.LastName,
                       SuccessfulReservations=s.Reservations.Count(r=>r.Status==ReservationStatusEnum.Succeded),
                       PostponedReservations=s.Reservations.Count(r=>r.Status==ReservationStatusEnum.Postponed),
                       UnCompletedReservations = s.Reservations.Count(r=>r.Date<DateTime.Now)
                   }).OrderBy(x=>x.PostponedReservations).ToListAsync();

        var timespan = TimeSpan.Parse("00:20:00");

        if (res is not null)
        {
            foreach(var u in res)
            {
                var res1 = await (from ch in RepositoryContext.CheckIn
                            join r in RepositoryContext.Reservation on ch.ReservationId equals r.Id
                            where r.ServiceDoctor.StaffId==u.Id 
                            select new {
                            ReservationTime=r.StartTime,
                            CheckInTime=ch.StartTime
                            }).ToListAsync();

                u.Delays = 0;

                foreach(var r1 in res1)
                {
                    if (r1.CheckInTime.Subtract(r1.ReservationTime) > timespan)
                    {
                        u.Delays++;
                    }
                }
            }
        }
        return res;
    }

    public async Task<ReservationReportDTO> GetReservationsReport()
    {
        var res1 = (from r in RepositoryContext.Reservation where r.Status == ReservationStatusEnum.Pending || r.Status == ReservationStatusEnum.Postponed select r.Id).Count();
        var res2 = (from r in RepositoryContext.Reservation where r.Status == ReservationStatusEnum.Succeded select r.Id).Count();
        var res3 = (from r in RepositoryContext.Reservation where r.Status == ReservationStatusEnum.Canceled select r.Id).Count();


        return new ReservationReportDTO
        {
            TotalWaitingReservations = res1,
            TotalSuccessfulReservations = res2,
            TotalCanceledReservations = res3
        };
    }

    public async Task<List<ReceptionReportDTO>> GetReceptionReport()
    {
        var res = await (from r in RepositoryContext.Reservation
                         where r.EmployeeId!=null
                         group r by  new { r.EmployeeId,r.Employee.FirstName,r.Employee.LastName } into gr
                         select new ReceptionReportDTO
                         {
                             FullName =gr.Key.FirstName+" "+gr.Key.LastName,
                             TotalReservations = gr.Count(),
                             TotalCanceled = gr.Count(x => x.Status == ReservationStatusEnum.Canceled)

                         }).ToListAsync();


        return res;
    }


    public async Task<ReservationReportDTO> GetReservationsReportForStaff(int staffId)
    {
        var res1 = (from r in RepositoryContext.Reservation where (r.Status == ReservationStatusEnum.Pending || r.Status == ReservationStatusEnum.Postponed)&& r.ServiceDoctor.StaffId==staffId  select r.Id).Count();
        var res2 = (from r in RepositoryContext.Reservation where r.Status == ReservationStatusEnum.Succeded &&r.ServiceDoctor.StaffId==staffId select r.Id).Count();
        var res3 = (from r in RepositoryContext.Reservation where r.Status == ReservationStatusEnum.Canceled&& r.ServiceDoctor.StaffId==staffId select r.Id).Count();


        return new ReservationReportDTO
        {
            TotalWaitingReservations = res1,
            TotalSuccessfulReservations = res2,
            TotalCanceledReservations = res3
        };
    }


}
