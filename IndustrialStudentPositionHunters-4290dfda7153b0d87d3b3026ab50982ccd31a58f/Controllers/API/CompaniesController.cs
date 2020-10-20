using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndustrialStudentPositionHunters;
using IndustrialStudentPositionHunters.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IEntityRepository<Company> _repos;
        public CompaniesController(IEntityRepository<Company> repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public async Task<IList<Company>> GetAllCompanies()
        {
            return await _repos.GetAll();
        }

        [HttpGet("id={id}")]
        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> AddCompanyAsync(CompanyDTO com)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Company company = new Company { Name = com.Name };
            if (await _repos.HasEntity(company)) return BadRequest("Company is already in database");
            if (await _repos.Create(company)) return Ok("Added new company");
            return BadRequest("Failed to add company");
        }

        [HttpPut("id={id}/update")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyDTO com, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Company company = await GetCompanyByIdAsync(id);
            if (!(await _repos.HasEntity(company))) return BadRequest("Company is not in in database");
            company.Name = com.Name;
            if (await _repos.Update(company)) return Ok("Updated company");
            return BadRequest("Failed to add company");
        }

        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            Company company = await GetCompanyByIdAsync(id);
            if (company == null) return BadRequest("This company is already deleted");
            if (await _repos.Delete(company)) return Ok("Deleted company");
            return BadRequest("Failed to delete company");
        }
    }
}
