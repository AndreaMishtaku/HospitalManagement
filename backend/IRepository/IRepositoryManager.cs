using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface IRepositoryManager
{
    IEquipmentRepository EquipmentRepository { get; }
    IMenuRepository MenuRepository { get; }
    IReservationRepository ReservationRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IServiceEquipmentRepository ServiceEquipmentRepository { get; }
    IUserRepository UserRepository { get; }
    IServiceStaffRepository ServiceStaffRepository { get; }
    IWorkingHoursRepository WorkingHoursRepository { get; }

    IClientRepository ClientRepository { get; }
    ICheckInRepository CheckInRepository { get; }

    Task SaveAsync();
}
