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
        // Method to return Imagedescextmsion by CategoryID and ImageID.
        public ImageDescExtension FormView_GetItem([RouteData] int CategoryID, [RouteData] int ImageID)
        {
            return Service.GetImageDesc(CategoryID, ImageID); 
        }

        // Method that make SQL call that returns imagedesc by imagedescID.
        public void FormView_UpdateItem(int ImgDescID)
        {

            var imageDesc = Service.GetImageDescByImageDescID(ImgDescID);
            if (imageDesc == null)
            {
                // The item wasn't found
                ModelState.AddModelError(String.Empty, String.Format("Bilden kunde med ImageID {0} inte hittas.", ImgDescID));
                return;
            }
            // Validating on server that imageDesc is valid.
            if (TryUpdateModel(imageDesc))
            {
                imageDesc.Edited = DateTime.Now;
                Service.SaveImageDesc(imageDesc);

                Page.SetTempData("Confirmation", String.Format(" Efter Redigering är uppgifterna  Bildnamn: {0} sparade.", imageDesc.ImgName));
                Response.RedirectToRoute("Image");
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        public void FormView_DeleteItem(int ImgDescID, [RouteData] short CategoryID, [RouteData] int ImageID)
        {
            
            //First get my imagedescextension object by categoryID and ImageID.
            //Then return an int and if its 0 it means image is't tied to any categories, then i delete it from disk.
            try
            {
                var imageDescExt = Service.GetImageDesc(CategoryID, ImageID);
                var remainingCategories = Service.DeleteImage(ImageID, CategoryID);
               
                if (remainingCategories == 0)
                {
                    ImageProp.DeleteImage(imageDescExt.SaveName);
                    Page.SetTempData("Confirmation", String.Format(" Du har tagit bort bilden : {0}", imageDescExt.ImgName));
                }
                Page.SetTempData("Confirmation", String.Format(" Du har tagit bort bilden : {0}", imageDescExt.ImgName));
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