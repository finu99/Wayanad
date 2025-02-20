﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_country_add : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    CommFuncs CommFuncs = new CommFuncs();
    string json;
    static string querry;
    string name = "", mobile = "",email="",location="",district="",state="",no_of_emp="";
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        chk_tocken();
    }
    public void chk_tocken()
    {
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
        }
        else if (id != "")
        {
            getdata(id);
        }
        else
        {
            json = "{'status':false,'Message' :'Oops! Unauthorised Access!'}";
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.StatusCode = 403;
            Response.Write(json);
            Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
    public void getdata(string empid)
    {
        string querry1 = @"SELECT  
        e.id,
        e.name,
        e.email,
        e.mobile,
e.status,
e.role
FROM
        tbl_employees e
    WHERE
        e.delete_status = 0
   ";

        querry1 += "AND e.id = '" + empid + "' ";

        DataSet ds = cc.joinselect(querry1);
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message' :'Success.','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                json += "{'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "','id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "','mobile':'" + ds.Tables[0].Rows[i]["mobile"].ToString() + "','email':'" + ds.Tables[0].Rows[i]["email"].ToString() + "','role':'" + ds.Tables[0].Rows[i]["role"].ToString() + "','status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'},";
            }
            json = json.TrimEnd(',');
            json += "]}";
            ds.Dispose();
            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();
        }
        else
            json = "{'status':false,'Message' :'No data found!'}";

        ds.Dispose();

        json = json.Replace("'", "\"");

        Response.ContentType = "application/json";

        Response.Write(json);

        Response.End();

    }

}