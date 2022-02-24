using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AngularEshop.Core.Utilities.Common
{
    public static class PathTools
    {
        #region domain
        public static string Domain = "https://localhost:44381";
        #endregion

        #region product
        public static string ProductImagePath = "/image/products/";
        public static string ProductImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "/image/products/");
        #endregion
    }
}
