using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Options
{
    /// <summary>
    /// Base options for each control
    /// Respresented by the fields node in the JSON
    /// </summary>
    public class BaseOptions
    {
        public bool @readonly { get; set; }
        public bool hidden { get; set; }
    }
}
