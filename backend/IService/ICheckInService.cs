using Shared.DTO.Request;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface ICheckInService
{
    Task<BaseResponse> CreateCheckIn(CheckInDTO checkInDTO);
}
