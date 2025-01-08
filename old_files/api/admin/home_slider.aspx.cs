using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.Linq;
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
        //public string description;
        //public string designation;
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

        string query = "";
        DataResponse DataResponse = JsonConvert.DeserializeObject<DataResponse>(bodyText);
        string message = "Data added successfully.";
        string json = "";

        if (DataResponse.type.ToLower() == "add")
        {
            query = @"
            INSERT INTO tbl_home_slider
            (name, image) 
            VALUES
            ('" + DataResponse.name + "', '" + DataResponse.image + "')";
            int status = cc.Insert(query);

            if (status > 0)
            {
                json = "{'status':true,'Message':'Data added successfully'}";
            }
            else
            {
                json = "{'status':false,'Message':'Oops! Something went wrong.'}";
            }
        }
        else if (DataResponse.type.ToLower() == "edit")
        {
            query = @"
            UPDATE tbl_home_slider
            SET name = '" + DataResponse.name + "', image = '" + DataResponse.image + "' WHERE id = " + DataResponse.id;
            int status = cc.Insert(query);

            if (status > 0)
            {
                json = "{'status':true,'Message':'Data Updated successfully','id':'" + DataResponse.id + "'}";
            }
            else
            {
                json = "{'status':false,'Message':'Oops! Something went wrong.'}";
            }
        }
        else if (DataResponse.type.ToLower() == "delete")
        {
            query = @"
            DELETE FROM tbl_home_slider
            WHERE id = " + DataResponse.id;
            int status = cc.Insert(query);

            if (status > 0)
            {
                json = "{'status':true,'Message':'Data deleted successfully.'}";
            }
            else
            {
                json = "{'status':false,'Message':'Oops! Something went wrong.'}";
            }
        }

        json = json.Replace("'", "\"");
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }

}