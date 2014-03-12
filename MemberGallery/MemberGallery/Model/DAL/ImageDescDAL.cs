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

                    cmd.Parameters.Add("@ImgDescID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
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