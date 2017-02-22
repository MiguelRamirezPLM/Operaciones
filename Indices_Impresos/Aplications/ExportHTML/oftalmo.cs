using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    class oftalmo
    {
        static void Main(string[] args)
        {
            //DirectoryInfo dirInfo = new DirectoryInfo(@"C:\angel.parra\productos");
            //FileInfo[] fileInfo = dirInfo.GetFiles("*.htm");

           
            //GetHTML pHtml = new GetHTML();

            //for (int i = 0; i < fileInfo.Length; i++)
            //{
            //    string fileContent = pHtml.getHtmlContent(fileInfo[i].FullName);

            //    string[] parts = fileInfo[i].ToString().Split('_','.');

            //    pHtml.updatehtmlContent(parts[1].ToString(), parts[2].ToString(), "15", fileContent);


                
            //}

            GetHTML phtml = new GetHTML();
            phtml.replaceHTMLContent();
            
            
        }
    }
}
