using ProjetTNAI.Entities;

namespace ProjetTNAI.DataAccessLayer.Repositories.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext context;

        public BaseRepository()
        {
            context = AppDbContext.Create();
        }
    }
}