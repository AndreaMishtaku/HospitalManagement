using AutoMapper;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;



public class MenuService : IMenuService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public MenuService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
        //IEmailSender emailSender
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }


    public async Task<IEnumerable<ApplicationMenuDTO>> GetMenuByRole(string userRole)
    {


        var applicationMenu = await _repositoryManager.MenuRepository.GetMenuByUserRole(userRole);

        if (applicationMenu == null)
            throw new NotFoundException($"Menu nuk u gjet për rolin {userRole}");

        return applicationMenu;
    }

}