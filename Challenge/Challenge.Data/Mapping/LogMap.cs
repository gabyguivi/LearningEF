using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            //key
            HasKey(t => t.log_id);
            Property(t => t.level).HasMaxLength(256);
            Property(t => t.logger).HasMaxLength(256);
            Property(t => t.message).HasMaxLength(2048);
            //relation
            HasRequired(t => t.application).WithMany().HasForeignKey(t => t.application_id);
            //table
            ToTable("log");
        }
    }
}
