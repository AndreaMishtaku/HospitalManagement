using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class ServiceEquipmentRepository : RepositoryBase<ServiceEquipment>, IServiceEquipmentRepository
{
    public ServiceEquipmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(ServiceEquipment serviceEquipment) => Create(serviceEquipment);

    public void DeleteRecord(ServiceEquipment serviceEquipment) => Delete(serviceEquipment);

    public async Task<ServiceEquipment> GetRecordByIdAsync(int id)=>
      await FindByCondition(c => c.Id.Equals(id))
      .SingleOrDefaultAsync();


    public void UpdateRecord(ServiceEquipment serviceEquipment) => Update(serviceEquipment);

    public async Task<IEnumerable<ServiceEquipment>> GetRecordsByServiceIdAsync(int serviceId) =>
        await FindByCondition(c => c.ServiceId.Equals(serviceId)).ToListAsync();

    public void DeleteMultipleRecords(IEnumerable<ServiceEquipment> serviceEquipments)
    {
        foreach(var sE in serviceEquipments)
        {
            Delete(sE);
        }
    }

    public async Task<IEnumerable<ServiceEquipment>> GetAllRecords() => await FindAll()
        .Include(x => x.Equipment).ToListAsync();
    
    
}
