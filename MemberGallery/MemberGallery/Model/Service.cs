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

        // Method to generate all categories from my Category Table
        public IEnumerable<Category> GetCategories()
        {
            return CategoryDAL.GetCategories();
        }

        // Method to generate all Images from CategoryID
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            return ImageDAL.GetImagesByCategoryID(categoryID);
        }
    }
}