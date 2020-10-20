using Microsoft.EntityFrameworkCore;
using StudentPositionHunters.Data;
using StudentPositionHunters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPositionHunters.Repositories
{
    public class AdvertisementsRepository : IEntityRepository<Advertisement>
    {
        private readonly EntityContext _context;
        public AdvertisementsRepository(EntityContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Advertisement entity)
        {
            _context.Advertisements.Add(entity);
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount++;
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> HasEntity(Advertisement entity)
        {
            return await _context.Advertisements.AnyAsync(Advertisement => Advertisement.Title == entity.Title);
        }
        public async Task<bool> Delete(Advertisement entity)
        {
            _context.Advertisements.Remove(entity);
            var position = await _context.Positions.FirstOrDefaultAsync(pos => pos.Name == entity.PositionName);
            position.Amount--;
            _context.Positions.Update(position);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<Advertisement>> GetAll()
        {
          return await _context.Advertisements.AsQueryable().Include(adv => adv.Employer).
                OrderBy(adv => adv.AdvertisementId).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsByEmployerId(int id)
        {
           return await _context.Advertisements.AsQueryable().
                Where(adv => adv.EmployerId == id).Include(adv => adv.Employer).ToListAsync();
        }

        public async Task<IList<Advertisement>> GetAdvertisementsForPosition(string pos)
        {
            return await _context.Advertisements.AsQueryable().Where(adv => adv.PositionName == pos).ToListAsync();
        }

        public async Task<Advertisement> GetById(int id)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            ad.Employer = await _context.Employers.FirstOrDefaultAsync(emp => emp.EmployerId == ad.EmployerId);
            return ad;
        }

        public async Task<bool> Update(Advertisement entity)
        {
            _context.Advertisements.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
