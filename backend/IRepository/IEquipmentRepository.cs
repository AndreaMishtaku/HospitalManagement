using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IEquipmentRepository
{
    void CreateRecord(Equipment equipment);
    Task<Equipment> GetRecordByIdAsync(int id);
    void UpdateRecord(Equipment equipment);
    void DeleteRecord(Equipment equipment);
    Task<IEnumerable<Equipment>> GetAllRecords();
}
