using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


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
    }
}
