<%@ Page Title="" Language="C#" MasterPageFile="~/prelogin.master" AutoEventWireup="true"
  CodeFile="rebuild-more.aspx.cs" Inherits="scholorship" %>


  <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link rel="stylesheet" href="../comkoz/css/validationEngine.jquery.css" type="text/css" />
      <script src="../comkoz/js/jquery-1.8.2.min.js" type="text/javascript">
      </script>
      <script type='text/javascript'>
        var $jq = jQuery.noConflict();    
      </script>
      <script src="../comkoz/js/jquery.validationEngine-en.js" type="text/javascript" charset="utf-8">
      </script>
      <script src="../comkoz/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8">
      </script>
      <script type="text/javascript">
        $jq(document).ready(function () {
          $jq("#aspnetForm").validationEngine();
          $jq("#form1").validationEngine();
        });
      </script>

      <style>
        .donate-group {
          margin-bottom: 10px;
          width: 100%;
          float: left;
        }

        .xtra_button {
          background: #1587c2;
          padding: 10px 80px;
          text-align: center;
          margin: 20px 0;
          display: inline-block;
          color: #fff;
          transition: 0.4s;
          border: none;

        }

        .xtra_button:hover {
          background: #7c7c7c
        }

        .letter {
          padding: 6px 9px;
          color: #fff;
          background-color: #3c6857;
          border: 1px solid #1ae60a;
          margin-right: 3px;
          border-radius: 50%;
        }

        .team-name hr {
          margin-top: 0px;
          margin-bottom: 10px;
        }
      </style>







    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </ajaxToolkit:ToolkitScriptManager>

      <section class="inner_banner">
        <div class="container">
          <div class="page_name">
            <h3>REBUILD WAYANAD</h3>
          </div>
          <div class="paging">
            <ul>
              <li><a href="../home">HOME</a></li>
              <li><a href="../rebuildwayanad">REBUILD WAYANAD</a></li>


            </ul>
          </div>
        </div>
      </section>


      <!-- end slider inner -->



      <!-- end BG slider-->


      <div class="clearfix"></div>

      <!--end caution-->


      <div class="home-inner">
        <section class="section-institute">
          <div class="container">
            <asp:Label Font-Bold="true" ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label Font-Bold="true" ID="lbl_ins" runat="server" Text="" Visible="false"></asp:Label>
            <div class="tittle-5">
              <h2>
                <asp:Label ID="lbl_head" runat="server" Text=""></asp:Label>
              </h2>

            </div>
            <div class="dt_body row">
              <div class="col-lg-6 col-sm-6 col-md-6">
                
                 <%-- <div class="post-image"><div class="swiper mySwiper">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide">
                            
                        </div>
                        <div class="swiper-slide">
                        
                        </div>
                    </div>
                    <div class="swiper-button-next"></div>
                    <div class="swiper-button-prev"></div>
                </div></div>--%>
                  <asp:Label ID="lbl_img" runat="server" Text=""></asp:Label>
                
                <p>
                  <asp:Label ID="lbl_desc" runat="server" Text=""></asp:Label>
                </p>







                <style>
                  .swiper {
                    width: 100%;
                    height: 100%;
                  }

                  .swiper-slide {
                    text-align: center;
                    font-size: 18px;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    padding: 50px;
                  }

                  .swiper-slide img {
                    display: block;
                    width: 100%;
                    height: 100%;
                    object-fit: cover;
                  }

                  .swiper .icon {
                    width: 80px;
                    height: 80px;
                    margin: 0 auto;
                    margin-top: -66px;
                  }
                </style>

                <div class="teamcolumn ">
                  <div class="teamcolumn-inner box-shadow">



                    <div style="float:left;width:50%;">
                      <h3 class="tittle-61"><img src="/images/UserGroup.svg" alt="" /> &nbsp;&nbsp;<asp:Label
                          ID="Lbl_supporters2" runat="server" Text=""></asp:Label>&nbsp;&nbsp;Supporters</h3>
                    </div>
                    <div class="teamcolumn">
                      <div class="teamcolumn-inner">
                        <div class="team-name">
                          <asp:Label ID="lbl_supporters" runat="server" Text=""></asp:Label>
                        </div>
                      </div>
                    </div>

                  </div>
                </div>














              </div>

              <div class="col-lg-6 col-sm-6 col-md-6">


                <div class="teamcolumn ">
                  <div class="teamcolumn-inner box-shadow">



                    <div style="float:left;width:50%;">
                      <h3 class="tittle-6"><img src="/images/donate.svg" alt="" /> &nbsp;&nbsp;DONATE</h3>
                    </div>
                    <div style="float:left;width:50%;">
                      <h3 class="tittle-61"><img src="/images/UserGroup.svg" alt="" /> &nbsp;&nbsp;<asp:Label
                          ID="Lbl_supporters1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;Supporters</h3>
                    </div>


                    <div class="teamcolumn">
                      <div class="teamcolumn-inner">
                        <div class="team-name">
                          <%--<div class="smll">Approximate. Unit Budget : ₹ <asp:Label ID="lbl_unit_price"
                              runat="server" Text=""></asp:Label>
                        </div>--%>
                        <hr>
                        <div class="col-md-6 col-sm-2 smll">Committed Quantity :<h3 class="d-amount">  <asp:Label
                              ID="lbl_achieved" runat="server" Text=""></asp:Label>
                          </h3>
                        </div>
                        <div class="col-md-6 col-sm-2 smll1">Required Quantity :<h3 class="d-amount">  <asp:Label
                              ID="lbl_total" runat="server" Text=""></asp:Label>
                          </h3>
                        </div>
                      </div>
                    </div>
                  </div>


                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Amount:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_amount" CssClass="form-control" TextMode="number" placeholder="Quantity" runat="server">
                      </asp:TextBox>
                    </div>
                  </div>

                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Hide Identity:</label>--%>
                    <div class="col-sm-12">
                      <asp:CheckBox ID="CheckBox1" class="form-control" style="background: #62c19954;" Text="Hide Identity" runat="server" />
                    </div>
                  </div>

                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Name:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_name" class="form-control validate[required]" placeholder="Name"
                        runat="server"></asp:TextBox>
                    </div>
                  </div>




                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Mobile:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_mobile" CssClass="form-control validate[maxSize[12],custom[onlyNumberSp]]"
                        placeholder="Mobile" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Email:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txtemail" class="form-control validate[required,custom[email]"
                        placeholder="Email" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Address:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_address" CssClass="form-control" TextMode="MultiLine" placeholder="Address"
                        runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Pincode:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_pin" CssClass="form-control validate[maxSize[6],custom[onlyNumberSp]]"
                        placeholder="Pincode" runat="server"></asp:TextBox>
                    </div>
                  </div>
                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">District:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_district" class="form-control validate[required]" placeholder="District"
                        runat="server"></asp:TextBox>
                    </div>
                  </div>
                  <div class="donate-group">
                    <%--<label for="inputEmail3" class="col-sm-3 control-label">Institute:</label>--%>
                    <div class="col-sm-12">
                      <asp:TextBox ID="txt_insti" class="form-control validate[required]" placeholder="Institute Name"
                        runat="server"></asp:TextBox>
                    </div>
                  </div>
                  <div class="donate-group" style="display: block;">
                    <label for="inputEmail3" class="col-sm-3 control-label">Institute Logo:</label>
                    <div class="col-sm-12">
                      <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" />
                    </div>
                  </div>

                  <div class="donate-group">
                    <div class="col-sm-12">

                      <asp:Button ID="Button1" class="xtra_button" runat="server" Text="I Agree to Support"
                        style="text-transform:uppercase; width: 100%; border-radius: 4px;" onclick="Button1_Click"></asp:Button>


                    </div>
                  </div>


                </div>
              </div>


            </div>

          </div>
          <!--end doante body-->
      </div>
      </section>

      <!--end section-institute-->



      <!--end volunteer-->

      </div>


      <!-- Swiper JS -->
      <script src="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.js"></script>

      <!-- Initialize Swiper -->
      <script>
        var swiper = new Swiper(".mySwiper", {
          navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
          },
          autoplay: {
            delay: 2000,
          },
        });
      </script>


    </asp:Content>