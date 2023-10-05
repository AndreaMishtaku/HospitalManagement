using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IWorkingHoursRepository
{ 
    void CreateRecord(WorkingHours workingHours);
    void UpdateRecord(WorkingHours workingHours);
    void DeleteRecord(WorkingHours workingHours);
    Task<WorkingHours> GetRecordByIdAsync(int id);

    Task<IEnumerable<WorkingHours>> GetRecordsByServiceId(int serviceId);

    void DeleteMultipleRecords(IEnumerable<WorkingHours> entities);

}
