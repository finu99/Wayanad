<%@ Application Language="C#" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Web.Routing" %>


<script runat="server">

    //void Application_BeginRequest(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string originalURL = Request.Url.ToString().ToLower();
    //        if (originalURL.IndexOf("/news_more/") > 0)
    //        {
    //            string productid = originalURL.Substring(originalURL.IndexOf("/news_more/") + "/news_more/".Length).Split(new char[] { '/' })[0];
    //            string rewriteURL = Request.Url.AbsolutePath.Substring(0, Request.Url.AbsolutePath.IndexOf("/news_more/")) + "/news_more.aspx?id=" + productid;
    //            Context.RewritePath(rewriteURL);
    //        }

    //    }
    //    catch
    //    {

    //    }
    //}
    
    
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

        
        RegisterRoutes(RouteTable.Routes);
    }
    public static void RegisterRoutes(RouteCollection routeCollection)
    {

        
        routeCollection.MapPageRoute("RouteForhome", "home", "~/index.aspx");
        routeCollection.MapPageRoute("RouteForhome-malayalam", "home-malayalam", "~/index_malayalam.aspx");
        
        
        
        routeCollection.MapPageRoute("RouteForcontact", "contact", "~/contact_us.aspx");
        routeCollection.MapPageRoute("RouteForabout", "about", "~/about.aspx");
        routeCollection.MapPageRoute("RouteForabout-malayalam", "about-malayalam", "~/about_malayalam.aspx");
        
        
        
        routeCollection.MapPageRoute("RouteFordcip", "dcip", "~/about_dcip.aspx");
        routeCollection.MapPageRoute("RouteForfaq", "faq", "~/faq.aspx");
        routeCollection.MapPageRoute("RouteForfaq-malayalam", "faq-malayalam", "~/malayalam_faq.aspx");
        
        
        routeCollection.MapPageRoute("RouteForsuccess", "success/{id}", "~/success.aspx");
        routeCollection.MapPageRoute("RouteForblog", "blog", "~/blog.aspx");
        
        routeCollection.MapPageRoute("RouteForinstitutions", "institutions", "~/institute.aspx");
        routeCollection.MapPageRoute("RouteForinstitutions-details", "institutions-details/{id}", "~/institute_more.aspx");

        routeCollection.MapPageRoute("RouteForvolunteer", "volunteer", "~/do_you_have_time.aspx");
        routeCollection.MapPageRoute("RouteForvolunteer1", "volunteer/{id}", "~/do_you_have_time.aspx");
        routeCollection.MapPageRoute("RouteForvolunteer-details", "volunteer-details/{id}", "~/do_you_have_time_more.aspx");        

        routeCollection.MapPageRoute("RouteFordonate", "donate", "~/can_you_give.aspx");
        routeCollection.MapPageRoute("RouteFordonate1", "donate/{id}", "~/can_you_give.aspx");
        routeCollection.MapPageRoute("RouteFordonate-details", "donate-details/{id}", "~/can_you_give_more.aspx");

        routeCollection.MapPageRoute("RouteForinitiatives", "rebuildwayanad1", "~/initiatives.aspx");
        routeCollection.MapPageRoute("RouteForinitiatives-details", "rebuildwayanad1-details/{id}", "~/initiative-more.aspx");


        routeCollection.MapPageRoute("RouteForscholarship-list", "scholarship-list", "~/scholarship.aspx");
        routeCollection.MapPageRoute("RouteForscholarship", "scholarship/{id}", "~/scholorship.aspx");

        routeCollection.MapPageRoute("RouteForsamaritan-list", "rebuildwayanad", "~/rebuild.aspx");
        routeCollection.MapPageRoute("RouteForsamaritan", "rebuildwayanad/{id}", "~/rebuild.aspx");
        routeCollection.MapPageRoute("RouteForsamaritan1", "details/{id}", "~/rebuild-more.aspx");

        routeCollection.MapPageRoute("RouteForinitiatives_malayalam", "initiatives_malayalam", "~/initiatives_malayalam.aspx");
        routeCollection.MapPageRoute("RouteForinitiatives_malayalam-details", "initiatives_malayalam-details/{id}", "~/initiative_malayalam-more.aspx");
        
            
    }
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        //Session.Timeout = 60;
        //if (Session["log_info"] == null)
        //{
        //    Response.Redirect("~/admin_login.aspx");           
        //}
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                        (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new GenericPrincipal(id, roles);
                }
            }
        }
    }
       
</script>
