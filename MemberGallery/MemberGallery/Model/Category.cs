using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MemberGallery.Model
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Fält Kategori får ej lämnas tomt.")]
        [StringLength(20, ErrorMessage = "Kategori kan max vara 20 tecken långt.")]
        public string CategoryProp { get; set; }
    }
}