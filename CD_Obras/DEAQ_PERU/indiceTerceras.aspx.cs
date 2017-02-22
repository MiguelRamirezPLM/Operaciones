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

public partial class indiceTerceras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.empresaId = this.Request.QueryString["empresaId"] == null ? -1 :
            Convert.ToInt32(this.Request.QueryString["empresaId"]);

        DEAQTableAdapters.Empresas1TableAdapter tAEmpresa = new DEAQTableAdapters.Empresas1TableAdapter();

        DEAQ.Empresas1DataTable empresa = tAEmpresa.GetLabs(Convert.ToInt32(this.empresaId.ToString()));

        rptLevel_0.DataSource = empresa;
        rptLevel_0.DataBind();


    }

    protected void rptLevel_0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater rpt0 = (Repeater)e.Item.FindControl("rptLevel_00");
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEAQTableAdapters.CategoriasTableAdapter tACat = new DEAQTableAdapters.CategoriasTableAdapter();
        DEAQ.CategoriasDataTable categoria = tACat.GetCategorias(Convert.ToInt32(this.empresaId.ToString()));



        rpt0.DataSource = categoria;
        rpt0.DataBind(); 
        
                     
    }

    protected void rptLevel_00_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        Repeater rpt = (Repeater)e.Item.FindControl("rptLevel_1");
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEAQTableAdapters.Productos1TableAdapter tAprods = new DEAQTableAdapters.Productos1TableAdapter();
        DEAQ.Productos1DataTable prods = tAprods.GetProds(Convert.ToInt32(this.empresaId.ToString()), row[0].ToString()); 
        

        rpt.DataSource = prods;
        rpt.DataBind();
         

    }

    public string getLink(string Asterisco, string ProductName, string PharmaForm, int ProductId, int PharmaFormId, string Description, string Image)
   {
       
       string cadenahtml = "";

       if (Asterisco != "")
           cadenahtml += "<span class='Producto'>" +
                         "<table>" +
                         "   <tr>"  +
                         "     <td width='200'><a style='TEXT-DECORATION: none'target='_self' class='ProductoTercera' href='../productos/" + ProductId.ToString() + "_" + PharmaFormId.ToString() + ".htm'>" + " <b>* " + ProductName + ".</b></a> " +
                         "     </td>" +
                         "     <td >" + Description +  
                         "     </td>" +
                         "   </tr>" +
                         "   <tr>" +
                         "     <td><span class='forma'>" + PharmaForm + "</span>" +
                         "     </td>" +
                         //"     <td><img src=\"img\\" + Image + "\" \\>" +
                         //"     </td>" +
                         "   </tr>" +
                         "</table></span>";                     
       else
           cadenahtml += "<span class='Producto'><table>" +
                         "   <tr>" +
                         "     <td width='200'><b>" + ProductName + ".</b> " +
                         "     </td>" +
                         "     <td >" + Description +
                         "     </td>" +
                         "   </tr>" +
                         "   <tr>" +
                         "     <td><span class='forma'>" + PharmaForm + "</span>" +
                         "     </td>" +
                         //"     <td ><img src=\"img\\" + Image + "\" \\>" +
                         //"     </td>" +
                         "   </tr>" +
                         "</table></span>";
               
               
               
      
       return cadenahtml;
         
   }
   
    public string getDatos(string Address, string Suburb, string Location, string ZipCode, string Telephone, string Lada, string Fax, string Web, string City, string State, string Email)
    {       
              
            string cadena="<p class="+"\"Direccion"+"\">";

            if (Address != "")
                cadena += Address + "<br>";
            if (Suburb != "")
                cadena += Suburb + "<br>";
            if (Location != "")
                cadena += Location + "<br>";            
            if (ZipCode != "")
                cadena += ZipCode + " ";
            if (City != "")
                cadena += City + "<br>";
            if (State != "")
                cadena += State + "<br>";
            if (Telephone != "")
                cadena += "Tels.: " + Telephone + "<br>";
            if (Lada != "")
                cadena += Lada + "<br>";
            if (Fax != "")
                cadena += "Fax: " + Fax + "<br>";
            
            if (Email != "")
                cadena += "Email: <a href=" + "\"mailto:" + Email + "\">" + Email + "</a><br>";
            if (Web != "")
                cadena += "Página Web: <a href=" + "\"http://" + Web + "\">" + Web + "</a><br> ";   
            cadena += "</p>";            
        return cadena;
    }

    public string getImage(string Image)
    {
        string cadena = "";
        if (Image != "")
        {
            cadena = "<img src=\"img\\" + Image + "\" \\>";
        }

        return cadena;

    }

    private int empresaId=3;
}
