﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Model.Interfaces;

namespace TestExam.Model
{
    public abstract class EntityInt : IEntityInt
    {
        public virtual int Id { get; set; }
    }
}