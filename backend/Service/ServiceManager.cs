using AutoMapper;
using Cryptography;
using EmailService;
using Entities.Configuration;
using Entities.Models;
using IRepository;
using IService;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class ServiceManager : IServiceManager
{

    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IServicesService> _servicesService;
    private readonly Lazy<IEquipmentService> _equipmentService;
    private readonly Lazy<IStaffService> _staffService;
    private readonly Lazy<IMenuService> _menuService;
    private readonly Lazy<IClientService> _clientService;
    private readonly Lazy<IReservationService> _reservationService;
    private readonly Lazy<ICheckInService> _checkInService;

    public ServiceManager(IRepositoryManager repositoryManager
    , ILoggerManager logger
    , IMapper mapper
    , UserManager<User> userManager
    , IOptions<JwtConfiguration> configuration
    , IEmailSender emailSender
    , DefaultConfiguration defaultConfig
    , SignInManager<User> signInManager
    , ICryptoUtils cryptoUtils
    )
    {
        _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration, repositoryManager, defaultConfig, signInManager, cryptoUtils));
        _userService = new Lazy<IUserService>(() => new UserService(logger, mapper, repositoryManager, userManager,  defaultConfig));
        _servicesService = new Lazy<IServicesService>(() => new ServicesService(logger, mapper, repositoryManager));
        _equipmentService = new Lazy<IEquipmentService>(() => new EquipmentService(logger, mapper, repositoryManager));
        _staffService = new Lazy<IStaffService>(() => new StaffService(logger, mapper, repositoryManager));
        _menuService = new Lazy<IMenuService>(() => new MenuService(logger, mapper, repositoryManager));
        _clientService = new Lazy<IClientService>(() => new ClientService(logger, mapper, repositoryManager));
        _reservationService = new Lazy<IReservationService>(() => new ReservationService(logger, mapper, repositoryManager,emailSender));
        _checkInService = new Lazy<ICheckInService>(() => new CheckInService(logger, mapper, repositoryManager));
    }
    public IUserService UserService => _userService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IServicesService ServicesService => _servicesService.Value;
    public IEquipmentService EquipmentService => _equipmentService.Value;
    public IStaffService StaffService => _staffService.Value;
    public IMenuService MenuService => _menuService.Value;
    public IClientService ClientService => _clientService.Value;
    public IReservationService ReservationService => _reservationService.Value;
    public ICheckInService CheckInService => _checkInService.Value;
}
