using MemberGallery.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MemberGallery.Model
{
    public class Service
    {
        private CategoryDAL _categoryDAL;
        private ImageDescDAL _imageDescDAL;
        private ImageDAL _imageDAL;

        /// <summary>
        /// Properties with lazy initialization to initialize classes.
        /// </summary>
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

        /// <summary>
        /// Method to generate all Images from CategoryID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Image List</returns>
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            return ImageDAL.GetImagesByCategoryID(categoryID);
        }


        /// <summary>
        /// Metod check if valid Image and also redirect if Update or new Image.
        /// </summary>
        /// <param name="image"></param>
        public void SaveImage(Image image)
        {
            ICollection<ValidationResult> validationresults;
            if (!image.Validate(out validationresults))
            {
                throw new ApplicationException();
            }
            ImageDAL.SaveImage(image);


        }
     
        /// <summary>
        /// Metod to delete image that belongs to certain category.
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="categoryID"></param>
        /// <returns>Interger</returns>
        public int DeleteImage(int imageID, short categoryID)
        {
            return ImageDAL.DeleteImage(imageID, categoryID);
        }

        // Methods to make SQL Calls toward Category table.

       
        /// <summary>
        ///  Method to validate Categorys, and also redirect if new Category or Updating.
        /// </summary>
        /// <param name="category"></param>
        public void SaveCategory(Category category)
        {
            ICollection<ValidationResult> validationresults;
            if (!category.Validate(out validationresults))
            {
                throw new ApplicationException();
            }

            if (category.CategoryID == 0)
            {
                CategoryDAL.SaveCategory(category);
            }
            else
            {
                CategoryDAL.UpdateCategory(category);
            }
        }
        /// <summary>
        /// Method to generate all categories from my Category Table
        /// </summary>
        /// <returns>Category IEnumerable</returns>
        public IEnumerable<Category> GetCategories()
        {
            return CategoryDAL.GetCategories();
        }

        /// <summary>
        /// Method to Get my Category by CategoryID.
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns>Category</returns>
        public Category GetCategoryByCategoryID(int CategoryID)
        {
            return CategoryDAL.GetCategoryByCategoryID(CategoryID);
        }

        /// <summary>
        /// Method to delete my Category by CategoryID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Interger Count</returns>
        public int DeleteCategory(int categoryID)
        {
            return CategoryDAL.DeleteCategory(categoryID);
        }

        // Methods toward ImageDesc Table.


        /// <summary>
        /// First validate then Update/Save new imagedesc object..
        /// </summary>
        /// <param name="imagedesc"></param>
        public void SaveImageDesc(ImageDesc imagedesc)
        {
            ICollection<ValidationResult> validationresults;
            if (!imagedesc.Validate(out validationresults))
            {
                throw new ApplicationException();
            }

            if (imagedesc.ImgDescID == 0)
            {
                ImageDescDAL.SaveImageDesc(imagedesc);
            }
            else
            {
                ImageDescDAL.UpdateImageDesc(imagedesc);
            }
        }
        /// <summary>
        /// Method to get ImageDescbyImageDescID
        /// </summary>
        /// <param name="ImageDescID"></param>
        /// <returns>ImageDesc</returns>
        public ImageDesc GetImageDescByImageDescID(int ImageDescID)
        {
            return ImageDescDAL.GetImageDescByImageDescID(ImageDescID);
        }

        //Method toward my imagedescExtension

        /// <summary>
        /// Method i need when to get Imagedescextension when i don't have ImageDescID.
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="ImageID"></param>
        /// <returns>ImageDescExtension that Contain SaveName from Image Table.</returns>
        public ImageDescExtension GetImageDescExtension(int CategoryID, int ImageID)
        {
            return ImageDescDAL.GetImageDescExtension(CategoryID, ImageID);
        }
    }
}