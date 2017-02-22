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
using Accessibility;

public partial class DEF_Interacciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = GenIndice.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            //this.rptProd.DataSource = GenIndice.Instance.getProductInteractions(this._editionId);
            _cont = 0;
        }
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow productoRow = rowView.Row;

        //_ds.Letra.AddLetraRow(letraRow.ItemArray[0].ToString());

        //_ds.Image.AddImageRow("file:///C:/Capitulares/" + letraRow.ItemArray[0].ToString().ToLower() + ".tif", (GenIndices.LetraRow)_ds.Letra.Rows[_cont]);
        ////_ds.Image.AddImageRow("", (GenIndices.LetraRow)_ds.Letra.Rows[_cont]);

        //Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        //rptProd.DataSource = GenIndice.Instance.getProducts(this._editionId, letraRow.ItemArray[0].ToString());
        //rptProd.DataBind();
        //_cont += 1;

        _ds.Productos.AddProductosRow(productoRow[0].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GenIndice.Instance.getProducts(this._editionId);
        rptProd.DataBind();
        _cont += 1;


    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        //_ds.Productos.AddProductosRow((Interacciones.ProductosRow)_ds.Productos.Rows[_cont]);

        _ds.Producto.AddProductoRow(prodRow.ItemArray[0].ToString(), prodRow.ItemArray[1].ToString(),
            
            //prodRow.ItemArray[3].ToString(),
            prodRow.ItemArray[2].ToString(), prodRow.ItemArray[3].ToString(), prodRow.ItemArray[4].ToString(), prodRow.ItemArray[5].ToString(),
            prodRow.ItemArray[6].ToString(), (Interacciones.ProductosRow)_ds.Productos.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "Interacciones2.xml");

    }

    private string setToken(string substances)
    {
        //if (substances.IndexOf("Vitamina ") != -1)
        //{
        substances = substances.Replace("Amfotericina b", "Amfotericina B");
        substances = substances.Replace("Antigeno de superficie de la hepatitis b", "Antigeno de superficie de la Hepatitis B");
        substances = substances.Replace("Ascórbico, ácido (vitamina c)", "Ascórbico, ácido (Vitamina C)");
        substances = substances.Replace("Estearato peg-100", "Estearato PEG-100");
        substances = substances.Replace("Factor ii (protrombina)", "Factor II (Protrombina)");
        substances = substances.Replace("Factor ix (christmas, factor antihemofílico b)", "Factor IX (Christmas, Factor Antihemofílico B)");
        substances = substances.Replace("Factor viii (factor antihemofílico a)", "Factor VIII (Factor Antihemofílico A)");
        substances = substances.Replace("Folitropina beta (fsh recombinante)", "Folitropina Beta (FSH Recombinante)");
        substances = substances.Replace("Hepatitis a, virus de la", "Hepatitis A, Virus de la");
        substances = substances.Replace("Hepatitis b, virus de la", "Hepatitis B, Virus de la");
        substances = substances.Replace("Interferón alfa 2b", "Interferón Alfa 2B");
        substances = substances.Replace("Interferón beta 1a", "Interferón Beta 1A");
        substances = substances.Replace("Niacinamida (vitamina b3)", "Niacinamida (Vitamina B3)");
        substances = substances.Replace("Parsol mcx", "Parsol MCX");
        substances = substances.Replace("Peginterferón alfa 2a", "Peginterferón Alfa 2A");
        substances = substances.Replace("Peginterferón alfa 2b", "Peginterferón Alfa 2B");
        substances = substances.Replace("Penicilina g clemizol", "Penicilina G Clemizol");
        substances = substances.Replace("Penicilina g potásica", "Penicilina G Potásica");
        substances = substances.Replace("Penicilina g sódica cristalina", "Penicilina G Sódica Cristalina");
        substances = substances.Replace("Penicilina v potásica", "Penicilina V Potásica");
        substances = substances.Replace("Polimixina b", "Polimixina B");
        substances = substances.Replace("Retinol (vitamina a)", "Retinol (Vitamina A)");
        substances = substances.Replace("Senósidos a-b", "Senósidos A-B");
        substances = substances.Replace("Sunspheres lcg", "Sunspheres LCG");
        substances = substances.Replace("Tinosorb m", "Tinosorb M");
        substances = substances.Replace("Tinosorb s", "Tinosorb S");
        substances = substances.Replace("Tocoferol (vitamina e)", "Tocoferol (Vitamina E)");
        substances = substances.Replace("Vitamina a (retinol)", "Vitamina A (Retinol)");
        substances = substances.Replace("Vitamina b, complejo (vitaminas b1, b6 y b12)", "Vitamina B, Complejo (Vitaminas B1, B6 y B12)");
        substances = substances.Replace("Vitamina b1 (tiamina)", "Vitamina B1 (Tiamina)");
        substances = substances.Replace("Vitamina b12 (cobalamina, cianocobalamina, hidroxocobalamina)", "Vitamina B12 (Cobalamina, Cianocobalamina, Hidroxocobalamina)");
        substances = substances.Replace("Vitamina b2 (riboflavina)", "Vitamina B2 (Riboflavina)");
        substances = substances.Replace("Vitamina b3 (nicotínico, ácido, niacina, niacinamida, nicotinamida)", "Vitamina B3 (Nicotínico, ácido, Niacina, Niacinamida, Nicotinamida)");
        substances = substances.Replace("Vitamina b5 (pantoténico, ácido)", "Vitamina B5 (Pantoténico, Ácido)");
        substances = substances.Replace("Vitamina b6 (piridoxina)", "Vitamina B6 (Piridoxina)");
        substances = substances.Replace("Vitamina b7 (inositol)", "Vitamina B7 (Inositol)");
        substances = substances.Replace("Vitamina c (ascórbico, ácido)", "Vitamina C (Ascórbico, ácido)");
        substances = substances.Replace("Vitamina d (calciferol)", "Vitamina D (Calciferol)");
        substances = substances.Replace("Vitamina d2 (ergocalciferol)", "Vitamina D2 (Ergocalciferol)");
        substances = substances.Replace("Vitamina d3 (colecalciferol)", "Vitamina D3 (Colecalciferol)");
        substances = substances.Replace("Vitamina e (tocoferol)", "Vitamina E (Tocoferol)");

        //}
        return substances;

    }

    private int _cont;
    private Interacciones _ds = new Interacciones();       
    private int _editionId;
}
