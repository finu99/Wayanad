using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.IO;

public partial class index : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter sda = new SqlDataAdapter();
    Country_DAL cc = new Country_DAL();
    static string date = "MMMM d yyyy", images, querry, condition;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        con.Open();
        cmd = con.CreateCommand();

        if (!IsPostBack)
        { 
            cat();
            samaritan();
            
        }
    }

    private void cat()
    {
        lbl_cat.Text = "";
        string querry1 = @"SELECT top 7 * FROM tbl_category 
WHERE delete_status = 0 AND parent_id =0
 ORDER BY display_order asc";
        string aa = "", classes = "";
        DataSet ds = cc.joinselect(querry1);
        if (ds.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                classes += "." + ds.Tables[0].Rows[i]["name"].ToString().Replace(" ", "_") + ",";
                //aa += @"<li><span class='filter' data-filter='." + ds.Tables[0].Rows[i]["name"].ToString().Replace(" ", "_") + "'>" + ds.Tables[0].Rows[i]["name"].ToString() + @"</span></li>";
                aa += @"<li><span class='filter' data-filter='." + ds.Tables[0].Rows[i]["name"].ToString().Replace(" ", "_") + "'>";
                aa += @"<div class='box'>
               <img src='https://" + HttpContext.Current.Request.Url.Authority+ "/uploads/category/" + ds.Tables[0].Rows[i]["id"].ToString() + "/"+ ds.Tables[0].Rows[i]["icon"].ToString() + @"' class='icon' alt='' />
                <h4 style='font-family: 'Outfit', sans-serif;'>"+ ds.Tables[0].Rows[i]["name"].ToString() + @"</h4>
            </div></span></li>";

            }

            //            lbl_cat.Text += @"<ul id='filters' class='clearfix'>
            //                                <li><span class='filter active' data-filter='" + classes.TrimEnd(',') + @"'>
            //<div class='box'>
            //               <img src='https://" + HttpContext.Current.Request.Url.Authority+ @"/images/icon/icon1.png' class='icon' alt='' />
            //                <h4 style='font-family: 'Outfit', sans-serif;'>All</h4>
            //            </div>
            //</span></li>";
            lbl_cat.Text += @"<ul id='filters' class='clearfix'>";
            lbl_cat.Text += "" + aa;
            lbl_cat.Text += "</ul>";
        }
    }
    private void samaritan()
    {
        string querry1 = @"SELECT r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name cat,r.descri,r.cat_id,isnull(sum(s.qty),0) achieved,r.display_order
FROM tbl_requirements r
inner join tbl_category c on r.cat_id=c.id 
left join tbl_supporters s on r.id=s.req_id and s.status='approved'
WHERE r.delete_status = 0 
--AND r.status = 'pending'
group by r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name,r.descri,r.cat_id,r.display_order
 ORDER BY r.display_order asc";

        DataSet ds = cc.joinselect(querry1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_cat.Text += @"<div id='portfoliolist'>";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<string> imageFiles = new List<string>();
                string[] imageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp", "*.tiff" };
                string folderPath = Server.MapPath("uploads/requirements/" + ds.Tables[0].Rows[i]["id"].ToString());
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
                string img = "";
                for (int i1 = 0; i1 < imageFiles.Count; i1++)
                {
                    string list1 = Path.GetFileName(imageFiles[0]);
                    img = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/" + ds.Tables[0].Rows[i]["id"].ToString() + "/" + list1.ToString() + "'},";
                }

                if (img == "")
                {
                    img = "https://" + HttpContext.Current.Request.Url.Authority + "/images/noimage.jpg";
                }







                lbl_cat.Text += "<div class='portfolio " + ds.Tables[0].Rows[i]["cat"].ToString().Replace(" ", "_") + "' data-cat='" + ds.Tables[0].Rows[i]["cat"].ToString().Replace(" ", "_") + "'>";
                lbl_cat.Text += "<div class=''>";
                lbl_cat.Text += "<div class='teamcolumn'>";
                lbl_cat.Text += "<div class='teamcolumn-inner'>";
                lbl_cat.Text += "<figure class='view1 view-first1'>";
                lbl_cat.Text += "<img src='" + img + "' alt='' style='width:100%;'>";
                lbl_cat.Text += "<figcaption class='mask'>";
                lbl_cat.Text += "<div class='maskinner'>";
                lbl_cat.Text += "<a class='text' href='https://" + HttpContext.Current.Request.Url.Authority + "/details/" + ds.Tables[0].Rows[i]["id"].ToString() + "'> Contribute </a>";
                lbl_cat.Text += "</div> ";
                lbl_cat.Text += "</figcaption>";
                lbl_cat.Text += "</figure>  ";
                lbl_cat.Text += "<div class='team-name height_300px'><div class='height_110px'>";

                string aa = ds.Tables[0].Rows[i]["heading"].ToString();
                aa = Regex.Replace(aa, @"<[^>]*>", String.Empty);

                if (aa.Length > 60)
                    lbl_cat.Text += "<h4>  " + aa.Substring(0, 60) + "... </h4>";
                else
                    lbl_cat.Text += "<h4>  " + aa + " </h4>";



                string aa1 = ds.Tables[0].Rows[i]["descri"].ToString();
                aa1 = Regex.Replace(aa1, @"<[^>]*>", String.Empty);

                if (aa1.Length > 130)
                    lbl_cat.Text += "<p>" + aa1.Substring(0, 130) + "... </p>";
                else
                    lbl_cat.Text += "<p>" + aa1 + "</p>";


                // lbl_samaritan.Text += "<div class='smll'>Approximate. Unit Budget : ₹" + ds.Tables[0].Rows[i]["esti_budj"].ToString() + "</div>";
                lbl_cat.Text += "</div><hr/>";

                lbl_cat.Text += "<div class='col-md-6 col-sm-2 smll'>Committed Quantity :";
                lbl_cat.Text += "<h3 class='d-amount'> " + ds.Tables[0].Rows[i]["achieved"].ToString() + "</h3></div>";
                lbl_cat.Text += "<div class='col-md-6 col-sm-2 smll1'>Required Quantity :";
                lbl_cat.Text += "<h3 class='d-amount'> " + ds.Tables[0].Rows[i]["qty"].ToString() + "</h3></div>";


                double total = Convert.ToDouble(ds.Tables[0].Rows[i]["qty"].ToString());
                double achieved = Convert.ToDouble(ds.Tables[0].Rows[i]["achieved"].ToString());

                double perc = Math.Round(Convert.ToDouble(achieved / total) * 100, 0);



                if (perc <= 0)
                {
                    lbl_cat.Text += @"
<div class='clearfix'></div>
<div class='w3-light-grey w3-round-xlarge'>
    <div class='w3-container w3-round-xlarge' style='width:" + perc + "%'>" + perc + @"%</div>
  </div>";
                }
                else
                {
                    lbl_cat.Text += @"
<div class='clearfix'></div>
<div class='w3-light-grey w3-round-xlarge'>
    <div class='w3-container w3-blue w3-round-xlarge' style='width:" + perc + "%'>" + perc + @"%</div>
  </div>";
                }



                lbl_cat.Text += "<a href='https://" + HttpContext.Current.Request.Url.Authority + "/details/" + ds.Tables[0].Rows[i]["id"].ToString() + "' class='intrested-btn'> Contribute </a>";

                lbl_cat.Text += "</div>";
                lbl_cat.Text += "</div>";
                lbl_cat.Text += "</div>";
                lbl_cat.Text += "</div>";
                lbl_cat.Text += "</div>";


            }
            lbl_cat.Text += "</div>";
        }
    }

    //    private void cat()
    //    {
    //        lbl_cat.Text = "";
    //        string querry1 = @"SELECT top 6 * FROM tbl_category 
    //WHERE delete_status = 0 AND parent_id =0
    // ORDER BY display_order asc";

    //        DataSet ds = cc.joinselect(querry1);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                lbl_cat.Text += @"<div class='col-sm-2 col-md-2 col-lg-2'>
    //        <div class='box'>
    //           <a href='rebuildwayanad/" + ds.Tables[0].Rows[i]["id"].ToString() + "'> <img src='https://" + HttpContext.Current.Request.Url.Authority+ "/uploads/category/" + ds.Tables[0].Rows[i]["id"].ToString() + "/"+ ds.Tables[0].Rows[i]["icon"].ToString() + @"' class='icon' alt='' />
    //            <h4 style='font-family: 'Outfit', sans-serif;'>"+ ds.Tables[0].Rows[i]["name"].ToString() + @"</h4></a>
    //        </div>
    //    </div>";
    //            }

    //        }
    //    }
    //    private void samaritan()
    //    {
    //        string querry1 = @"SELECT top 9 r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name cat,r.descri,r.cat_id,isnull(sum(s.qty),0) achieved,r.display_order
    //FROM tbl_requirements r
    //inner join tbl_category c on r.cat_id=c.id 
    //left join tbl_supporters s on r.id=s.req_id and s.status='approved'
    //WHERE r.delete_status = 0 AND r.status = 'pending'
    //group by r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name,r.descri,r.cat_id,r.display_order
    // ORDER BY r.display_order asc";

    //        DataSet ds = cc.joinselect(querry1);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {
    //                List<string> imageFiles = new List<string>();
    //                string[] imageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp", "*.tiff" };
    //                string folderPath = Server.MapPath("uploads/requirements/" + ds.Tables[0].Rows[i]["id"].ToString());
    //                try
    //                {
    //                    foreach (string extension in imageExtensions)
    //                    {
    //                        imageFiles.AddRange(Directory.GetFiles(folderPath, extension, SearchOption.AllDirectories));
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                }
    //                string img = "";
    //                for (int i1 = 0; i1 < imageFiles.Count; i1++)
    //                {
    //                    string list1 = Path.GetFileName(imageFiles[0]);                    
    //                    img = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/" + ds.Tables[0].Rows[i]["id"].ToString() + "/" + list1.ToString() + "'},";
    //                }

    //                if(img=="")
    //                {
    //                    img = "https://" + HttpContext.Current.Request.Url.Authority + "/images/noimage.jpg";
    //                }

    //                lbl_samaritan.Text += "<div class='col-md-4'>";
    //                lbl_samaritan.Text += "<div class='teamcolumn'>";
    //                lbl_samaritan.Text += "<div class='teamcolumn-inner'>";
    //                lbl_samaritan.Text += "<figure class='view1 view-first1'>";                               
    //                lbl_samaritan.Text += "<img src='" + img + "' alt='' style='width:100%;'>";
    //                lbl_samaritan.Text += "<figcaption class='mask'>";
    //                lbl_samaritan.Text += "<div class='maskinner'>";
    //                lbl_samaritan.Text += "<a class='text' href='https://" + HttpContext.Current.Request.Url.Authority +"/details/" + ds.Tables[0].Rows[i]["id"].ToString() + "'> Contribute </a>";
    //                lbl_samaritan.Text += "</div> ";
    //                lbl_samaritan.Text += "</figcaption>"; 
    //                lbl_samaritan.Text += "</figure>  ";
    //                lbl_samaritan.Text += "<div class='team-name height_300px'><div class='height_110px'>";

    //                string aa = ds.Tables[0].Rows[i]["heading"].ToString();
    //                aa = Regex.Replace(aa, @"<[^>]*>", String.Empty);

    //                if (aa.Length > 60)
    //                    lbl_samaritan.Text += "<h4>  " + aa.Substring(0,60) + "... </h4>";
    //                else
    //                    lbl_samaritan.Text += "<h4>  " + aa + " </h4>";



    //                string aa1 = ds.Tables[0].Rows[i]["descri"].ToString();
    //                aa1 = Regex.Replace(aa1, @"<[^>]*>", String.Empty);

    //                if(aa1.Length>130)
    //                    lbl_samaritan.Text += "<p>"+aa1.Substring(0,130)+"... </p>";
    //                else
    //                    lbl_samaritan.Text += "<p>"+aa1+"</p>";


    //               // lbl_samaritan.Text += "<div class='smll'>Approximate. Unit Budget : ₹" + ds.Tables[0].Rows[i]["esti_budj"].ToString() + "</div>";
    //                lbl_samaritan.Text += "</div><hr/>";

    //                lbl_samaritan.Text += "<div class='col-md-6 col-sm-2 smll'>Committed Quantity :";
    //                lbl_samaritan.Text += "<h3 class='d-amount'> " + ds.Tables[0].Rows[i]["achieved"].ToString() + "</h3></div>";
    //                lbl_samaritan.Text += "<div class='col-md-6 col-sm-2 smll1'>Required Quantity :";
    //                lbl_samaritan.Text += "<h3 class='d-amount'> " + ds.Tables[0].Rows[i]["qty"].ToString() + "</h3></div>";


    //                double total = Convert.ToDouble(ds.Tables[0].Rows[i]["qty"].ToString());
    //                double achieved = Convert.ToDouble(ds.Tables[0].Rows[i]["achieved"].ToString());

    //                double perc = Math.Round(Convert.ToDouble(achieved/total)*100,0);



    //                if (perc <= 0)
    //                {
    //                    lbl_samaritan.Text += @"
    //<div class='clearfix'></div>
    //<div class='w3-light-grey w3-round-xlarge'>
    //    <div class='w3-container w3-round-xlarge' style='width:" + perc + "%'>" + perc + @"%</div>
    //  </div>";
    //                }
    //                else
    //                {
    //                    lbl_samaritan.Text += @"
    //<div class='clearfix'></div>
    //<div class='w3-light-grey w3-round-xlarge'>
    //    <div class='w3-container w3-blue w3-round-xlarge' style='width:" + perc + "%'>" + perc + @"%</div>
    //  </div>";
    //                }



    //                lbl_samaritan.Text += "<a href='https://" + HttpContext.Current.Request.Url.Authority + "/details/" + ds.Tables[0].Rows[i]["id"].ToString() + "' class='intrested-btn'> Contribute </a>";

    //                lbl_samaritan.Text += "</div>";
    //                lbl_samaritan.Text += "</div>";
    //                lbl_samaritan.Text += "</div>";
    //                lbl_samaritan.Text += "</div>";


    //            }

    //        }
    //    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string query = @"
                INSERT INTO tbl_donate
                (amount, name, mob, email, loc, addedon, status) 
                VALUES
                ('" + txt_amt.Text + "'" +
                ", '" + txt_name.Text + "', '" + txt_mobile.Text + "','" + txtemail.Text + "','" + txt_loc.Text + "'" +                              
                ", GETDATE(),'pending')";
        int status = cc.Insert(query);
        if (status > 0)
        {            
            txt_amt.Text = "";
            txt_mobile.Text = "";
            txt_name.Text = "";
            txtemail.Text = "";
            txt_loc.Text = "";           
            Response.Redirect("success/2");
        }
    }
}