﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Questions
{
    public class Boolean : Base
    {
        public string description { get; set; }
        public Boolean()
        {
            type = "boolean";
        }
    }
}
