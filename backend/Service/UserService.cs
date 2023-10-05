using AutoMapper;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Shared.DataTableColumns;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;
public class UserService : IUserService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly DefaultConfiguration _defaultConfig;
    //private readonly IEmailSender _emailSender;

    public UserService(ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager,
       UserManager<User> userManager,
       DefaultConfiguration defaultConfig
       //IEmailSender emailSender
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _defaultConfig = defaultConfig;
    }


    public async Task<IdentityResult> AddUser(AddUserDTO addUserDto, int userId)
    {
        try
        {
            var user = new User
            {
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Email = addUserDto.Email,
                PhoneNumber = addUserDto.PhoneNumber,
                BirthDay= addUserDto.BirthDay,
                Address = addUserDto.Address,
                UserName = addUserDto.Email,
                Specialization=addUserDto.Specialization,
                Gender=addUserDto.Gender,
            };

            string password = $"{addUserDto.FirstName.First().ToString().ToUpper() + addUserDto.FirstName.Substring(1)}{addUserDto.LastName.ToLower()}123@";

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, addUserDto.Role);
            }
            else
            {
                throw new BadRequestException("Regjistrimi i perdoruesit nuk u krye");
            }

            await _repositoryManager.SaveAsync();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(AddUser), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<UserListDTO> GetUserById(int userId)
    {
        try
        {
            var existingUser = await GetUserAndCheckIfExistsAsync(userId);

            var userById = _mapper.Map<UserListDTO>(existingUser);
            var roles = await _userManager.GetRolesAsync(existingUser);

            foreach (var role in roles)
            {
                userById.Role = role;
            }

            return userById;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetUserById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<IEnumerable<UserDetailsDTO>> GetAllUsers()
    {
        try
        {  
            
            var users =await _repositoryManager.UserRepository.GetAllRecords();

            var userResponse= _mapper.Map<IEnumerable<UserDetailsDTO>>(users);

            return userResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllUsersWithPagination), ex.Message));
            throw new BadRequestException(ex.Message);
        }


    }

    public async Task<PagedListResponse<IEnumerable<UserDetailsDTO>>> GetAllUsersWithPagination(LookupDTO filter)
    {
        try
        {
            var userWithMetaData = await _repositoryManager.UserRepository.GetUsersWithMetaData(filter);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<UserDetailsDTO>> response = new PagedListResponse<IEnumerable<UserDetailsDTO>>
            {
                RowCount = userWithMetaData.MetaData.TotalCount,
                Page = userWithMetaData.MetaData.CurrentPage,
                PageSize = userWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows =userWithMetaData,
                HasNext = userWithMetaData.MetaData.HasNext,
                HasPrevious = userWithMetaData.MetaData.HasPrevious,
                TotalPages=userWithMetaData.MetaData.TotalPages
            };
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllUsersWithPagination), ex.Message));
            throw new BadRequestException(ex.Message);
        }


    }




    private async Task<User> GetUserAndCheckIfExistsAsync(int userId)
    {
        var existingUser = await _repositoryManager.UserRepository.GetRecordByIdAsync(userId);
        if (existingUser is null)
            throw new NotFoundException(string.Format("Përdoruesi me Id: {0} nuk u gjet!", userId));

        return existingUser;
    }

    private List<DataTableColumn> GetDataTableColumns()
    {
        // get the columns
        var columns = GenerateDataTableColumn<UserColumns>.GetDataTableColumns();

        // return all columns
        return columns;
    }

   
}
