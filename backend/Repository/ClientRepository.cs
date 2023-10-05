using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Request;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(Client client) => Create(client);



    public void DeleteRecord(Client client) => Delete(client);

    public async Task<IEnumerable<Client>> GetAllRecords() => await FindAll().ToListAsync();


    public async Task<Client> GetRecordByIdAsync(int id) =>
        await FindByCondition(c => c.Id.Equals(id))
        .FirstOrDefaultAsync();

    public async Task<Client> GetRecordByIdentityNumber(string identityNumber) =>
        await FindByCondition(c => c.IdentityNumber.Equals(identityNumber))
        .FirstOrDefaultAsync();



    public void UpdateRecord(Client client) => Update(client);

    public async Task<PagedListRequest<ClientDTO>> GetClientsWithMetaData(LookupDTO filter)
    {
        var query = (from c in RepositoryContext.Client
                     select new ClientDTO
                     {
                         Id = c.Id,
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         Birthday = c.Birthday.ToString(),
                         Gender = c.Gender,
                         Email = c.Email,
                         RegisteredAt = c.RegisteredAt.ToString(),
                         IdentityNumber = c.IdentityNumber,

                     });


        if (filter.SearchValue is not null && filter.SearchValue.Length > 0)
        {
            var resultWithCondition = await query.Where(q => q.FirstName.ToLower().Contains(filter.SearchValue) || q.LastName.ToLower().Contains(filter.SearchValue)).ToListAsync();
            return PagedListRequest<ClientDTO>.ToPagedList(resultWithCondition, filter.PageNumber, filter.PageSize);
        }

        var result = await query.ToListAsync();
        return PagedListRequest<ClientDTO>.ToPagedList(result, filter.PageNumber, filter.PageSize);

    }
}

