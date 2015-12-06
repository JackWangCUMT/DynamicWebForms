using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AlpacaForms.Config
{

    /// <summary>
    /// represents the form object that describes header form information,
    /// such as POST address and buttons for an Alpaca Form
    /// 
    /// "form":{
    ///      "attributes":{
    ///          "action":"../../endpoints/echo.php",
    ///          "method":"post"
    ///       },
    ///       "buttons":{
    ///          "submit":{},
    ///          "reset":{}
    ///       }
    /// },
    /// "fields":{
    ///    "name":{
    ///          "hideInitValidationError" : true
    ///      },
    ///      "photo":{
    ///         "type":"file",
    ///           "styled": true
    ///      },
    ///      "member":{
    ///          "rightLabel":"Alpaca Club Member"
    ///     },
    ///      "address":{
    ///         "type":"address",
    ///            "addressValidation":true
    ///     }       
    //   }

    /// </summary>
    public class Form
    {
        public Buttons buttons { get; set; }

        public Attributes attributes { get; set; }

        public Form() 
        {
            buttons = new Buttons();
            attributes = new Attributes();
        }

    }
}
