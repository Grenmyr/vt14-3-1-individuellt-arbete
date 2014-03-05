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

    }
}