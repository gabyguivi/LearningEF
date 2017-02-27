using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Model
{
    public class Log : Entity
    {
        [Key]
        public int log_id { get; set; }
        public string logger { get; set; }
        public string level { get; set; }
        public string message { get; set; }
        public string application_id { get; set; }        
        public virtual Application application { get; set; }        
    }
}
