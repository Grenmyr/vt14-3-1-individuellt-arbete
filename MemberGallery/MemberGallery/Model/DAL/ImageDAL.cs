using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MemberGallery.Model.DAL
{
    public class ImageDAL : DALBase
    {
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{   // Se metod GetContacs för kommentarer.
                var imglist = new List<Image>(100);

                var cmd = new SqlCommand("AppSchema.GetImgByCategory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {

                 
                    var imageIDIndex = reader.GetOrdinal("ImageID");
                    var upLoadedIndex = reader.GetOrdinal("UpLoaded");
                    var imgNameIndex = reader.GetOrdinal("ImgName");

                    while (reader.Read())
                    {
                        imglist.Add(new Image

                        {
                            ImageID = reader.GetInt16(imageIDIndex),
                            UpLoaded = reader.GetDateTime(upLoadedIndex),
                            ImgName = reader.GetString(imgNameIndex)
                        });
                    }
                    imglist.TrimExcess();
                    return imglist;
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