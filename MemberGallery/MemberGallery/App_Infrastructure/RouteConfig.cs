using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MemberGallery.App_Infrastructure
{
    public class RouteConfig
    {
        // TODO : Ska lägga in RoutecConfig senare.

         //Extensionclass to "hide" page source. Using 2 atm error and Contact.
        public static void SetRoute(RouteCollection route)
        {
            route.MapPageRoute("error", "Error", "~/error.aspx");
            route.MapPageRoute("Start", "kontakter", "~/default.aspx");
            route.MapPageRoute("Default", "", "~/Pages/MemberGalleryPages/CategoryList.aspx");
            
            route.MapPageRoute("ImageList", "Bilder/{CategoryID}", "~/Pages/MemberGalleryPages/ImageList.aspx");

            // ska lägga in routen.
        }
    }
}