using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;
using System.IO;

public partial class indiceUsos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(Session["editionId"]);

        DEAQTableAdapters.DistinctLettersTableAdapter letterAdapter = new DEAQTableAdapters.DistinctLettersTableAdapter();
        DEAQ.DistinctLettersDataTable letterTable = letterAdapter.GetCrops(editionId);

        foreach (DataRow letterRow in letterTable.Rows)
        {
            DEAQ.DistinctLettersRow row = (DEAQ.DistinctLettersRow)letterRow;

            this._letter = row.letter;

            this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

            DEAQTableAdapters.TIPOSCULTIVOSTableAdapter tcultivos = new DEAQTableAdapters.TIPOSCULTIVOSTableAdapter();
            DEAQ.TIPOSCULTIVOSDataTable cultivo = tcultivos.getCultivos(this._letter);

            this.rptCultivos.DataSource = cultivo;
            this.rptCultivos.DataBind();

            string fileName = _letter;

            WebRequest mywebReq = WebRequest.Create("http://localhost:51940/DEAQ%20PERU/indiceCultivos.aspx");

            WebResponse mywebResp = mywebReq.GetResponse();

            StreamReader sr  = new StreamReader(mywebResp.GetResponseStream());

            string strHTML = sr.ReadToEnd();

            StreamWriter sw = File.CreateText(@"C:\Users\ulises.orihuela\Desktop\Productos_Discos\deaq\cultivos\"+fileName+ ".html");

            sw.WriteLine(strHTML);
            sw.Close();

            Response.WriteFile(@"C:\Users\ulises.orihuela\Desktop\Productos_Discos\deaq\cultivos\"+fileName+ ".html");
        }  
      
    }

    protected void rptCultivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlControl divControl = (HtmlControl)e.Item.FindControl("z_divLevelChildren_0");
        HtmlControl aControl = (HtmlControl)e.Item.FindControl("displayChildren_0");

        aControl.Attributes.Add("onClick", "document.getElementById('" + divControl.ClientID + "').style.display='block'; guardSearch('" + divControl.ClientID + "', 1); return false;");
        aControl.Attributes.Add("onDblClick", "document.getElementById('" + divControl.ClientID + "').style.display='none'; guardSearch('" + divControl.ClientID + "', 0); return false");

        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEAQTableAdapters.CULTIVOSTableAdapter cprod = new DEAQTableAdapters.CULTIVOSTableAdapter();
        DEAQ.CULTIVOSDataTable prods = cprod.getProducts(row[0].ToString());        

        Repeater rpt = (Repeater)e.Item.FindControl("rptProd");
        rpt.DataSource = prods;
        rpt.DataBind();
    }

    Methods _methods = new Methods();
    private string _letter;

}
