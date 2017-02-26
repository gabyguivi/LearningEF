using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //key
            HasKey(t => t.Id);            
            //table
            ToTable("Users");
        }
    }
}
