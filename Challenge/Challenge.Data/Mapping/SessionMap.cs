using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Mapping
{
    public class SessionTimeMap : EntityTypeConfiguration<SessionTime>
    {
        public SessionTimeMap()
        {
            //key
            HasKey(t => t.minutes);
            //table
            ToTable("session_time");
        }
    }
}
