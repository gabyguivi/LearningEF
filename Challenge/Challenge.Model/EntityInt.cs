using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model.Interfaces;

namespace Challenge.Model
{
    public abstract class EntityInt : IEntityInt
    {
        public virtual int Id { get; set; }
    }
}
