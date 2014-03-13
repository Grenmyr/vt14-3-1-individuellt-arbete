using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberGallery.Pages.Shared
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // presenting Confirmation msg from all pages using pagextension.cs
            Confirmation.Text = Page.GetTempData("Confirmation") as string;
            Confirmation.Visible =! String.IsNullOrWhiteSpace(Confirmation.Text);
        }
    }
}