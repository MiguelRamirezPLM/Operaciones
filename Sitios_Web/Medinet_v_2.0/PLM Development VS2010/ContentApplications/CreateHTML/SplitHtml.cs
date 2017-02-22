using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CreateHTML 
{
    public class SplitHtml 
    {
        public SplitHtml() { } 


        #region Private methods

        private string getFileContent(string path) 
        {
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);

            string fileContent = sr.ReadToEnd();
            sr.Close();

            return fileContent;
        }

        #endregion
    }
}
