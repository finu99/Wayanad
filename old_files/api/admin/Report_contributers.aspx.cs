using AjaxControlToolkit.HTMLEditor.Popups;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
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
    string keyword = "", id = "";
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
        //int page = 1;
        //int pageSize = 10;
        //if (Request.Form["page"] != null)
        //{
        //    int.TryParse(Request.Form["page"], out page);
        //}
        //if (Request.Form["page_size"] != null)
        //{
        //    int.TryParse(Request.Form["page_size"], out pageSize);
        //}

        //int offset = (page - 1) * pageSize;

        string querry1 = @"SELECT 
        s.id,
        s.name,
        s.mobile,
        s.email,
s.address,
s.pincode,
s.district,
s.institute,
     s.qty,
     s.status,
     s.amount,
r.heading as requirement,
c.name as category,
s.remark,
s.edit_remark,
s.addedon,
s.status_updated_on
    FROM 
        tbl_supporters s
    INNER JOIN 
        tbl_requirements r ON s.req_id = r.id
    INNER JOIN 
        tbl_category c ON r.cat_id = c.id
    WHERE (1=1)";

        querry1 += " ORDER BY s.id DESC";
        //querry1 += " OFFSET " + offset + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";

        DataSet ds = cc.joinselect(querry1);

        if (ds.Tables[0].Rows.Count > 0)
        {

            json = "{'status':true,'Message':'Success','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateTime addedOn = Convert.ToDateTime(ds.Tables[0].Rows[i]["addedon"]);

                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                        "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "'," +
                        "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'," +
                        "'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "'," +
                        "'mobile':'" + ds.Tables[0].Rows[i]["mobile"].ToString() + "'," +
                        "'pincode':'" + ds.Tables[0].Rows[i]["pincode"].ToString() + "'," +
                        "'address':'" + ds.Tables[0].Rows[i]["address"].ToString() + "'," +
                        "'district':'" + ds.Tables[0].Rows[i]["district"].ToString() + "'," +
                        "'institute':'" + ds.Tables[0].Rows[i]["institute"].ToString() + "'," +
                        "'amount':'" + ds.Tables[0].Rows[i]["amount"].ToString() + "'," +
                        "'requirement':'" + ds.Tables[0].Rows[i]["requirement"].ToString() + "'," +
                        "'category':'" + ds.Tables[0].Rows[i]["category"].ToString() + "'," +
                        "'remark':'" + ds.Tables[0].Rows[i]["remark"].ToString() + "'," +
                        "'edit_remark':'" + ds.Tables[0].Rows[i]["edit_remark"].ToString() + "'," +
                        "'addedon':'" + addedOn.ToString("yyyy-MM-dd") + "'," +
                        "'status_updated_on':'" + ds.Tables[0].Rows[i]["status_updated_on"].ToString() + "'," +
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
            json = "{'status': false, 'Message': 'No data found!'}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
            ds.Dispose();
        }
    }


}
