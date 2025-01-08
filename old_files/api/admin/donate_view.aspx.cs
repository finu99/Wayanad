using DocumentFormat.OpenXml.Drawing.Charts;
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
    static string querry;
    string keyword = "", cat_id = "", status = "", supporters_status="";
    protected void Page_Load(object sender, EventArgs e)
    {
        chk_tocken();
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
        string querry1 = @"SELECT * FROM tbl_donate WHERE (1=1) ";
        if (!string.IsNullOrEmpty(status))
        {
            querry1 += " AND status = '" + status + "' ";
        }
        querry1 += " ORDER BY id desc";
        DataSet ds = cc.joinselect(querry1);
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message' :'Success.','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id"].ToString();


                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                    "'amount':'" + ds.Tables[0].Rows[i]["amount"].ToString() + "','name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "'," +
                    "'mob':'" + ds.Tables[0].Rows[i]["mob"].ToString() + "','email':'" + ds.Tables[0].Rows[i]["email"].ToString() + "'," +
                    "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'" +
                    ",'loc':'" + ds.Tables[0].Rows[i]["loc"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "'" +
                    ",'addedon':'" + ds.Tables[0].Rows[i]["addedon"].ToString() + "'" +
                    ",'delete_remark':'" + ds.Tables[0].Rows[i]["delete_remark"].ToString() + "'" +
                    ",'remark':'" + ds.Tables[0].Rows[i]["remark"].ToString() + "'" +                   
                    "},";
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
