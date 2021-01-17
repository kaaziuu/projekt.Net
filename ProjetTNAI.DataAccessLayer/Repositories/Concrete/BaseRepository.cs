using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities;
using ProjetTNAI.Entities.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ProjetTNAI.DataAccessLayer.Repositories.Concrete
{
    public class BaseRepository : IPlanKeyValidator
    {
        protected AppDbContext context;

        public BaseRepository()
        {
            context = AppDbContext.Create();
        }

        public async Task<bool> KeyIsValid(int? planId, string editKey)
        {
            if (planId == null)
                return false;

            var plan = await context.Plany.FirstOrDefaultAsync(x => x.Id == planId.Value && x.KluczEdycji.Equals(editKey));

            return plan != default(Plan);
        }
    }
}