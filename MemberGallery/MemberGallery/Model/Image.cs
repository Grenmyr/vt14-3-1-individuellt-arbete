using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
    }
}