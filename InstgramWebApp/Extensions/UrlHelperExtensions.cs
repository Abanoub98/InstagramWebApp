using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
namespace InstgramWebApp.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GenerateHref(this UrlHelper urlHelper, string actionName, string controllerName , HttpRequestBase request)
        {
            return urlHelper.Action(actionName, controllerName.Replace("Controller", ""), null, request.Url.Scheme);
        }

        public static string GenerateHref(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues, HttpRequestBase request)
        {
            return urlHelper.Action(actionName, controllerName.Replace("Controller", ""), routeValues, request.Url.Scheme);
        }
    }
}