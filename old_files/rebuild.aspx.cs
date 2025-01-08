using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;

public partial class scholarship : System.Web.UI.Page
{
    SqlDataAdapter sda = new SqlDataAdapter();
    Country_DAL cc = new Country_DAL();
    SafeSqlLiteral safesql = new SafeSqlLiteral();
    static string querry, condition, photo_upload, doc, date = "MMMM d yyyy";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           cat();
            scholarship_display();
        }

    }
    private void cat()
    {
        drp_cat.Items.Clear();
        string querry1 = @"SELECT id,name FROM tbl_category 
WHERE delete_status = 0 AND parent_id =0
 ORDER BY display_order asc";
        DataSet ds = cc.joinselect(querry1);
        drp_cat.DataSource = ds.Tables[0].DefaultView;
        drp_cat.DataTextField = "name";
        drp_cat.DataValueField = "id";
        drp_cat.DataBind();

        drp_cat.Items.Add(new ListItem("All", ""));
        drp_cat.SelectedValue = "";

        try {
            drp_cat.SelectedValue = safesql.SafeSqlLiterall(Page.RouteData.Values["Id"].ToString(), 2);
        }
        catch { }
       
        subcat();
    }
    private void subcat()
    {
        drp_subcat.Items.Clear();
        if (drp_cat.SelectedValue != "")
        {
            string querry1 = @"SELECT id,name FROM tbl_category 
WHERE delete_status = 0 AND parent_id ='" + drp_cat.SelectedValue + @"'
 ORDER BY display_order asc";
            DataSet ds = cc.joinselect(querry1);
            drp_subcat.DataSource = ds.Tables[0].DefaultView;
            drp_subcat.DataTextField = "name";
            drp_subcat.DataValueField = "id";
            drp_subcat.DataBind();

            drp_subcat.Items.Add(new ListItem("All", ""));
            drp_subcat.SelectedValue = "";
        }
        else
        {
            drp_subcat.Items.Add(new ListItem("All", ""));
            drp_subcat.SelectedValue = "";
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        scholarship_display();
    }


    private void scholarship_display()
    {
        string querry1 = @"SELECT r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name cat,r.descri,r.cat_id,isnull(sum(s.amount),0) achieved,'' photo,'' perc
FROM tbl_requirements r
inner join tbl_category c on r.cat_id=c.id 
left join tbl_supporters s on r.id=s.req_id and s.status='approved'
WHERE r.delete_status = 0 AND r.status = 'pending'  AND r.status = 'pending' ";

        //if(drp_cat.SelectedValue!="")
        //{
        //    querry1 += " and r.";
        //}


querry1+= @"group by r.id,r.heading,r.esti_budj,r.qty,r.total_amt,r.status,c.name,r.descri,r.cat_id
 ORDER BY r.id desc";
        DataSet ds = cc.joinselect(querry1);
        for (int intCount = 0; intCount < ds.Tables[0].Rows.Count; intCount++)
        {
            string heading_encoded = ds.Tables[0].Rows[intCount]["heading"].ToString();

            heading_encoded = Regex.Replace(heading_encoded, @"<[^>]*>", String.Empty);

            if (heading_encoded.Length > 150)
            {
                heading_encoded = heading_encoded.Substring(0, 150) + "...";
            }

            ds.Tables[0].Rows[intCount]["heading"] = heading_encoded;

            string des_encoded = ds.Tables[0].Rows[intCount]["descri"].ToString();

            des_encoded = Regex.Replace(des_encoded, @"<[^>]*>", String.Empty);

            if (des_encoded.Length > 150)
            {
                des_encoded = des_encoded.Substring(0, 150) + "...";
            }

            ds.Tables[0].Rows[intCount]["descri"] = des_encoded;



            List<string> imageFiles = new List<string>();
            string[] imageExtensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp", "*.tiff" };
            string folderPath = Server.MapPath("uploads/requirements/" + ds.Tables[0].Rows[intCount]["id"].ToString());
            if (Page.RouteData.Values["Id"]!=null)
            {
                folderPath = Server.MapPath("../uploads/requirements/" + ds.Tables[0].Rows[intCount]["id"].ToString());
            }
            
            
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
                img = "https://" + HttpContext.Current.Request.Url.Authority + "/uploads/requirements/" + ds.Tables[0].Rows[intCount]["id"].ToString() + "/" + list1.ToString() + "'},";
            }

            ds.Tables[0].Rows[intCount]["photo"] = "" + img;



            double total = Convert.ToDouble(ds.Tables[0].Rows[intCount]["qty"].ToString());
            double achieved = Convert.ToDouble(ds.Tables[0].Rows[intCount]["achieved"].ToString());

            double perc = Convert.ToDouble(achieved / total) * 100;

           
            ds.Tables[0].Rows[intCount]["perc"] = "" + perc;
           
        }

        ds.Tables[0].AcceptChanges();

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label L6 = (Label)e.Row.FindControl("Label3");
        Label L4 = (Label)e.Row.FindControl("Label4");
        Label L5 = (Label)e.Row.FindControl("Label5");
        Label L66 = (Label)e.Row.FindControl("Label6");

        System.Web.UI.WebControls.Image im1 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

        HtmlGenericControl div = e.Row.FindControl("gd_div") as HtmlGenericControl;

        if (e.Row.DataItem != null)
        {

            //if (L6.Text == "profile_photo.jpg")
            //{
            //    im1.ImageUrl = "images/profile_photo.jpg";
            //}
            //else
            //{
            //    im1.ImageUrl = L6.Text;
            //}

        }
    }
  
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        scholarship_display();
    }


    protected void drp_cat_SelectedIndexChanged(object sender, EventArgs e)
    {
        subcat();
    }
}