using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Data;
using StudentPositionHunters.Models;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Repositories
{
    public class CompaniesRepository : IEntityRepository<Company>, ICompanyRepository
    {
        private readonly EntityContext _context;
        public CompaniesRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Company entity)
        {
            _context.Companies.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Company entity)
        {
            _context.Companies.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<Company>> GetAll()
        {
            var companies = await _context.Companies.AsQueryable().Include(company => company.Employers).
               OrderBy(company => company.CompanyId).ToListAsync();
            return companies;
        }

        public async Task<Company> GetById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            company.Employers = await _context.Employers.AsQueryable().Where(emp => emp.CompanyName == company.Name).ToListAsync();
            return company;
        }
       
        public async Task<bool> HasEntity(Company entity)
        {
            return await _context.Companies.AnyAsync(company => company.Name == entity.Name);
        }

        public async Task<bool> Update(Company entity)
        {
            _context.Companies.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        
        public async Task<Company> GetCompanyByName(string name)
        {
            return await _context.Companies.FirstOrDefaultAsync(company => company.Name == name);
        }
    }
}
