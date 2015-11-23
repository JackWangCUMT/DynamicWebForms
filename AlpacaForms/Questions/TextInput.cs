using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Questions
{
    /// <summary>
    /// A basic question with a free text answer
    /// </summary>
    public class TextInput : Base
    {
        public TextInput() { type = "string"; }
    }
}
