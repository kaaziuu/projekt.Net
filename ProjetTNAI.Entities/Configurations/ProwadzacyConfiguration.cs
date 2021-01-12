using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.Entities.Configurations
{
    public class ProwadzacyConfiguration : EntityTypeConfiguration<Prowadzacy>
    {
        public ProwadzacyConfiguration()
        {
            Property(X => X.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            // Reszta parametrów w prowadzącym może być domyślnie
        }
    }
}
