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
        public string heading;
        public string descri;
        public string esti_budj;
        public string qty;
        public string total_amt;
        public string addedby;       
        public string cat_id;       
        public string id;       
        public string type;       
        public string display_order;       
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
                INSERT INTO tbl_requirements
                (heading, descri, esti_budj, qty, total_amt, delete_status, addedon, addedby, status,cat_id,display_order) 
                VALUES
                ('" + data.heading + "', '" + data.descri + "', '" + data.esti_budj + "','" + data.qty + "','" + data.total_amt + "',0" +
                    ", GETDATE(), '" + data.addedby + "','pending','" + data.cat_id + "','" + data.display_order + "')";

                int status = cc.Insert(query);

                if (status > 0)
                {
                    query = @"SELECT TOP 1 id FROM tbl_requirements WHERE addedby = '" + data.addedby + "' order by id desc";

                    DataSet ds = cc.joinselect(query);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);

                        json = "{'status':true,'Message':'Data added successfully','id':'" + id + "'}";
                        json = json.Replace("'", "\"");
                        Response.ContentType = "application/json";
                        Response.Write(json);
                        Response.End();
                    }
                    else
                    {
                        json = "{'status':false,'Message':'Oops! Something went wrong.'}";
                    }
                }
                else
                {
                    json = "{'status':false,'Message':'Oops! Something went wrong.'}";
                }

            }
            else if (data.type.ToLower() == "edit")
            {
                double total = 0.0;
                string q = "select isnull(sum(qty),0) amt from tbl_supporters where status='approved' and req_id=" + data.id;
                DataSet ds = cc.joinselect(q);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    total = Convert.ToDouble(ds.Tables[0].Rows[0]["amt"].ToString());
                }
                ds.Dispose();

                string status1= "pending";
                if(Convert.ToDouble(data.qty)<=total)
                {
                    status1 = "completed";
                }


                string query = @"
                update tbl_requirements set 
                heading='" + data.heading + "',descri= '" + data.descri + "', esti_budj='" + data.esti_budj + "',qty='" + data.qty + @"'
                ,total_amt='" + data.total_amt + "',status='"+ status1 + "',cat_id='" + data.cat_id + "',display_order='" + data.display_order + "' where id="+data.id;
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
                string query1 = @"UPDATE tbl_requirements 
SET delete_status = 1, 
    deleted_on = GETDATE() WHERE id = '" + data.id + "'";

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