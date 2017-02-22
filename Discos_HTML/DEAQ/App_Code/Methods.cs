using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;


/// <summary>
/// Descripción breve de Methods
/// </summary>
public class Methods
{
	public Methods (){}//(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }


    /*public void saveHtmlFile(string fileName, string fileContent)
    {

        if (string.IsNullOrEmpty(this._destinationPath))
            throw new ArgumentException("The destination path cannot be null or empty.");

        FileStream htmlFile = new FileStream(this._destinationPath + "\\" + fileName, FileMode.Create);
        StreamWriter sw = new StreamWriter(htmlFile);

        sw.Write(fileContent);

        sw.Close();
        sw.Dispose();

    }*/
}