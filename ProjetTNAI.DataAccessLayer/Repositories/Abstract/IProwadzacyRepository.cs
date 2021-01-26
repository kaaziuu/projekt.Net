using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.DataAccessLayer.Repositories.Abstract
{
    public interface IProwadzacyRepository : IPlanKeyValidator
    {
        Task<Prowadzacy> GetProwadzacyAsync(int id);
        Task<List<Prowadzacy>> GetWieluProwadzacychAsync();
        Task<List<Prowadzacy>> GetProwadzacyWPlanieAsync(int planId);

        Task<bool> ZapiszProwadzacyAsync(Prowadzacy prowadzacy);
        Task<bool> UsunProwadzacyAsync(Prowadzacy prowadzacy);
        Task<List<Prowadzacy>> getLectures(int classId);

    }
}