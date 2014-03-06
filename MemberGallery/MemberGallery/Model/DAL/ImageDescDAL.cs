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
        public List<ImageDesc> GetImageDescByID(short categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{   // Se metod GetContacs för kommentarer.
                var imgDesc = new List<ImageDesc>(100);

                var cmd = new SqlCommand("AppSchema.GetImgByCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {

                    var categoryIDIndex = reader.GetOrdinal("CategoryID");
                    var imageIDIndex = reader.GetOrdinal("ImageID");
                    var yearIndex = reader.GetOrdinal("Year");
                    var upLoadedIndex = reader.GetOrdinal("UpLoaded");
                    var imgNameIndex = reader.GetOrdinal("ImgName");

                    while (reader.Read())
                    {
                        imgDesc.Add(new ImageDesc

                        {
                            CategoryID = reader.GetInt16(categoryIDIndex),
                            ImageID = reader.GetInt16(imageIDIndex),
                            Year = reader.GetDateTime(yearIndex),
                   
                            
                        });
                    }
                    imgDesc.TrimExcess();
                    return imgDesc;
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