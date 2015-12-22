using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;
using AlpacaForms.Questions;
using AlpacaForms.Options;

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
        private DataTable questionOptions;

        private dynamic frm; //the form that is returned

        /// <summary>
        /// Does all the work in creating the form, and its child objects
        /// Dataset conforms to the data standard xxx
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public ExpandoObject ReturnForm(DataSet ds)
        {
            SetVariables(ds);
            SetBase();
            SetOptions();
            SetProperties(); 
            return frm;
        }

        private void SetVariables(DataSet ds)
        {   //sets the class variables
            header = ds.Tables["header"];
            questions = ds.Tables["questions"];
            questionEnums = ds.Tables["questionEnums"];
            questionOptions = ds.Tables["questionOptions"];
        }

        /// <summary>
        /// returns the base object
        /// includes title, attributes and buttons
        /// </summary>
        /// <returns></returns>
        private void SetBase()
        {
            frm = new ExpandoObject(); //the main object to be returned   
            frm.title = header.Rows[0]["title"];
            frm.type = "object";

            frm.properties = new ExpandoObject(); //the form data
            frm.fields = new ExpandoObject(); //the options (set as each control is created)

            //buttons and submit config           
            frm.form = new AlpacaForms.Config.Form(header.Rows[0]);
            //frm.form = new AlpacaForms.Config.Form();
            frm.form.attributes.action = header.Rows[0]["actionUrl"].ToString();
            frm.form.attributes.method = header.Rows[0]["actionType"].ToString();

           
        }

        /// <summary>
        /// Sets the .properties structure - these are the actual questions 
        /// </summary>
        /// <returns></returns>
        private void SetProperties()
        {
            dynamic properties = new ExpandoObject(); //the form data
            properties.internalRef = ReturnInternalReference(); //hidden internal reference

            //add each quetion, in order
            for(int i = 0; i < questions.Rows.Count; i++)
            {
                DataRow dr = questions.Rows[i];

                //new object, based on the type of control
                string controlType = dr["controlType"].ToString().ToLower();
                var dictionary = (IDictionary<string, object>)properties;

                if (controlType == "textinput") 
                {                 
                    dictionary.Add(dr["name"].ToString(), ReturnTextInput(dr));
                }
                else if(controlType == "multiplechoice")
                {
                    dictionary.Add(dr["name"].ToString(), ReturnMultipleChoice(dr));
                }
                else if(controlType == "boolean")
                {
                    dictionary.Add(dr["name"].ToString(), ReturnBoolean(dr));
                }
                else if(controlType == "numberinput")
                {
                    dictionary.Add(dr["name"].ToString(), ReturnNumberInput(dr));
                }
                else if(controlType == "textarea")
                {
                    dictionary.Add(dr["name"].ToString(), ReturnTextAreaInput(dr));
                }
                else if(controlType == "fileupload")
                {
                    dictionary.Add(dr["name"].ToString(), ReturnFileUpload(dr));
                }
                
            }//each question

            frm.properties = properties;

        }//ReturnProperties

        /// <summary>
        /// sets the .fields structure - these are the preferences for each question (such as readonly)
        /// the options can be contained in the questions table or the questions options table
        /// </summary>
        private void SetOptions()
        {
            frm.fields = new ExpandoObject();

            //internal reference is always hidden
            var dictionary = (IDictionary<string, object>)frm.fields;
            dictionary.Add("internalRef", new HiddenOption(true));
            
            //check each question for any options 
            for (int i = 0; i < questions.Rows.Count; i++)
            {
                DataRow dr = questions.Rows[i];
                bool hidden = dr["hidden"].ToString().StringToBool();
                if (hidden)
                {
                    dictionary = (IDictionary<string, object>)frm.fields;
                    dictionary.Add(dr["name"].ToString(), new HiddenOption(hidden));
                }

                if(dr["controlType"].ToString().ToLower() == "fileupload")
                {
                    dictionary = (IDictionary<string, object>)frm.fields;
                    dictionary.Add(dr["name"].ToString(), new FileUploadOption());
                }
         
            }//each question

            //iterate through the options table
            for(int i = 0; i < questionOptions.Rows.Count; i++)
            {
                DataRow dr = questionOptions.Rows[i];

                if(dr["label"].ToString() != String.Empty)
                {                
                    dictionary.Add(ReturnQuestionName(dr["formQuestionId"].ToString().StringToInt()), new LabelOption(dr["label"].ToString()));
                }
                
                if(dr["placeholder"].ToString().ToLower() != String.Empty)
                {
                    TextInputOption txt = new TextInputOption();
                    txt.placeholder = dr["placeholder"].ToString();
                    dictionary.Add(ReturnQuestionName(dr["formQuestionId"].ToString().StringToInt()), txt);
                }

                if (dr["disabled"].ToString().StringToBool())
                {
                    dictionary.Add(ReturnQuestionName(dr["formQuestionId"].ToString().StringToInt()), new DisabledOption(true));
                }

            }//each option



        }//SetOptions


        /// <summary>
        /// Each form contains a reference field, which is hidden from the user
        /// this allows the calling application to set it in the background, this
        /// reference field is added to the database when answers are saved
        /// </summary>
        /// <returns></returns>
        private TextInput ReturnInternalReference()
        {
            TextInput txt = new TextInput();
            txt.title = "internalRef";
            txt.hidden = true;
            return txt;
        }

        private void SetTitle(AlpacaForms.Questions.Base obj, DataRow dr)
        {
            obj.title = dr["title"].ToString();
        }

        private TextInput ReturnTextInput(DataRow dr)
        {
            TextInput txt = new TextInput();
            SetTitle(txt, dr);
            if (dr["readonly"].ToString().StringToBool()) { txt.@readonly = true; }
            if (dr["defaultText"].ToString() != String.Empty) { txt.@default = dr["defaultText"].ToString(); }
            return txt;
        }

        private MultipleChoice ReturnMultipleChoice(DataRow dr)
        {
            MultipleChoice m = new MultipleChoice();
            SetTitle(m, dr);

            m.@enum = ReturnEnums(dr["formQuestionId"].ToString().StringToInt());
            return m;
        }

        private List<String>ReturnEnums(int formQuestionId)
        {
            List<String> ret = new List<string>();

            foreach(DataRow dr in questionEnums.AsEnumerable().Where(x => x["formQuestionId"].ToString().StringToInt() == formQuestionId))
            {
                ret.Add(dr.Field<String>("selectionName"));
            }

            return ret;         
        }

        private YesNo ReturnBoolean(DataRow dr)
        {
            YesNo ret = new YesNo();
            SetTitle(ret, dr);
            return ret;
        }

        private NumberInput ReturnNumberInput(DataRow dr)
        {
            NumberInput ret = new NumberInput();
            SetTitle(ret, dr);
            return ret;
        }

        private TextAreaInput ReturnTextAreaInput(DataRow dr)
        {
            TextAreaInput txt = new TextAreaInput();
            SetTitle(txt, dr);
            if (dr["readonly"].ToString().StringToBool()) { txt.@readonly = true; }
            if (dr["defaultText"].ToString() != String.Empty) { txt.@default = dr["defaultText"].ToString(); }
            return txt;
        }

        private FileUpload ReturnFileUpload(DataRow dr)
        {
            FileUpload ret = new FileUpload();
            SetTitle(ret, dr);
            return ret;
        }

        private String ReturnQuestionName(int questionId)
        {
            return (from row in questions.AsEnumerable() where row["formQuestionId"].ToString().StringToInt() == questionId select row["name"]).First().ToString();
        }
    }
}
