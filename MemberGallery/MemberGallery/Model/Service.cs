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

        public List<ImageDesc> GetImageDescByID(short categoryID)
        {
             return ImageDescDAL.GetImageDescByID(categoryID);
        }

        public static void GenerateImages(List<ImageDesc> galleryDesc)
        {
            ICollection<ValidationResult> validationresults;
            if (!galleryDesc.Validate(out validationresults))
            {
                throw new ApplicationException();
            }

        }
    }
}