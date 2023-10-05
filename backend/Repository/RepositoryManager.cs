using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{   private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IEquipmentRepository> _equipmentRepository;
    private readonly Lazy<IMenuRepository> _menuRepository;
    private readonly Lazy<IReservationRepository> _reservationRepository;
    private readonly Lazy<IServiceEquipmentRepository> _serviceEquipmentRepository;
    private readonly Lazy<IServiceRepository> _serviceRepository;
    private readonly Lazy<IServiceStaffRepository> _serviceStaffRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IWorkingHoursRepository> _workingHoursRepository;
    private readonly Lazy<IClientRepository> _clientRepository;
    private readonly Lazy<ICheckInRepository> _checkInRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _equipmentRepository = new Lazy<IEquipmentRepository>(() => new EquipmentRepository(repositoryContext));
        _menuRepository = new Lazy<IMenuRepository>(() => new MenuRepository(repositoryContext));
        _reservationRepository = new Lazy<IReservationRepository>(() => new ReservationRepository(repositoryContext));
        _serviceEquipmentRepository = new Lazy<IServiceEquipmentRepository>(() => new ServiceEquipmentRepository(repositoryContext));
        _serviceRepository = new Lazy<IServiceRepository>(() => new ServiceRepository(repositoryContext));
        _serviceStaffRepository = new Lazy<IServiceStaffRepository>(() => new ServiceStaffRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        _workingHoursRepository = new Lazy<IWorkingHoursRepository>(() => new WorkingHoursRepository(repositoryContext));
        _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(repositoryContext));
        _checkInRepository = new Lazy<ICheckInRepository>(() => new CheckInRepository(repositoryContext));
    }

    public IEquipmentRepository EquipmentRepository =>_equipmentRepository.Value;

    public IMenuRepository MenuRepository => _menuRepository.Value;

    public IReservationRepository ReservationRepository => _reservationRepository.Value;

    public IServiceRepository ServiceRepository => _serviceRepository.Value;

    public IServiceEquipmentRepository ServiceEquipmentRepository => _serviceEquipmentRepository.Value;

    public IUserRepository UserRepository => _userRepository.Value;

    public IServiceStaffRepository ServiceStaffRepository => _serviceStaffRepository.Value;

    public IWorkingHoursRepository WorkingHoursRepository =>_workingHoursRepository.Value;


    public IClientRepository ClientRepository => _clientRepository.Value;

    public ICheckInRepository CheckInRepository => _checkInRepository.Value;

    public async Task SaveAsync()
    {
        _repositoryContext.ChangeTracker.AutoDetectChangesEnabled = false;
        await _repositoryContext.SaveChangesAsync();
    }
}
