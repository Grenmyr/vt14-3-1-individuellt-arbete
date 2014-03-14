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
        // Method to load all images from selected Category ID..
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

                        // Saving Image Stream and image saveName onto disk.
                        image.SaveImage(selectedPic, image.SaveName);
                        // Saving the new picture with its properties, from stored procedure i return the newly created Image primary key.
                        Service.SaveImage(image);

                        // Creating a foreach loop that for eatch selected checkbox make a SQL call binding the image to that category. 
                        // Forgott the reference but i googled this solution ;-).
                        var cat = new ImageDesc();
                        foreach (var item in CheckBoxLisT.Items.Cast<ListItem>().Where(item => item.Selected))
                        {
                            // TODO: här e jag.
                            cat.CategoryID = int.Parse(item.Value);
                            cat.ImageID = image.ImageID;
                            Service.SaveImageDesc(cat);
                        }
                        // Message saved in Extension method and redirect to last saved Category.
                        Page.SetTempData("Confirmation", String.Format("Bilden {0} har sparats", image.ImgName));
                        Response.RedirectToRoute("ImageList", new  { CategoryID = cat.CategoryID });
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(String.Empty, "Fel inträffade när bild  skulle Sparas.");
                    }
                }
            }
        }
        // Used by Listview as Selectmethod. to generate thumbnails, it return a IEnumerable list.
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }

        // Method from Custovalidator validating checkboxlist. If any checked boxes, it is set to true, else false. Used to prevent saving Images without choosing category.
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

        protected void ShowUpload_Click(object sender, EventArgs e)
        {
            UploadPlaceholder.Visible = true;
        }
    }
}