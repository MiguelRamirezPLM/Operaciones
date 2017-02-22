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
using System.Xml;
using System.IO;


public partial class FarmaPrecios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._countryId = this.Request.QueryString["CountryId"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["CountryId"]);

        if (this._countryId > 0)
        {
            this.rptLet.DataSource = FarmaPrecio.Instance.getAlphabet(this._countryId);

            this.rptLet.DataBind();
            _cont = 0;
            _cont1 = 0;
        }
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letraRow = rowView.Row;

        //_ds.FPrecios.AddFPreciosRow(letraRow.ItemArray[0].ToString().ToUpper());

        _dsPP.FPrecios.AddFPreciosRow(letraRow.ItemArray[0].ToString().ToUpper());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");

        rptProd.DataSource = FarmaPrecio.Instance.getProducts(_countryId, letraRow.ItemArray[0].ToString());
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow productPRow = rowView.Row;

        //_ds.Product.AddProductRow(productPRow.ItemArray[1].ToString(), productPRow.ItemArray[3].ToString(), productPRow.ItemArray[5].ToString(), productPRow.ItemArray[6].ToString(),
        //                 (PreciosFarma.FPreciosRow)_ds.FPrecios.Rows[_cont]);


        _dsPP.Product.AddProductRow(productPRow.ItemArray[1].ToString(), productPRow.ItemArray[3].ToString(), productPRow.ItemArray[5].ToString(), productPRow.ItemArray[6].ToString(),
                    (PreciosFarmaPP.FPreciosRow)_dsPP.FPrecios.Rows[_cont]);

        Repeater rptProdPresentation = (Repeater)e.Item.FindControl("rptProdPresentation");
        rptProdPresentation.DataSource = FarmaPrecio.Instance.getProductPresentations(Convert.ToInt32(productPRow.ItemArray[0].ToString()) ,
            Convert.ToInt32(productPRow.ItemArray[2].ToString()), Convert.ToInt32(productPRow.ItemArray[4].ToString()), Convert.ToInt32(productPRow.ItemArray[7].ToString()));
        
        rptProdPresentation.DataBind();

      _cont1 += 1;

    }

    protected void rptProdPresentation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow presentationRow = rowView.Row;

        st = presentationRow.ItemArray[2].ToString();
        str = st.Remove(st.Length - 2);


        //_ds.Presentation.AddPresentationRow(presentationRow.ItemArray[1].ToString(), str, presentationRow.ItemArray[3].ToString(), (PreciosFarma.ProductRow)_ds.Product.Rows[_cont1]);

        //_ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["FarmaP"] + "FarmaPsF.xml");

        _dsPP.Presentation.AddPresentationRow(presentationRow.ItemArray[1].ToString(), presentationRow.ItemArray[2].ToString(), (PreciosFarmaPP.ProductRow)_dsPP.Product.Rows[_cont1]);

        _dsPP.WriteXml(System.Configuration.ConfigurationManager.AppSettings["FarmaP"] + "FarmaPsPP1.xml");

 
        remplazo();

    }

    public static void remplazo()
    {
        StreamReader SR;
        StreamWriter SW;
        string cont = "";
        string archivo = "";
        string doc = "";

        doc = System.Configuration.ConfigurationManager.AppSettings["FarmaP"] + "FarmaPsPP.xml";

        SR = File.OpenText(doc);
        cont = SR.ReadToEnd();
        archivo = cont;

       
        archivo = archivo.Replace("</PriceP>\r\n      </Presentation>\r\n      <Presentation>\r\n", "</PriceP>\r\n");
        archivo = archivo.Replace("<Presentation>", "<Presentation xmlns:aid=\"http://ns.adobe.com/AdobeInDesign/4.0/\" aid:table=\"table\">");
 
        SR.Close();

        SW = File.CreateText(doc);
        SW.Write(archivo);
        SW.Close();
    }
   

    private int _cont, _cont1 ;
    private PreciosFarma _ds = new PreciosFarma();
    private PreciosFarmaPP _dsPP = new PreciosFarmaPP(); 
    private int _countryId;
    private string str;
    private string st;
}