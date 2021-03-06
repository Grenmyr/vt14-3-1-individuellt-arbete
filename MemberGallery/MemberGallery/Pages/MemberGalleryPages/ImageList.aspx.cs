﻿using MemberGallery.Model;
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

        /// <summary>
        /// Property to return a Image reference, if null create new one.
        /// </summary>
        private MemberGallery.Model.Image ImageProp
        {
            get { return _image ?? (_image = new MemberGallery.Model.Image()); }
        }
        private Service _service;

        /// <summary>
        /// Property to return a Service reference, if null create new one.
        /// </summary>
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            PictureName.Focus();
        }

        /// <summary>
        /// Method to load all images from selected Category ID..
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns>Ienumerable Image List</returns>
        public IEnumerable<MemberGallery.Model.Image> ImageListView_GetData([RouteData] short CategoryID)
        {
            //var test = Service.GetFiles();
            var galleryDesc = Service.GetImagesByCategoryID(CategoryID);

            return galleryDesc;
        }

        /// <summary>
        /// Method to Upload image reference on server and save on disk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                      
                        image.UpLoaded = DateTime.Now;
                        
                        //Generating "Uniqe" filename, but removing extension and also adding selecte.filename extension instead.
                        image.SaveName = String.Format("{0}{1}",Path.GetFileNameWithoutExtension(Path.GetRandomFileName()),extension);

                        // Saving Image Stream and image saveName onto disk.
                        image.SaveImage(selectedPic, image.SaveName);
                        // Saving the new picture with its properties, from stored procedure i return the newly created Image primary key.
                        Service.SaveImage(image);

                        // Creating a foreach loop that convert all checkbox items into listitem then for eatch selected checkbox make a SQL call binding the image to that category. 
                        var imagedesc = new ImageDesc();
                        foreach (var item in CheckBoxLisT.Items.Cast<ListItem>().Where(item => item.Selected))
                        {
                            // TODO: här e jag.
                            imagedesc = new ImageDesc();
                            imagedesc.CategoryID = int.Parse(item.Value);
                            imagedesc.ImageID = image.ImageID;
                            imagedesc.Edited = DateTime.Now;
                            imagedesc.ImgName = PictureName.Text;
                            Service.SaveImageDesc(imagedesc);
                        }
                        // Message saved in Extension method and redirect to last saved Category.
                        Page.SetTempData("Confirmation", String.Format("Bilden {0} har sparats", imagedesc.ImgName));
                        Response.RedirectToRoute("ImageList", new  { CategoryID = imagedesc.CategoryID });
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(String.Empty, "Fel inträffade när bild  skulle Sparas.");
                    }
                }
            }
        }

        /// <summary>
        /// Used by Listview as Selectmethod. to generate thumbnails, it return a IEnumerable list.
        /// </summary>
        /// <returns>Ienumerable Cageory list</returns>
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();
        }

        /// <summary>
        /// Method from Custovalidator validating checkboxlist. If any checked boxes, it is set to true, else false. Used to prevent saving Images without choosing category.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
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