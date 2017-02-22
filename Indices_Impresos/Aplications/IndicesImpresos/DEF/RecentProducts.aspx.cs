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

public partial class DEF_RecentProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["Edition"] != null)
        {
            this._editionId = Convert.ToInt32(this.Request.QueryString["Edition"]);

            if (this._editionId > 0)
            {
                this.rptProducts.DataSource = GenIndice.Instance.getRecentProducts(this._editionId);
                this.rptProducts.DataBind();

            }
        }

    }

    protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds.Products.AddProductsRow(prodRow["Brand"].ToString(), prodRow["PharmaForm"].ToString(), this.setToken(prodRow["Substances"].ToString()),
            prodRow["DivisionName"].ToString(), prodRow["Page"].ToString());

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "ProdRecent.xml");


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

        substances = substances.Replace("(aa)", "(AA)");
        substances = substances.Replace("Acido", "Ácido");
        substances = substances.Replace(" adn ", " ADN ");
        substances = substances.Replace("ah's", "AH´S");
        substances = substances.Replace("bht", "BHT");
        substances = substances.Replace("cmv", "CMV");
        substances = substances.Replace("dha", "DHA");
        substances = substances.Replace(" eca ", " ECA ");
        substances = substances.Replace("Edta", "EDTA");
        substances = substances.Replace("Fps", "FPS");
        substances = substances.Replace("hemofilia a", "hemofilia A");
        substances = substances.Replace("hepatitis b", "hepatitis B");
        substances = substances.Replace("hepatitis c", "hepatitis C");
        substances = substances.Replace("hepatitis crónica c", "hepatitis crónica C");
        substances = substances.Replace("isrs", "ISRS");
        substances = substances.Replace("nph", "NPH");
        substances = substances.Replace(" Peg ", " PEG ");
        substances = substances.Replace("Pha's", "PHA'S");
        substances = substances.Replace("Ppg", "PPG");
        substances = substances.Replace(" Sca ", " SCA ");
        substances = substances.Replace("tcm", "TCM");
        substances = substances.Replace("tipo i", "tipo I");
        substances = substances.Replace("vii", "VII");
        substances = substances.Replace("vih", "VIH");
        substances = substances.Replace("vitamina a", "vitamina A");
        substances = substances.Replace("vitamina b", "vitamina B");
        substances = substances.Replace("vitamina c", "vitamina C");
        substances = substances.Replace("vitamina d", "vitamina D");
        substances = substances.Replace("vitamina e", "vitamina E");
        substances = substances.Replace("vitamina k", "vitamina K");

        //}
        return substances;

    }


    private RecentProd _ds = new RecentProd();
    private int _editionId;


    
}
