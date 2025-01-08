using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text.html.simpleparser;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class api_country_add : System.Web.UI.Page
{
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    string json, data = "", id = "", type = "", img_name = "";
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["data"] != null)
        {
            data = Request.Form["data"];
        }
        //if (Request.Form["id"] != null)
        //{
        //    id = Request.Form["id"];
        //}
        if (Request.Form["type"] != null)
        {
            type = Request.Form["type"];
        }
        if (Request.Form["img_name"] != null)
        {
            img_name = Request.Form["img_name"];
        }

        byte[] imgBytes = Convert.FromBase64String(data);


        string folder = ""; string countryImagePath = "";
        if (type == "slider")
        {
            folder = "/uploads/slider/";
            countryImagePath = folder + "/" + img_name; 
        }
    

        if (!Directory.Exists(Server.MapPath(folder)))
        {
            Directory.CreateDirectory(Server.MapPath(folder));
        }


        // Save byte array as image file
        string savePath = Server.MapPath(countryImagePath);



        File.WriteAllBytes(savePath, imgBytes);



        string imagePath = countryImagePath.Replace("../", "");

        string path = "";
        if (imagePath != "")
            path = "http://" + HttpContext.Current.Request.Url.Authority + "/" + imagePath + "";

        json = "{'status':true,'Message' :'Image Uploaded Successfully','data':[{";
        json += "'img':'" + path + "'}]}";




        json = json.Replace("'", "\"");
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();



    }


}