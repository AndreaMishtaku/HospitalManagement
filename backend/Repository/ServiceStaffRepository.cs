using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class ServiceStaffRepository : RepositoryBase<ServiceStaff>, IServiceStaffRepository
{
    public ServiceStaffRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public  void CreateRecord(ServiceStaff serviceStaff) {  
        Create(serviceStaff);
    }

    public void DeleteRecord(ServiceStaff serviceStaff) => Delete(serviceStaff);

    public async Task<IEnumerable<ServiceStaff>> GetAllRecords() => await FindAll().Include(x=>x.Staff).Include(x=>x.Service).ToListAsync();

    public async Task<IEnumerable<ServiceStaff>> GetAllRecordsByServiceId(int serviceId)=>
         await FindByCondition(c => c.ServiceId.Equals(serviceId))
        .ToListAsync();

    public async Task<ServiceStaff> GetRecordByIdAsync(int id)=>
        await FindByCondition(c => c.Id.Equals(id)).Include(x=>x.Service)
        .SingleOrDefaultAsync();

    public void UpdateRecord(ServiceStaff serviceStaff) => Update(serviceStaff);

    public async Task<IEnumerable<StaffResponseDTO>> GetStaffInfo()
    {
        var staffs = await (from ss in RepositoryContext.ServiceStaff
                     select new StaffResponseDTO
                     {
                         Id = ss.Id,
                         ServiceId = ss.ServiceId,
                         ServiceName = ss.Service.Name,
                         StaffSpecialization=ss.Staff.Specialization,
                         DoctorFirstName = ss.Staff.FirstName,
                         DoctorLastName = ss.Staff.LastName,

                     }).ToListAsync();


           

        return staffs;
    }

    public async Task<StaffResponseDTO> GetStaffInfoById(int staffId)
    {
        var staff = await (from ss in RepositoryContext.ServiceStaff
                           where ss.Id == staffId
                           select new StaffResponseDTO
                           {
                               Id = ss.Id,
                               ServiceName = ss.Service.Name,
                               DoctorFirstName = ss.Staff.FirstName,
                               DoctorLastName = ss.Staff.LastName,

                           }).SingleOrDefaultAsync();
        


        return staff;
    }



  
}
