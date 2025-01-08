using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.IO;
using System.Security.Cryptography;

public partial class scholorship : System.Web.UI.Page
{

    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, photo_upload, doc, date = "MMMM d yyyy";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            display();
        }
    }

    public void display()
    {
        string querry1 = @"SELECT r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name cat,r.descri,r.cat_id,isnull(sum(s.amount),0) achieved,'' photo
FROM tbl_requirements r
inner join tbl_category c on r.cat_id=c.id 
left join tbl_supporters s on r.id=s.req_id and s.status='approved'
WHERE r.delete_status = 0 AND r.status = 'pending'  AND r.status = 'pending'   and r.id='" + safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) + @"'
group by r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name,r.descri,r.cat_id
 ORDER BY r.id desc";
        DataSet ds = cc.joinselect(querry1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_head.Text = ds.Tables[0].Rows[0]["heading"].ToString();
            lbl_desc.Text = ds.Tables[0].Rows[0]["descri"].ToString();


            List<string> imageFiles = new List<string>();
            string[] imageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp", "*.tiff" };
            string folderPath = Server.MapPath("../uploads/requirements/" + ds.Tables[0].Rows[0]["id"].ToString());
            try
            {
                foreach (string extension in imageExtensions)
                {
                    imageFiles.AddRange(Directory.GetFiles(folderPath, extension, SearchOption.AllDirectories));
                }
            }
            catch (Exception ex)
            {
            }
            string img = "",a= @" <div class='post-image'><div class='swiper mySwiper'>
  <div class='swiper-wrapper'>
    ";
            for (int i1 = 0; i1 < imageFiles.Count; i1++)
            {
                string list1 = Path.GetFileName(imageFiles[0]);
                img = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/" + ds.Tables[0].Rows[0]["id"].ToString() + "/" + list1.ToString() + "'},";


                a += @"
        <div class='swiper-slide'>
     <img src='" + img+@"' class='' alt='' />    
</div>
";
            
            
            
            }
            a += @"
  </div>
  <div class='swiper-button-next'></div>
  <div class='swiper-button-prev'></div>
</div></div>";

           // lbl_unit_price.Text = "" + ds.Tables[0].Rows[0]["esti_budj"].ToString() + "";
            lbl_achieved.Text = "" + ds.Tables[0].Rows[0]["achieved"].ToString() + "";
            lbl_total.Text = "" + ds.Tables[0].Rows[0]["qty"].ToString() + "";


            //lbl_img.Text = "<img class='img-responsive' src='" + img + "' >";
            lbl_img.Text = a;








            lbl_supporters.Text += "";
            Lbl_supporters1.Text = "0";
            Lbl_supporters2.Text = "0";
            DataSet ds1 = new DataSet();
            querry1 = @"select name,mobile,hide_identity,sum(amount) amount from tbl_supporters 
where req_id=" + ds.Tables[0].Rows[0]["id"].ToString()+ @" and status='approved'
group by name,mobile,hide_identity ";            
            ds1 = cc.joinselect(querry1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Lbl_supporters1.Text = ds1.Tables[0].Rows.Count+"";
                Lbl_supporters2.Text = ds1.Tables[0].Rows.Count+"";
                for (int i1 = 0; i1 < ds1.Tables[0].Rows.Count; i1++)
                {
                    if(ds1.Tables[0].Rows[i1]["hide_identity"].ToString()=="1")
                    {
                        lbl_supporters.Text += " <div class='row'><div class='col-md-6 col-sm-2 smll'><span class='letter'>U</span>Unknown</div>";
                    }
                    else
                    {
                        lbl_supporters.Text += " <div class='row'><div class='col-md-6 col-sm-2 smll'><span class='letter'>" + ds1.Tables[0].Rows[i1]["name"].ToString().Substring(0, 1) + "</span>" + ds1.Tables[0].Rows[i1]["name"] + "</div>";
                    }
                    
                    lbl_supporters.Text += " <div class='col-md-6 col-sm-2 smll1'><h3 class='d-amount'>  "+ ds1.Tables[0].Rows[i1]["amount"] + " </h3></div>";
                    lbl_supporters.Text += " <div class='clearfix'></div><hr/></div>";
                }
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int hide = 0;
        if(CheckBox1.Checked)
        {
            hide = 1;
        }

        if(Convert.ToInt32(txt_amount.Text)>Convert.ToInt32(lbl_achieved.Text))
        {
            Response.Write("<script>alert('You have entered exceeded quantity!');</script>");
            return;
        }

        string query = @"
                INSERT INTO tbl_supporters
                (req_id, hide_identity, name, email, mobile, address, pincode, district, institute,logo,amount,qty,remark,status,addedon) 
                VALUES
                ('" + safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) + "'" +
                ", '" +hide+ "', '" + safesql.SafeSqlLiterall(txt_name.Text, 2) + "','" + safesql.SafeSqlLiterall(txtemail.Text, 2) + "','" + safesql.SafeSqlLiterall(txt_mobile.Text, 2) + "'" +
                ",'" + safesql.SafeSqlLiterall(txt_address.Text, 2) + "', '" + safesql.SafeSqlLiterall(txt_pin.Text, 2) + "', '" + safesql.SafeSqlLiterall(txt_district.Text, 2) + "'" +
                ",'" + safesql.SafeSqlLiterall(txt_insti.Text, 2) + "','" + FileUpload1.FileName + "'" +
                ",'" + safesql.SafeSqlLiterall(txt_amount.Text, 2) + "', '0', '','pending'" +
                ", GETDATE())";

        int status = cc.Insert(query);

       if (status > 0)
       {
            query = @"select top 1 id from tbl_supporters 
where req_id=" + safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2) + @" and mobile='"+ safesql.SafeSqlLiterall(txt_mobile.Text, 2) + @"'
order by id desc ";
            DataSet ds1 = cc.joinselect(query);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                string id = ds1.Tables[0].Rows[0]["id"].ToString() + "";
                if (FileUpload1.HasFile)
                {

                    if (!Directory.Exists(Server.MapPath("../uploads/institute_logo/" + id)))
                    {
                        Directory.CreateDirectory(Server.MapPath("../uploads/institute_logo/" + id));
                    }


                    FileUpload1.SaveAs(Server.MapPath("../uploads/institute_logo/" + id + "/" + FileUpload1.FileName));
                }
            }
            


            // String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            // string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");

            // String sub = "Compassionate Kozhikode - Good Samaritan";
            // string desc = "";

            // desc += "<div style='height: auto;width: 500px;border:3px solid #ed7705;float:left;'>";
            // desc += "<div style='width:500px;height:60px;float:left;border-bottom:1px dashed #666;padding-top:10px;'><div style='width:240px;height:60px;float:left;'><a href='" + url + "' target='_blank'><img src='" + url + "images/logo.png' border='0' alt='" + url + "' /></a></div></div>";

            // desc += "<div style='width:480px;height:auto;float:left;padding:5px;font-family:Arial, Helvetica, sans-serif;font-size:14px;text-align:justify;line-height:20px;'><h3>Dear " + txt_name.Text + "</h3>";
            // desc += "<p>Thank you for registering with us .<br/><br/>We will contact you soon<br/><br/> ";

            // desc += "<div style='width:500px;height:auto;float:left;text-align:center;font-family:Arial, Helvetica, sans-serif;font-size:11px;'></div></div>";

            // mail mail_obj = new mail();
            // mail_obj.sendMail(txtemail.Text, sub, desc);

            // ///

            // String sub1 = "Compassionate Kozhikode - Good Samaritan";
            // string desc1 = "";

            // desc1 += "<div style='height: auto;width: 500px;border:3px solid #ed7705;float:left;'>";
            // desc1 += "<div style='width:500px;height:60px;float:left;border-bottom:1px dashed #666;padding-top:10px;'><div style='width:240px;height:60px;float:left;'><a href='" + url + "' target='_blank'><img src='" + url + "images/logo.png' border='0' alt='" + url + "' /></a></div></div>";

            // desc1 += "<div style='width:480px;height:auto;float:left;padding:5px;font-family:Arial, Helvetica, sans-serif;font-size:14px;text-align:justify;line-height:20px;'>";
            // desc1 += "<h3>" + DateTime.Now.ToShortDateString() + "</h3> </br>";

            // desc1 += "<p>A member has been registered for " + lbl_head.Text + " </p> ";
            // desc1 += "<p>[NAME] : <b> " + txt_name.Text + "</b></p> ";
            // desc1 += "<p>[EMAIL ID] : <b> " + txtemail.Text + "</b></p> ";
            // desc1 += "<p>[CONTACT NUMBER] : <b> " + txt_mobile.Text + "</b></p> ";

            // desc1 += "<p>Best Wishes,</p> ";
            // desc1 += "<p>Compassionate Kozhikode</p><br/> ";


            // desc1 += "<div style='width:500px;height:auto;float:left;text-align:center;font-family:Arial, Helvetica, sans-serif;font-size:11px;'></div></div>";

            // mail mail_obj1 = new mail();
            // mail_obj1.sendMail("info@compassionatekozhikode.in", sub1, desc1);
            //// mail_obj1.sendMail("subhisha89@gmail.com", sub1, desc1);

            txt_address.Text = "";
           txt_mobile.Text = "";
           txt_name.Text = "";
           txtemail.Text = "";
           txt_amount.Text = "";
                CheckBox1.Checked = false;
                txt_pin.Text = "";
                txt_district.Text = "";
                txt_insti.Text = "";
           Response.Redirect("../success/6");
            
       }
    }
}