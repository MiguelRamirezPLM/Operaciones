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
using System.Data.SqlClient;

public partial class Organicos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataAdapter sql;
        DataSet ds = new DataSet();
        string cadena = "select p.Nombre,Coalesce(f.FFarmaceutica,'') as FFarmaceutica,nArchivo as [File],p.Laboratorio " +
                        "from tmpProds t " +
                        "inner join producto p on(t.idprod=p.idprod) " +
                        "left join RelFFarmaceutica r on(p.idprod = r.IdProducto) " +
                        "left join FFarmaceutica f on(r.IDFFarmaceutica = f.IDFF) " +
                        "inner join tmpHTMLSF h on(p.idprod = h.idprod) " +
                        "where p.dedicion = 1 and  [Orgánico] like '*%' " +
                        " ORDER BY p.Nombre ";

        sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["PLMConectorDB.Properties.Settings.DEAQConnection"].ToString());
        sql.Fill(ds, "Organicos");
        this.rptProds.DataSource = ds.Tables["Organicos"];
        this.rptProds.DataBind();


    }
}
