using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AgroNet.Models;

namespace AgroNet.Models
{
    public class RenderImage
    {
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}
