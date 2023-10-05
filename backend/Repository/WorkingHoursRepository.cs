using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class WorkingHoursRepository : RepositoryBase<WorkingHours>, IWorkingHoursRepository
{
    public WorkingHoursRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(WorkingHours workingHours) => Create(workingHours);

    public void DeleteRecord(WorkingHours workingHours) => Delete(workingHours);

    public async Task<WorkingHours> GetRecordByIdAsync(int id) =>
        await FindByCondition(c => c.Id.Equals(id)).SingleOrDefaultAsync();

    public void UpdateRecord(WorkingHours workingHours) => Update(workingHours);

    public async Task<IEnumerable<WorkingHours>> GetRecordsByServiceId(int serviceId) =>
        await FindByCondition(c => c.ServiceId.Equals(serviceId)).ToListAsync();

    public void DeleteMultipleRecords(IEnumerable<WorkingHours> workingHours)
    {
        foreach(var wH in workingHours)
        {
            Delete(wH);
        }
    }

}