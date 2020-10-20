using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Services
{
    public abstract class DataHashService<T>
    {
        public void CreateHashedPassword(string password, out byte[] hashedPassword, out byte[] SaltPassword)
        {
            using var hmac = new HMACSHA512();
            SaltPassword = hmac.Key;
            hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public abstract bool CheckHashedPassword(T user, string password);

    }

    
}
