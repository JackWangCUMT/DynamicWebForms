﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlpacaForms;
using AlpacaForms.DAL;
using System.Data;

namespace DynamicWebForms
{
    class Program
    {
        static void Main(string[] args)
        {

            Db db = new Db();
            DataSet ds = db.ReturnFormData(1);
            var x = "x";
        }
    }
}
