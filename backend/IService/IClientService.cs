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

public interface IClientService
{
    Task<PagedListResponse<IEnumerable<ClientDTO>>> GetAllClientsWithPagination(LookupDTO filter);
    Task<ClientDTO> GetClientByIdentityNumber(ClientIdentityNumberDTO nr);
}
