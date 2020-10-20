using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndustrialStudentPositionHunters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using WebApplication2.DTO;

namespace StudentPositionHunters.Controllers
{
    [Authorize]
    [Route("users/[controller]/")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IEntityRepository<Student> _repos;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public StudentsController(IEntityRepository<Student> repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IList<StudentDTO>> GetAllStudents()
        {
            logger.Info("Getting all students from database");
            return (await _repos.GetAll()).Select(st => new StudentDTO() { FirstName = st.FirstName, Email = st.Email,
                LastName = st.LastName, ID = st.StudentId }).ToList();
        }
        [HttpGet("id={id}")]
        public async Task<Student> GetStudentAsync(int id)
        {
            return await _repos.GetById(id);
        }
        

        [HttpPut("id={id}/update/email")]
        public async Task<IActionResult> UpdateStudentEmailAsync(StudentDTO st, int id)
        {
            if(!ModelState.IsValid) return BadRequest("Fill all fields");
            Student student = await _repos.GetById(id);
                if (student != null)
                {
                    student.Email = st.Email;
                        if (await _repos.Update(student))
                        {
                            return Ok("Updated student");
                        }

                    return BadRequest("Failed to update student");
                }
                return BadRequest("This student doesn't exist");
        }

        [HttpPut("id={id}/update/password")]
        public async Task<IActionResult> UpdateStudentPasswordAsync(StudentDTO st, int id)
        {
            if (!ModelState.IsValid) return BadRequest("Fill all fields");
            Student student = await _repos.GetById(id);
            if (await _repos.HasEntity(student))
            {
                if (await (_repos as StudentRepository).UpdatePassword(student, st.Password)) return Ok("Updated");
                return BadRequest("Failed to update employer");
            }
            return BadRequest("This employer is not in database");
        }


        [HttpDelete("id={id}/delete")]
        [Authorize(Roles = RoleType.Admin)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            Student student = await _repos.GetById(id);
            if (student == null) return BadRequest("This student is already deleted");
            if (await _repos.Delete(student))
            {
                return Ok("Deleted student");
            }
            return BadRequest("Failed to delete student");
        }
    }
}
