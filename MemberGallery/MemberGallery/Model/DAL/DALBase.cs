using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace MemberGallery.Model.DAL
{
    public abstract class DALBase
    {
        private static string _connectionString;
    

        protected SqlConnection CreateConnection() { return new SqlConnection(_connectionString); }
       

        static DALBase()
        {
            // Setting my Connectionstring, and since its a static field it will not be able to be changed..
            _connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
    }
}