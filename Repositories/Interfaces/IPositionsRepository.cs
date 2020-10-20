using IndustrialStudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Interfaces
{
   public interface IPositionsRepository
    {
        public Task<Position> GetPositionByName(string name);
        public Task<Position> GetPositionById(int id);
    }
    
}
