using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_employee_Default : System.Web.UI.Page
{


    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    CommFuncs com = new CommFuncs();
    string json;
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
       // chk_tocken();
        insert();
    }
    public void chk_tocken()
    {
        CommFuncs CommFuncs = new CommFuncs();

        string id = "";
        if (Request.Headers["Authorization"] != null)
        {
            id = CommFuncs.get_tocken_details(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
        }


        if (id == "Oops! Tocken Expired!")
        {
            json = "{'status':false,'Message' :'Oops! Tocken Expired!'}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.StatusCode = 403;
            Response.Write(json);
            Response.End();
            return;
        }
        else if (id != "")
        {

        }
        else
        {
            json = "{'status':false,'Message' :'Oops! Unauthorised Access!'}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.StatusCode = 403;
            Response.Write(json);
            Response.End();
            return;
        }
    }
    public class DataResponse
    {
      
        public string name;
        public string description;
        public string designation;
        public string image;       
        public string type;       
        public string id;       
    }


    public void insert()
    {
        var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
        bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = bodyStream.ReadToEnd();

        string enc_key = com.generate_tocken();

        string querry = "";
        List<DataResponse> DataResponse = JsonConvert.DeserializeObject<List<DataResponse>>(bodyText);
        string message = "Data added successfully.";
        string json = "";

        foreach (var data in DataResponse)
        {
            if (data.type.ToLower() == "add")
            {
                string query = @"
                INSERT INTO tbl_testimonials
                ( name, image, description, designation,delete_status) 
                VALUES
                ( '" + data.name + "','" + data.image + "','" + data.description + "','"+data.designation+"',0)";

                int status = cc.Insert(query);
                if (status > 0)
                {
                    string id = data.id;
                    if (data.type == "add")
                    {
                        query = @"SELECT TOP 1 id FROM tbl_testimonials order by id desc";
                        DataSet ds = cc.joinselect(query);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            id = "" + Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                        }
                        ds.Dispose();
                    }

                    json = "{'status':true,'Message' :'" + message + "','id':'" + id + "'}";

                    json = json.Replace("'", "\"");
                    Response.ContentType = "application/json";
                    Response.Write(json);
                    Response.End();
                }
                if (status > 0)
                {
                    json = "{'status':true,'Message':'Data added successfully'}";
                }
                else
                {
                    json = "{'status':false,'Message':'Oops! Something went wrong.'}";
                }                                
            }
            else if (data.type.ToLower() == "edit")
            {
                string query = @"
                update tbl_testimonials set
                 name='" + data.name + "',image='" + data.image + "',description='" + data.description + "'" +
                ",designation='" + data.designation + "' where id=" + data.id;
                int status = cc.Insert(query);

                if (status > 0)
                {                   
                        json = "{'status':true,'Message':'Data Updated successfully','id':'" + data.id + "'}";                   
                }
                else
                {
                    json = "{'status':false,'Message':'Oops! Something went wrong.'}";
                }

            }
            else if (data.type.ToLower() == "delete")
            {
                string query1 = @"update  tbl_testimonials set delete_status=1,deleted_on=getdate() WHERE id = '" + data.id + "'";

                int status = cc.Insert(query1);

                if (status > 0)
                {
                    json = "{'status':true,'Message' :'Data deleted successfully.'}";
                }
                else
                {
                    json = "{'status':false,'Message' :'Oops! Something went wrong'}";
                }
            }


            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();

        }
    }
}