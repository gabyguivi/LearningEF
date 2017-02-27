using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Mapping
{
    public class ApplicationMap : EntityTypeConfiguration<Application>
    {
        public ApplicationMap()
        {
            //key            
            HasKey(t => t.application_id);
            Property(t => t.application_id).HasMaxLength(32);
            Property(t => t.display_name).HasMaxLength(25);
            Property(t => t.secret).HasMaxLength(25);            
            //table
            ToTable("application");
        }
    }
}
