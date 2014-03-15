using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MemberGallery.App_Infrastructure
{
    public class RouteConfig
    {
        public static void SetRoute(RouteCollection route)
        {
            // Along with global.asx this routes are used to navigate on pages. First value is routename, second Name in browserfield, third is location and name of webforms name.
            route.MapPageRoute("error", "Error", "~/Pages/Shared/Error.aspx");
    
            route.MapPageRoute("CategoriesList", "", "~/Pages/MemberGalleryPages/CategoryList.aspx");
            route.MapPageRoute("Default", "", "~/Pages/Shared/Categories.ascx");        
            // Sending using CategoryID and Image in this 2 pages browserfield.
            route.MapPageRoute("ImageList", "BildKategori/{CategoryID}", "~/Pages/MemberGalleryPages/ImageList.aspx");
            route.MapPageRoute("Image", "BildKategori/{CategoryID}/Bild/{ImageID}", "~/Pages/MemberGalleryPages/Image.aspx");
        }
    }
}