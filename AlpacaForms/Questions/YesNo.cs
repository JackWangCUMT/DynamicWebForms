using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Questions
{
    public class YesNo : Base
    {
        public string rightLabel { get; set; }
        public YesNo()
        {
            type = "boolean";
        }
    }
}
