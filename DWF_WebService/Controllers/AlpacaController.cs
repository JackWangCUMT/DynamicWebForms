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
            dynamic obj = new ExpandoObject(); //the main object to be returned   
            obj.title = "This form rocks";
            obj.type = "object";

            obj.properties = new ExpandoObject(); //the form data
            obj.fields = new ExpandoObject(); //the options (set as each control is created)
            obj.form = new AlpacaForms.Config.Form(); 

            obj.fields.buttons = new AlpacaForms.Config.Buttons();


            //text input
            obj.properties.name = new AlpacaForms.Questions.TextInput();
            obj.properties.name.title = "Who are you?";

            //radio button
            var test = new AlpacaForms.Questions.MultipleChoice();
            test.title = "Choose a ranking";
            test.@enum.Add("Great");
            test.@enum.Add("Not so great");
            test.@enum.Add("Shit");

            var dictionary = (IDictionary<string, object>)obj.properties; 
            dictionary.Add("ranking", test);

            //select
            var test2 = new AlpacaForms.Questions.MultipleChoice();
            test2.title = "Choose a second ranking";
            test2.@enum.Add("Great");
            test2.@enum.Add("Not so great");
            test2.@enum.Add("Really Shit");
            test2.@enum.Add("Utter Shit");
            test2.@enum.Add("Dog Shit");
            test2.@enum.Add("Arsenal");

            dictionary.Add("slider", test2);

            //yes, no
            obj.properties.member = new  AlpacaForms.Questions.Boolean();
            obj.properties.member.title = "Male or Female";

            obj.fields.member = new ExpandoObject();
            obj.fields.member.rightLabel = "Which one?";

            //text area
            obj.properties.comments = new AlpacaForms.Questions.TextAreaInput();
            obj.properties.comments.title = "Enter comments";
            obj.properties.comments.@default = "This is default text";

            obj.fields.comments = new ExpandoObject();
            obj.fields.comments.type = "textarea";
            obj.fields.comments.placeholder = "Some text here";


            return Ok(obj);
        }
    }
}
