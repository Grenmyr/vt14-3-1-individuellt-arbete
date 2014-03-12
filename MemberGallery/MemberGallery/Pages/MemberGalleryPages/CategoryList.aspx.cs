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
       
     
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
       
        public void  CategoryListView_InsertItem(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveCategory(category);

                    Page.SetTempData("Confirmation", String.Format("Bildkategori {0} har Lags till ", category.CategoryProp));
                    Response.RedirectToRoute("CategoriesList");
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Läggas till.");
                }
            }
        }
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CategoryListView_UpdateItem(int categoryID)
        {
           
                var category = Service.GetCategoryByCategoryID(categoryID);
                // Load the item here, e.g. item = MyDataLayer.Find(id);
                if (category == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Category with id {0} was not found", categoryID));
                    return;
                }
                if (TryUpdateModel(category))
                {
                    Service.SaveCategory(category);
                }
                
                Page.SetTempData("Confirmation", String.Format("Kategorinamn har ändrats till {0}", category.CategoryProp));
                Response.RedirectToRoute("CategoriesList");
                Context.ApplicationInstance.CompleteRequest();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CategoryListView_DeleteItem(int categoryID)
        {
            try
            {
                Service.DeleteCategory(categoryID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Raderas.");
            }
        }
    }
}