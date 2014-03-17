using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MemberGallery.Model
{
    public class ImageDesc 
    {
        public int ImageID { get; set; }
        public int ImgDescID { get; set; }
        public int CategoryID { get; set; }
        public DateTime Edited { get; set; }

        [Required(ErrorMessage = "Fält Bildnamn får ej lämnas tomt.")]
        [StringLength(20, ErrorMessage = "Bildnamn kan max vara 20 tecken långt.")]
        public string ImgName { get; set; }
    }
}