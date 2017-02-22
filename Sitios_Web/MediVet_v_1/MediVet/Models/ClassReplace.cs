using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class ClassReplace
    {
        public static string replacestring(string _string)
        {
            _string = _string.Replace(" ","_");
            _string = _string.Replace(",", "_");
            _string = _string.Replace(".", "_");
            _string = _string.Replace("Á","A");
            _string = _string.Replace("É", "E");
            _string = _string.Replace("Í", "I");
            _string = _string.Replace("Ó", "O");
            _string = _string.Replace("Ú", "U");
            _string = _string.Replace("Ü", "U");

            _string = _string.Replace("á", "a");
            _string = _string.Replace("é", "e");
            _string = _string.Replace("í", "i");
            _string = _string.Replace("ó", "o");
            _string = _string.Replace("ú", "u");
            _string = _string.Replace("ü", "u");


            return _string;
        }
    }
}