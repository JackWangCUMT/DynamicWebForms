using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AlpacaForms;

namespace AlpacaForms.DAL
{
    public class Db
    {
        private string stConn = @"Data Source=J-S7\MSSQLSERVER14;Initial Catalog=J;Persist Security info=True;Integrated Security=SSPI;";
        private SqlConnection con;
        public DataSet ReturnFormData(int formId)
        {
            SqlParameter p = new SqlParameter()
            {
                ParameterName = "@formId",
                Value = formId,
                DbType = DbType.Int16
            };


            //change this to a using block?
            con = new SqlConnection();
            con.ConnectionString = stConn;
            con.Open();

            //create the dataset
            DataSet ds = new DataSet();
            ds.Tables.Add(ReturnFormHeader(p));
            ds.Tables.Add(ReturnFormQuestions(p));
            ds.Tables.Add(ReturnFormQuestionEnums(p));

            con.Close();
            con.Dispose();

            return ds;
        }

        public DataTable ReturnFormHeader(SqlParameter formId)
        {
            DataTable dt = DataTableFromProc("returnFormHeader", formId);
            dt.TableName = "header";
            return dt;
        }

        public DataTable ReturnFormQuestions(SqlParameter formId)
        {
            DataTable dt = DataTableFromProc("returnFormQuestions", formId);
            dt.TableName = "questions";
            return dt;
        }

        public DataTable ReturnFormQuestionEnums(SqlParameter formId)
        {
            DataTable dt = DataTableFromProc("returnFormQuestionEnums", formId);
            dt.TableName = "questionEnums";
            return dt;
        }
      
        private DataTable DataTableFromProc(string procName, List<SqlParameter> paras)
        {
            if (con.State == ConnectionState.Closed) { con.Open(); }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;

            foreach (SqlParameter p in paras)
            {
                cmd.Parameters.AddWithValue(p.ParameterName, p.Value);
            }

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        private DataTable DataTableFromProc(string procName, SqlParameter para)
        {

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(para);
            return DataTableFromProc(procName, paras);

        }


    }
}
