using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace AlpacaForms.Config
{
    public class Buttons
    {
        public object submit { get; set; }
        public object reset { get; set; }

        public Buttons()
        {
            submit = new Object();
            reset = new Object();
        }
    }
}
