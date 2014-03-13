using MemberGallery.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace MemberGallery.Pages.MemberGalleryPages
{
    public partial class ImageList : System.Web.UI.Page
    {
        private MemberGallery.Model.Image _image;
        // Property to return a Image reference, if null create new one.
        private MemberGallery.Model.Image ImageProp
        {
            get { return _image ?? (_image = new MemberGallery.Model.Image()); }
        }
        private Service _service;
        // Property to return a Service reference, if null create new one.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // Method to load all images from selected Category ID.
        public IEnumerable<MemberGallery.Model.Image> ImageListView_GetData([RouteData] short CategoryID)
        {
            var galleryDesc = Service.GetImagesByCategoryID(CategoryID);

            return galleryDesc;
        }
        // Method to Upload image reference on server and save on disk.
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            // Have Onservervalidate="CustomValidator_ServerValidate" on my checkboxlist so need to have ISValid to prevent from saving file.
            if (IsValid) { 
                if (ModelState.IsValid)
                {         
                    try
                    {
                        // Catching stream and extension from filename. from Select button.
                        var selectedPic = Select.FileContent;
                        var extension = Path.GetExtension(Select.FileName);
                     
                        // Creating Image Reference object and populating it..
                        var image = ImageProp;   
                        // Name selected by user from Textbox.
                        image.ImgName = PictureName.Text;
                        image.UpLoaded = DateTime.Now;    
                        //Generating "Uniqe" filename, but removing extension and also adding selecte.filename extension instead.
                        image.SaveName = String.Format("{0}{1}",Path.GetFileNameWithoutExtension(Path.GetRandomFileName()),extension);

                        // Saving the new picture with its properties, from stored procedure i return the newly created Image primary key.
                        Service.SaveImage(image);

                        // Saving Image Stream and image saveName onto disk.
                        image.SaveImage(selectedPic, image.SaveName);

                        // Saving what categories Image belong to, 1 SQL command for eatch Category. Parsing them as into and
                        foreach (var item in CheckBoxLisT.Items.Cast<ListItem>().Where(item => item.Selected))
                        {
                            var cat = new ImageDesc();
                            cat.CategoryID = int.Parse(item.Value);
                            cat.ImageID = image.ImageID;
                            Service.SaveImageDesc(cat);
                        }

                        Page.SetTempData("Confirmation", String.Format("Bilden {0} har sparats", image.ImgName));
                        Response.RedirectToRoute("ImageList");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(String.Empty, "Fel inträffade när Bild  skulle Sparas.");
                    }
                }
            }
        }

       
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }

        // The id parameter name should match the DataKeyNames value set on the control.
        //public void ImageListView_DeleteItem(int imageID, [RouteData] short CategoryID)
        //{
        //    // Anropar Lagrad procedur och ta bort från bilden från kategorin. 
        //    //Retunerar en out parameter som är en count på hur många katerier som är kvar. om 0 så går jag in i IF satsen och tar även bort fil från disk.
        //    try
        //    {
        //        var remainingCategories = Service.DeleteImage(imageID, CategoryID);
        //        if (remainingCategories == 0)
        //        {
        //            var image = Service.GetImageByImageID(imageID);    
        //            var savename = String.Format("{0}{1}", image.SaveName, image.Extension);

        //            ImageProp.DeleteImage(savename);       
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Raderas.");
        //    }

        //}

        // The id parameter name should match the DataKeyNames value set on the control
        //public void ImageListView_UpdateItem(int ImageID)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var image = Service.GetImageByImageID(ImageID);
        //        if (image == null)
        //        {
        //            // The item wasn't found
        //            ModelState.AddModelError("", String.Format("Image with id {0} was not found", ImageID));
        //            return;
        //        }

        //        TryUpdateModel(image);
        //        if (ModelState.IsValid)
        //        {
        //            Service.SaveImage(image);
        //        }
        //        Page.SetTempData("Confirmation", String.Format(" Efter Redigering är uppgifterna | Bildnamn: {0} | | År: {1} |sparade.", ImageProp.ImgName, ImageProp.Year));
        //        Response.RedirectToRoute("ImageList");
        //        Context.ApplicationInstance.CompleteRequest();
        //    }
        //}

        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var validate = CheckBoxLisT.SelectedItem;

            if (validate != null)
            {
                args.IsValid = true;
            }
            else 
            {
                args.IsValid = false;
            }
        }
    }
}