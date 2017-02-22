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

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptTerN0.DataSource = TherIndice.Instance.getAlphabet(this._editionId);
            this.rptTerN0.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
            _cont4 = 0;
        }
    }
    
    protected void rptTerN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN0Row = rowView.Row;

        _ds.TherapeuticN0.AddTherapeuticN0Row(terN0Row.ItemArray[1].ToString().ToUpper() + " - " + terN0Row.ItemArray[0].ToString().ToUpper());

        Repeater rptTherN1 = (Repeater)e.Item.FindControl("rptTerN1");
        rptTherN1.DataSource = TherIndice.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN0Row.ItemArray[2].ToString()), this._editionId);
        rptTherN1.DataBind();
        _cont += 1;

    }

    protected void rptTerN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN1Row = rowView.Row;

        _ds.TherapeuticN1.AddTherapeuticN1Row(terN1Row.ItemArray[1].ToString().ToUpper() + " - " + terN1Row.ItemArray[0].ToString().ToUpper(), 
            (Therapeutics.TherapeuticN0Row)_ds.TherapeuticN0.Rows[_cont]);

        Repeater rptTherN2 = (Repeater)e.Item.FindControl("rptTerN2");
        rptTherN2.DataSource = TherIndice.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN1Row.ItemArray[2].ToString()), this._editionId);
        rptTherN2.DataBind();
        _cont2 += 1;

    }

    protected void rptTerN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN2Row = rowView.Row;

        _ds.TherapeuticN2.AddTherapeuticN2Row(terN2Row.ItemArray[1].ToString() + " - " + terN2Row.ItemArray[0].ToString(),
            (Therapeutics.TherapeuticN1Row)_ds.TherapeuticN1.Rows[_cont2]);

        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdN2");
        rptProdN2.DataSource = TherIndice.Instance.getInformation(Convert.ToInt32(terN2Row.ItemArray[2].ToString()), this._editionId);
        rptProdN2.DataBind();
        
        Repeater rptTerN3 = (Repeater)e.Item.FindControl("rptTerN3");
        rptTerN3.DataSource = TherIndice.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN2Row.ItemArray[2].ToString()), this._editionId);
        rptTerN3.DataBind();
        _cont3 += 1;

    }

    protected void rptTerN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN3Row = rowView.Row;

        _ds.TherapeuticN3.AddTherapeuticN3Row(terN3Row.ItemArray[1].ToString() + " - " + terN3Row.ItemArray[0].ToString(),
            (Therapeutics.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        Repeater rptProdN3 = (Repeater)e.Item.FindControl("rptProdN3");
        rptProdN3.DataSource = TherIndice.Instance.getInformation(Convert.ToInt32(terN3Row.ItemArray[2].ToString()), this._editionId);
        rptProdN3.DataBind();
        _cont4 += 1;
    }

    protected void rptProdN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        _ds.ProductsN2.AddProductsN2Row(prodN2Row.ItemArray[1].ToString(), 
            setToken(prodN2Row.ItemArray[2].ToString()),
            //prodN2Row.ItemArray[2].ToString(),
            prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(),prodN2Row.ItemArray[5].ToString(),(Therapeutics.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["Therapeutic"] + "Therapeutics.xml");
    }

    protected void rptProdN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN3Row = rowView.Row;

        _ds.ProductsN3.AddProductsN3Row(prodN3Row.ItemArray[1].ToString(), 
            setToken(prodN3Row.ItemArray[2].ToString()),
            //prodN3Row.ItemArray[2].ToString(), 
            prodN3Row.ItemArray[3].ToString(), prodN3Row.ItemArray[4].ToString(), prodN3Row.ItemArray[5].ToString(), (Therapeutics.TherapeuticN3Row)_ds.TherapeuticN3.Rows[_cont4]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["Therapeutic"] + "Therapeutics.xml");
    }

    private string setToken(string substances)
    {
        //if (substances.IndexOf("Vitamina ") != -1)
        //{
        substances = substances.Replace("Edta", "EDTA");
        substances = substances.Replace("Filtro uva", "Filtro UVA");
        substances = substances.Replace("Filtro uvb", "Filtro UVB");
        substances = substances.Replace("Amfotericina b", "Amfotericina B");
        substances = substances.Replace("Antigeno de superficie de la hepatitis b", "Antigeno de superficie de la Hepatitis B");
        substances = substances.Replace("Ascórbico, ácido (vitamina c)", "Ascórbico, ácido (Vitamina C)");
        substances = substances.Replace("Estearato peg-100", "Estearato PEG-100");
        substances = substances.Replace("Factor ii (protrombina)", "Factor II (Protrombina)");
        substances = substances.Replace("Factor ix (christmas, factor antihemofílico b)", "Factor IX (Christmas, Factor Antihemofílico B)");
        substances = substances.Replace("Factor viii (factor antihemofílico a)", "Factor VIII (Factor Antihemofílico A)");
        substances = substances.Replace("Folitropina beta (fsh recombinante)", "Folitropina Beta (FSH Recombinante)");
        substances = substances.Replace("Folitropina alfa", "Folitropina Alfa");
        substances = substances.Replace("Gamma amino beta hidroxibutírico, ácido (gabob)", "Gamma Amino Beta Hidroxibutírico, ácido (GABOB)");
        substances = substances.Replace("Hepatitis a, virus de la", "Hepatitis A, Virus de la");
        substances = substances.Replace("Hepatitis b, virus de la", "Hepatitis B, Virus de la");
        substances = substances.Replace("Hilano g-f 20", "Hilano G-F 20");
        substances = substances.Replace("Interferón alfa 2b", "Interferón Alfa 2B");
        substances = substances.Replace("Interferón beta 1a", "Interferón Beta 1A");
        substances = substances.Replace("Inmunoglobulina g no modificada", "Inmunoglobulina G no modificada");
        substances = substances.Replace("Niacinamida (vitamina b3)", "Niacinamida (Vitamina B3)");
        substances = substances.Replace("Liraglutida (adn recombinante)", "Liraglutida (ADN recombinante)");
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
        substances = substances.Replace("Vitamina k", "Vitamina K");

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

    private int _cont,_cont2,_cont3,_cont4;
    private Therapeutics _ds = new Therapeutics();
    private int _editionId;
    
}
