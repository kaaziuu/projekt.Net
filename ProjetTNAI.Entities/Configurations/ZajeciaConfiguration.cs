using System.Data.Entity.ModelConfiguration;
using ProjetTNAI.Entities.Models;

namespace ProjetTNAI.Entities.Configurations
{
    public class ZajeciaConfiguration : EntityTypeConfiguration<Zajecia>
    {
        public ZajeciaConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            HasMany(x => x.Prowadzacy).WithMany(x => x.Zajecia);
        }
    }
}