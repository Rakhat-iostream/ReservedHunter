using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Data;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories
{
    public class ResumesRepository : IEntityRepository<Resume>
    {
        private readonly EntityContext _context;
        public ResumesRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<Resume> GetById(int StudentId)
        {
            return await _context.Resumes.FindAsync(StudentId);
        }

        public async Task<bool> Create(Resume resume)
        {
            _context.Resumes.Add(resume);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Resume resume)
        {
            _context.Resumes.Update(resume);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Resume resume)
        {
            _context.Resumes.Remove(resume);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasEntity(Resume resume)
        {
            return await _context.Resumes.AnyAsync(res => res.StudentId == resume.StudentId);
        }

        public async Task<IList<Resume>> GetAll()
        {
            return await _context.Resumes.AsQueryable().Include(res => res.Student).ToListAsync();
        }
    }
}
