using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_employee_Default : System.Web.UI.Page
{


    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    CommFuncs com = new CommFuncs();
    string json;
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
       // chk_tocken();
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
        public string req_id;
        public string hide_identity;
        public string name;
        public string email;
        public string mobile;
        public string address;       
        public string pincode;       
        public string district;       
        public string institute;       
        public string logo;       
        public string amount;       
        public string qty;       
        public string remark;       
        public string edit_remark;       
        public string status;       
        public string id;       
        public string type;       
    }


    public void insert()
    {
        var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
        bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = bodyStream.ReadToEnd();

        string enc_key = com.generate_tocken();

        string querry = "";
        List<DataResponse> DataResponse = JsonConvert.DeserializeObject<List<DataResponse>>(bodyText);
        string message = "Data added successfully.";
        string json = "";

        foreach (var data in DataResponse)
        {
            if (data.type.ToLower() == "add")
            {
                string query = @"
                INSERT INTO tbl_supporters
                (req_id, hide_identity, name, email, mobile, address, pincode, district, institute,logo,amount,qty,remark,status,addedon,edit_remark) 
                VALUES
                ('" + data.req_id + "', '" + data.hide_identity + "', '" + data.name + "','" + data.email + "','" + data.mobile + "'" +
                ",'" + data.address + "', '" + data.pincode + "', '" + data.district + "','" + data.institute + "','" + data.logo + "'" +
                ",'" + data.amount + "', '" + data.qty + "', '" + data.remark + "','pending'" +
                ", GETDATE(),'"+ data.edit_remark + "')";

                int status = cc.Insert(query);

                if (status > 0)
                {
                    json = "{'status':true,'Message':'Data added successfully'}";
                }
                else
                {
                    json = "{'status':false,'Message':'Oops! Something went wrong.'}";
                }                                
            }
            else if (data.type.ToLower() == "edit")
            {
                string query = "UPDATE tbl_supporters SET ";

                if (!string.IsNullOrEmpty(data.name))
                    query += "name = '" + data.name + "',";
                if (!string.IsNullOrEmpty(data.email))
                    query += "email = '" + data.email + "',";
                if (!string.IsNullOrEmpty(data.mobile))
                    query += "mobile = '" + data.mobile + "',";
                if (!string.IsNullOrEmpty(data.address))
                    query += "address = '" + data.address + "',";
                if (!string.IsNullOrEmpty(data.pincode))
                    query += "pincode = '" + data.pincode + "',";
                if (!string.IsNullOrEmpty(data.district))
                    query += "district = '" + data.district + "',";
                if (!string.IsNullOrEmpty(data.institute))
                    query += "institute = '" + data.institute + "',";
                if (!string.IsNullOrEmpty(data.remark))
                    query += "remark = '" + data.remark + "',";
                if (!string.IsNullOrEmpty(data.qty))
                    query += "qty = '" + data.qty + "',";
                if (!string.IsNullOrEmpty(data.amount))
                    query += "amount = '" + data.amount + "',";
                if (!string.IsNullOrEmpty(data.status))
                    query += "status = '" + data.status + "',";
                if (!string.IsNullOrEmpty(data.req_id))
                    query += "req_id = '" + data.req_id + "',";
                if (!string.IsNullOrEmpty(data.edit_remark))
                    query += "edit_remark = '" + data.edit_remark + "',";
                query = query.TrimEnd(',');

                query += " WHERE id = " + data.id + " AND status = 'pending'";

                int status = cc.Insert(query);

                if (status > 0)
                {
                    json = "{'status':true,'Message':'Data Updated successfully'}";
                }
                else
                {
                    json = "{'status':false,'Message':'Edit permission is only set for pending status'}";
                }
            }

            else if (data.type.ToLower() == "delete")
            {
                string query1 = @"delete from tbl_supporters WHERE id = '" + data.id + "' and status='pending'";

                int status = cc.Insert(query1);

                if (status > 0)
                {
                    json = "{'status':true,'Message' :'Data deleted successfully.'}";
                }
                else
                {
                    json = "{'status':false,'Message' :'Oops! Something went wrong'}";
                }
            }


            json = json.Replace("'", "\"");
            Response.ContentType = "application/json";
            Response.Write(json);
            Response.End();

        }
    }
}