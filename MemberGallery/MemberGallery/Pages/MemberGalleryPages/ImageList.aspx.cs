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
    public partial class ImageList : System.Web.UI.Page
    {
        private Service _service;
        // Property to return a Servince reference, if null create new one.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<MemberGallery.Model.Image> ImageListView_GetData([RouteData] short CategoryID)
        {
            // TODO: Om denna sidan laddas först krachar aplikationen efter detta ska jag debugga och kolla att IMGKategorier sparas.

            var galleryDesc = Service.GetImagesByCategoryID(CategoryID);
            return galleryDesc;
        }

        public void UploadButton_Click(object sender, EventArgs e)
        {
            // IF tryupdatemodel e valid
            var selectedPic = Select.FileContent;
            var selectedName = Select.FileName;
            MemberGallery.Model.Image image = new MemberGallery.Model.Image();
            image.ImgName = selectedName;
            Service.SaveFileName(image);

            var xxx = Categories.ID;

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

        }
        public IEnumerable<Category> CategoryListView_GetData()
        {
            return Service.GetCategories();


        }

        
    }
}