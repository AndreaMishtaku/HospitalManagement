using Entities.Models;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IServiceRepository
{
    void CreateRecord(Services service);
    void UpdateRecord(Services service);
    void DeleteRecord(Services service);
    Task<Services> GetRecordByIdAsync(int id);

    Task<IEnumerable<Services>> GetAllRecords();

    Task<IEnumerable<ServiceResponseDTO>> GetServicesInfo();

    Task<ServiceResponseDTO> GetServiceInfoById(int serviceId);
}
