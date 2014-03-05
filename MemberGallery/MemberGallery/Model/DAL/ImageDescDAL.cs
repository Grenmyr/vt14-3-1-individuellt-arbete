using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MemberGallery.Model.DAL
{
    public class ImageDescDAL : DALBase
    {
        // TODO: Implement ImageDescriptionDAL
        public ImageDesc GetImageDescByID(short categoryID) 
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{   // Se metod GetContacs för kommentarer.
                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var categoryIDIndex = reader.GetOrdinal("CategoryID");
                        var imageIDIndex = reader.GetOrdinal("ImgName");

                        if (reader.Read())
                        {
                            return new ImageDesc
                            {
                                 CategoryID = reader.GetInt16(categoryIDIndex),
                                 ImageID = reader.GetInt16(imageIDIndex),                            
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