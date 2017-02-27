using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Model
{
    public class Application : Entity
    {
        [Key]
        public string application_id { get; set; }
        public string display_name { get; set; }
        public string secret { get; set; }
    }
}
