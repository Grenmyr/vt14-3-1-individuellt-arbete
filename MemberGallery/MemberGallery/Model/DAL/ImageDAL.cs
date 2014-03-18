using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MemberGallery.Model.DAL
{
    public class ImageDAL : DALBase
    {    // Using this SQLConnection to Access my Image and ImageDesc tables. I get a list of images that belong to certain category from this call.
        public List<Image> GetImagesByCategoryID(short categoryID)
        {
            using (SqlConnection conn = CreateConnection())
            {

                try
                {
                    var imglist = new List<Image>(30);

                    var cmd = new SqlCommand("AppSchema.GetImgByCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //@CategoryID is input parameter.
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = categoryID;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var imageIDIndex = reader.GetOrdinal("ImageID");
                        var upLoadedIndex = reader.GetOrdinal("UpLoaded");
                        var saveNameIndex = reader.GetOrdinal("SaveName");

                        while (reader.Read())
                        {
                            imglist.Add(new Image
                            {
                                ImageID = reader.GetInt16(imageIDIndex),
                                UpLoaded = reader.GetDateTime(upLoadedIndex),
                                SaveName = reader.GetString(saveNameIndex)
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

        // Saving Images, 3 imput variables and return ImageID as int output parameter.
        public void SaveFileName(Image image)
        {
            using (SqlConnection conn = CreateConnection())
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveFileName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UpLoaded", SqlDbType.DateTime2).Value = image.UpLoaded;
                    cmd.Parameters.Add("@SaveName", SqlDbType.VarChar, 15).Value = image.SaveName;

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

        // Used to DeleteImage, If there is still categories Image belong to, Image will not be deleted.
        // However if image belong to no Categories after deleting insterted CategoryID image will be delete.
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
        // Retriving image by inserting imageID
        //public Image GetImageByImageID(int imageID)
        //{
        //    using (SqlConnection conn = CreateConnection())
        //    {
        //        try
        //        {
        //            var cmd = new SqlCommand("AppSchema.GetImageByImageID", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ImageID", imageID);

        //            conn.Open();

        //            using (var reader = cmd.ExecuteReader())
        //            {

        //                if (reader.Read())
        //                {
        //                    var imageIDIndex = reader.GetOrdinal("ImageID");
        //                    var uploadedIndex = reader.GetOrdinal("UpLoaded");
        //                    var saveNameIndex = reader.GetOrdinal("SaveName");
                   
        //                    return new Image
        //                    {
        //                        ImageID = reader.GetInt16(imageIDIndex),
        //                        UpLoaded = reader.GetDateTime(uploadedIndex),
        //                        SaveName = reader.GetString(saveNameIndex),
        //                    };
        //                }
        //                return null;
        //            }
        //        }
        //        catch
        //        {
        //            throw new ApplicationException("Fel inträffade i DAL vid hämtning av bild.");
        //        }
        //    }
        //}

        // TODO: Make change only on the Category its set on. I need a Date on Imagedesc table to do this.
        // Updating ImgName and UpLoaded date. Uploaded is also used as "Last modyfied msg". I know i got an error here, as Image will be updated on all Categories if changed on 1.
        //public void UpdateImage(Image image)
        //{
     
        //    try
        //    {
        //        using (SqlConnection conn = CreateConnection())
        //        {
        //            SqlCommand cmd = new SqlCommand("AppSchema.UpdateImage", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@ImageID", SqlDbType.SmallInt, 4).Value = image.ImageID;
        //            cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = image.ImgName;
        //            // Best choise would be set valute to Edited property in Code behind, by getting Imaagedesc from database by send in ImageID and CategoryID
        //            // and then set value in code behind but i did't have time.
        //            cmd.Parameters.Add("@Edited", SqlDbType.DateTime2).Value = DateTime.Now;

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }

        //    }
        //    catch
        //    {
        //        throw new ApplicationException("Ett fel har skett i DAL när bild skulle uppdateras.");
        //    }
        //}
    }
}