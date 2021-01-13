using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
