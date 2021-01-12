using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.Entities.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Rok { get; set; }
        public string Grupa { get; set; }
        public string KluczEdycji { get; set; }

        public virtual ICollection<Zajecia> ZajeciaWPlanie { get; set; }
    }
}
