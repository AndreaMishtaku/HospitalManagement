using AutoMapper;
using Exceptions;
using IRepository;
using IService;
using LoggerService;
using Shared.DataTableColumns;
using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.RequestFeatures;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class ClientService : IClientService
{


    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;


    public ClientService(
       ILoggerManager logger,
       IMapper mapper,
       IRepositoryManager repositoryManager
        )
    {
        _logger = logger;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
    }
    public async Task<PagedListResponse<IEnumerable<ClientDTO>>> GetAllClientsWithPagination(LookupDTO filter)
    {
        try
        {
            var clientWithMetaData = await _repositoryManager.ClientRepository.GetClientsWithMetaData(filter);
            var columns = GetDataTableColumns();

            PagedListResponse<IEnumerable<ClientDTO>> response = new PagedListResponse<IEnumerable<ClientDTO>>
            {
                RowCount = clientWithMetaData.MetaData.TotalCount,
                Page = clientWithMetaData.MetaData.CurrentPage,
                PageSize = clientWithMetaData.MetaData.PageSize,
                Columns = columns,
                Rows = clientWithMetaData,
                HasNext = clientWithMetaData.MetaData.HasNext,
                HasPrevious = clientWithMetaData.MetaData.HasPrevious,
                TotalPages = clientWithMetaData.MetaData.TotalPages
            };
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllClientsWithPagination), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }

    public async Task<ClientDTO> GetClientByIdentityNumber(ClientIdentityNumberDTO nr)
    {
        try
        {
            var client = await _repositoryManager.ClientRepository.GetRecordByIdentityNumber(nr.IdentityNumber);

            if(client is null)
            {
                throw new NotFoundException("Klienti me kete numer identifikimi nuk ekziston");
            }

            var clientDTO = new ClientDTO
            {
                IdentityNumber = client.IdentityNumber,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthday = client.Birthday.ToString(),
                Id = client.Id,
                Gender = client.Gender,
            };

                
            return clientDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(string.Format("{0}: {1}", nameof(GetAllClientsWithPagination), ex.Message));
            throw new BadRequestException(ex.Message);
        }
    }


    private List<DataTableColumn> GetDataTableColumns()
    {
        // get the columns
        var columns = GenerateDataTableColumn<ClientColumns>.GetDataTableColumns();

        // return all columns
        return columns;
    }

}
