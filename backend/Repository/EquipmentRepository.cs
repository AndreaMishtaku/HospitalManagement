using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(Equipment equipment)=> Create(equipment);

    public void DeleteRecord(Equipment equipment) => Delete(equipment);

    public async Task<IEnumerable<Equipment>> GetAllRecords() => await FindAll().ToListAsync();

    public async Task<Equipment> GetRecordByIdAsync(int id) =>
        await FindByCondition(c => c.Id.Equals(id))
        .FirstOrDefaultAsync();

    public void UpdateRecord(Equipment equipment) => Update(equipment);
}
