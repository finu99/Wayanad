using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;

public partial class contact_us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {



        //String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
        //string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");


        //string email = "info@compassionatekozhikode.in";

        ////string email = "subhisha89@gmail.com ";

        //string sub = "";
        //string desc1 = "";
        //desc1 += "<div style='padding:10px;border:3px solid #27788d;'><div style='padding:10px;background-color:#ffffff;'><img src='" + url + "images/logo.png' /></div>";
        //desc1 += "<br/><br/>Hi Admin, an Enquiry from <br/><br/>";


        //desc1 += "Name: " + txtname.Text.ToString() + "<br/>";


        //desc1 += "Mobile: " + txtmobile.Text.ToString() + "<br/>";
        //desc1 += "Email: " + txtemail.Text.ToString() + "<br/>";
        //desc1 += "Message: " + txtmessage.Text.ToString() + "<br/><br/>";

        //desc1 += "<br/><br/>Thank you,<br/> compassionate kozhikode<br/></div>";

        //sendMail(email, sub, desc1);

        //Response.Write("<script>alert('Your enquiry has been posted successfully'); ");
        //Response.Write("window.location='home'</script>");



    }
    private void sendMail(string to, string sub, string desc)
    {
        try
        {
            string from = "noreplygitgroup@gmail.com";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            MailAddress tos = new MailAddress(to);
            mail.To.Add(tos);
            mail.From = new MailAddress(from, "Compassionate Kozhikode", System.Text.Encoding.UTF8);
            mail.Subject = "[Enquiry from - Compassionate Kozhikode ]" + sub;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                mail.Body += desc;
            }
            catch (Exception ex)
            {
                mail.Body += desc;
            }
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential(from, "globaltechnologies");

            client.Port = 25; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer

            client.Send(mail);






        }
        finally
        {

        }
    }
}