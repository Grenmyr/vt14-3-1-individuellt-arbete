using MemberGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberGallery.Pages.MemberGalleryPages
{
    public partial class Image : System.Web.UI.Page
    {
        private Service _service;
        // Property to return a Servince reference, if null create new one.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        private MemberGallery.Model.Image _image;
        // Property to return a Image reference, if null create new one.
        private MemberGallery.Model.Image ImageProp
        {
            get { return _image ?? (_image = new MemberGallery.Model.Image()); }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Method to return Image by ID. I collect ID using Routedata from Browser.
        public MemberGallery.Model.Image FormView_GetItem([RouteData] int ImageID)
        {
             

            return Service.GetImageByImageID(ImageID); 
        }

        // Method that make SQL call that returns image by ID.
        public void FormView_UpdateItem(int ImageID)
        {

            var image = Service.GetImageByImageID(ImageID);
            if (image == null)
            {
                // The item wasn't found
                ModelState.AddModelError(String.Empty, String.Format("Bilden kunde med ImageID {0} inte hittas.", image.ImageID));
                return;
            }
            // Validating on server that image is valid.
            if (TryUpdateModel(image))
            {
                Service.SaveImage(image);

                Page.SetTempData("Confirmation", String.Format(" Efter Redigering är uppgifterna  Bildnamn: {0} sparade.", image.ImgName));
                Response.RedirectToRoute("Image");
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        public void FormView_DeleteItem(int imageID, [RouteData] short CategoryID)
        {
            // Anropar Lagrad procedur och ta bort från bilden från kategorin. 
            //Retunerar en out parameter som är en count på hur många katerier som är kvar. om 0 så går jag in i IF satsen och tar även bort fil från disk.
            try
            {
                var image = ImageProp;
                var remainingCategories = Service.DeleteImage(imageID, CategoryID);
                image = Service.GetImageByImageID(imageID);
                if (remainingCategories == 0)
                {
                    Page.SetTempData("Confirmation", String.Format(" Du har tagit bort bilden : {0}", image.ImgName));
                    image.DeleteImage(image.SaveName);

                }
                Page.SetTempData("Confirmation", String.Format(" Du har tagit bort bilden : {0}", image.ImgName));
                Response.RedirectToRoute("ImageList");
                Context.ApplicationInstance.CompleteRequest();

            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Raderas.");
            }

        }

        // The id parameter should match the DataKeyNames value set on the control
        // or be decorated with a value provider attribute, e.g. [QueryString]int id
        public object CategoryFormView_GetCategoryByID([RouteData] int CategoryID)
        {
            return Service.GetCategoryByCategoryID(CategoryID);
        }
    }
}