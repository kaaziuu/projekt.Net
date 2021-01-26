using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ProjetTNAI.Entities.Models
{
    public class Zajecia
    {
        
        public int Id { get; set; }

        public int Godzina { get; set; }
        public int CzasTrwania { get; set; }
        public int DzienTygodnia { get; set; } = 1;


            [NotMapped]
        public DateTime Poczatek => new DateTime(1970, 6, DzienTygodnia).AddSeconds(Godzina);
        
        [NotMapped]
        public DateTime Koniec => Poczatek + TimeSpan.FromSeconds(CzasTrwania);

        public string Nazwa { get; set; }

        public string LinkDoZajec { get; set; }

        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }

        public ICollection<Prowadzacy> Prowadzacy { get; set; }
    }
}