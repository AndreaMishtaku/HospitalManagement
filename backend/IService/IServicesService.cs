using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IServicesService
{

    Task<BaseResponse> CreateService(ServiceRequestDTO serviceDTO);

    Task<ServiceResponseDTO> GetServiceById(int id);

    Task<IEnumerable<ServiceResponseDTO>> GetAllServices();

    Task<BaseResponse> UpdateService(int id,ServiceRequestDTO serviceDTO);

    Task<BaseResponse> DeleteService(int id);


}
