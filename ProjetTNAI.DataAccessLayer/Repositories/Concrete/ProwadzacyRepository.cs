using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.DataAccessLayer.Repositories.Concrete
{
    public class ProwadzacyRepository : BaseRepository, IProwadzacyRepository
    {
        public async Task<Prowadzacy> GetProwadzacyAsync(int id)
        {
            return await context.Prowadzacy.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Prowadzacy>> GetWieluProwadzacychAsync()
        {
            return await context.Prowadzacy.ToListAsync();
        }

        public async Task<List<Prowadzacy>> GetProwadzacyWPlanieAsync(int planId)
        {
            return await context.Prowadzacy.Where(x => x.Zajecia.Any(y => y.PlanId == planId)).ToListAsync();
        }

        public async Task<bool> UsunProwadzacyAsync(Prowadzacy prowadzacy)
        {
            if (prowadzacy == null) return false;

            context.Prowadzacy.Remove(prowadzacy);

            try
            {
                await context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ZapiszProwadzacyAsync(Prowadzacy prowadzacy)
        {
            if (prowadzacy == null) return false;

            try
            {
                context.Entry(prowadzacy).State = prowadzacy.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Prowadzacy>> getLectures(int classId)
        {
            return await context.Prowadzacy.Where(x => x.Zajecia.Any(y => y.Id == classId)).ToListAsync();
        }
    }
}