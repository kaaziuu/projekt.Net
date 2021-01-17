using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.DataAccessLayer.Repositories.Abstract
{
    public interface IPlanKeyValidator
    {
        Task<bool> KeyIsValid(int? planId, string editKey);
    }
}
