using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Interfaces
{
    public interface IUserAuthRepository<T>
    {
        public Task<T> Register(T user, string password);
        public Task<T> Login(string email, string password);
        public Task<bool> UserExists(T user);
    }
}
