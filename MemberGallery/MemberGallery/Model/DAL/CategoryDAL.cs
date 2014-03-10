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
        // TODO: Implement CategoryDAL
        public IEnumerable<Category> GetCategories()
        {  
            using (var conn = CreateConnection())
            {
                //try
                //{
     
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
                //}
                //catch
                //{
                //    throw new ApplicationException("Ett fel har skett i DAL för kategorier");
                //}
            }
        }


        public void UpdateContact(Category category)
        {
            //try
            //{
                using (SqlConnection conn = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.UpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = category.CategoryID;
                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 20).Value = category.CategoryProp;
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            //}
            //catch
            //{
            //    throw new ApplicationException("Ett fel har skett i DAL");
            //}
        }

        public void InsertContact(Category category)
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = category.CategoryProp;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // Setting the ID of my newly created contact to my contacID parameter.
                    category.CategoryID = (int)cmd.Parameters["@CategoryID"].Value;
                //}
                //catch
                //{
                //    throw new ApplicationException("Ett fel har skett i DAL");
                //}
            }
        }

        public Category GetCategoryByCategoryID(int categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{   // Se metod GetContacs för kommentarer.
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
                //}
                //catch
                //{
                //    throw new ApplicationException("Ett fel har skett i DAL");
                //}
            }
        }
    }
}