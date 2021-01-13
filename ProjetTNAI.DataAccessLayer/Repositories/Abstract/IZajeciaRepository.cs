using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
