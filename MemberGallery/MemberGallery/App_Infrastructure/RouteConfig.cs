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
    
            route.MapPageRoute("Default1", "", "~/Pages/MemberGalleryPages/CategoryList.aspx");
            route.MapPageRoute("Default", "", "~/Pages/Shared/Categories.ascx");
        
            //route.MapPageRoute("Copy", "Bilder/{CategoryID}/{ImageID}", "~/MemberGalleryPages/Image.aspx");


            route.MapPageRoute("Categories", "Kategorier", "~/Pages/MemberGalleryPages/Categories.ascx");
            route.MapPageRoute("ImageList", "BildKategori/{CategoryID}", "~/Pages/MemberGalleryPages/ImageList.aspx");
            route.MapPageRoute("Image", "BildKategori/{CategoryID}/Bild/{ImageID}", "~/Pages/MemberGalleryPages/Image.aspx");
           
        }
    }
}