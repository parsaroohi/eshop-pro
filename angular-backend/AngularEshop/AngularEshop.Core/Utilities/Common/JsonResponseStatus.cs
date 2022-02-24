using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.Core.Utilities.Common
{
    public static class JsonResponseStatus
    {
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" });
        }

        public static JsonResult Success(object returnData)
        {
            return new JsonResult(new { status = "Success", data = returnData });
        }

        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound" });
        }

        public static JsonResult NotFound(object returnData)
        {
            return new JsonResult(new { status = "NotFound", data = returnData });
        }

        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }

        public static JsonResult Error(object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }

        public static JsonResult UnAuthorized()
        {
            return new JsonResult(new { status = "UnAuthorized" });
        }

        public static JsonResult UnAuthorized(object returnData)
        {
            return new JsonResult(new { status = "UnAuthorized", data = returnData });
        }

        public static JsonResult NoAccess()
        {
            return new JsonResult(new { status = "NoAccess" });
        }

        public static JsonResult NoAccess(object returnData)
        {
            return new JsonResult(new { status = "NoAccess", data = returnData });
        }

    }
}
