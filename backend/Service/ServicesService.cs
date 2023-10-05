using AutoMapper;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.ResponseFeatures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class ServicesService : IServicesService
{

    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;

    public ServicesService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<BaseResponse> CreateService(ServiceRequestDTO serviceDTO)
    {

        try
        {
            var service = new Services()
            {
                Name= serviceDTO.Name,
                Description=serviceDTO.Description,
                Duration=TimeSpan.Parse(serviceDTO.Duration),

            };
            service.IsActive = true;
            _repositoryManager.ServiceRepository.CreateRecord(service);

            await _repositoryManager.SaveAsync();
            

            await CreateServiceWorkingHoursRelation(serviceDTO.WorkingHours, service.Id);

            await CreateServiceEquipmentRelation(serviceDTO.EquipmentIds, service.Id);

            await _repositoryManager.SaveAsync();

            return new BaseResponse
            {
                Result = true,
                Message = "Sherbimi u krijua me sukses",
                StatusCode=200
            };
        }catch (Exception ex)
        {

            _logger.LogError(string.Format("{0}: {1}", nameof(CreateService), ex.Message));
            throw new BadRequestException(ex.Message);

        }
    } 
    
    public async Task<BaseResponse> DeleteService(int id)
    {
        try
        {
            var existingService = await _repositoryManager.ServiceRepository.GetRecordByIdAsync(id);

            if (existingService is null)
            {
                throw new NotFoundException(string.Format("Sherbimi me Id: {0} nuk u gjet!", id));
            }

            existingService.IsActive =false;

            _repositoryManager.ServiceRepository.UpdateRecord(existingService);

            await _repositoryManager.SaveAsync();
            return new BaseResponse()
            {
                Result= true,
                Message="Sherbimi u fshi me sukses",
                StatusCode=200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetServiceById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<IEnumerable<ServiceResponseDTO>> GetAllServices()
    {
        try
        {

            var servicesResponse = await _repositoryManager.ServiceRepository.GetServicesInfo();

            return servicesResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllServices), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<ServiceResponseDTO> GetServiceById(int id)
    {
        try
        {
            var existingServiceResponse = await _repositoryManager.ServiceRepository.GetServiceInfoById(id);

            if (existingServiceResponse is null)
            {
                throw new NotFoundException(string.Format("Sherbimi me Id: {0} nuk u gjet!",id));
            }

           
            return existingServiceResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetServiceById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<BaseResponse> UpdateService(int id, ServiceRequestDTO serviceDTO)
    {
        try
        {
            var existingService = await _repositoryManager.ServiceRepository.GetRecordByIdAsync(id);

            if (existingService is null)
            {
                throw new NotFoundException(string.Format("Përdoruesi me Id: {0} nuk u gjet!", id));
            }
            else { 
                _mapper.Map(serviceDTO, existingService);
                _repositoryManager.ServiceRepository.UpdateRecord(existingService);

                await UpdateServiceWorkingHoursRelation(serviceDTO.WorkingHours, id);
                await UpdateServiceEquipmentRelation(serviceDTO.EquipmentIds, id);

                await _repositoryManager.SaveAsync();

                return new BaseResponse
                {
                    Result = true,
                    Message = "Sherbimi u modifikua",
                    StatusCode=200
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(UpdateService), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


    private async Task CreateServiceWorkingHoursRelation(List<WorkingHoursDTO> workingHours, int serviceId)
    {
        if (workingHours is not null)
        {
            foreach (var wh in workingHours)
            {
                var workingHour = new WorkingHours()
                {
                    WeekDay = wh.Weekday,
                    StartHour =TimeSpan.Parse(wh.StartHour),
                    EndHour = TimeSpan.Parse(wh.EndHour),
                    ServiceId=serviceId

                };
                _repositoryManager.WorkingHoursRepository.CreateRecord(workingHour);
            }
        }
    }


    private async Task CreateServiceEquipmentRelation(List<int> equipmentIds, int serviceId)
    {
        if (equipmentIds is not null)
        {
            foreach (var equipmentId in equipmentIds)
            {
                var existingEquipment = await _repositoryManager.EquipmentRepository.GetRecordByIdAsync(equipmentId);
                if(existingEquipment is null)
                {
                    throw new BadRequestException("Njera ose me shume prej pajisjeve te shtuara nuk egziston");
                }

                var serviceEquipment = new ServiceEquipment
                {
                    EquipmentId = equipmentId,
                    ServiceId = serviceId
                };
                _repositoryManager.ServiceEquipmentRepository.CreateRecord(serviceEquipment);
            }
        }
    }

    private async Task UpdateServiceWorkingHoursRelation(List<WorkingHoursDTO> workingHours, int serviceId)
    {
     
            var existingWorkingHours = await _repositoryManager.WorkingHoursRepository.GetRecordsByServiceId(serviceId) ;


            if ((workingHours.Count() == 0 || workingHours is null) && existingWorkingHours.Count()>0)
            {
                _repositoryManager.WorkingHoursRepository.DeleteMultipleRecords(existingWorkingHours);
            }
            else
            {
                foreach(var wH in workingHours)
                {
                    if(existingWorkingHours is not null&& existingWorkingHours.Count() > 0) { 
                        if (existingWorkingHours.FirstOrDefault(element=>element.WeekDay==wH.Weekday) is not null)
                        {
                            var temp = existingWorkingHours.FirstOrDefault(element => element.WeekDay == wH.Weekday);
                            temp.StartHour= TimeSpan.Parse(wH.StartHour);
                            temp.EndHour = TimeSpan.Parse(wH.EndHour);

                            _repositoryManager.WorkingHoursRepository.UpdateRecord(temp);
                        }
                    }else
                    {
                        _repositoryManager.WorkingHoursRepository.CreateRecord(new WorkingHours() { 
                            WeekDay=wH.Weekday,
                            StartHour= TimeSpan.Parse(wH.StartHour),
                            EndHour = TimeSpan.Parse(wH.EndHour),
                            ServiceId=serviceId
                        });
                    }
                }
            }
        
    }


    private async Task UpdateServiceEquipmentRelation(List<int> equipmentIds, int serviceId)
    {
        var existingServiceEquipments = await _repositoryManager.ServiceEquipmentRepository.GetRecordsByServiceIdAsync(serviceId);

        if ((equipmentIds.Count() == 0 || equipmentIds is null) && existingServiceEquipments.Count() > 0)
        {
            _repositoryManager.ServiceEquipmentRepository.DeleteMultipleRecords(existingServiceEquipments);
        }
        else
        {
            foreach (var eId in equipmentIds )
            {
                var existingEquipment = await _repositoryManager.EquipmentRepository.GetRecordByIdAsync(eId);
                if (existingEquipment is null)
                {
                    throw new BadRequestException("Njera ose me shume prej pajisjeve te shtuara nuk egziston");
                }

                if (existingServiceEquipments.FirstOrDefault(se=>se.EquipmentId==eId)is  null)
                {
                     _repositoryManager.ServiceEquipmentRepository.CreateRecord(new ServiceEquipment() { EquipmentId =eId, ServiceId = serviceId });
                }
            }
        }

    }

}
