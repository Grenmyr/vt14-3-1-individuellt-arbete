using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MemberGallery.App_Infrastructure
{
    public class RouteConfig
    {
        // TODO : Ska lägga in Error page senare.

        public static void SetRoute(RouteCollection route)
        {
            // Along with global.asx this routes are used to navigate on pages. First value is routename, second Name in browserfield, third is location and name of webforms name.
            route.MapPageRoute("error", "Error", "~/Error.aspx");
    
            route.MapPageRoute("CategoriesList", "", "~/Pages/MemberGalleryPages/CategoryList.aspx");
            route.MapPageRoute("Default", "", "~/Pages/Shared/Categories.ascx");
            //route.MapPageRoute("Categories", "Kategorier", "~/Pages/MemberGalleryPages/Categories.ascx");
            route.MapPageRoute("UpLoad", "Ladda UPP", "~/Pages/MemberGalleryPages/Shared/Categories.ascx");
            // Sending using CategoryID and Image in this 2 pages browserfield.
            route.MapPageRoute("ImageList", "BildKategori/{CategoryID}", "~/Pages/MemberGalleryPages/ImageList.aspx");
            route.MapPageRoute("Image", "BildKategori/{CategoryID}/Bild/{ImageID}", "~/Pages/MemberGalleryPages/Image.aspx");
        }
    }
}