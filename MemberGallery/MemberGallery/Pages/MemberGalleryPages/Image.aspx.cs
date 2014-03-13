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
        public string PrevPage
        {
            get
            {
                var message = Session["text"] as string;

                return message;
            }

            set { Session["text"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public MemberGallery.Model.Image FormView_GetItem([RouteData] int ImageID)
        {
            return Service.GetImageByImageID(ImageID);
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void FormView_UpdateItem(int ImageID)
        {
            if (ModelState.IsValid)
            {
                var image = Service.GetImageByImageID(ImageID);
                if (image == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Image with id {0} was not found", ImageID));
                    return;
                }

                TryUpdateModel(image);
                if (ModelState.IsValid)
                {
                    Service.SaveImage(image);
                   
                }
                Page.SetTempData("Confirmation", String.Format(" Efter Redigering är uppgifterna | Bildnamn: {0} | | Redigerad: {1} | sparade.", image.ImgName, image.UpLoaded));
                
                //Page.SetPrevPage("PrevPage")
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
                    
                 
                    image.DeleteImage(image.SaveName);

                }
                Page.SetTempData("Confirmation", String.Format(" Du har tagit bort bilden : {0}{1}", image.ImgName, image.Extension));
                Response.RedirectToRoute("ImageList");
                Context.ApplicationInstance.CompleteRequest();
                    
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Raderas.");
            }

        }
    }
}