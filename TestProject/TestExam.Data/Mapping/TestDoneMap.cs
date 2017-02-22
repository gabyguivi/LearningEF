using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Model;

namespace TestExam.Data.Mapping
{
    public class TestDoneMap : EntityTypeConfiguration<TestDone>
    {
        public TestDoneMap()
        {
            //key
            HasKey(t => t.Id);
            //relation
            HasRequired(t => t.Test).WithMany().HasForeignKey(t => t.TestExamId);
            HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId);
            //table
            ToTable("TestsDone");
        }
    }
}
