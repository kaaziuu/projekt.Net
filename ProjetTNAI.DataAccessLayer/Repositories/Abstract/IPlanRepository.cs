using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.DataAccessLayer.Repositories.Abstract
{
    public interface IPlanRepository
    {
        Task<Plan> GetPlanAsync(int id);
        Task<List<Plan>> GetWielePlanowAsync();
        Task<bool> ZapiszPlanAsync(Plan plan);
        Task<bool> UsunPlanAsync(Plan plan);
    }
}