using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Linq;

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;

public partial class api_country_add : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    string json;
    static string querry;
    string name = "";
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
        string querry1 = @"select c.id,c.name,c.display_order,c.parent_id,c.icon,c1.name parentname
from tbl_category c
left join tbl_category c1 on c.parent_id=c1.id 
where c.delete_status=0";
        
        if (Request.Form["parent_id"] != null)
        {
            querry1 += " and c.parent_id="+Request.Form["parent_id"];
        }
        
        querry1 += " order by c.name asc";
        DataSet ds = cc.joinselect(querry1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message' :'Success.','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id"].ToString();
                string imagePath = ds.Tables[0].Rows[i]["icon"].ToString();
                string path = "";
                if (imagePath != "")
                    path = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/category/"+id+"/" + imagePath + "";

                json += @"{'name':'" + ds.Tables[0].Rows[i]["name"].ToString() + @"'
                    ,'id':'" + ds.Tables[0].Rows[i]["id"].ToString() + @"','icon':'" + path + @"'
                    ,'display_order':'"+ ds.Tables[0].Rows[i]["display_order"].ToString() + @"'
                    ,'parent_id':'" + ds.Tables[0].Rows[i]["parent_id"].ToString() + @"'
                    ,'parentname':'" + ds.Tables[0].Rows[i]["parentname"].ToString() + @"'},";
            }
            ds.Dispose();
            json = json.TrimEnd(',');
            json += "]}";

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