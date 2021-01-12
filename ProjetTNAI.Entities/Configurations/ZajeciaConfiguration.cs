using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetTNAI.Entities.Configurations
{
    public class ZajeciaConfiguration : EntityTypeConfiguration<Zajecia>
    {
        public ZajeciaConfiguration()
        {
            Property(X => X.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            HasMany(x => x.Prowadzacy).WithMany(x => x.Zajecia);
        }
    }
}
