using System.Data.Entity.ModelConfiguration;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.Entities.Configurations
{
    public class ProwadzacyConfiguration : EntityTypeConfiguration<Prowadzacy>
    {
        public ProwadzacyConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            // Reszta parametrów w prowadzącym może być domyślnie
        }
    }
}