using System.Collections.Generic;

namespace ProjetTNAI.Entities.Models
{
    public class Zajecia
    {
        public int Id { get; set; }

        public int Godzina { get; set; }
        public int CzasTrwania {get; set;}
        public int DzienTygodnia { get; set; }

        public string LinkDoZajec { get; set; }

        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }

        public ICollection<Prowadzacy> Prowadzacy { get; set; }
    }
}