using IndustrialStudentPositionHunters.Models;
using IndustrialStudentPositionHunters.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Data;
using StudentPositionHunters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndustrialStudentPositionHunters.Repositories
{
    public class PositionsRepository : IEntityRepository<Position>, IPositionsRepository
    {
        private readonly EntityContext _context;
        public PositionsRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Position entity)
        {
            _context.Positions.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Position entity)
        {
            _context.Positions.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<Position>> GetAll()
        {
           return await _context.Positions.AsQueryable().Include(pos => pos.Advertisements).ToListAsync();
        }

        public async Task<Position> GetById(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            position.Advertisements = await _context.Advertisements.Where(ads => ads.PositionId == position.PositionId).ToListAsync();
            return position;
        }

        public Task<Position> GetPositionById(int id)
        {
            return GetById(id);
        }

        public async Task<Position> GetPositionByName(string name)
        {
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == name);
            position.Advertisements = await _context.Advertisements.Where(ads => ads.PositionId == position.PositionId).ToListAsync();
            return position;
        }

        public async Task<bool> HasEntity(Position entity)
        {
            return await _context.Positions.AnyAsync(pos => pos.Name == entity.Name);
        }

        public async Task<bool> Update(Position entity)
        {
            _context.Positions.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
