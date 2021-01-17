using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.DataAccessLayer.Repositories.Abstract
{
    public interface IZajeciaRepository
    {
        Task<Zajecia> GetZajeciaAsync(int id);
        Task<List<Zajecia>> GetWieleZajecAsync();

        Task<bool> ZapiszZajeciaAsync(Zajecia zajecia);
        Task<bool> UsunZajeciaAsync(Zajecia zajecia);
    }
}