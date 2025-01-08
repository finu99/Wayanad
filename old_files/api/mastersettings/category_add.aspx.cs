using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
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
        public string name;
        public string icon;        
        public string delete_status;        
        public string type;     
        public string id;     
        public string display_order;     
        public string parent_id;     
    }

    public void insert()
    {



        var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
        bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = bodyStream.ReadToEnd();

        querry = "";
        List<DataResponse> DataResponse = JsonConvert.DeserializeObject<List<DataResponse>>(bodyText);

        string qry = "";
        int count = 0;
        string message = "";

        foreach (var data in DataResponse)
        {
            string querry1 = "";

            if (data.type == "add")
            {
                querry1 = @"insert into tbl_category (name,icon,delete_status,display_order,parent_id) values('" + data.name + "','"+ data.icon+"',0,'"+data.display_order+"','"+data.parent_id+"')";
                message = "Data added successfully.";
            }
            else if (data.type == "edit")
            {
                querry1 = @"update tbl_category set name='" + data.name + "',icon='" + data.icon + "',display_order='" + data.display_order+ "',parent_id='"+data.parent_id+"' where id=" + data.id + " ";
                message = "Data updated successfully.";
            }
            else if (data.type == "delete")
            {
                querry1 = @"update tbl_category set delete_status=1 where id=" + data.id + " ";

                message = "Data deleted successfully.";

            }
            int status = cc.Insert(querry1);
            if (status > 0)
            {
                string id = data.id;
                if (data.type == "add")
                {
                    querry1 = @"SELECT TOP 1 id FROM tbl_category order by id desc";
                    DataSet ds = cc.joinselect(querry1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = ""+Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    }
                    ds.Dispose();
                }

                json = "{'status':true,'Message' :'" + message + "','id':'"+id+"'}";

                json = json.Replace("'", "\"");
                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
            }
            else
            {
                json = "{'status':false,'Message' :'Failed'}";
                json = json.Replace("'", "\"");
                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
            }
        }

        json = "{'status':false,'Message' :'" + message + "','data':" + JsonConvert.SerializeObject(DataResponse) + "}";
        
        json = json.Replace("'", "\"");
        Response.ContentType = "application/json";
        Response.Write(json);

        Response.End();

      
    }
}