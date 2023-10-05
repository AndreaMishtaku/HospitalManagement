using AutoMapper;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class StaffService : IStaffService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;


    public StaffService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
    )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }


    public async Task<BaseResponse> CreateStaffForService(StaffRequestDTO staffDTO)
    {
        try
        {
            var existingUser = await _repositoryManager.UserRepository.GetRecordByIdAsync(staffDTO.StaffId);
            var existingService = await _repositoryManager.ServiceRepository.GetRecordByIdAsync(staffDTO.ServiceId);

            if(existingUser is null || existingService is null)
            {
                throw new BadRequestException("Te dhena jo te sakta");
            }

            var existingServiceStaff = await _repositoryManager.ServiceStaffRepository.GetAllRecordsByServiceId(staffDTO.ServiceId);



            var serviceDoctorStaff = new ServiceStaff
            {
                StaffId = staffDTO.StaffId,
                ServiceId = staffDTO.ServiceId,
            };
            _repositoryManager.ServiceStaffRepository.CreateRecord(serviceDoctorStaff);
            await _repositoryManager.SaveAsync();


            await _repositoryManager.SaveAsync();

            return new BaseResponse
            {
                Result = true,
                Message = "Stafi u krijua me sukses",
                StatusCode=200
            };
        }
        catch (Exception ex)
        {

            _logger.LogError(string.Format("{0}: {1}", nameof(CreateStaffForService), ex.Message));
            throw new BadRequestException(ex.Message);

        }
    }

    public async Task<IEnumerable<StaffResponseDTO>> GetAllStaffs()
    {
        try
        {

            var staffsResponse = await _repositoryManager.ServiceStaffRepository.GetStaffInfo();
        

            return staffsResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllStaffs), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<StaffResponseDTO> GetStaffById(int id)
    {
        try
        {
            var existingStaffResponse = await _repositoryManager.ServiceStaffRepository.GetStaffInfoById(id);

            if (existingStaffResponse is null)
            {
                throw new NotFoundException(string.Format("Stafi me Id: {0} nuk u gjet!", id));
            }

            return existingStaffResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetStaffById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


}
