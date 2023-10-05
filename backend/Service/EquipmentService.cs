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

public class EquipmentService : IEquipmentService
{

    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
  

    public EquipmentService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<BaseResponse> CreateEquipment(EquipmentRequestDTO equipmentDTO)
    {

        try
        {
            var equipment = _mapper.Map<Equipment>(equipmentDTO);
            _repositoryManager.EquipmentRepository.CreateRecord(equipment);
            await _repositoryManager.SaveAsync();

            return new BaseResponse
            {
                Result = true,
                Message = "Pajisja u shtua",
                StatusCode=200
            };
        }
        catch (Exception ex)
        {

            _logger.LogError(string.Format("{0}: {1}", nameof(CreateEquipment), ex.Message));
            throw new BadRequestException(ex.Message);

        }
    }

    public async Task<BaseResponse> DeleteEquipment(int id)
    {
        try
        {
            var existingEquipment = await _repositoryManager.EquipmentRepository.GetRecordByIdAsync(id);

            if (existingEquipment is null)
            {
                throw new NotFoundException(string.Format("Pajisja me Id: {0} nuk u gjet!", id));
            }

            _repositoryManager.EquipmentRepository.DeleteRecord(existingEquipment);
            return new BaseResponse()
            {
                Result=true,Message="Pajisja u fshi",StatusCode=200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(DeleteEquipment), ex.Message));
            throw new BadRequestException(ex.Message);
        }

    }

    public async Task<IEnumerable<EquipmentResponseDTO>> GetAllEquipments()
    {
        try
        {

            var equipments = await _repositoryManager.EquipmentRepository.GetAllRecords();

            var equipmentResponse = _mapper.Map<IEnumerable<EquipmentResponseDTO>>(equipments);

            return equipmentResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllEquipments), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<EquipmentResponseDTO> GetEquipmentById(int id)
    {
        try
        {
            var existingEquipment = await _repositoryManager.EquipmentRepository.GetRecordByIdAsync(id);

            if (existingEquipment is null)
            {
                throw new NotFoundException(string.Format("Pajisja me Id: {0} nuk u gjet!", id));
            }

            var serviceResponse = _mapper.Map<EquipmentResponseDTO>(existingEquipment);
            return serviceResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetEquipmentById), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<BaseResponse> UpdateEquipment(int id, EquipmentRequestDTO equipmentDTO)
    {
        try
        {
            var existingEquipment = await _repositoryManager.EquipmentRepository.GetRecordByIdAsync(id);

            if (existingEquipment is null)
            {
                throw new NotFoundException(string.Format("Pajisja me Id: {0} nuk u gjet!", id));
            }
            else
            {

                _mapper.Map(equipmentDTO, existingEquipment);
                _repositoryManager.EquipmentRepository.UpdateRecord(existingEquipment);

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
            _logger.LogError(string.Format("{0}: {1}", nameof(UpdateEquipment), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }
}
