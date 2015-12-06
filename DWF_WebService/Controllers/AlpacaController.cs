﻿using System;
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
using AlpacaForms.DAL;
using AlpacaForms.Config;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Data;

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

            //buttons and submit config           
            obj.form = new AlpacaForms.Config.Form();
            obj.form.attributes.action = "api/FormPost";
            obj.form.attributes.method = "post";

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
            
            //number only
            obj.properties.age = new AlpacaForms.Questions.NumberInput();
            obj.properties.age.title = "Age";
            obj.properties.age.description = "How old are you?";
            obj.properties.age.required = true;
            
            //file upload
            obj.properties.fileUpload = new AlpacaForms.Questions.TextInput();
            obj.properties.fileUpload.title = "Upload your file here";
            obj.properties.fileUpload.format = "uri";

            obj.fields.fileUpload = new ExpandoObject();
            obj.fields.fileUpload.type = "file";

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

            //do the layout
           // obj.layout = new ExpandoObject();
            //obj.layout.template = "../Views/header.html";

            return Ok(obj);
        }

        public IHttpActionResult GetForm(int id)
        {
            try
            {
                DataSet ds = new Db().ReturnFormData(1);
           
                if(ds.Tables.Count == 3)
                {
                    AlpacaFactory frm = new AlpacaFactory();
                    dynamic obj = frm.ReturnForm(ds);
                    return Ok(obj);
                }
                else
                {
                    return InternalServerError(new Exception("Database output incorrect"));
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }//GetForm

    }
}
