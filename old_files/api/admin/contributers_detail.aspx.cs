using AjaxControlToolkit.HTMLEditor.Popups;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    string keyword = "", cat_id = "", status = "", supporters_status="";
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
        string cat_id = "";
        string status = "";
        string type = "";
        string json = "";

        int page = 1;
        int pageSize = 10;

        if (Request.Form["cat_id"] != null)
        {
            cat_id = Request.Form["cat_id"];
        }
        if (Request.Form["status"] != null)
        {
            status = Request.Form["status"];
        }
        //if (Request.Form["type"] != null)
        //{
        //    type = Request.Form["type"];
        //}
        if (Request.Form["page"] != null)
        {
            int.TryParse(Request.Form["page"], out page);
        }
        if (Request.Form["page_size"] != null)
        {
            int.TryParse(Request.Form["page_size"], out pageSize);
        }

        int offset = (page - 1) * pageSize;

        string query = "";

      
            query = @"
        SELECT 
            s.id,
            r.qty AS total_qty,
            s.status,
            r.heading AS requirement,
            r.cat_id,
            c.id AS catid,
            c.name AS category,
            (r.qty - s.qty) AS pending_qty,
            s.name, s.mobile, s.email, s.address, s.qty, 
            s.req_id, s.amount
        FROM 
            tbl_supporters s
        INNER JOIN 
            tbl_requirements r ON s.req_id = r.id
        INNER JOIN 
            tbl_category c ON r.cat_id = c.id
        WHERE (1=1)";
       

        // Add conditions dynamically
        if (!string.IsNullOrEmpty(cat_id))
        {
            query += " AND r.cat_id = '" + cat_id + "' ";
        }

        if (!string.IsNullOrEmpty(status))
        {
            query += " AND s.status = '" + status + "' ";
        }

        // Pagination and Ordering
        query += " ORDER BY s.id DESC ";
        query += " OFFSET " + offset + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

        // Execute the query
        DataSet ds = cc.joinselect(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message':'Success','page':" + page + ",'page_size':" + pageSize + ",'data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                        "'requirement':'" + ds.Tables[0].Rows[i]["requirement"].ToString() + "'," +
                        "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'," +
                        "'category':'" + ds.Tables[0].Rows[i]["category"].ToString() + "'," +
                        "'cat_id':'" + ds.Tables[0].Rows[i]["cat_id"].ToString() + "'," +
                        "'total_qty':'" +ds.Tables[0].Rows[i]["total_qty"].ToString() + "'," +
                        "'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "'," +
                        "'mobile':'" + ds.Tables[0].Rows[i]["mobile"].ToString() + "'," +
                        "'email':'" + ds.Tables[0].Rows[i]["email"].ToString() + "'," +
                        "'address':'" + ds.Tables[0].Rows[i]["address"].ToString() + "'," +
                        "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "'," +
                        "'amount':'" + ds.Tables[0].Rows[i]["amount"].ToString() + "'," +
                        "'pending_qty':'" + ds.Tables[0].Rows[i]["pending_qty"].ToString() + "'},";
            }
            json = json.TrimEnd(',');
            json += "]}";

            json = json.Replace("'", "\"");
        }
        else
        {
            json = "{'status': false, 'Message': 'No data found!', 'page':" + page + ", 'page_size':" + pageSize + "}";
            json = json.Replace("'", "\"");
        }

        // Send the response
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
        ds.Dispose();
    }

}
