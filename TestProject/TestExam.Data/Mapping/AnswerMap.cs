using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Model;

namespace TestExam.Data.Mapping
{
    public class AnswerMap : EntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            //key
            HasKey(t => t.Id);
            //relation
            HasRequired(t => t.Question) 
                        .WithMany(t => t.Answers).HasForeignKey(t=>t.QuestionId); 
            //table
            ToTable("Answers");
        }
    }
}
