using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Options
{
    /// <summary>
    /// used for any control that accepts text
    /// </summary>
    public class TextInputOption
    {
        public TextInputOption()
        {
            this.type = "textarea";
        }
        public TextInputOption(string defaultText)
        {
            this.type = "textarea";
            this.@default = defaultText;
        }
        public string type { get; set; }
        public string placeholder { get; set; }
        public string @default { get; set; }
    }
}
