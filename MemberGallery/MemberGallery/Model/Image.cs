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
        // TODO: Not sure about YEAR and DateTime format
        public int ImageID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "Fält Bildnamn får ej lämnas tomt.")]
        [StringLength(20, ErrorMessage = "Bildnamn kan max vara 20 tecken långt.")]
        public string ImgName { get; set; }

        [Required(ErrorMessage = "Datum Förnamn får ej lämnas tomt.")]
        public DateTime UpLoaded { get; set; }

        public DateTime Year { get; set; }

        //[RegularExpression(ErrorMessage="fdsafdsa")]
        public string Extension { get; set; }
        public string SaveName { get; set; }

        //private static readonly Regex ApprovedExtensions;
        //private static readonly Regex SantizePath;
        private static string PhysicalUploadedImagesPath;
        private static string PhysicalUploadedThumbNailPath;

        // Constructor
        static Image()
        {
            ////Setting Regex pattern to field.
            //string pattern = @"^.*\.(gif|jpg|png)$";
            //ApprovedExtensions = new Regex(pattern, RegexOptions.IgnoreCase);

            ////Setting physical direction to my files
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Pictures");
            PhysicalUploadedThumbNailPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"Content\Thumbnails");

            // //"GetInvalidFileNameChars()" is a built in collection of illegal chars, which i after saving into variable "invalidchars".
            // //Using my expression "invalidchars "to set my field "sanitizePath" IF the regex escape "invalidchars"
            //var invalidChars = new string(Path.GetInvalidFileNameChars());
            //SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        }

        //public IEnumerable<string> GetImageNames()
        //{
        //    // Getting files from the path saving them into an array.
        //    var images = new DirectoryInfo(PhysicalUploadedImagesPath).GetFiles();

        //    //foreach (var image in images)
        //    //{
        //    //    using (var image2 = System.Drawing.Image.FromFile(image.FullName))
        //    //    using (var thumbnail = image2.GetThumbnailImage(60, 45, null, System.IntPtr.Zero))
        //    //    {
        //    //        thumbnail.Save(Path.Combine(PhysicalUploadedThumbNailPath, image.Name));
        //    //    }
        //    //}

        //    List<string> imagesAdressList = new List<string>(images.Length);
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        imagesAdressList.Add(images[i].ToString());
        //    }

        //    // Using "Select" loop my list to match against Regexobject approved extensions.....
        //    imagesAdressList.Select(imageName => ApprovedExtensions.IsMatch(imageName));
        //    imagesAdressList.TrimExcess();
        //    imagesAdressList.Sort();

        //    return imagesAdressList.AsEnumerable();
        //}

        // Metod that returns true if file and filepatch match.
        public bool ImageExist(string name)
        {
            return File.Exists(string.Format("{0}/{1}", PhysicalUploadedImagesPath, name));
        }

        // Return true if valid image

        public void SaveImage(Stream stream, string saveName)
        {
            try
            {
                // Kontrollerar om filtyp är äkta bild. Sparar sen bild/thumbnail med "ImageID.JPG/PNG" på disk.
                using (var image = System.Drawing.Image.FromStream(stream))
                {
                    //var fileName = "";
                    if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                    {
                        //fileName = String.Format("{0}.JPG", saveName);
                        image.Save(Path.Combine(PhysicalUploadedImagesPath, saveName));

                        using (var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero))
                        {
                            thumbnail.Save(Path.Combine(PhysicalUploadedThumbNailPath, saveName));
                        }
                    }
                    //else if (   image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                    //{
                    //    fileName = String.Format("{0}.PNG", saveName);
                    //}
                    else
                    {
                        throw new ArgumentException("Filen är ej en bild av typen JPG eller PNG.");
                    }
                    
                }
               
            }
            catch (Exception)
            {
                throw new ArgumentException("Ett oväntat undantag inträffade, när bild skulle sparas.");
            }
        }
        public void DeleteImage(string fileName)
        {
            // Om bilden finns på disk, så tas den bort.
            if (ImageExist(fileName))
            {
                try
                {
                    File.Delete(Path.Combine(PhysicalUploadedImagesPath, fileName));
                    File.Delete(Path.Combine(PhysicalUploadedThumbNailPath, fileName));
                }
                catch (Exception)
                {
                    throw new ArgumentException("Ett oväntat undantag inträffade när bild skulle tas bort..");
                }
            }
        }
    }
}