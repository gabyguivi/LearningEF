using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Model
{
    public class Question : EntityInt
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasMany { get; set; }
        public int Order { get; set; }
        public int TestExamId { get; set; }        
        public TestExam TestExam { get; set; }
        public ICollection<Answer> Answers {get;set;}
    }
}
