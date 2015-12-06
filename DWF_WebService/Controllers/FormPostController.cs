using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Dynamic;

namespace DWF_WebService.Controllers
{
    public class FormPostController : ApiController
    {

        //public IHttpActionResult AnswerForm()
        //{

            
        //    string s = "mkdl;";



        //    return Ok();
        //}

        [HttpPost]
       // public void Post(Models.RequestForm request)
        public void Post(FormDataCollection data)
        {
            var x = "j;d";
        }


    }
}
