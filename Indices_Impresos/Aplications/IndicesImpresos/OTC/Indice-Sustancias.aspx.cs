using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indice_Sustancias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            List<string> letters = Querys.Instance.getAlphabetSubstances(this._editionId);

            foreach (string let in letters)
            {
                _ds.Letter.AddLetterRow(let);

                this.rptSubstances.DataSource = Querys.Instance.getSubstances(this._editionId, let);
                this.rptSubstances.DataBind();
                this._cont += 1;
            }
            this._cont = 0;
            this._cont2 = 0;

        }

    }
    protected void rptSubstances_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SubstanceByEdition subs = (SubstanceByEdition)e.Item.DataItem;

        _ds.Substances.AddSubstancesRow(subs.Substance, (IndActiveSubstances.LetterRow)_ds.Letter.Rows[this._cont]);

        Repeater rptAloneProducts = (Repeater)e.Item.FindControl("rptAloneProducts");
        rptAloneProducts.DataSource = Querys.Instance.getAloneProductsBySubstance(this._editionId, Convert.ToInt32(subs.SubstanceId));
        rptAloneProducts.DataBind();

        Repeater rptCombinedProducts = (Repeater)e.Item.FindControl("rptCombinedProducts");
        rptCombinedProducts.DataSource = Querys.Instance.getCombinedProductsBySubstance(this._editionId, Convert.ToInt32(subs.SubstanceId));
        rptCombinedProducts.DataBind();


        this._cont2 += 1;
    }

    protected void rptAloneProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProductByEdition prod = (ProductByEdition)e.Item.DataItem;

        _ds.SProduct.AddSProductRow(prod.Brand, prod.PharmaForm, prod.Division, prod.Pages == null ? "" : prod.Pages, (IndActiveSubstances.SubstancesRow)_ds.Substances.Rows[this._cont2]);

        _ds.WriteXml("C:/OTC/XML/Sustancias.xml");
    }

    protected void rptCombinedProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProductByEdition prod = (ProductByEdition)e.Item.DataItem;

        _ds.CProduct.AddCProductRow(prod.Brand, prod.Substances, prod.PharmaForm, prod.Division, prod.Pages == null ? "" : prod.Pages, (IndActiveSubstances.SubstancesRow)_ds.Substances.Rows[this._cont2]);

        _ds.WriteXml("C:/OTC/XML/Sustancias.xml");
    }


    private int _cont, _cont2;
    private int _editionId;
    private IndActiveSubstances _ds = new IndActiveSubstances();
}