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
    string name = "", mobile = "", email = "", location = "", district = "", state = "", no_of_emp = "";
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //chk_tocken();
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
        {
            string querry1 = @"SELECT  id,name,image from tbl_home_slider";



            DataSet ds = cc.joinselect(querry1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                json = "{'status':true,'Message' :'Success.','data':[";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                { 
                    string id = ds.Tables[0].Rows[i]["id"].ToString();
                string imagePath = ds.Tables[0].Rows[i]["image"].ToString();
                  
                string path = "";
                if (imagePath != "")
                    path = "http://" + HttpContext.Current.Request.Url.Authority + "/uploads/slider/"  + imagePath + "";

                json += "{'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "','name':'" + ds.Tables[0].Rows[i]["name"].ToString() + "','image':'" + ds.Tables[0].Rows[i]["image"].ToString() + "','image_path':'"+path+"'},";

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
}