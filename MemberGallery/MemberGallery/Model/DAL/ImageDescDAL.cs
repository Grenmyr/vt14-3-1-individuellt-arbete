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
        /* Saving Image under Categories. 
         I have another SQL connection in ImageDal named "DeleteImage" That can DeleteImage and Category
         if no images left in the Category, i was unsure where to place it, but its in ImageDAL. 
         This Method however binds 1 image Toward 1 category for eatch call. I repeadely call this Stored procedure to bind image to serveral categories if its choosed.*/
        public void SaveImageDesc(ImageDesc imageDesc)
        {
            using (SqlConnection conn = CreateConnection())
            {
                //try
                //{
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveImageDesc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = imageDesc.ImageID;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = imageDesc.CategoryID;
                    cmd.Parameters.Add("@Edited", SqlDbType.DateTime2).Value = imageDesc.Edited;
                    cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = imageDesc.ImgName;  
                    cmd.Parameters.Add("@ImgDescID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // Returning last created Imadesc as out parameter, Currently not used in aplication.
                    imageDesc.ImgDescID = (int)cmd.Parameters["@ImgDescID"].Value;
                //}
                //catch
                //{
                //    throw new ApplicationException("Fel inträffade i DAL vid Sparande av Bildkategori");
                //}

            }
        }
        public void UpdateImageDesc(ImageDesc imagedesc)
        {

            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.UpdateImageDesc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ImageID", SqlDbType.SmallInt, 4).Value = imagedesc.ImageID;
                    cmd.Parameters.Add("@ImgName", SqlDbType.VarChar, 20).Value = imagedesc.ImgName;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.SmallInt, 4).Value = imagedesc.CategoryID;
                    // Best choise would be set valute to Edited property in Code behind, by getting Imaagedesc from database by send in ImageID and CategoryID
                    // and then set value in code behind but i did't have time.
                    cmd.Parameters.Add("@Edited", SqlDbType.DateTime2).Value = imagedesc.Edited;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch
            {
                throw new ApplicationException("Ett fel har skett i DAL när bild skulle uppdateras.");
            }
        }

        public  ImageDesc GetImageDescByImageDescID(int ImageDescID)
        {
            using (SqlConnection conn = CreateConnection())
            {

                //try
                //{
                var cmd = new SqlCommand("AppSchema.GetImageDescByImageDescID", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@ImageDescID", ImageDescID);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var imageIDIndex = reader.GetOrdinal("ImageID");
                        var imgNameIndex = reader.GetOrdinal("ImgName");
                        var CategoryIDIndex = reader.GetOrdinal("CategoryID");
                        var editedIndex = reader.GetOrdinal("Edited");

                        return new ImageDesc
                        {
                            ImageID = reader.GetInt16(imageIDIndex),
                            ImgName = reader.GetString(imgNameIndex),
                            CategoryID = reader.GetInt16(CategoryIDIndex),
                            Edited = reader.GetDateTime(editedIndex)
                        };
                    }
                    return null;
                }
                //}
                //catch
                //{
                //    throw new ApplicationException("Fel inträffade i DAL vid hämtning av bild.");
                //}
            }
        }

        public ImageDescExtension GetImageDesc(int CategoryID, int ImageID)
        {
            using (SqlConnection conn = CreateConnection())
            {

                //try
                //{
                var cmd = new SqlCommand("AppSchema.GetImageDesc", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                cmd.Parameters.AddWithValue("@ImageID", ImageID);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        var imgDescIDIndex = reader.GetOrdinal("ImgDescID");
                        var imgNameIndex = reader.GetOrdinal("ImgName");
                        var editedIndex = reader.GetOrdinal("Edited");
                        var savename = reader.GetOrdinal("SaveName");

                        return new ImageDescExtension
                        {
                            ImgDescID = reader.GetInt16(imgDescIDIndex),
                            ImageID = ImageID,
                            ImgName = reader.GetString(imgNameIndex),
                            CategoryID = CategoryID,
                            Edited = reader.GetDateTime(editedIndex),
                            SaveName = reader.GetString(savename)
                        };
                    }
                    return null;
                }
                //}
                //catch
                //{
                //    throw new ApplicationException("Fel inträffade i DAL vid hämtning av bild.");
                //}
            }
        }
    }
}