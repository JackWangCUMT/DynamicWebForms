using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaForms.Options;

namespace AlpacaForms.Questions
{
    public class Base : BaseOptions
    {
        public string type { get; set; }
        public string title { get; set; }
        public bool required { get; set; }
        public string @default { get; set; }
        public string description { get; set; }
        public string format { get; set; }

    }
}
