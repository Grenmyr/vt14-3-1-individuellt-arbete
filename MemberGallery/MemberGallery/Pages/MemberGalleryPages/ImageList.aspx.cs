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
            if (URL != null)
            {
                CurrentImage.ImageUrl = "~/Content/Pictures/" + URL;
            }
           
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
            /// image obj
            var image = ImageProp;
            // upload button.
            var selectedPic = Select.FileContent;
            var selectedFilename = Select.FileName;
            var extension = Path.GetExtension(selectedFilename);
            FileName = PictureName.Text;
            image.Extension = extension;
            image.ImgName = FileName;

            // Sparar den nya bilden med alla dess egenskaper, returnerar ID som out från lagrade procedur.
            Service.SaveFileName(image);

            // Sparar bilden på disk under ImageID, i SaveImage hackar jag även in extension till filnamn på disk.
            image.SaveImage(selectedPic, image.ImageID);

            
            // sparar vilka kategetorier användare sagt att bilden ska tillhöra. Ett Anrop för varje kategori.
            foreach (var item in CheckBoxList.Items.Cast<ListItem>().Where(item => item.Selected))
            {
                var cat = new ImageDesc();
                cat.CategoryID = int.Parse(item.Value);
                cat.ImageID = image.ImageID;
                Service.SaveImageDesc(cat);
            }
            Response.RedirectToRoute("ImageList");
            
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (URL != null)
                {
                    try
                    {
                        // Method to delete image from database.
                        //Service.DeleteImage();
                        // Method to delete file physically.
                        //ImageProp.DeleteImage(URL);
                        //Message = String.Format("Du har tagit bort{0}", FileName);
                        //Response.Redirect("?name=" + FileName);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                }
            }
        }
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void ImageListView_DeleteItem(int imageID, [RouteData] short CategoryID)
        {
            // Anropar Lagrad procedur och ta bort från bilden från kategorin. 
            //Retunerar en out parameter som är en count på hur många katerier som är kvar. om 0 så tas filen bort från disk.
            try
            {
                var remainingCategories = Service.DeleteImage(imageID, CategoryID);
                if (remainingCategories == 0)
                {
                    ImageProp.DeleteImage(imageID);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade när Kategori skulle Raderas.");
            }
            
        }


    }
}