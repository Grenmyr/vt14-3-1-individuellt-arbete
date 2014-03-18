using MemberGallery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MemberGallery
{
    /// <summary>
    /// PageExtension used in masterpage to handle Session messages.
    /// </summary>
    public static class PageExtension
    {
        public static object GetTempData(this Page page, string key)
        {
            var value = page.Session[key];
            page.Session.Remove(key);
            return value;
        }

        public static void SetTempData(this Page page, string key, object value)
        {
            page.Session[key] = value;
        }
    }
}