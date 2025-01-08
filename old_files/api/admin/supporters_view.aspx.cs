using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

public partial class api_country_add : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    string json;
    string querry;
    string keyword = "", status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       // chk_tocken();
        getdata();
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

    public void getdata()
    {
     

        if (Request.Form["status"] != null)
        {
            status = Request.Form["status"];
        }
      
        string query1 = @"SELECT s.id, s.name, s.mobile, s.email, s.address, s.qty, s.edit_remark,
                         s.req_id, s.amount, s.qty, s.status, r.heading as requirement
                  FROM tbl_supporters s
                  INNER JOIN tbl_requirements r ON s.req_id = r.id";


        if (!string.IsNullOrEmpty(status))
        {
            query1 += " WHERE s.status = '" + status + "'";
        }
        query1 += " ORDER BY s.id DESC";

        DataSet ds = cc.joinselect(query1);
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message':'Success','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id"].ToString();


                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                    "'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "'," +
                    "'mobile':'" + ds.Tables[0].Rows[i]["mobile"].ToString() + "'," +
                    "'email':'" + ds.Tables[0].Rows[i]["email"].ToString() + "'," +
                    "'address':'" + ds.Tables[0].Rows[i]["address"].ToString() + "'," +
                    "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "'," +
                    "'amount':'" + ds.Tables[0].Rows[i]["amount"].ToString() + "'," +
                    "'requirement':'" + ds.Tables[0].Rows[i]["requirement"].ToString() + "'," +
                    "'edit_remark':'" + ds.Tables[0].Rows[i]["edit_remark"].ToString() + "'," +
                    "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'},";
            }
            json = json.TrimEnd(',');
            json += "]}";
        
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
            ds.Dispose();
        }
        else
        {
            json = "{'status': false, 'Message': 'No data found!'}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
            ds.Dispose();

        }
    }


}
