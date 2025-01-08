<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

    <!DOCTYPE html
        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html lang="en">

    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <title>LET US HELP WAYANAD</title>
        <link rel="icon" href="images/icon_butterfly.png" type="image/gif" sizes="16x16">

        <!-- Bootstrap -->
        <link href="css/bootstrap.min.css" rel="stylesheet">
        <link href="css/style.css" rel="stylesheet">
        <link href="css/responsive.css" rel="stylesheet">
        <link rel="stylesheet" type="text/css" href="css/slider/css/style.css" />
        <link href="https://cdnjs.cloudflare.com/ajax/libs/normalize/3.0.1/normalize.css" rel="stylesheet"
            type="text/css">

        <!--FONT-->
        <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
        <link href='https://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css'>
        <link href='https://fonts.googleapis.com/css?family=Basic' rel='stylesheet' type='text/css'>
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Outfit:wght@100..900&display=swap" rel="stylesheet">

        <!--[if lt IE 9]>
      <script src="js/html5shiv.min.js"></script>
      <script src="js/respond.min.js"></script>
    <![endif]-->

        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/swiper@11/swiper-bundle.min.css" />

        <link rel="stylesheet" href="css/preloader/main.css">
        <link rel="stylesheet" href="css/progress.css">



        <style>
            /* .teamcolumn .team-name {
                min-height: 300px;
            } */
            .fullstrip {
                background-color: #00000073;
            }
        </style>
        <style>
            #info {
                -webkit-border-radius: 5px;
                -moz-border-radius: 5px;
                border-radius: 5px;
                background: #fcf8e3;
                border: 1px solid #fbeed5;
                width: 95%;
                max-width: 900px;
                margin: 0 auto 40px auto;
                font-family: arial;
                font-size: 12px;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                -o-box-sizing: border-box;
            }

            #info .info-wrapper {
                padding: 10px;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                -o-box-sizing: border-box;

            }

            #info a {
                color: #c09853;
                text-decoration: none;
            }

            #info p {
                margin: 5px 0 0 0;
            }



            #filters {
                margin: 0;
                padding: 0;
                list-style: none;
                margin-top: 14px;
                display: flex;
                flex-wrap: wrap;
                justify-content: center;
                padding: 10px 0;
            }

            #filters li {
                float: left;
                min-width: 150px;
            }

            @media (max-width: 767px) {
                #filters li {
                    min-width: 180px;
                }
            }


            #filters li span {
                display: block;
                padding: 1px 0px;
                text-decoration: none;
                color: #666;
                cursor: pointer;
                margin: 4px 4px;
                font-size: 14px;
            }

            #filters li span.active .box {
                background: #BE9F6E;
                color: #fff;
            }

            #portfoliolist {
                display: flex;
                flex-wrap: wrap;
                justify-content: center;
            }


            #portfoliolist .portfolio {
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                -o-box-sizing: border-box;
                width: 30%;
                margin: 1%;
                display: none;
                float: left;
                overflow: hidden;
            }

            .portfolio-wrapper {
                overflow: hidden;
                position: relative !important;
                background: #666;
                cursor: pointer;
            }

            .portfolio img {
                max-width: 100%;
                position: relative;
                top: 0;
                -webkit-transition: all 600ms cubic-bezier(0.645, 0.045, 0.355, 1);
                transition: all 600ms cubic-bezier(0.645, 0.045, 0.355, 1);
            }

            .portfolio .label {
                position: absolute;
                width: 100%;
                height: 40px;
                bottom: -40px;
                -webkit-transition: all 300ms cubic-bezier(0.645, 0.045, 0.355, 1);
                transition: all 300ms cubic-bezier(0.645, 0.045, 0.355, 1);
            }

            .portfolio .label-bg {
                background: #e95a44;
                width: 100%;
                height: 100%;
                position: absolute;
                top: 0;
                left: 0;
            }

            .portfolio .label-text {
                color: #fff;
                position: relative;
                z-index: 500;
                padding: 5px 8px;
            }

            .portfolio .text-category {
                display: block;
                font-size: 9px;
            }

            .portfolio:hover .label {
                bottom: 0;
            }

            .portfolio:hover img {
                top: -30px;
            }






            /*  #Mobile (Portrait) - Note: Design for a width of 320px */
            @media only screen and (max-width: 767px) {

                #portfoliolist .portfolio {
                    width: 100%;
                    margin: 1%;
                }

                #ads {
                    display: none;
                }

            }


            /* #Mobile (Landscape) - Note: Design for a width of 480px */
            @media only screen and (min-width: 480px) and (max-width: 767px) {

                #ads {
                    display: none;
                }

            }

            /* Self Clearing Goodness */
            .container:after {
                content: "\0020";
                display: block;
                height: 0;
                clear: both;
                visibility: hidden;
            }

            .clearfix:before,
            .clearfix:after,
            .row:before,
            .row:after {
                content: '\0020';
                display: block;
                overflow: hidden;
                visibility: hidden;
                width: 0;
                height: 0;
            }

            .row:after,
            .clearfix:after {
                clear: both;
            }

            .row,
            .clearfix {
                zoom: 1;
            }

            .clear {
                clear: both;
                display: block;
                overflow: hidden;
                visibility: hidden;
                width: 0;
                height: 0;
            }
        </style>
    </head>

    <body>
        <img src="https://letushelpwayanad.com/images/pinarayi_vijayan.png" class="icon"
                                        alt="" style="width: 0; height: 0;" />

        <%--<div id="loader-wrapper">
            <div id="loader"></div>

            <div class="loader-section section-left"></div>
            <div class="loader-section section-right"></div>

            </div>--%>

        <form id="from1" runat="server">
            <div class="clearfix"></div>

            <section class="section_headder ">

                <div class="inner">

                    <div class="headder fullstrip">
                        <div class="container">
                            <div class="row">
                                <div class="col-sm-4 col-lg-4 col-md-4 col-xs-10">
                                    <h3 class="logo text-center">
                                        <a href="#"> <img src="https://letushelpwayanad.com/images/logo_svg_2.svg"
                                                class="img-responsive desk"></a>
                                        <a href="#"> <img src="https://letushelpwayanad.com/images/logo_svg_2.svg"
                                                class="img-responsive mob"></a>
                                    </h3>
                                </div>
                                <div class="col-sm-8 col-lg-8 col-md-8 col-xs-2 ">
                                    <div class="row">
                                        <div class="mobimenu_button">
                                            <button class="nav-toggle">
                                                <div class="icon-menu"> <span class="line line-1"></span>
                                                    <span class="line line-2"></span> <span class="line line-3"></span>

                                                </div>
                                            </button>
                                        </div>
                                    </div>
                                    <nav class="nav is-fixed" role="navigation">
                                        <div class="wrapper wrapper-flush">

                                            <div class="nav-container">
                                                <ul class="nav-menu menu">
                                                    <!-- <li class="menu-item has-dropdown"> <a href="" class="menu-link">HOME</a>
       <ul class="nav-dropdown menu">
          <li class="menu-item"> <a href="" class="menu-link">&nbsp;</a> </li>
          <li class="menu-item has-dropdown"> <a href="" class="menu-link">&nbsp;</a>
            <ul class="nav-dropdown menu">
              <li class="menu-item"><a href="" class="menu-link">&nbsp;</a></li>
              
            </ul>
          </li>
          <li class="menu-item"> <a href="#!" class="menu-link">&nbsp;</a> </li>
        </ul>
      </li>-->
                                                    <li class="menu-item"> <a href="home" class="menu-link">HOME</a>
                                                    </li>
                                                    <li class="menu-item"> <a href="rebuildwayanad"
                                                            class="menu-link">CONTRIBUTE</a></li>
                                                            <li class="menu-item"><a href="https://donation.cmdrf.kerala.gov.in/" class="menu-link" target="_blank">CMDRF</a></li>
                                                    <li class="menu-item"> <a href="faq" class="menu-link">FAQ</a></li>
                                                    <li class="menu-item"> <a href="about" class="menu-link">ABOUT</a>
                                                    </li>
                                                    <li class="menu-item"> <a href="contact"
                                                            class="menu-link">CONTACT</a> </li>

                                                </ul>
                                            </div>
                                        </div>
                                    </nav>
                                    <section class="section_nav">

                                        <div class="home_nav">
                                            <ul style="padding-left:15px;">
                                                <li><a href="contact">CONTACT</a></li>
                                                <li><a href="about">ABOUT</a></li>
                                                <li><a href="faq">FAQ</a></li>
                                                <li><a href="https://donation.cmdrf.kerala.gov.in/" target="_blank">CMDRF</a></li>
                                                <li><a href="rebuildwayanad">CONTRIBUTE</a></li>
                                                
                                            </ul>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="container">
                                            <%-- <div class="donate_volunteer_block">
                                                <a href="rebuildwayanad" class="press_button">REBUILD WAYANAD</a>
                                                <a href="#" class="press_button">Donate</a>
                                                <a href="#" class="press_button">EDUCATIONAL SUPPORT</a>
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    </div>
            </section>
         





            <%--<a class="home-ml-eng-button" href="home-malayalam"></a>--%>
                <!-- end slider inner -->

                <style>
                    .slider_img {
                        background-image: url(https://letushelpwayanad.com/images/slider/slide_01.jpg);
                        background-size: cover;
                        z-index: 1;
                    }

                    .slider_img1 {
                        background-image: url(https://letushelpwayanad.com/images/slider/slide_02.jpg);
                        background-size: cover;
                        z-index: 1;
                    }

                    .slider_img2 {
                        background-image: url(https://letushelpwayanad.com/images/slider/slide_03.jpg);
                        background-size: cover;
                        z-index: 1;
                    }
                </style>
                <section class="section_bg-slider">
                    <div class="bg_slider">


                        <div id="wowslider-container1">
                            <div class="ws_images">
                                <ul>
                                    <li class="slider_img"><img
                                            src="https://letushelpwayanad.com/images/slider/blankimage.png" alt=""
                                            title="" id="wows1_0" /></li>
                                    <li class="slider_img1"><img
                                            src="https://letushelpwayanad.com/images/slider/blankimage.png" alt=""
                                            title="" id="wows1_0" /></li>
                                    <li class="slider_img2"><img
                                            src="https://letushelpwayanad.com/images/slider/blankimage.png" alt=""
                                            title="" id="wows1_0" /></li>

                                </ul>
                            </div>
                            <div class="ws_bullets">
                                <div>
                                    <%--<a href="#" title=""><span><img src="" alt="" />1</span></a>
                                        <a href="#" title=""><span><img src="" alt="" />2</span></a>--%>
                                </div>
                            </div>

                        </div>

                    </div>

                </section>
                <!-- end BG slider-->


                <div class="clearfix"></div>



                <section class="section_caution" style="padding: 0;">

                    <img src="https://letushelpwayanad.com/images/small-banner.png" width="100%" alt="" />


                </section>


                <%-- <section class="section_caution">
                    <div class="container">


                    </div>

                    </section>--%>

                    <div class="clearfix"></div>


                    <div class="clearfix"></div>
                    <section class="good-samaritan-div section_caution">
                        <div class="container">
                            <div class="headder_center">
                                <h3 class="mb-3">STAND WITH WAYANAD</h3>
                            </div>
                            <p class="big-heart mt-3">Wayanad, a land of unparalleled beauty,  is grappling with unimaginable pain, leaving behind a trail of devastation. <br>
                                Homes, dreams, and lives have been swept away in an instant.  The heart-wrenching cries of a disaster-stricken community, its people, animals, and the ravaged land echo through the mountains of Wayanad. <br>
                                Let us be the compassionate hands that rebuild shattered lives. With every contribution, we sow seeds of hope, nurturing a future where the scars of disaster fade and resilience blooms. <br>
                                Join us in this urgent mission to restore Wayanad. Your generosity is a lifeline, a testament to the enduring human spirit. <br>
                                No matter how small, every contribution can make a world of difference.
                            </p>







                            <asp:Label ID="lbl_cat" class="row" runat="server" Text=""></asp:Label>



                            <%--<div class=" row ">
                                <asp:Label ID="lbl_samaritan" runat="server"></asp:Label>
                        </div>--%>
                        <div class="view-all-row">
                            <a class="view-all" href="rebuildwayanad"> VIEW ALL </a>
                        </div>
                        </div>
                    </section>
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

                    <div class="clearfix"></div>

                    <section class="section_caution" style="padding: 0px;">
                        <style>
                              .modal .form-control{
                                  padding: 24px 12px;
                               }
                               .modal .btn{
                                  margin-top: 20px;
                               }
                        </style>
                        
                        <div class="container-fluid call">
                            <div class="container">
                                <div class="col-sm-12 col-md-4 col-lg-6 text-center">
                                    <h4>Love to make any other contribution</h4>

                                </div>
                                <div class="col-sm-12 col-md-8 col-lg-6 text-left d-flex"
                                    style="padding-top: 10px; padding-bottom: 10px;">
                                    <!-- <h5>+91 99999 99999</h5> -->
                                    <a href="#" data-toggle="modal" data-target="#myModal">Click to offer your contribution</a>
                                    <!-- Modal -->
                  <div class="modal fade" id="myModal" tabindex="-1" role="dialog"
                  aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                  <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">CONTRIBUTE NOW</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div class="modal-body" style="padding: 0;">

                            <div class="donate-group">
      <div class="col-sm-12">
        <asp:TextBox ID="txt_amt" onblur="vv(this)" CssClass="form-control" TextMode="number"
          required placeholder="*Amount" runat="server">
        </asp:TextBox>
      </div>   
                                  
  </div>

  <div class="donate-group">
      <div class="col-sm-6">
    <asp:TextBox ID="txt_name" class="form-control validate[required]" required
      placeholder="*Name" runat="server"></asp:TextBox>
  </div>
    <div class="col-sm-6">
      <asp:TextBox ID="txt_mobile"
        CssClass="form-control validate[maxSize[12],custom[onlyNumberSp]]"
        placeholder="*Phone Number" required runat="server" onkeypress="return isNumberKey(event);"></asp:TextBox>
        <asp:RegularExpressionValidator ID="rev_mobile" 
        ControlToValidate="txt_mobile" 
        ValidationExpression="^\d{12}$" 
        ErrorMessage="Phone number must be exactly 12 digits." 
        CssClass="text-danger" 
        runat="server"></asp:RegularExpressionValidator>
    </div>
    
      <div class="col-sm-6">
        <asp:TextBox ID="txtemail" required class="form-control validate[required,custom[email]"
          placeholder="*Email Id" runat="server"></asp:TextBox>
          <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtemail" 
        ErrorMessage="Please enter a valid email address" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
        CssClass="text-danger"></asp:RegularExpressionValidator>

      </div>      
      <div class="col-sm-6">
        <asp:TextBox ID="txt_loc" CssClass="form-control"
          placeholder="Location" runat="server"></asp:TextBox>
      </div>
  </div>


                    </div>
                         <div class="modal-footer" style="border: 0;">
   <!-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button> -->
   <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="I Agree to Contribute"
     style="text-transform:uppercase; border-radius: 4px;" onclick="Button1_Click"></asp:Button>
 </div>
                  </div>
                </div>
                                    
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </section>
                    <section class="section_caution" style="padding: 0px;">
                        <div style="background-image:url('https://letushelpwayanad.com/images/bg1.png');background-attachment: inherit;padding:40px 0px;"
                            class="xx1">
                            <div class="container cm">
                                <div class="col-sm-6 col-md-6 col-lg-4" style="text-align: center;">
                                    <img src="https://letushelpwayanad.com/images/pinarayi_vijayan.png" class="icon"
                                        alt="" style="max-height:290px;" />

                                </div>
                                <div class="col-sm-6 col-md-6 col-lg-8 text">
                                    As we tirelessly work to rebuild Kerala, still recling from the devastation of two
                                    consccutive
                                    floods, we are now faced with yet another grave natural calamity. The recent
                                    landslide in
                                    Wayanad has struck us with unprecedented force, taking many precious lives and
                                    causing
                                    extensive damage to both private and public property. The suffering and challenges
                                    brought by
                                    this disaster are immense and will persist for a long time.
                                    <br />
                                    In these trying times, we carnestly seek your support and generosity to help us
                                    overcome this
                                    adversity and restore hope to the afected communitics. Your contributions will play
                                    a crucial
                                    role in our efforts to rebuild and heal.
                                    <br />
                                    Thank you for standing with us. <br />
                                    <img class="mt-3" src="https://letushelpwayanad.com/images/cm_sign.png" class="icon" alt="" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </section>

                    <section class="section_caution" style="padding: 0px;background-color:#f7f7f7;">
                        <div class="container">
                            <a href="https://donation.cmdrf.kerala.gov.in/" target="_blank"><img
                                    src="https://letushelpwayanad.com/images/cmdrf.jpg" alt=""
                                    style="width:100%;" /></a>
                        </div>
                    </section>

                    <%-- <div style="margin-top:20px;">
                        <img src="https://letushelpwayanad.com/images/bg4.png" width="100%" alt="" />
                        </div>
                        <section class="section_caution" style="background-color:#f7f7f7;">

                            <div class="container">
                                <div class="headder_center" style="">
                                    <h3>What people are saying</h3>
                                </div>

                                <div class="swiper mySwiper">
                                    <div class="swiper-wrapper">
                                        <div class="swiper-slide">
                                            <div class="testi">
                                                <img src="https://letushelpwayanad.com/images/profile.png" class="icon"
                                                    alt="" style="max-height:290px;" />
                                                At a time, when we are engaged in rebuilding Kerala, which was heavily
                                                devastated by two consecutive floods, our state is confronted with a
                                                grave
                                                natural calamity of unprecedented proportions. Only with a heavy heart
                                                can I
                                                speak of the landslide that occurred in Wayanad the other day. We have
                                                lost many
                                                precious human lives. Besides, there has been heavy loss of property,
                                                both
                                                private and public. The miseries due to landslides and the subsequent
                                                floods
                                                will not cease in the near future. We are yet to estimate the losses,
                                                arising
                                                out of this natural calamity. We can assess it only when the flood
                                                recedes.
                                                <br />
                                                <br />
                                                <b>Varun Prakash P P</b>
                                            </div>
                                        </div>
                                        <div class="swiper-slide">
                                            <div class="testi">
                                                <img src="https://letushelpwayanad.com/images/profile.png" class="icon"
                                                    alt="" style="max-height:290px;" />
                                                At a time, when we are engaged in rebuilding Kerala, which was heavily
                                                devastated by two consecutive floods, our state is confronted with a
                                                grave
                                                natural calamity of unprecedented proportions. Only with a heavy heart
                                                can I
                                                speak of the landslide that occurred in Wayanad the other day. We have
                                                lost many
                                                precious human lives. Besides, there has been heavy loss of property,
                                                both
                                                private and public. The miseries due to landslides and the subsequent
                                                floods
                                                will not cease in the near future. We are yet to estimate the losses,
                                                arising
                                                out of this natural calamity. We can assess it only when the flood
                                                recedes.
                                                <br />
                                                <br />
                                                <b>Varun Prakash P P</b>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="swiper-button-next"></div>
                                    <div class="swiper-button-prev"></div>
                                </div>




                            </div>

                        </section>--%>
                        <div>
                            <img src="https://letushelpwayanad.com/images/bg3.png" width="100%" alt="" />
                        </div>


                        <div class="clearfix"></div>
                        <!--about-->









                        <section class="section_footer">
                            <footer>
                                <div class="container">
                                    <div class="footer_top">
                                        <div class="row">
                                            <div class="col-lg-10 col-sm-10 col-md-10">
                                                <nav>
                                                    <ul>
                                                        <li><a href="home"> HOME</a></li>
                                                        <!-- <li><a href="rebuildwayanad">REBUILD WAYANAD</a></li> -->
                                                        <li><a
                                                                href="https://letushelpwayanad.com/rebuildwayanad">CONTRIBUTE</a>
                                                        </li>
                                                        <li><a href="https://donation.cmdrf.kerala.gov.in/" target="_blank">CMDRF</a></li>
                                                        <li><a href="https://letushelpwayanad.com/faq">FAQ</a></li>
                                                        <li><a href="https://letushelpwayanad.com/about">ABOUT</a></li>
                                                        <!-- <li><a href="faq">CONTACT</a></li> -->
                                                        <li><a href="https://letushelpwayanad.com/contact">CONTACT US</a></li>

                                                    </ul>
                                                </nav>
                                            </div>
                                        </div>
                                    </div>




                                </div>

                                <div class="footer_bottom">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-lg-8">
                                                <p style="padding-top: 14px;display:inline-block;">
                                                    &copy;
                                                Copyright 2024. All rights reserved by Government of Kerala, Managed By District Administration Wayanad, <br> Designed and Developed By git DGTL</p>

                                            </div>
                                            <div class="col-lg-2">
                                                <div style="text-align:right;padding:10px 0px;"
                                                    class="copy-right txt_c">


                                                    <p style="line-height: 30px;display:inline-block;">
                                                        Powered by:
                                                    </p>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class="copy-right txt_c">
                                                    <a href="http://www.gitonline.in/" target="_blank"><img
                                                            src="https://letushelpwayanad.com/images/git.png"
                                                            alt="" /></a>

                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <!--end footer top-->

                            </footer>

                        </section>

                
            
               </form>
            
            <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
                        <script>
                            $(function () {
                                var html = $('html, body'),
                                    navContainer = $('.nav-container'),
                                    navToggle = $('.nav-toggle'),
                                    navDropdownToggle = $('.has-dropdown');

                                // Nav toggle
                                navToggle.on('click', function (e) {
                                    var $this = $(this);
                                    e.preventDefault();
                                    $this.toggleClass('is-active');
                                    navContainer.toggleClass('is-visible');
                                    html.toggleClass('nav-open');
                                });

                                // Nav dropdown toggle
                                navDropdownToggle.on('click', function () {
                                    var $this = $(this);
                                    $this.toggleClass('is-active').children('ul').toggleClass('is-visible');
                                });

                                // Prevent click events from firing on children of navDropdownToggle
                                navDropdownToggle.on('click', '*', function (e) {
                                    e.stopPropagation();
                                });
                            });
                        </script>
                        

                        <!-- <script src="http://code.jquery.com/jquery-2.1.1.min.js"></script>  -->

                        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->



                        <!-- Include all compiled plugins (below), or include individual files as needed -->
                        <script src="js/bootstrap.min.js"></script>
                        <!--BG slider-->
                        <script type="text/javascript" src="css/slider/js/jquery.js"></script>
                        <script type="text/javascript" src="css/slider/js/wowslider.js"></script>
                        <script type="text/javascript" src="css/slider/js/script.js"></script>

                        <!--end slider-->

                        <script src="css/preloader/vendor/modernizr-2.6.2.min.js"></script>

                        <script>	    window.jQuery || document.write('<script src="css/preloader/vendor/jquery-1.9.1.min.js"><\/script>')</script>
                        <script src="css/preloader/main.js"></script>




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

                            function vv(x) {
  if (x.value=="" || x.value=="0") {
    alert('Please enter amount to proceed!');
  }
}
                        </script>

                        <script
                            src="https://www.queness.com/resources/html/simple-portfolio-page/js/jquery.mixitup.min.js"></script>
                        <script id="rendered-js">
                            $(function () {

                                var filterList = {

                                    init: function () {

                                        // MixItUp plugin
                                        // http://mixitup.io
                                        $('#portfoliolist').mixItUp({
                                            selectors: {
                                                target: '.portfolio',
                                                filter: '.filter'
                                            },

                                            load: {
                                                filter: '.Basic_Essentials'
                                            }
                                        });



                                    }
                                };



                                // Run the show!
                                filterList.init();


                            });
                            //# sourceURL=pen.js
                        </script>
                        
                        <script>
                            $(document).ready(function() {
        $("#form1").validate({
            rules: {
                txtemail: {
                    required: true,
                    email: true
                }
            },
            messages: {
                txtemail: {
                    required: "Please enter your email address",
                    email: "Please enter a valid email address"
                }
            }
        });
    });

                        </script>

<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        // Allow only numbers
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
</script>



    </body>

    </html>