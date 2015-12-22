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
using AlpacaForms.DAL;
using AlpacaForms.Config;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DWF_WebService.Controllers
{
    public class AlpacaController : ApiController
    {
        public IHttpActionResult GetAllForms()
        {
            return NotFound(); //only accept per id requests
        }

        public IHttpActionResult GetForm(int id)
        {
            try
            {
                DataSet ds = new Db().ReturnFormData(id);
           
                if(ds.Tables.Count == 4)
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
