using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    class replaceDEF56
    {
        static void Main(string[] args)
        {
            string path = @"C:\Documents and Settings\angel.parra\Escritorio\DEF 56 PALM\src\laboratorios";
            
            string content;

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            FileInfo[] fileInfo = dirInfo.GetFiles("*.htm");

            GetHTML filecontent = new GetHTML();
            
            for (int x = 0; x < fileInfo.Length; x++)
            {
                content = filecontent.getHtmlContent(fileInfo[x].FullName);

                content=filecontent.quitAccents(content);

                filecontent.saveHtmlFile(@"C:\Documents and Settings\angel.parra\Escritorio\DEF 56 PALM\src\laboratorios", fileInfo[x].Name, content);
               

            }

        }

    }

}
