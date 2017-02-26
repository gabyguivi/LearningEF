using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Model
{
    public class TestExam : EntityInt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PassScore { get; set; }
        public string TotalScore { get; set; }
        public ICollection<Question> Questions { get; set; }
        public int Duration { get; set; }
    }
}
