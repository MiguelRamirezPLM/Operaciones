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

public partial class DEAQ_IndiceIng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["Letter"] == null ? "%" : this.Request.QueryString["Letter"];

        this.img.ImageUrl = "letras/" + _letter + "1.jpg";

        SqlDataAdapter sql;
        DataSet ds = new DataSet();
        string cadena = "select Distinct c.IDIActivo,Upper(c.IActivo) as IActivo " +
                        "from IngreAct c " +
                        "inner join RelIngreAct r on(c.IDIActivo = r.IDIngreAct) " +
                        "inner join Producto p on(r.IDProducto = p.IDProd) " +
                        "where p.dedicion = 1 and c.IActivo like '" + this._letter + "%' " +
                        " ORDER BY 2 ";

        sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql.Fill(ds, "1");
        this.rptUsos.DataSource = ds.Tables["1"];
        this.rptUsos.DataBind();
    }

    protected void rptUsos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow usosRow = rowView.Row;

        SqlDataAdapter sql,sql2;
        DataSet ds = new DataSet();
        string cadena = "select Upper(p.Nombre) as Nombre,nArchivo as [File],p.Laboratorio,dbo.deaq_dfGetIngActByProdAndIng(t.idprod,ru.IDIngreAct) " +
                        "from tmpProds t " +
                        "inner join producto p on(t.idprod=p.idprod) " +
                        "inner join tmpHTMLSF h on(p.idprod = h.idprod) " +
                        "inner join RelIngreAct ru on(p.IDProd = ru.IDProducto) " +
                        "where p.dedicion = 1 and  ru.IDIngreAct = " + usosRow.ItemArray[0].ToString() +
                        " and dbo.deaq_dfGetIngActByProdAndIng(t.idprod,ru.IDIngreAct) = '' " +
                        "ORDER BY p.Nombre ";

        sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql.Fill(ds, "SProductos");

        if (ds.Tables["SProductos"].Rows.Count > 0)
        {
            Label lblSolo = (Label)e.Item.FindControl("LSolo");
            lblSolo.Visible = true;

            Repeater rptsProds = (Repeater)e.Item.FindControl("rptSProds");
            rptsProds.DataSource = ds.Tables["SProductos"];
            rptsProds.DataBind();
        }

        string cadena2 = "select Upper(p.Nombre) as Nombre,nArchivo as [File],p.Laboratorio,dbo.deaq_dfGetIngActByProdAndIng(t.idprod,ru.IDIngreAct) " +
                        "from tmpProds t " +
                        "inner join producto p on(t.idprod=p.idprod) " +
                        "inner join tmpHTMLSF h on(p.idprod = h.idprod) " +
                        "inner join RelIngreAct ru on(p.IDProd = ru.IDProducto) " +
                        "where p.dedicion = 1 and  ru.IDIngreAct = " + usosRow.ItemArray[0].ToString() +
                        " and dbo.deaq_dfGetIngActByProdAndIng(t.idprod,ru.IDIngreAct) <> '' " +
                        "ORDER BY p.Nombre ";

        sql2 = new SqlDataAdapter(cadena2, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql2.Fill(ds, "CProductos");

        if (ds.Tables["CProductos"].Rows.Count > 0)
        {
            Label lblCombinado = (Label)e.Item.FindControl("LCombinado");
            lblCombinado.Visible = true;

            Repeater rptCProds = (Repeater)e.Item.FindControl("rptCProds");
            rptCProds.DataSource = ds.Tables["CProductos"];
            rptCProds.DataBind();
        }

    }

    private string _letter;
}
