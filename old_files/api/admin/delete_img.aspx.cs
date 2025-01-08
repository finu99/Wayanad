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
    string json,data="",id="",type="", img_name="";
    static string querry;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Retrieve the type and image name from the form data
        if (Request.Form["type"] != null)
        {
            type = Request.Form["type"];
        }
        if (Request.Form["img_name"] != null)
        {
            img_name = Request.Form["img_name"];
        }

        // Determine the folder path based on the type
        string folder = "";
        string countryImagePath = "";
        if (type == "slider")
        {
            folder = "/uploads/slider/";
            countryImagePath = folder + img_name;
        }

        // Check if the file exists and delete it
        if (File.Exists(Server.MapPath(countryImagePath)))
        {
            try
            {
                // Delete the file
                File.Delete(Server.MapPath(countryImagePath));
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                // LogError(ex);
            }
        }

        // Return a JSON response
        json = "{'status':true,'Message' :'Deleted Successfully'}";
        json = json.Replace("'", "\""); // Convert to valid JSON format
        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }


}