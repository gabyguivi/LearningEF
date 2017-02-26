using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            //key
            HasKey(t => t.Id);
            //relation
            HasRequired(t => t.TestExam) 
                        .WithMany(t => t.Questions).HasForeignKey(t => t.TestExamId); 
            //table
            ToTable("Questions");
        }
    }
}
