using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace AngularEshop.Core.Utilities.Extensions.FileExtensions
{
    public static class ImageUploaderExtension
    {
        public static void AddImageToServer(this Image image, string fileName,
            string originalPath, string deleteFileName = null)
        {
            if (image != null)
            {
                if (!Directory.Exists(originalPath))
                    Directory.CreateDirectory(originalPath);
                if (!string.IsNullOrEmpty(deleteFileName))
                    File.Delete(originalPath + deleteFileName);
                string imageName = originalPath + fileName;
                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    if (!Directory.Exists(imageName))
                        image.Save(stream, ImageFormat.Jpeg);
                }
            }
        }

        public static byte[] DecodeUrlBase64(string s)
        {
            return Convert.FromBase64String(s.Substring(s.LastIndexOf(',') + 1));
        }

        public static Image Base64ToImage(string base64string)
        {
            var res = DecodeUrlBase64(base64string);
            MemoryStream ms = new MemoryStream(res, 0, res.Length);
            ms.Write(res, 0, res.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
