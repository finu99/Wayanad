using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class success : System.Web.UI.Page
{
   
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, photo_upload, doc;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "1")
        {
            Label1.Text += "<p>Thank you for expressing your interest to volunteer.We will contact you soon to update more </p>";
            Label1.Text += "<h3 class='style-success'>Congratulations !!</h3>";
            Label1.Text += "<a href='volunteer' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";
        
        }
        else if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "2")
        {
            Label1.Text += "<p>Congratulations! The Donation Process has been Intiated!</p>";
            Label1.Text += "<h3 class='style-success'>Successfully !!</h3>";
            Label1.Text += "<a href='donate' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";
        
        }
        else if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "3")
        {
            Label1.Text += "<p>Congratulations! Scholarship Registration Process has been Intiated!</p>";
            Label1.Text += "<h3 class='style-success'>Successfully !!</h3>";
            Label1.Text += "<a href='donate' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";

        }
        else if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "4")
        {
            Label1.Text += "<p>Congratulations! Junior Development Fellow - APPLICATION FORM Completed!</p>";
            Label1.Text += "<h3 class='style-success'>Successfully !!</h3>";
            Label1.Text += "<a href='donate' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";

        }
        else if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "5")
        {
            Label1.Text += "<p>Congratulations! Professional Volunteer - APPLICATION FORM Completed!</p>";
            Label1.Text += "<h3 class='style-success'>Successfully !!</h3>";
            Label1.Text += "<a href='volunteer' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";

        }
        else if (safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) == "6")
        {
            Label1.Text += "<p>Thank You for your Support. Our team will get back to you soon!</p>";
            Label1.Text += "<h3 class='style-success'>Thank You !!</h3>";
            Label1.Text += "<a href='../rebuildwayanad' class='button_red'> &nbsp;&nbsp; OK &nbsp;&nbsp;</a>";

        }
    }
}