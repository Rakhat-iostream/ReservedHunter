using IndustrialStudentPositionHunters.Repositories.Interfaces;
using IndustrialStudentPositionHunters.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Data;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPositionHunters.Repositories
{
    public class EmployersRepository : IEntityRepository<Employer>, IUserAuthRepository<Employer>
    {
        private readonly EntityContext _context;
        private readonly DataHashService<Employer> hashService = new EmployersHashService();
        public EmployersRepository(EntityContext context)
        {
            _context = context;
        }

        public async Task<IList<Employer>> GetAll()
        {
           return await _context.Employers.AsQueryable().Include(emp => emp.Company).Include(emp => emp.Advertisements).
                OrderBy(emp => emp.EmployerId).ToListAsync();
        }
        public async Task<Employer> GetById(int id)
        {
            var employer = await _context.Employers.FindAsync(id);
            employer.Advertisements = await _context.Advertisements.AsQueryable().Where(adv => adv.EmployerId == id).ToListAsync();
            return employer;
        }
        public async Task<bool> Create(Employer entity)
        {
            _context.Employers.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> HasEntity(Employer entity)
        {
            return await _context.Employers.AnyAsync(company => company.Email == entity.Email);
        }
        public async Task<bool> Delete(Employer entity)
        {
            _context.Employers.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Employer entity)
        {
            Employer employer = await GetById(entity.EmployerId);
            if(employer.CompanyName != entity.CompanyName)
            {
                Company company = await _context.Companies.FirstOrDefaultAsync(company => company.Name == entity.CompanyName);
                entity.CompanyId = company.CompanyId;
                entity.Company = company;
            }
            _context.Employers.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdatePassword(Employer entity, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            entity.HashedPassword = hashedPass;
            entity.SaltPassword = SaltPass;
            _context.Employers.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        //Auth

        public async Task<Employer> Register(Employer user, string password)
        {
            hashService.CreateHashedPassword(password, out byte[] hashedPass, out byte[] SaltPass);
            user.HashedPassword = hashedPass;
            user.SaltPassword = SaltPass;
            if (await Create(user)) return await _context.Employers.FirstOrDefaultAsync(emp => emp.Email == user.Email);
            return null;
        }

        public async Task<Employer> Login(string email, string password)
        {
            var user = await _context.Employers.FirstOrDefaultAsync(em => em.Email == email);
            if (user != null)
            {
                if (hashService.CheckHashedPassword(user, password)) return user;
            }
            return null;
        }

        public async Task<bool> UserExists(Employer user)
        {
            return await _context.Employers.AnyAsync(em => em.Email == user.Email);
        }
    }
}
