using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters
{
    public class AuthentificationOptions
    {
        public const string ISSUER = "AuthServer"; 
        public const string AUDIENCE = "AuthClient";
        public const int LIFETIME = 1;
    }
}
