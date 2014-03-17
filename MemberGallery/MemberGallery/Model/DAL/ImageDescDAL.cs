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
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.SaveImageDesc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ImageID", SqlDbType.Int, 4).Value = imageDesc.ImageID;
                    cmd.Parameters.Add("@CategoryID", SqlDbType.Int, 4).Value = imageDesc.CategoryID;
                    cmd.Parameters.Add("@Edited", SqlDbType.DateTime2).Value = imageDesc.Edited;  
                    cmd.Parameters.Add("@ImgDescID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // Returning last created Imadesc as out parameter, Currently not used in aplication.
                    imageDesc.ImgDescID = (int)cmd.Parameters["@ImgDescID"].Value;
                }
                catch
                {
                    throw new ApplicationException("Fel inträffade i DAL vid Sparande av Bildkategori");
                }

            }
        }
    }
}