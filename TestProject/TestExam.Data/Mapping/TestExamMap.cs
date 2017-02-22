using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Model;

namespace TestExam.Data.Mapping
{
    public class TestExamMap : EntityTypeConfiguration<TestExam.Model.TestExam>
    {
        public TestExamMap()
        {
            //key
            HasKey(t => t.Id);
            //table
            ToTable("TestsExam");
        }
    }
}
