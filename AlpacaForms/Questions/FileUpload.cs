using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Questions
{
    public class FileUpload : Base
    {
        public string format { get; set; }
        public FileUpload() { type = "file"; format = "uri"; }
    }
}
