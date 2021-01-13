using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.DataAccessLayer.Repositories.Abstract
{
    public interface IProwadzacyRepository
    {
        Task<Prowadzacy> GetProwadzacyAsync(int id);
        Task<List<Prowadzacy>> GetWieluProwadzacychAsync();

        Task<bool> ZapiszProwadzacyAsync(Prowadzacy prowadzacy);
        Task<bool> UsunProwadzacyAsync(Prowadzacy prowadzacy); 
    }
}
