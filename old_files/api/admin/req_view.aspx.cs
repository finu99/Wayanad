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


        //string a = "", b = "";
        //a = "username=admin&password=123";
        //b = EncodeDecode.base64Encode(a);
        //Response.Write(b);
        //return;
        if (Request.Form["keyword"] != null)
        {
            keyword = Request.Form["keyword"];
        }
        if (Request.Form["cat_id"] != null)
        {
            cat_id = Request.Form["cat_id"];
        }
        if (Request.Form["status"] != null)
        {
            status = Request.Form["status"];
        }
        if (Request.Form["supporters_status"] != null)
        {
            supporters_status = " and s.status='"+Request.Form["supporters_status"]+"' ";
        }


        string querry1 = @"SELECT r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name cat,r.descri,r.cat_id,isnull(sum(s.qty),0) achieved,r.display_order
FROM tbl_requirements r
inner join tbl_category c on r.cat_id=c.id 
left join tbl_supporters s on r.id=s.req_id " + supporters_status + @"
WHERE r.delete_status = 0
";


        if (!string.IsNullOrEmpty(keyword))
        {
            querry1 += " AND r.heading LIKE '%" + keyword + "%' ";
        }

        if (!string.IsNullOrEmpty(cat_id))
        {
            querry1 += " AND r.cat_id = '" + cat_id + "' ";
        }

        if (!string.IsNullOrEmpty(status))
        {
            querry1 += " AND r.status = '" + status + "' ";
        }

        querry1 += " group by r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name,r.descri,r.cat_id,r.display_order ";
        
        
        
        querry1 += " ORDER BY r.display_order asc";

        DataSet ds = cc.joinselect(querry1);
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message' :'Success.','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id = ds.Tables[0].Rows[i]["id"].ToString();
               // string imagePath = ds.Tables[0].Rows[i]["img"].ToString();


                //string path = "";
                //if (imagePath != "")
                //    path = "http://" + HttpContext.Current.Request.Url.Authority + "/uploads/employee/" + ds.Tables[0].Rows[i]["id"].ToString() + "/" + imagePath + "";


                json += "{'heading':'" + ds.Tables[0].Rows[i]["heading"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "','id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                    "'category':'" + ds.Tables[0].Rows[i]["cat"].ToString() + "','esti_budj':'" + ds.Tables[0].Rows[i]["esti_budj"].ToString() + "'," +
                    "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "','total_amt':'" + ds.Tables[0].Rows[i]["total_amt"].ToString() + "'," +
                    "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'" +
                    ",'descri':'" + ds.Tables[0].Rows[i]["descri"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "'" +
                    ",'cat_id':'" + ds.Tables[0].Rows[i]["cat_id"].ToString() + "'" +
                    ",'achieved':'" + ds.Tables[0].Rows[i]["achieved"].ToString() + "'" +
                    ",'display_order':'" + ds.Tables[0].Rows[i]["display_order"].ToString() + "'" +
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
