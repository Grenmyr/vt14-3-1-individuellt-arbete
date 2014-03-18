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
        private MemberGallery.Model.Image _image;
        private Service _service;
        /// <summary>
        /// Property to return a Servince reference, if null create new one.
        /// </summary>
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        

        /// <summary>
        /// Property to return a Image reference, if null create new one.
        /// </summary>
        private MemberGallery.Model.Image ImageProp
        {
            get { return _image ?? (_image = new MemberGallery.Model.Image()); }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Method to return Imagedescextmsion by CategoryID and ImageID.
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="ImageID"></param>
        /// <returns>ImageDescExtension</returns>
        public ImageDescExtension FormView_GetItem([RouteData] int CategoryID, [RouteData] int ImageID)
        {
            return Service.GetImageDescExtension(CategoryID, ImageID); 
        }

        /// <summary>
        /// Method that make SQL call that call for imagedesc by imagedescID.
        /// </summary>
        /// <param name="ImgDescID"></param>
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
        /// <summary>
        /// First get my imagedescextension that contains extension of SaveName (IS located in Image table) by categoryID and ImageID.
        /// Then return an int and if its 0 it means image is't tied to any categories, then i delete it from disk.
        /// </summary>
        /// <param name="ImgDescID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="ImageID"></param>
        public void FormView_DeleteItem(int ImgDescID, [RouteData] short CategoryID, [RouteData] int ImageID)
        {
            try
            {
                var imageDescExt = Service.GetImageDescExtension(CategoryID, ImageID);
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

        /// <summary>
        /// The id parameter should match the DataKeyNames value set on the control
        /// or be decorated with a value provider attribute, e.g. [QueryString]int id
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns>Category</returns>
        public Category CategoryFormView_GetCategoryByID([RouteData] int CategoryID)
        {
            return Service.GetCategoryByCategoryID(CategoryID);
        }
    }
}