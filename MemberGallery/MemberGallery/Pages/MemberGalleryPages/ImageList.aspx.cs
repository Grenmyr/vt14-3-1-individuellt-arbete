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
        private Service _service;
        // Property to return a Servince reference, if null create new one.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        public string FileName
        {
            get
            {
                var message = Session["text"] as string;

                return message;
            }

            set { Session["text"] = value; }
        }
        public string URL
        {
            get { return Request.QueryString["name"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private MemberGallery.Model.Image _image;
        // Property to return a Image reference, if null create new one.
        private MemberGallery.Model.Image ImageProp
        {
            get { return _image ?? (_image = new MemberGallery.Model.Image()); }

        }

        public IEnumerable<MemberGallery.Model.Image> ImageListView_GetData([RouteData] short CategoryID)
        {
            // TODO: Om denna sidan laddas först krachar aplikationen efter detta ska jag debugga och kolla att IMGKategorier sparas.

            var galleryDesc = Service.GetImagesByCategoryID(CategoryID);

            return galleryDesc;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid) { 
                if (ModelState.IsValid)
                {         
                    try
                    {
                        var image = ImageProp;
                        // upload button.
                        var selectedPic = Select.FileContent;
                        var selectedFilename = Select.FileName;
                        var extension = Path.GetExtension(selectedFilename);
                        FileName = PictureName.Text;
                        image.Extension = extension;
                        image.ImgName = FileName;
                        image.UpLoaded = DateTime.Now;
                        var saveName = Path.GetRandomFileName();

                        image.SaveName = Path.GetFileNameWithoutExtension(saveName);



                        // Sparar den nya bilden med alla dess egenskaper, returnerar ID som out från lagrade procedur.
                        Service.SaveImage(image);

                        // Sparar bilden på disk under ImageID, i SaveImage hackar jag även in extension till filnamn på disk.
                        //image.SaveImage(selectedPic, image.ImageID);
                        image.SaveImage(selectedPic, image.SaveName);


                        // sparar vilka kategetorier användare sagt att bilden ska tillhöra. Ett Anrop för varje kategori.
                        foreach (var item in CheckBoxLisT.Items.Cast<ListItem>().Where(item => item.Selected))
                        {
                            var cat = new ImageDesc();
                            cat.CategoryID = int.Parse(item.Value);
                            cat.ImageID = image.ImageID;
                            Service.SaveImageDesc(cat);
                        }
                        String.Format(" Efter Redigering är uppgifterna | Bildnamn: {0} | | År: {1} |sparade.", ImageProp.ImgName, ImageProp.Year);

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

        // The id parameter name should match the DataKeyNames value set on the control
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
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