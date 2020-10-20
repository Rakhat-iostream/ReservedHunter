using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<Company> GetCompanyByName(string name);
    }
}
