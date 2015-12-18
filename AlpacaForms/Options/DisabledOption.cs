using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Options
{
    public class DisabledOption
    {
        public DisabledOption(bool inp) { disabled = inp; }
        public bool disabled { get; set; }
    }
}
