using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

/// <summary>
/// Descripción breve de Utility
/// </summary>

public class Utilities
{

    #region Constructors

    private Utilities() { }

    #endregion

    #region Enumerations

    public enum Countries : int
    {
        CAD = 3,
        Colombia = 4,
        Ecuador = 6,
        Mexico = 11,
        Peru = 14,
        Venezuela = 16,
        West_Indies = 36
    }

    public enum Roles : int
    {
        Administrador = 1,
        Diagramador = 2,
        Medico = 3,
        Clientes = 4,
        Vendedor = 5,
        Formador = 6,
        Diseñador = 10,
        Agroquimico=11
    }

    public enum Menues : byte
    {
        Marcas = 1,
        Clasificacion = 2,
        Paginacion = 3,
        Formacion = 4
    }

    public enum WebSections : byte
    {
        HEADER = 1,
        CENTER = 2,
        FOOTER = 3,
        MIDDLE = 4
    }

    public enum WebPages : int
    {
        MasterPage = 1
    }

    public enum Module : byte
    {
        Ventas = 1,
        Producción = 2,
        Médico = 3,
        Diseño = 4
    }

    public enum ReportVendorType : short
    {
        Dia = 1,
        Global = 2
    }

    #endregion

    #region Public Methods

    public DataTable getSpinoffsByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        DbCommand dbCmd = Utilities.MedinetInstance.GetStoredProcCommand("dbo.plm_spGetSpinoffsByProduct");

        //Add Parameters
        Utilities.MedinetInstance.AddParameter(dbCmd, "@editionId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@divisionId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@categoryId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@productId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

        DataSet ds = Utilities.MedinetInstance.ExecuteDataSet(dbCmd);

        return ds.Tables[0];
    }

    public DataTable getProductsByEditionByATC(int parentEditionId, int editionId, int therapeuticId)
    {
        DbCommand dbCmd = Utilities.MedinetInstance.GetStoredProcCommand("dbo.plm_spGetProductsByEditionByATC");

        //Add Parameters
        Utilities.MedinetInstance.AddParameter(dbCmd, "@parentEditionId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, parentEditionId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@editionId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
        Utilities.MedinetInstance.AddParameter(dbCmd, "@therapeuticId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, therapeuticId);

        DataSet ds = Utilities.MedinetInstance.ExecuteDataSet(dbCmd);

        return ds.Tables[0];
    }

    //public void sendError(Exception ex, string mapPath)
    //{
    //    PLMUsersBusinessEntities.ErrorInfo error = new PLMUsersBusinessEntities.ErrorInfo();

    //    error.ApplicationId = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["HashKey"]).ApplicationId;

    //    PLMUsersBusinessEntities.FolioInfo folioNumber = PLMUsersBusinessLogicComponent.FoliosBLC.Instance.getByapplication(error.ApplicationId);

    //    folioNumber.CurrentNumber = folioNumber.CurrentNumber + 1;

    //    error.Message = ex.Message;
    //    error.StackTrace = ex.StackTrace;
    //    error.Status = true;
    //    error.Folio = folioNumber.Prefix + folioNumber.CurrentNumber.ToString();

    //    string folioN = PLMUsersBusinessLogicComponent.ErrorsBLC.Instance.registerError(error);

    //    PLMUsersBusinessLogicComponent.FoliosBLC.Instance.updateFolio(folioNumber);

    //    // Get the HtmlTemplate:  

    //    string htmlContent = Utility.Instance.getHtmlContent(mapPath);

    //    StringBuilder sbHtmlContent = new StringBuilder(htmlContent);

    //    // Set the requested information:
    //    Utility.Instance.setParameters(sbHtmlContent, "@@@Application@@@," + PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["HashKey"]).Description
    //        , "@@@ErrorId@@@," + error.ErrorId.ToString(), "@@@Message@@@," + error.Message, "@@@Date@@@," + DateTime.Today, "@@@Folio@@@," + error.Folio);

    //    // Send the email:
    //    //this._email.sendHTMLMail("juan.ramirez@plmlatina.com", "Sistemas", "Registro de Errores", sbHtmlContent.ToString(), true, "beatriz.vazquez@plmlatina.com", "angel.parra@plmlatina.com");


    //}

    public string getHtmlContent(string sourcePath)
    {
        if (string.IsNullOrEmpty(sourcePath))
            throw new ArgumentException("The source path cannot be null or empty.");

        StreamReader sr = new StreamReader(sourcePath, System.Text.Encoding.UTF8);
        string strHtml = sr.ReadToEnd();

        sr.Close();
        sr.Dispose();

        return strHtml;
    }

    public void setParameters(StringBuilder sbBody, params string[] htmlParameters)
    {
        for (int i = 0; i < htmlParameters.Length; i++)
        {
            string paramName = htmlParameters[i].Split(',')[0];
            string paramValue = htmlParameters[i].Split(',')[1];

            sbBody.Replace(paramName, paramValue);
        }
    }

    public string encryptParameter(string cadena)
    {
        return _cryp.encrypt(cadena);
    }

    public string decryptParameter(string cadena)
    {
        cadena = cadena.Replace(" ", "+");

        return _cryp.decrypt(cadena);
    }

    #endregion

    public static readonly Utilities Instance = new Utilities();
    public static Database MedinetInstance = DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.MedinetConnectionString");
    private PLMCryptographyComponent.CryptographyComponent _cryp = new PLMCryptographyComponent.CryptographyComponent();

}
