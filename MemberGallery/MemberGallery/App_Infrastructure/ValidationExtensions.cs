using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MemberGallery
{
    public static class ValidationExtensions
    {
        // TODO: Implementera validering för mina object i service klass sen.
        public static bool Validate<ImageDesc>(this ImageDesc instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}