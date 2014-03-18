using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MemberGallery.Model.DAL
{
    public class CategoryDAL : DALBase
    {   
        /// <summary>
        /// Get all Categories from Category table....
        /// </summary>
        /// <returns>Ienumerable Category List</returns>
        public IEnumerable<Category> GetCategories()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var categories = new List<Category>(100);
                    var cmd = new SqlCommand("AppSchema.GetCategories", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();


                    using (var reader = cmd.ExecuteReader())
                    {
                        var categoryIDIndex = reader.GetOrdinal("CategoryID");
                        var categoryIndex = reader.GetOrdinal("Category");

                        while (reader.Read())
                        {

                            categories.Add(new Category
                            {
                                CategoryID = reader.GetInt16(categoryIDIndex),
                                CategoryProp = reader.GetString(categoryIndex)
                            });
                        }
                    }
                    categories.TrimExcess();
                    return categories.OrderBy(c => c.CategoryProp);
                }
                catch
                {
                    throw new ApplicationException("Ett fel har skett i DAL när kategorier skulle hämtas.");
                }
            }
        }

        /// <summary>
        /// Changing CategoryName field by sending in Value and ID to Category Table.
        /// </summary>
        /// <param name="category"></param>
        public void UpdateCategory(Category category)
        {
            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.UpdateCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = category.CategoryID;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 20).Value = category.CategoryProp;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch
            {
                throw new ApplicationException("Ett fel har skett i DAL när kategori skulle uppdateras.");
            }
        }

        /// <summary>
        /// Saving new Category and returning new categoryID as out parameter. At this stage i don't use the out parameter.
        /// </summary>
        /// <param name="category"></param>
        public void SaveCategory(Category category)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = category.CategoryProp;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // Setting the ID of my newly created contact to my contacID parameter.
                    category.CategoryID = (int)cmd.Parameters["@CategoryID"].Value;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har skett i DAL när kategori skulle sparas.");
                }
            }
        }

        /// <summary>
        /// Get a category by ID.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category GetCategoryByCategoryID(int categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("AppSchema.GetCategoryByCategoryID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var categoryIDIndex = reader.GetOrdinal("CategoryID");
                            var categoryIndex = reader.GetOrdinal("Category");

                            return new Category
                            {
                                CategoryID = reader.GetInt16(categoryIDIndex),
                                CategoryProp = reader.GetString(categoryIndex),
                            };
                        }
                        return null;
                    }
                }
                catch
                {
                    throw new ApplicationException("Ett fel har skett i DAL när kategori skulle laddas.");
                }
            }
        }

        /// <summary>
        /// Deleting Categories, it returns a Count parameter on how many images that is in the category. If more then 0 i prenset msg in code behind.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns>Int Count parameter to check if I can delete Category</returns>
        public int DeleteCategory(int categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.DeleteCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                    cmd.Parameters.Add("@Count", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return (int)cmd.Parameters["@Count"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException("Ett fel har skett i DAL när borttagning av kategori skulle ske.");
                }
            }
        }
    }
}