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
                try
                { 
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
                }
                catch
                {
                    throw new ApplicationException("Fel inträffade i DAL vid hämtning av bildlista.");
                }
            }
        }

        public void SaveFileName(Image image)
        {
            using (SqlConnection conn = CreateConnection())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveFileName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = image.ImgName;
                    cmd.Parameters.Add("@Extension", SqlDbType.VarChar, 5).Value = image.Extension;
                    cmd.Parameters.Add("@UpLoaded", SqlDbType.DateTime2).Value = image.UpLoaded;
                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
              


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    image.ImageID = (int)cmd.Parameters["@ImageID"].Value;
                }
                catch 
                {
                    throw new ApplicationException("Fel inträffade i DAL vid skapande av bild..");
                }

            }
        }

        public int DeleteImage(int imageID, short categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                   
                    SqlCommand cmd = new SqlCommand("AppSchema.DeleteImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = imageID;
                    cmd.Parameters.Add("@Count", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return (int)cmd.Parameters["@Count"].Value;
                 
                }
                catch
                {
                    throw new ApplicationException("Fel inträffade i DAL vid borttagning av bild.");
                }
            }
        }

        public Image GetImageByImageID(int imageID)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {   
                var cmd = new SqlCommand("AppSchema.GetImageByImageID", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ImageID", imageID);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var imageIDIndex = reader.GetOrdinal("ImageID");
                        var imgNameIndex = reader.GetOrdinal("ImgName");
                        var yearIndex = reader.GetOrdinal("Year");
                        var extensionIndex = reader.GetOrdinal("Extension");

                        return new Image
                        {
                            ImageID = reader.GetInt16(imageIDIndex),
                            ImgName = reader.GetString(imgNameIndex),
                            Year = reader.GetDateTime(yearIndex),
                            Extension = reader.GetString(extensionIndex)
                        };
                    }
                    return null;
                }
                }
                catch
                {
                    throw new ApplicationException("Fel inträffade i DAL vid hämtning av bild.");
                }
            }
        }

        public void UpdateImage(Image image)
        {
            try
            {
            using (SqlConnection conn = CreateConnection())
            {
                SqlCommand cmd = new SqlCommand("AppSchema.UpdateImage", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ImageID", SqlDbType.SmallInt, 4).Value = image.ImageID;
                cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = image.ImgName;
                cmd.Parameters.Add("@UpLoaded", SqlDbType.DateTime2).Value = DateTime.Now;

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            }
            catch
            {
                throw new ApplicationException("Ett fel har skett i DAL när bild skulle uppdateras.");
            }
        }
    }
}