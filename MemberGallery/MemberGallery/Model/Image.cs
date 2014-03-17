using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MemberGallery.Model.DAL;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace MemberGallery.Model
{
    public class Image 
    {
        public int ImageID { get; set; }
        public int UserID { get; set; }


        public DateTime UpLoaded { get; set; }
        public DateTime Year { get; set; }

        [StringLength(12, ErrorMessage = "Savename kan max vara 12 tecken långt.")]
        [Required(ErrorMessage = "Fält SaveName får ej lämnas tomt.")]
        public string SaveName { get; set; }

        private static string PhysicalUploadedImagesPath;
        private static string PhysicalUploadedThumbNailPath;

        // Constructor
        static Image()
        {
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Pictures");
            PhysicalUploadedThumbNailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Thumbnails");
        }
        // Metod that returns true if file and filepatch match.
        public bool ImageExist(string saveName)
        {
            return File.Exists(string.Format("{0}/{1}", PhysicalUploadedImagesPath, saveName));
        }

        public void SaveImage(Stream stream, string saveName)
        {
            // If valid imageformat i save file on disk.
            using (var image = System.Drawing.Image.FromStream(stream))
            {

                if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                {
                    image.Save(Path.Combine(PhysicalUploadedImagesPath, saveName));

                    using (var thumbnail = image.GetThumbnailImage(120, 90, null, System.IntPtr.Zero))
                    {
                        thumbnail.Save(Path.Combine(PhysicalUploadedThumbNailPath, saveName));
                    }
                }
                else
                {
                    throw new ArgumentException("Filen är ej en bild av typen JPG eller PNG.");
                }
            }
        }
        public void DeleteImage(string saveName)
        {
            // If image exist on disk, i remove picture and thumbnail.
            if (ImageExist(saveName))
            {
                try
                {
                    File.Delete(Path.Combine(PhysicalUploadedImagesPath, saveName));
                    File.Delete(Path.Combine(PhysicalUploadedThumbNailPath, saveName));
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ett oväntat undantag inträffade när bild skulle tas bort..");
                }
            }
        }

    }
}