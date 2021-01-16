using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.DataAccessLayer.Repositories.Concrete
{
    public class PlanRepository : BaseRepository, IPlanRepository
    {
        public async Task<Plan> GetPlanAsync(int id)
        {
            return await context.Plany.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<Plan>> GetWielePlanowAsync()
        {
            return await context.Plany.ToListAsync();
        }

        public async Task<bool> UsunPlanAsync(Plan plan)
        {
            if (plan == null) return false;

            context.Plany.Remove(plan);

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

        public async Task<bool> CheckKey(Plan plan)
        {
            var currentPlan = await context.Plany.AsNoTracking().FirstOrDefaultAsync(x => x.Id == plan.Id);
            if (currentPlan.KluczEdycji == plan.KluczEdycji)
            {
                return true;
            }
            
            return false;
        }


        public async Task<bool> ZapiszPlanAsync(Plan plan)
        {
            if (plan == null) return false;

            try
            {
                context.Entry(plan).State = plan.Id == default(int) ? EntityState.Added : EntityState.Modified;

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