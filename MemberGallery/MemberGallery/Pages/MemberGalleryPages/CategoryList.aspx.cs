using MemberGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberGallery.Pages.MemberGalleryPages
{
    public partial class CategoryList : System.Web.UI.Page
    {
        private Service _service;
        // Property to return a Servince reference, if null create new one.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        public string SessionProp
        {
            get
            {
                var confirmationMessage = Session["text"] as string;
                Session.Remove("text");
                return confirmationMessage;
            }
            set { Session["text"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Service.GetContacts();
        }
       
        public void  CategoryListView_InsertItem(Category category)
        {
            //if (ModelState.IsValid)
            //{
                //try
                //{
                    Service.SaveCategory(category);
                    SessionProp = String.Format("Du har laddat upp | Kategori: {0} |", category.CategoryProp);
                //}
                //catch (Exception)
                //{
                //    ModelState.AddModelError(String.Empty, "Fel inträffade när Kunduppgift skulle Läggas till.");
                //}
                    Response.RedirectToRoute("Default");
            //}
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }
    }
}