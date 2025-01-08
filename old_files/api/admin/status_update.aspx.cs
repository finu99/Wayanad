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
        chk_tocken();
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
        public string id;
        public string status;
        public string delivery_date;
        public string delete_remark;              
        public string req_id;              
        public string req_qty;              
        public string remark;              
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

            


            string query = @"
            update tbl_supporters set 
            delivery_date='" + data.delivery_date + "',remark= '" + data.remark + "',delete_remark= '" + data.delete_remark + "', status='" + data.status + "',status_updated_on=GETDATE() where id=" + data.id;
            int status = cc.Insert(query);
            if (status > 0)
            {
                double total = 0.0;
                string q = "select isnull(sum(qty),0) amt from tbl_supporters where status='approved' and req_id=" + data.req_id;
                DataSet ds = cc.joinselect(q);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    total = Convert.ToDouble(ds.Tables[0].Rows[0]["amt"].ToString());
                }
                ds.Dispose();

                string status1 = "pending";
                if (Convert.ToDouble(data.req_qty) <= total)
                {
                    status1 = "completed";
                }
                query = @" update tbl_requirements set status='" + status1 + "',status_updated_on=GETDATE() where id=" + data.req_id;
                int a = cc.Insert(query);

                json = "{'status':true,'Message':'Data Updated successfully','id':'" + data.id + "'}";
            }
            else
            {
                json = "{'status':false,'Message':'Oops! Something went wrong.'}";
            }

           


            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();

        }
    }
}