using MemberGallery.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberGallery.Model
{
    public class Service
    {
        // TODO: My serviceclass, to acces my DAL.
        private CategoryDAL _categoryDAL;
        private ImageDescDAL _imageDescDAL;
        private ImageDAL _imageDAL;

        public ImageDAL ImageDAL
        {
            get { return _imageDAL ?? (_imageDAL = new ImageDAL()); }
        }

        public CategoryDAL CategoryDAL
        {
            get { return _categoryDAL ?? (_categoryDAL = new CategoryDAL()); }
        }

        public ImageDescDAL ImageDescDAL
        {
            get { return _imageDescDAL ?? (_imageDescDAL = new ImageDescDAL()); }
        }

        public IEnumerable<Category> GetCategories()
        {
            return CategoryDAL.GetCategories();
        }

        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            return ImageDAL.GetImagesByCategoryID(categoryID);
        }

        //public void GenerateImages(List<Image> galleryDesc)
        //{
        //    ICollection<ValidationResult> validationresults;
        //    if (!galleryDesc.Validate(out validationresults))
        //    {
        //        throw new ApplicationException();
        //    }
        //    ImageDAL.GetImageByID(galleryDesc);

        //}
    }
}