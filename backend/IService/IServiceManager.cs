using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IServiceManager
{
    IUserService UserService { get; }
    IAuthenticationService AuthenticationService { get; }
    IServicesService ServicesService { get; }
    IEquipmentService EquipmentService { get; }

    IStaffService StaffService { get; }

    IMenuService MenuService { get; }
    IClientService ClientService { get; }

    IReservationService ReservationService { get; }

    ICheckInService CheckInService { get; }
}
