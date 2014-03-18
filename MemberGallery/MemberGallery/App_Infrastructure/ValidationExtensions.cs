using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MemberGallery
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Extension method used to validate against my properties using data annotaions, works on all classes with data annotations on the properties.
        /// </summary>
        /// <typeparam name="Object"></typeparam>
        /// <param name="instance"></param>
        /// <param name="validationResults"></param>
        /// <returns>True/False</returns>
        public static bool Validate<Object>(this Object instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}