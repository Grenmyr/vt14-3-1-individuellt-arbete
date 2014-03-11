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
                    var extensionIndex = reader.GetOrdinal("Extension");

                    while (reader.Read())
                    {
                        imglist.Add(new Image

                        {
                            ImageID = reader.GetInt16(imageIDIndex),
                            UpLoaded = reader.GetDateTime(upLoadedIndex),
                            ImgName = reader.GetString(imgNameIndex),
                            Extension = reader.GetString(extensionIndex)
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

        public void SaveFileName(Image image)
        {
            using (SqlConnection conn = CreateConnection())
            {

                SqlCommand cmd = new SqlCommand("AppSchema.SaveFileName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = image.ImgName;
                cmd.Parameters.Add("@Extension", SqlDbType.VarChar, 5).Value = image.Extension;
                cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
               

                conn.Open();
                cmd.ExecuteNonQuery();
                 image.ImageID = (int)cmd.Parameters["@ImageID"].Value;

            }
        }

        public void DeleteImage(int imageID, short categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                   
                    SqlCommand cmd = new SqlCommand("AppSchema.DeleteImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = imageID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                 
                }
                catch
                {
                    throw new ApplicationException("Ett fel har skett i DAL");
                }
            }
        }
    }
}