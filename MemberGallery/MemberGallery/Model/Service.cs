using MemberGallery.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

       

        // Method to generate all Images from CategoryID
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            return ImageDAL.GetImagesByCategoryID(categoryID);
        }
        // Metod för Spara Images.
        public void SaveFileName(Image image)
        {
         ImageDAL.SaveFileName(image);
        }
        // Metod för Spara Imagedescriptions
        public void SaveImageDesc(ImageDesc imageDesc) 
        {
            ImageDescDAL.SaveImageDesc(imageDesc);
        }

        // Metoder till Category

        public void SaveCategory(Category category)
        {
            ICollection<ValidationResult> validationresults;
            if (!category.Validate(out validationresults))
            {
                throw new ApplicationException();
            }

            if (category.CategoryID == 0)
            {
                CategoryDAL.InsertContact(category);
            }
            else
            {
                CategoryDAL.UpdateContact(category);
            }
        }
        // Method to generate all categories from my Category Table
        public IEnumerable<Category> GetCategories()
        {
            return CategoryDAL.GetCategories();
        }

        public Category GetCategoryByCategoryID(int CategoryID)
        {
            return CategoryDAL.GetCategoryByCategoryID(CategoryID);
        }
    }
}