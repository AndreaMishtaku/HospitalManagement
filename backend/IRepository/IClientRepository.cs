using Entities.Models;
using Shared.DTO.Request;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IClientRepository
{
    void CreateRecord(Client client);
    Task<Client> GetRecordByIdAsync(int id);
    void UpdateRecord(Client client);
    void DeleteRecord(Client client);
    Task<IEnumerable<Client>> GetAllRecords();

    Task<Client> GetRecordByIdentityNumber(string identityNumber);

    Task<PagedListRequest<ClientDTO>> GetClientsWithMetaData(LookupDTO filter);
}
