using MemberGallery.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberGallery.Model
{
    public class Service
    {
        // TODO: My serviceclass, to acces my DAL.
        private CategoryDAL _categoryDAL;
        private ImageDescDAL _imageDescDAL;

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

        public ImageDesc GetImageDescByID(short categoryID)
        {
             return ImageDescDAL.GetImageDescByID(categoryID);
        }
    }
}