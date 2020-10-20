using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndustrialStudentPositionHunters;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using WebApplication2.DTO;
using WebApplication2.Repositories;

namespace StudentPositionHunters.Controllers
{
    [Route("users/[controller]/")]
    [Authorize]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEntityRepository<Employer> _repos;
        public EmployersController(IEntityRepository<Employer> repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IList<EmployerDTO>> GetAllEmployersAsync()
        {
            return (await _repos.GetAll()).Select(emp => new EmployerDTO()
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                CompanyName = emp.CompanyName,
                ID = emp.EmployerId
            }).ToList();
        }
        [HttpGet("id={id}")]
        public async Task<Employer> GetEmployerAsync(int id)
        {
            return await _repos.GetById(id);
        }


        [HttpPut("id={id}/update/email")]
        public async Task<IActionResult> UpdateEmployerEmailAsync(EmployerDTO em, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
                Employer employer = await _repos.GetById(id);
                if (await _repos.HasEntity(employer))
                {
                    employer.Email = em.Email;
                    if (await _repos.Update(employer)) return Ok("Updated employer");
                    return BadRequest("Failed to update employer");
                }
         return BadRequest("This employer is not in database");
        }

        [HttpPut("id={id}/update/company")]
        public async Task<IActionResult> UpdateEmployerCompanyAsync(EmployerDTO em, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Employer employer = await _repos.GetById(id);
            if (await _repos.HasEntity(employer))
            {
                employer.CompanyName = em.CompanyName;
                if (await _repos.Update(employer)) return Ok("Updated employer");
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }

        [HttpPut("id={id}/update/password")]
        public async Task<IActionResult> UpdateEmployerPasswordAsync(EmployerDTO st, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Employer employer = await _repos.GetById(id);
            if (await _repos.HasEntity(employer))
            {
                if (await (_repos as EmployersRepository).UpdatePassword(employer, st.Password)) return Ok(new { message = "Updated"});
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }


        [HttpDelete("id={id}/delete")]
        public async Task<IActionResult> DeleteEmployerAsync(int id)
        {
            Employer employer = await _repos.GetById(id);
            if (employer == null) return BadRequest("This employer is already deleted");
            if(await _repos.Delete(employer))
            return Ok("Deleted employer");
            return BadRequest("Failed to delete employer");
        }
    }
}
