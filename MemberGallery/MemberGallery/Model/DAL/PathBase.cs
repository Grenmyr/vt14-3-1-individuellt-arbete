using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MemberGallery.Model.DAL
{
    public abstract class PathBase
    {
         private static string _PhysicalUploadedImagesPath;
         static PathBase()
        {
            _PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Pictures");
        }
       
    }
}