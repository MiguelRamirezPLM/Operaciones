using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class ReplacesImageNames
    {
        public static string replaces(string img)
        {
            img = img.Replace("Á","A");
            img = img.Replace("É", "E");
            img = img.Replace("Í", "I");
            img = img.Replace("Ó", "O");
            img = img.Replace("Ú", "U");
            img = img.Replace("á", "a");
            img = img.Replace("é", "e");
            img = img.Replace("í", "i");
            img = img.Replace("ó", "o");
            img = img.Replace("ú", "u");
            img = img.Replace("ü", "u");
            img = img.Replace("Ü", "U");
            img = img.Replace(" ", "_");
            img = img.Replace("-", "_");
            img = img.Replace("  ", "_");
            img = img.Replace(",", "_");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");
            //img = img.Replace("", "");

            return img;
        }
    }
}