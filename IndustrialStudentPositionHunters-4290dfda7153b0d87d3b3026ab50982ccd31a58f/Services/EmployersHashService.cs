using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Services
{
    public class EmployersHashService : DataHashService<Employer>
    {
        public override bool CheckHashedPassword(Employer user, string password)
        {
            using var hmac = new HMACSHA512(user.SaltPassword);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(user.HashedPassword);
        }
    }
}
