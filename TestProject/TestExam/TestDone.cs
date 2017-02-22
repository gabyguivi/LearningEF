using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExam.Model
{
    public class TestDone : EntityInt
    {
        public int UserId { get; set; }        
        public User User { get; set; }
        public int TestExamId { get; set; }
        public TestExam Test { get; set; }
        public double Gradle { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
