using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberGallery.Pages.MemberGalleryPages
{
    public partial class Image : System.Web.UI.Page
    {
        public string URL
        {
            get { return Request.QueryString["name"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                var xxx = Request.QueryString["name"]; CurrentImage.ImageUrl = "~/Content/Pictures/" + URL;
        }
    }
}