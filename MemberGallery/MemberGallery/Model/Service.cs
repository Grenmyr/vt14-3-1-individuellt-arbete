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
        private CategoryDAL _categoryDAL;
        private ImageDescDAL _imageDescDAL;
        private ImageDAL _imageDAL;

        // Properties with lazy initialization to initialize classes.
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

        // Methods to make SQL Calls toward Image table.

        // Method to generate all Images from CategoryID
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            return ImageDAL.GetImagesByCategoryID(categoryID);
        }
      
        public void SaveImageDesc(ImageDesc imageDesc)
        {
            ImageDescDAL.SaveImageDesc(imageDesc);
        }
        // Metod check if valid Image and also redirect if Update or new Image.
        public void SaveImage(Image image)
        {
            ICollection<ValidationResult> validationresults;
            if (!image.Validate(out validationresults))
            {
                throw new ApplicationException();
            }

            if (image.ImageID == 0)
            {
                ImageDAL.SaveFileName(image);
            }
            else
            {
               ImageDAL.UpdateImage(image);
            }
        }
        // Metod to delete image that belongs to certain category.
        public int DeleteImage(int imageID, short categoryID)
        {
            return ImageDAL.DeleteImage(imageID, categoryID);
        }

        // Method to get image by ID.
        public Image GetImageByImageID(int imageID)
        {
            return ImageDAL.GetImageByImageID(imageID);
        }

       
        // Methods to make SQL Calls toward Category table.

        // Method to validate Categorys, and also redirect if new Category or Updating.
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
                CategoryDAL.UpdateCategory(category);
            }
        }
        // Method to generate all categories from my Category Table
        public IEnumerable<Category> GetCategories()
        {
            return CategoryDAL.GetCategories();
        }
        // Method to Get my Category by CategoryID.
        public Category GetCategoryByCategoryID(int CategoryID)
        {
            return CategoryDAL.GetCategoryByCategoryID(CategoryID);
        }
        // Method to delete my Category by CategoryID.
        public int DeleteCategory(int categoryID)
        {
            return CategoryDAL.DeleteCategory(categoryID);
        }
    }
}