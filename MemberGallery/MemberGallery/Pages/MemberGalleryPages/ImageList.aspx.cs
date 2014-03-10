﻿using MemberGallery.Model;
using System;
using System.Collections.Generic;
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
            set { }
            get { return Request.QueryString["name"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             CurrentImage.ImageUrl = "~/Content/Pictures/" + URL;
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
            // Hämtar filnamn och steam 
            var selectedPic = Select.FileContent;
            var selectedName = Select.FileName;

            // Skapar Image objekt 
            var image = ImageProp;
            // Anropar min imageklass och sparar filreferens och namn.
            var savedFilename = image.SaveImage(selectedPic, selectedName);

            // Sätter sparade namnet till mitt image objekt. och laddar därefter upp referens till databas.
            image.ImgName = savedFilename;
            Service.SaveFileName(image);
            FileName = savedFilename;

            // Saving imagedesc for the picture.
            foreach (var item in CheckBoxList.Items.Cast<ListItem>().Where(item => item.Selected))
            {
                var cat = new ImageDesc();
                cat.CategoryID = int.Parse(item.Value);
                cat.ImageID = image.ImageID;
                Service.SaveImageDesc(cat);
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (FileName != null)
                {
                    try
                    {
                        ImageProp.DeleteImage(FileName);
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

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            //ViewImage.SetURL= 
        }
    }
}