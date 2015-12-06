using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;
using AlpacaForms.Questions;

namespace AlpacaForms.Config
{
    /// <summary>
    /// creates the appropriate Alpaca forms objects, and populates
    /// with the DataSet data passed in 
    /// </summary>
    public class AlpacaFactory
    {
        private DataTable header;
        private DataTable questions;
        private DataTable questionEnums;

        /// <summary>
        /// Does all the work in creating the form, and its child objects
        /// Dataset conforms to the data standard xxx
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public ExpandoObject ReturnForm(DataSet ds)
        {
            SetVariables(ds);

            dynamic frm = ReturnBase();
            frm.properties = ReturnProperties();

            return frm;
        }

        private void SetVariables(DataSet ds)
        {
            header = ds.Tables["header"];
            questions = ds.Tables["questions"];
            questionEnums = ds.Tables["questionEnums"];
        }

        /// <summary>
        /// returns the base object
        /// includes title, attributes and buttons
        /// </summary>
        /// <returns></returns>
        private ExpandoObject ReturnBase()
        {
            dynamic obj = new ExpandoObject(); //the main object to be returned   
            obj.title = header.Rows[0]["title"];
            obj.type = "object";

            obj.properties = new ExpandoObject(); //the form data
            obj.fields = new ExpandoObject(); //the options (set as each control is created)

            //buttons and submit config           
            obj.form = new AlpacaForms.Config.Form();
            obj.form.attributes.action = header.Rows[0]["actionUrl"].ToString();
            obj.form.attributes.method = header.Rows[0]["actionType"].ToString();

            return obj;
        }

        /// <summary>
        /// Returns the properties - these are the actual questions 
        /// </summary>
        /// <returns></returns>
        private ExpandoObject ReturnProperties()
        {
            dynamic properties = new ExpandoObject(); //the form data

            //add each quetion, in order
            for(int i = 0; i < questions.Rows.Count; i++)
            {
                DataRow dr = questions.Rows[i];

                //do the dynamic name here
                if (dr["controlType"].ToString().ToLower() == "textinput") 
                {
                    var dictionary = (IDictionary<string, object>)properties;
                    dictionary.Add(dr["name"].ToString(), ReturnTextInput(dr));
                }


            }//each question

            return properties;

        }//ReturnProperties


        //use a generic method to set the titles!!

        private TextInput ReturnTextInput(DataRow dr)
        {
            TextInput txt = new TextInput();
            txt.title = dr["title"].ToString();
            return txt;
        }

    }
}
