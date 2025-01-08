<%@ Page Title="" Language="C#" MasterPageFile="~/prelogin.master" AutoEventWireup="true" CodeFile="rebuild.aspx.cs" Inherits="scholarship" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
.grid table tr{float:left;width:25%;
                   min-height: 1px;
    padding-left: 15px;
    padding-right: 15px;
    position: relative;
                   }
.grid table tr td{width:60%;}
.grid table tr table{width: 100%;}
.grid table tr th{display:none;}
@media(max-width: 991px)
{
    .grid table tr{width:33.33%;
                   }
}
@media(max-width: 768px)
{
    .grid table tr{width:50%;
                   }
}
@media(max-width: 668px)
{
    .grid table tr{width:100%;
                   }
}

.pagination table, .pagination td
    {
        padding:0px !important;
        border:none !important;
    }
    .pagination a, .pagination span {
   /* padding: 0 10px;
    color: #a8a8a8;
    float: left;
    margin-right: 6px;
    font-size: 15px; */
    position: relative;
    float: left;
    padding: 6px 12px;
    margin-left: -1px;
    line-height: 1.42857143;
    color: #337ab7;
    text-decoration: none;
    background-color: #fff;
    border: 1px solid #ddd;
}
.pagination span {
    background: #fecb77;
    border: 1px solid #e0e0e0;
    color: #373c43;
    font-weight: bold;
}
.pagination {
   clear:both;
    position: absolute;
}
</style>
    <link rel="stylesheet" href="https://wayanad.gitonline.in/css/progress.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	

	<section class="inner_banner">
            	<div class="container">
                	<div class="page_name">
                    	<h3>STAND WITH WAYANAD</h3>
                    </div>
                    <div class="paging">
                    	<ul>
                        	<li><a href="https://letushelpwayanad.com/home">HOME</a></li>
                            <li><a href='https://letushelpwayanad.com/rebuildwayanad'>STAND WITH WAYANAD</a></li>
                     
                            
                        </ul>
                    </div>
                </div>
      	    </section>
          
        
        	 <!-- end slider inner -->
        
  		
       
       <!-- end BG slider-->
        
      
        <div class="clearfix"></div>
        
        	<!--end caution-->		
        
        
        						<div class="home-inner">
        		<section class="good-samaritan-div" style="">
                		<div class="container" >
                         
<p class="big-heart"> Kerala is grappling with the devastation caused by multiple landslides and flash floods, which have claimed over 400 lives, injured many more, and destroyed human habitats in several areas, including Mundakkai, Wayanad, on July 30, 2024. Now, sustained and coordinated efforts are essential to compensate for the loss of lives and livelihoods, rehabilitate the displaced, and rebuild their homes. You can support the recovery of those affected by natural disasters by generously contributing. </p>
                        
                            


                            <div class="row">
                                 <div class="col-sm-4 col-lg-4 col-md-4 col-xs-10">
                                     <asp:DropDownList ID="drp_cat" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="drp_cat_SelectedIndexChanged"></asp:DropDownList>
 </div>
                                                                <div class="col-sm-4 col-lg-4 col-md-4 col-xs-10">
                                    <asp:DropDownList ID="drp_subcat" CssClass="form-control"  runat="server"></asp:DropDownList>
</div>
                                                                <div class="col-sm-4 col-lg-4 col-md-4 col-xs-10">
                                    <asp:Button ID="Button1" class="xtra_button" runat="server" Text="Search" style="margin-left:24%;text-transform:uppercase ;" 
    onclick="Button1_Click"></asp:Button>
</div>
                                </div>


                            	
                            <div class="dt_body">
                            	<div class="row">
                                <div class="grid">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" EmptyDataText="No Data Found !!!" PageSize="16"
                                        AutoGenerateColumns="False" GridLines="None" ShowHeader="False" Width="100%"
                                        onrowdatabound="GridView1_RowDataBound" 
                                        onpageindexchanging="GridView1_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="" id="gd_div" runat="server">



                                                      
                                                        <div class="teamcolumn">
                                                            <div class="teamcolumn-inner">
                                                                <figure class="view1 view-first1">
                                                                    <img src='<%# Eval("photo") %>' alt="" style="width:100%;">
                                                                    <figcaption class="mask"><div class="maskinner">
                                                                        <a class="text" href='<%# "https://" + HttpContext.Current.Request.Url.Authority +"/details/"+Eval("id") %>' > READ MORE </a>
                                                                        </div>
                                                                    </figcaption>
                                                                </figure>  
                                                                <div class="team-name">
                                                                    <h4><a class="text" href='<%# "https://" + HttpContext.Current.Request.Url.Authority +"/details/"+Eval("id") %>' ><%# Eval("heading") %></a></h4>
                                                                    <p><%# Eval("descri") %></p>
                                                                   <%-- <div class="smll">Approximate. Unit Budget : ₹<%# Eval("esti_budj") %></div>--%>
                                                                    <hr>
                                                                    <div class="col-md-6 col-sm-2 smll">Committed Quantity :<h3 class="d-amount"> <%# Eval("achieved") %></h3></div>
                                                                    <div class="col-md-6 col-sm-2 smll1">Required Quantity:<h3 class="d-amount"> <%# Eval("qty") %></h3></div>


                                                                    <div class='clearfix'></div>
<div class='w3-light-grey w3-round-xlarge'>
    <div class='w3-container w3-round-xlarge <%# Convert.ToDouble(Eval("perc"))<=0 ? "":"w3-blue" %>' style='width:<%# Eval("perc") %>%'><%# Eval("perc") %>%</div>
  </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                   



                                  
                                    </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                                    <PagerStyle CssClass="pagination"  Width="100%" />
                                    </asp:GridView>
</div>

                                	
                                  
                                    
                                </div>
                           <div class="clearfix"></div>
                              
                             <!--end scholarship-->

                            </div>

                    </div>
                </section>
                
                	<!--end donation-->
                
              
                
                <div class="clearfix"></div>
                                    <div>
    <img src="images/bg3.png" width="100%" alt="" />
    </div>
                 
        </div>



</asp:Content>


