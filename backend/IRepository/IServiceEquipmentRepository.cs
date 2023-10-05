using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IServiceEquipmentRepository
{
    void CreateRecord(ServiceEquipment serviceEquipment);
    void UpdateRecord(ServiceEquipment serviceEquipment);
    void DeleteRecord(ServiceEquipment serviceEquipment);
    Task<ServiceEquipment> GetRecordByIdAsync(int id);
    Task<IEnumerable<ServiceEquipment>> GetRecordsByServiceIdAsync(int serviceIds);

    void DeleteMultipleRecords(IEnumerable<ServiceEquipment> serviceEquipments);
}


