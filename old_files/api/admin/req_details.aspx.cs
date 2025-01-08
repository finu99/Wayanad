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
        string id="", supporters_status="";
        if (Request.Form["id"] != null)
        {
            id = Request.Form["id"];
        }
        if (Request.Form["supporters_status"] != null)
        {
            supporters_status = Request.Form["supporters_status"];
        }



        string querry1 = @"SELECT r.*,c.name cat FROM tbl_requirements r
inner join tbl_category c on r.cat_id=c.id and c.delete_status=0
WHERE r.delete_status = 0 and r.id="+id;

        DataSet ds = cc.joinselect(querry1);
       
        if (ds.Tables[0].Rows.Count > 0)
        {
            json = "{'status':true,'Message' :'Success.','data':[";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string ids = ds.Tables[0].Rows[i]["id"].ToString();
                
                json += "{'heading':'" + ds.Tables[0].Rows[i]["heading"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "','id':'" + ds.Tables[0].Rows[i]["id"].ToString() + "'," +
                    "'category':'" + ds.Tables[0].Rows[i]["cat"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "','esti_budj':'" + ds.Tables[0].Rows[i]["esti_budj"].ToString() + "'," +
                    "'qty':'" + ds.Tables[0].Rows[i]["qty"].ToString() + "','total_amt':'" + ds.Tables[0].Rows[i]["total_amt"].ToString() + "'," +
                    "'status':'" + ds.Tables[0].Rows[i]["status"].ToString() + "'" +
                    ",'descri':'" + ds.Tables[0].Rows[i]["descri"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + "'" +
                    ",'imges':[";



                // List to hold image file paths
                List<string> imageFiles = new List<string>();

                // Supported image extensions
                string[] imageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp", "*.tiff" };
                string folderPath = Server.MapPath("../../uploads/requirements/" + id);
                try
                {
                    // Loop through each extension and find files
                    foreach (string extension in imageExtensions)
                    {
                        // Add files matching the current extension to the list
                        imageFiles.AddRange(Directory.GetFiles(folderPath, extension, SearchOption.AllDirectories));                    
                    }                    
                }
                catch (Exception ex)
                {
                }

                for (int i1 = 0; i1 < imageFiles.Count; i1++)
                {
                    string list1 = Path.GetFileName(imageFiles[i1]);
                    json += "{'img':'https://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/"+ids+"/" + list1.ToString() + "'},";
                }
                json = json.TrimEnd(',');



                //querry1 = "select * from tbl_requirements_img where req_id="+ids;
                //DataSet ds1 = cc.joinselect(querry1);
                //if (ds1.Tables[0].Rows.Count > 0)
                //{                    
                //    for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                //    {
                //        string imagePath = ds1.Tables[0].Rows[i]["img"].ToString();
                //        string path = "";
                //        if (imagePath != "")
                //            path = "http://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/" + ids + "/" + imagePath + "";


                //        json += "{'id':'"+ ds1.Tables[0].Rows[i]["id"].ToString() + "','img':'"+ path + "'},";
                //    }
                //    json = json.TrimEnd(',');
                //}

                json += "],'supporters':[";

                DataSet ds1 = new DataSet();
                querry1 = "select * from tbl_supporters where req_id=" + ids;
                //if(supporters_status!="")
                //{
                //    querry1 += " and status='"+supporters_status+"'";
                //}
                if (supporters_status != "" && supporters_status.ToLower() != "all")
                {
                    querry1 += " and status='" + supporters_status + "'";
                }
                ds1 = cc.joinselect(querry1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                    {

                        string imagePath = ds1.Tables[0].Rows[i1]["logo"].ToString();
                        string path = "";
                        if (imagePath != "")
                            path = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/institute_logo/" + ds1.Tables[0].Rows[i1]["id"].ToString() + "/" + imagePath + "";


                        json += "{'id':'" + ds1.Tables[0].Rows[i1]["id"].ToString() + @"'
                                 ,'hide_identity':'" + ds1.Tables[0].Rows[i1]["hide_identity"].ToString() + @"'
                                 ,'name':'" + ds1.Tables[0].Rows[i1]["name"].ToString().Replace("'", "`").Replace("\"", "").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'
                                 ,'email':'" + ds1.Tables[0].Rows[i1]["email"].ToString().Replace("'", "`").Replace("\"", "").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'
                                 ,'mobile':'" + ds1.Tables[0].Rows[i1]["mobile"].ToString().Replace("'", "`").Replace("\"", "").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'
                                 ,'address':'" + ds1.Tables[0].Rows[i1]["address"].ToString().Replace("'", "`").Replace("\"", "").Replace("<br/>", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").TrimEnd() + @"'
                                 ,'pincode':'" + ds1.Tables[0].Rows[i1]["pincode"].ToString().Replace("'", "`").Replace("\"", "").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'
                                 ,'district':'" + ds1.Tables[0].Rows[i1]["district"].ToString().Replace("'", "`").Replace("\"", "").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'                        
                                 ,'institute':'" + ds1.Tables[0].Rows[i1]["institute"].ToString().Replace("'", "`").Replace("\"", "").Replace("<br/>", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").TrimEnd() + @"'                        
                                 ,'logo':'" + path + @"'                        
                                 ,'amount':'" + ds1.Tables[0].Rows[i1]["amount"].ToString() + @"'                        
                                 ,'qty':'" + ds1.Tables[0].Rows[i1]["qty"].ToString() + @"'                        
                                 ,'remark':'" + ds1.Tables[0].Rows[i1]["remark"].ToString().Replace("'", "`").Replace("\"", "").Replace("<br/>", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").TrimEnd() + @"'   
                                 ,'edit_remark':'" + ds1.Tables[0].Rows[i1]["edit_remark"].ToString().Replace("'", "`").Replace("\"", "").Replace("<br/>", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").TrimEnd() + @"'                        

                                 ,'status':'" + ds1.Tables[0].Rows[i1]["status"].ToString().Replace("'", "`").Replace("\r", " ").Replace("\n", "<br/>").Replace("\t", " ").TrimEnd() + @"'                        
                                 ,'addedon':'" + ds1.Tables[0].Rows[i1]["addedon"].ToString() + @"'                        
                                 ,'option_selected':'" + ds1.Tables[0].Rows[i1]["options"].ToString() + @"'                        
                                 },";
                    }
                    json = json.TrimEnd(',');
                }
                ds1.Dispose();

                json +="]},";
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
