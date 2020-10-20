using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;

namespace IndustrialStudentPositionHunters.Controllers
{
    [Route("users/students/id={id}/resume/")]
    [Authorize]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IEntityRepository<Resume> _repos;
        private readonly IWebHostEnvironment _env;
        public ResumesController(IEntityRepository<Resume> repos, IWebHostEnvironment environment)
        {
            _repos = repos;
            _env = environment;
        }

        [HttpGet]
        public async Task<Resume> GetResumeByStudentIdAsync(int id)
        {
            return await _repos.GetById(id);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddResume(IFormFile file)
        {
            if(file != null)
            {
                string path = "/Resumes/" + file.FileName;
                using(var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                Resume resume = new Resume() { Name = file.FileName, Path = path };
                if (await _repos.Create(resume)) return Ok("Uploaded new file");
                return BadRequest("Failed to upload file");
            }

            return BadRequest("You didn't upload file");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateResume(IFormFile file, int id)
        {
            if(file != null)
            {
                Resume resume = await GetResumeByStudentIdAsync(id);
                if (await _repos.HasEntity(resume))
                {
                    if (await _repos.Delete(resume))
                    {
                        return await AddResume(file);
                    }
                }
                return BadRequest("Nothing to update");
            }

            return BadRequest("You didnt' upload file");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteResumeAsync(int id)
        {
            if (await _repos.Delete(await _repos.GetById(id))) return Ok("Deleted resume");
            return BadRequest("Failed to delete resume");
        }
    }
}
