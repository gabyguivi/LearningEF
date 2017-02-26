using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Model
{
    public class Answer : EntityInt
    {
        public string Description { get; set; }
        public int QuestionId { get; set; }        
        public Question Question { get; set; }
        public int Order { get; set; }
        public bool Correct { get; set; }
    }
}
