using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.DataAccessLayer.Repositories.Concrete
{
    public class ZajeciaRepository : BaseRepository, IZajeciaRepository
    {
        public async Task<List<Zajecia>> GetWieleZajecAsync()
        {
            return await context.Zajecia.ToListAsync();
        }

        public async Task<Zajecia> GetZajeciaAsync(int id)
        {
            return await context.Zajecia.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UsunZajeciaAsync(Zajecia zajecia)
        {
            if (zajecia == null) return false;

            context.Zajecia.Remove(zajecia);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ZapiszZajeciaAsync(Zajecia zajecia)
        {
            if (zajecia == null) return false;

            try
            {
                context.Entry(zajecia).State = zajecia.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
