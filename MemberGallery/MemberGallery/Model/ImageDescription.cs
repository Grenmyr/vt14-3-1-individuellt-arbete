using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MemberGallery.Model
{
    public class ImageDescription
    {
        // TODO: Not sure about YEAR and DateTime format, also how to do with Index?
        public int ImageID { get; set; }
        public int ImageDescID { get; set; }
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Fält Year får ej lämnas tomt.")]
        public DateTime Year { get; set; }

    }
}