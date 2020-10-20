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
using System.Threading;
using System.Threading.Tasks;
using WebApplication2.DTO;

namespace IndustrialStudentPositionHunters.Controllers.API.Auth
{
    [Route("users/employers/auth/")]
    [ApiController]
    public class EmployersAuthentificationController : ControllerBase
    {
        private readonly IUserAuthRepository<Employer> _authRepos;
        private readonly ICompanyRepository _companyRepos;
        private IConfiguration Configuration { get; }
        public EmployersAuthentificationController(IUserAuthRepository<Employer> authRepos, IConfiguration config, ICompanyRepository companyRepos)
        {
            _authRepos = authRepos;
            Configuration = config;
            _companyRepos = companyRepos;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterEmployer(EmployerDTO em)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var employer = new Employer()
            {
                FirstName = em.FirstName,
                LastName = em.LastName,
                Email = em.Email,
                Role = "user",
                CompanyName = em.CompanyName
            };
            var company = await _companyRepos.GetCompanyByName(em.CompanyName);
            if (company == null) return BadRequest(new { message = "Such company doesn't exist" });
            if (await _authRepos.UserExists(employer)) return BadRequest(new { message = "This user already exists" });
            employer.CompanyId = company.CompanyId;
            employer.CompanyName = company.Name;
            employer = await _authRepos.Register(employer, em.Password);
            if (employer == null) return BadRequest(new { message = "Failed to register" });
            var identity = await CreateEmployerIdentity(em.Email, em.Password);
            string token = TokenCreatingService.CreateToken(identity, out string identityName, Configuration);

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", employer.EmployerId);
            HttpContext.Session.SetString("Name", employer.FirstName);
            HttpContext.Session.SetString("Company", employer.CompanyName);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginEmployer(EmployerLoginDTO em)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Fill all fields" });
            var employer = await CreateEmployerIdentity(em.Email, em.Password);
            if (employer == null) return Unauthorized(new { message = "Username or password is incorrect" });
            string token = TokenCreatingService.CreateToken(employer, out string identityName, Configuration);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("Id", int.Parse(employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value));
            HttpContext.Session.SetString("Name", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value);
            HttpContext.Session.SetString("Company", employer.Claims.SingleOrDefault(c => c.Type == ClaimTypes.UserData).Value);
            return Ok(new { token = token, name = identityName });
        }

        [HttpPost("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return LocalRedirect("~/home/index");
        }

        private async Task<ClaimsIdentity> CreateEmployerIdentity(string email, string password)
        {
            var employer = await _authRepos.Login(email, password);
            if (employer != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, employer.EmployerId.ToString()),
                    new Claim(ClaimTypes.Name, employer.FirstName),
                    new Claim(ClaimTypes.Email, employer.Email),
                    new Claim(ClaimTypes.Role, employer.Role),
                    new Claim(ClaimTypes.UserData, employer.CompanyName),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
                return claimsIdentity;
            }
            return null;
        }

    }
}
