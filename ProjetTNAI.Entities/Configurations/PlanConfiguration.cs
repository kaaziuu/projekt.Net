using ProjetTNAI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetTNAI.Entities.Configurations 
{
    public class PlanConfiguration: EntityTypeConfiguration<Plan>
    {
        public PlanConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            // Bo zajecia muszą być przypisane do jakiegoś planu
            HasMany(x => x.ZajeciaWPlanie).WithRequired(x => x.Plan).HasForeignKey(x=>x.PlanId);
        
        }
    }
}