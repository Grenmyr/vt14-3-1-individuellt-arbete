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

        public CategoryDAL CategoryDAL
        {
            get { return _categoryDAL ?? (_categoryDAL = new CategoryDAL()); }
        }

        public IEnumerable<Category> GetContacts()
        {
            return CategoryDAL.GetCategories();
        }
    }
}