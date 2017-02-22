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

public partial class Marcas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._companyId = this.Request.QueryString["EmpresaId"] == null ? -1 :
            Convert.ToInt32(this.Request.QueryString["EmpresaId"]);

        if (!this.IsPostBack)
        {
            DataTable tCompany = GuiaDataAccess.Instance.getCompany(this._companyId);

            string shortName = tCompany.Rows[0]["Nombre CORTO de la Empresa - GUIA 53"].ToString();
            int totalBrands = Convert.ToInt32(tCompany.Rows[0]["Núm de MARCAS"] == DBNull.Value ? 0 : tCompany.Rows[0]["Núm de MARCAS"]);

            this.lblShortCompanyName.Text = "Nombre corto de la empresa: " + (string.IsNullOrEmpty(shortName) ? "SIN NOMBRE CORTO" : shortName);
            this.lblTotalBrands.Text = "Total de marcas: " + totalBrands.ToString();

            this.rptBrands.DataSource = GuiaDataAccess.Instance.getBrands(this._companyId);
            this.rptBrands.DataBind();
        }
    }

    protected void rptBrands_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView dv = (DataRowView)e.Item.DataItem;
        DataRow dr = dv.Row;

        if (dr["Nombre del Archivo"].ToString().StartsWith("4") || dr["Nombre del Archivo"].ToString().StartsWith("3"))
        {
            Image img = (Image)e.Item.FindControl("idimg4");
            img.ImageUrl = ConfigurationManager.AppSettings["Ruta"] + dr["Nombre del Archivo"].ToString();
            img.Visible = true;
        }
        else
        {
            Image img = (Image)e.Item.FindControl("idimg");
            img.ImageUrl = ConfigurationManager.AppSettings["Ruta"] + dr["Nombre del Archivo"].ToString();
            img.Visible = true;
        }
    }

    private int _companyId;
}
