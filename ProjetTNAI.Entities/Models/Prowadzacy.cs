using System.Collections.Generic;

namespace ProjetTNAI.Entities.Models
{
    public class Prowadzacy
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }

        public ICollection<Zajecia> Zajecia { get; set; }
    }
}