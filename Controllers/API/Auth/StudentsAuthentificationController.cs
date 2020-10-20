using IndustrialStudentPositionHunters.DTO.Authorization;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using IndustrialStudentPositionHunters.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.DTO;

namespace IndustrialStudentPositionHunters.Controllers.API.Auth
{
    [Route("users/students/auth/")]
    [ApiController]
    public class StudentsAuthentificationController : ControllerBase
    {
        private readonly IUserAuthRepository<Student> _authRepos;
        private IConfiguration Configuration { get; }
        public StudentsAuthentificationController(IUserAuthRepository<Student> authRepos, IConfiguration config)
        {
            _authRepos = authRepos;
            Configuration = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(StudentDTO st)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields"});
            var student = new Student()
            {
                FirstName = st.FirstName,
                LastName = st.LastName,
                Email = st.Email,
                Role = "user",
            };
            if (await _authRepos.UserExists(student)) return BadRequest(new { message = "This user already exists" });
            student = await _authRepos.Register(student, st.Password);
            if (student == null) return BadRequest(new { message = "Oops, failed to register" });
            var identity = await CreateIdentity(st.Email, st.Password);
            string token = TokenCreatingService.CreateToken(identity, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", student.StudentId);
            HttpContext.Session.SetString("Name", student.FirstName);
            HttpContext.Session.SetString("Role", student.Role);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginStudent(StudentLoginDTO st)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var student = await CreateIdentity(st.Email, st.Password);
            if (student == null) return Unauthorized(new { message = "Username or password is incorrect" });
            string token = TokenCreatingService.CreateToken(student, out string identityName, Configuration);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", int.Parse(student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
            HttpContext.Session.SetString("Name", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Role", student.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return LocalRedirect("~/home/index");
        }


        private async Task<ClaimsIdentity> CreateIdentity(string email, string password)
        {
            var student = await _authRepos.Login(email, password);
            if (student != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                    new Claim(ClaimTypes.Name, student.FirstName),
                    new Claim(ClaimTypes.Email, student.Email),
                    new Claim(ClaimTypes.Role, student.Role),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                return claimsIdentity;
            }
            return null;
        }
    }
}
