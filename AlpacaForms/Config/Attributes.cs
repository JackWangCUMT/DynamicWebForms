using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlpacaForms.Config
{
    /// <summary>
    ///  The attributes section of an Alpaca form:
    /// 
    ///    "attributes":{
    ///      "action":"../../endpoints/echo.php",
    ///      "method":"post"
    ///     }
    /// </summary>
    public class Attributes
    {
        public string action { get; set; }
        public string method { get; set; }

    }
}
