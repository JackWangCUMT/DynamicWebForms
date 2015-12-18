using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Options
{
    public class HiddenOption
    {
        public HiddenOption(bool hidden)
        {
            this.hidden = hidden;
        }

        public bool hidden { get; set; }

    }
}
