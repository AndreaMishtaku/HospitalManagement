using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Request;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class ServiceRepository : RepositoryBase<Services>, IServiceRepository
{
    public ServiceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(Services service) => Create(service);

    public void DeleteRecord(Services service) => Delete(service);

    public async Task<Services> GetRecordByIdAsync(int id)=>
        await FindByCondition(c => c.Id.Equals(id))
        .SingleOrDefaultAsync();

    public void UpdateRecord(Services service) => Update(service);

    public async Task<IEnumerable<Services>> GetAllRecords()
    {
        return await FindAll().ToListAsync();
    }
    public async Task<IEnumerable<ServiceResponseDTO>> GetServicesInfo()
    {
        var services = await (from s in RepositoryContext.Services
                            select new ServiceResponseDTO
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Description = s.Description,
                                Duration=s.Duration.ToString()
                            }).ToListAsync();

        foreach (var service in services)
        {
            var workingHours = await (from wH in RepositoryContext.WorkingHours
                                    where wH.ServiceId == service.Id
                                    select new WorkingHoursDTO()
                                    {
                                        Weekday = wH.WeekDay,
                                        StartHour = wH.StartHour.ToString(),
                                        EndHour = wH.EndHour.ToString(),
                                    }).ToListAsync();


            var equipmentsService= await (from eS in RepositoryContext.ServiceEquipment
                                 where eS.ServiceId == service.Id
                                 select new EquipmentResponseDTO()
                                 {
                                     Id=eS.Equipment.Id,
                                     Name = eS.Equipment.Name,
                                     ProducedAt = eS.Equipment.ProducedAt,
                                 }).ToListAsync();
            service.WorkingHours = workingHours;
            service.Equipments = equipmentsService;
        }


        return services;
    }

    public async Task<ServiceResponseDTO> GetServiceInfoById(int serviceId)
    {
        var service = await (from s in RepositoryContext.Services
                             where s.Id==serviceId
                              select new ServiceResponseDTO
                              {
                                  Id = s.Id,
                                  Name = s.Name,
                                  Description = s.Description
                              }).SingleOrDefaultAsync();

        if (service is not null)
        {
            var workingHours = await (from wH in RepositoryContext.WorkingHours
                                      where wH.ServiceId == service.Id
                                      select new WorkingHoursDTO()
                                      {
                                          Weekday = wH.WeekDay,
                                          StartHour = wH.StartHour.ToString(),
                                          EndHour = wH.EndHour.ToString(),
                                      }).ToListAsync();


            var equipmentsService = await (from eS in RepositoryContext.ServiceEquipment
                                           where eS.ServiceId == service.Id
                                           select new EquipmentResponseDTO()
                                           {
                                               Id = eS.Equipment.Id,
                                               Name = eS.Equipment.Name,
                                               ProducedAt = eS.Equipment.ProducedAt,
                                           }).ToListAsync();
            service.WorkingHours = workingHours;
            service.Equipments = equipmentsService;
        }


        return service;
    }
}
