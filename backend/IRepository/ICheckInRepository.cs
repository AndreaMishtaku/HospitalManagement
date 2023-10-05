using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository;

public interface ICheckInRepository
{
    void CreateRecord(CheckIn checkIn);
}
