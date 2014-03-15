using MemberGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberGallery.Pages.Shared
{
    public partial class Upload : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private Service _service;
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        // Used by Listview as Selectmethod. to generate thumbnails, it return a IEnumerable list.
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }
    }
}