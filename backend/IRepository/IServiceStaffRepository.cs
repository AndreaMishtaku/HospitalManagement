using Entities.Models;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IServiceStaffRepository
{
    void CreateRecord(ServiceStaff serviceStaff);
    void UpdateRecord(ServiceStaff serviceStaff);
    void DeleteRecord(ServiceStaff serviceStaff);
    Task<ServiceStaff> GetRecordByIdAsync(int id);

    Task<IEnumerable<ServiceStaff>> GetAllRecords();

    Task<IEnumerable<ServiceStaff>> GetAllRecordsByServiceId(int serviceId);

    Task<IEnumerable<StaffResponseDTO>> GetStaffInfo();

    Task<StaffResponseDTO> GetStaffInfoById(int staffId);


}
