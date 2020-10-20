using NLog;
using StudentPositionHunters.Data;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories.Services
{
    public class StudentsHashService : DataHashService<Student>
    {
        public override bool CheckHashedPassword(Student user, string password)
        {
            using var hmac = new HMACSHA512(user.SaltPassword);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(user.HashedPassword);
        }
    }
}
