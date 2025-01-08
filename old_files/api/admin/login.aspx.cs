using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json.Linq;

public partial class api_country_add : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    string json;
    CommFuncs com = new CommFuncs();

    protected void Page_Load(object sender, EventArgs e)
    {
        string data = "";
        if (Request.Form["data"] != null)
        {
            try
            {
                data = EncodeDecode.base64Decode(Request.Form["data"]);
            }
            catch (Exception ex)
            {
                json = "{'status':false,'Message' :'Oops! Something went wrong!'}";
                json = json.Replace("'", "\"");
                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
                return;
            }

            string username = data.Split('&')[0].Replace("username=", "").Replace("amp;", "&");
            string password = data.Split('&')[1].Replace("password=", "").Replace("amp;", "&");
            string type = data.Split('&')[2].Replace("type=", "");
            getdata(username, password, type);
        }
    }

    public void getdata(string username, string password, string type)
    {
        string curdate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"; // Append the time part to the date
        string query = string.Empty;
        string message = string.Empty;

        if (type.ToLower() == "web")
        {
            query = @"select e.id, e.enc_key, e.enc_key_date, e.mobile, e.password ,e.status,e.role
                      from tbl_employees e 
                      where e.mobile = '" + username + "' and e.password = '" + EncodeDecode.base64Encode(password) + "'";
        }
        else if (type.ToLower() == "app")
        {
            query = @"select e.id, e.enc_key, e.enc_key_date,e.status, e.mobile, e.password ,e.role
                      from tbl_employees e
                      where e.mobile = '" + username + "' and e.password = '" + EncodeDecode.base64Encode(password) + "'";
        }

        if (!string.IsNullOrEmpty(query))
        {
            DataSet ds = cc.joinselect(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["status"].ToString() == "inactive")
                {
                    json = "{'status':false,'Message' :'Account is inactive please contact administrator'}";
                    json = json.Replace("'", "\"");
                    Response.ContentType = "application/json";
                    Response.Write(json);
                    Response.End();
                    return;
                }

                if (DateTime.Parse(ds.Tables[0].Rows[0]["enc_key_date"].ToString()).ToString("dd-MM-yyyy") == DateTime.Parse(curdate).ToString("dd-MM-yyyy"))
                {
                    json = "{'status':true,'Message' :'Successfully logged in','data':[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        json += "{'role':'" + ds.Tables[0].Rows[i]["role"].ToString() + "','enc_key':'" + ds.Tables[0].Rows[i]["enc_key"].ToString() + "','status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'},";
                    }
                    json = json.TrimEnd(',');
                    json += "]}";
                    ds.Dispose();
                    json = json.Replace("'", "\"");
                    Response.ContentType = "application/json";
                    Response.Write(json);
                    Response.End();
                    return;
                }
                else
                {
                    var tok = com.generate_tocken();
                    json = "{'status':true,'Message' :'Successfully logged in','data':[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string updateQuery = @"update tbl_employees set enc_key = '" + tok + "', enc_key_date = '" + curdate + "' where id = '" + ds.Tables[0].Rows[i]["id"].ToString() + "'";
                        int status1 = cc.Insert(updateQuery);
                        json += "{'role':'" + ds.Tables[0].Rows[i]["role"].ToString() + "','enc_key':'" + tok + "','status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'},";
                    }
                    json = json.TrimEnd(',');
                    json += "]}";
                    ds.Dispose();
                    json = json.Replace("'", "\"");
                    Response.ContentType = "application/json";
                    Response.Write(json);
                    Response.End();
                    return;
                }
            }
            else
            {
                json = "{'status':false,'Message' :'User does not exist'}";
                json = json.Replace("'", "\"");
                Response.ContentType = "application/json";
                Response.Write(json);
                Response.End();
                return;
            }
        }
    }
}
