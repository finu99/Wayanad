using AjaxControlToolkit.HTMLEditor.Popups;
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
    string querry;
    string keyword = "", req_id = "",status="";
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
        int page = 1;
        int pageSize = 10;
        if (Request.Form["req_id"] != null)
        {
            req_id = Request.Form["req_id"];
        }

        if (Request.Form["status"] != null)
        {
            status = Request.Form["status"];
        }
        if (Request.Form["page"] != null)
        {
            int.TryParse(Request.Form["page"], out page);
        }
        if (Request.Form["page_size"] != null)
        {
            int.TryParse(Request.Form["page_size"], out pageSize);
        }
        int offset = (page - 1) * pageSize;

        string querry1 = @"SELECT 
        s.id,
        s.qty,
        s.status,
        s.name,
        s.mobile,
        s.email
    FROM 
        tbl_supporters s
    INNER JOIN 
        tbl_requirements r ON s.req_id = r.id
    INNER JOIN 
        tbl_category c ON r.cat_id = c.id
    WHERE 
        r.id='"+ req_id + "'";
        if (!string.IsNullOrEmpty(status))
        {
            querry1 += " AND s.status = '" + status + "' ";
        }
        querry1 += " ORDER BY s.id DESC";
        querry1 += " OFFSET " + offset + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

        DataSet ds = cc.joinselect(querry1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message':'Success','page':" + page + ",'page_size':" + pageSize + ",'data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                        "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "'," +
                        "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'," +
                        "'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "'," +
                        "'mobile':'" + ds.Tables[0].Rows[i]["mobile"].ToString() + "'," +
                        "'email':'" + ds.Tables[0].Rows[i]["email"].ToString() + "'},";
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
            json = "{'status': false, 'Message': 'No data found!', 'page':" + page + ", 'page_size':" + pageSize + "}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
            ds.Dispose();
        }
    }


}
