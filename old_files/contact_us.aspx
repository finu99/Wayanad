<%@ Page Title="" Language="C#" MasterPageFile="~/prelogin.master" AutoEventWireup="true" CodeFile="contact_us.aspx.cs" Inherits="contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link rel="stylesheet" href="comkoz/css/validationEngine.jquery.css" type="text/css"/>
<script src="comkoz/js/jquery-1.8.2.min.js" type="text/javascript">
	</script>
    <script type='text/javascript'>
        var $jq = jQuery.noConflict();    
</script>
	<script src="comkoz/js/jquery.validationEngine-en.js" type="text/javascript" charset="utf-8">
	</script>
	<script src="comkoz/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8">
	</script>
	<script type="text/javascript">
	    $jq(document).ready(function () {
	        $jq("#aspnetForm").validationEngine();
	        $jq("#form1").validationEngine();
	    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<section class="inner_banner">
            	<div class="container">
                	<div class="page_name">
                    	<h3>CONTACT US</h3>
                    </div>
                    <div class="paging">
                    	<ul>
                        	<li><a href="home">HOME</a></li>
                            <li><a href="contact" >CONTACT US</a></li>
                     
                            
                        </ul>
                    </div>
                </div>
      	    </section>
          
        
        	 <!-- end slider inner -->
        
  		
       
       <!-- end BG slider-->
        
      
        <div class="clearfix"></div>
        
        	<!--end caution-->		
        
        
        						<div class="home-inner">
        		<section class="section-contact" >
                			<div class="container">
                            	
                                
                                <div class="row">
                                	<%--<div class="col-lg-7 col-md-7 col-sm-7 form-block">
                                    			<form>
  <div class="form-group">
    <label for="exampleInputEmail1">Your Name</label>
    
    <asp:TextBox ID="txtname" runat="server" class="form-control validate[required]" placeholder="Your name" aria-required="true" aria-invalid="false"></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="exampleInputPassword1">Your Email</label>
    <asp:TextBox ID="txtemail" runat="server" class="form-control validate[required,custom[email]" placeholder="Your email" aria-required="true" aria-invalid="false"></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="exampleInputEmail1">Your Mobile</label>
    
    <asp:TextBox ID="txtmobile" runat="server" class="form-control validate[maxSize[12],custom[onlyNumberSp]]"  aria-invalid="false" placeholder="Your mobile no"></asp:TextBox>
  </div>
  
  <div class="form-group">
    <label for="exampleInputEmail1">Your Message</label>
    	
        <asp:TextBox ID="txtmessage" TextMode="MultiLine" runat="server" class="form-control validate[required]" aria-invalid="false" placeholder="Your message"></asp:TextBox>
  </div>
  
  
  <asp:Button ID="Button1" runat="server" Text="Send" 
        class=" button_more" onclick="Button1_Click" />
</form>
                                    </div>--%>
                                    <div class="col-lg-5 col-md-5 col-sm-5">
                                    	<div class="cont-address">
                                        	<span class="right-icon"></span>
                                        	<h3>Contact info</h3>
                                            
                                            	<address>
                                                        <strong>Address Line1</strong>
                                                        <strong>Address Line2</strong>
                                                        <br><br>
                                                        <p><strong>Phone:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0495 1234567  </p>
                                                        <p><strong>Mobile:</strong>&nbsp;&nbsp;&nbsp;&nbsp;+91 9999999999 / +91 8888888888</p>
                                                         
                                                        <p><strong>E-mail:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;info@webisteaddress.com  </p>
                                                        <p><strong>Web:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;www.webisteaddress.com </p>
                                                
                                                </address>
                                        </div>
                                    </div>
                                    <div class="col-lg-7 col-md-7 col-sm-7">
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3909.893234476868!2d76.15338567413662!3d11.487626188707699!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3ba613c0059dc6bd%3A0x2816550e16e95853!2sMundakkai%2C%20Kerala%20673577!5e0!3m2!1sen!2sin!4v1723445636262!5m2!1sen!2sin" width="100%" height="300" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                                        </div>
                                </div>
                	<!--end section-institute-->
                
                
                	</div>   </section> </div>
      <div>
<img src="images/bg3.png" width="100%" alt="" />
</div>
</asp:Content>

