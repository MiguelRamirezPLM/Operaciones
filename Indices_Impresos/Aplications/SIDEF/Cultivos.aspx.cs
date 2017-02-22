using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class Cultivos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["Letter"] == null ? "%" : this.Request.QueryString["Letter"];

        SqlDataAdapter sql;
        DataSet ds = new DataSet();
        string cadena = "select Distinct c.IDCul,c.Cultivo as Uso " +
                        "from Cultivos c " +
                        "inner join RelCultivo r on(c.IDCul = r.IDCultivo) " +
                        "inner join Producto p on(r.IDProducto = p.IDProd) " +
                        "where p.dedicion = 1 and c.Cultivo like '" + this._letter + "%' " +
                        " ORDER BY 2 ";

        sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql.Fill(ds, "1");
        this.rptUsos.DataSource = ds.Tables["1"];
        this.rptUsos.DataBind();

    }
    private string _letter;

    protected void rptUsos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow usosRow = rowView.Row;

        SqlDataAdapter sql;
        DataSet ds = new DataSet();
        string cadena = "select p.Nombre,Coalesce(f.FFarmaceutica,'') as FFarmaceutica,nArchivo as [File],p.Laboratorio " +
                        "from tmpProds t " +
                        "inner join producto p on(t.idprod=p.idprod) " +
                        "left join RelFFarmaceutica r on(p.idprod = r.IdProducto) " +
                        "left join FFarmaceutica f on(r.IDFFarmaceutica = f.IDFF) " +
                        "inner join tmpHTMLSF h on(p.idprod = h.idprod) " +
                        "inner join RelCultivo ru on(p.IDProd = ru.IDProducto) " +
                        "where p.dedicion = 1 and  ru.IDCultivo = " + usosRow.ItemArray[0].ToString() +
                        " ORDER BY p.Nombre ";

        sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql.Fill(ds, "Productos");

        Repeater rptProds = (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = ds.Tables["Productos"];
        rptProds.DataBind();

    }
}
