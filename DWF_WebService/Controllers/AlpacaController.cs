using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Reflection;
using System.IO;
using System.Dynamic;
using AlpacaForms;


namespace DWF_WebService.Controllers
{
    public class AlpacaController : ApiController
    {
        public IHttpActionResult GetAllForms()
        {

            dynamic obj = new ExpandoObject();
            obj.title = "Does this work?";
            obj.type = "object";
            obj.properties = new ExpandoObject();
            obj.properties.name = new AlpacaForms.Questions.TextInput();
            obj.properties.name.title = "Who are you?";

            var test = new AlpacaForms.Questions.MultipleChoice();
            test.title = "Choose a ranking";
            test.@enum.Add("Great");
            test.@enum.Add("Not so great");
            test.@enum.Add("Shit");


            var dictionary = (IDictionary<string, object>)obj.properties; 

            dictionary.Add("ranking", test);


            return Ok(obj);
        }
    }
}
