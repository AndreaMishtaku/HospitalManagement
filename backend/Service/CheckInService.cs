using AutoMapper;
using Entities.Models;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Shared.DTO.Request;
using Shared.Enums;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class CheckInService : ICheckInService
{

    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;


    public CheckInService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }

    public async Task<BaseResponse> CreateCheckIn(CheckInDTO checkInDTO)
    {
        try
        {

            var reservation = await _repositoryManager.ReservationRepository.GetRecordByIdAsync(checkInDTO.ReservationId);
            if(reservation is null)
            {
                throw new NotFoundException("Rezervimi nuk egziston");
            }


            if (!(TimeSpan.Parse(checkInDTO.CheckInStartTime) >= reservation.StartTime && TimeSpan.Parse(checkInDTO.CheckInStartTime) < reservation.StartTime.Add(reservation.ServiceDoctor.Service.Duration).Subtract(TimeSpan.FromMinutes(1))))
            {
                throw new BadRequestException("Ora e vendosur nuk eshte brenda intervalit te rezervimit");
            }

            reservation.Status = ReservationStatusEnum.Succeded;

            _repositoryManager.ReservationRepository.UpdateRecord(reservation);

            var checkIn = new CheckIn
            {
                ReservationId = checkInDTO.ReservationId,
                StartTime = TimeSpan.Parse(checkInDTO.CheckInStartTime)
            };

            _repositoryManager.CheckInRepository.CreateRecord(checkIn);

            await _repositoryManager.SaveAsync();


            return new BaseResponse
            {
                Result = true,
                Message = "Pajisja u shtua",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(CreateCheckIn), ex.Message));
            throw new BadRequestException(ex.Message);

        }
    }
}
