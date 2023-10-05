using Entities.Models;
using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class CheckInRepository:RepositoryBase<CheckIn> ,ICheckInRepository
{
    public CheckInRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateRecord(CheckIn checkIn) => Create(checkIn);
}
