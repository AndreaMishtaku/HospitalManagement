using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IStaffService
{
    Task<BaseResponse> CreateStaffForService(StaffRequestDTO staffDTO);

    Task<StaffResponseDTO> GetStaffById(int id);

    Task<IEnumerable<StaffResponseDTO>> GetAllStaffs();

}
