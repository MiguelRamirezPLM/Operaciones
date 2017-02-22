using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class ReplacesImageNames
    {
        public static string replaces(string img)
        {
            img = img.Replace(" ","_");
            img = img.Replace(",", "_");
            img = img.Replace(", ", "_");
            img = img.Replace(",_", "_");
            img = img.Replace(".", "_");
            img = img.Replace("__", "_");
            img = img.Replace(". ", "_");
            img = img.Replace("Á", "A");
            img = img.Replace("á", "a");
            img = img.Replace("/", "_");
            img = img.Replace("Í", "I");
            img = img.Replace("í", "i");
            img = img.Replace("ü", "u");
            img = img.Replace("ü", "U");
            img = img.Replace("Ó", "O");
            img = img.Replace("ó", "o");
            img = img.Replace("Ú", "U");
            img = img.Replace("ú", "u");
            img = img.Replace("- ", "-");
            img = img.Replace(" - ", "-");
            img = img.Replace("É", "E");
            img = img.Replace("é", "e");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");
            //img = img.Replace("__", "_");

            return img;
        }
    }
}