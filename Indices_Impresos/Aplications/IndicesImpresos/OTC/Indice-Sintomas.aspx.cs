using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indice_Sintomas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            List<string> letters = Querys.Instance.getAlphabetSymtoms(this._editionId);

            foreach (string let in letters)
            {
                this.rptSymtoms.DataSource = Querys.Instance.getSymptoms(this._editionId, let);
                this.rptSymtoms.DataBind();
                
            }
            this._cont = 0;
        }


    }
    protected void rptSymtoms_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SymptomByEdition symp = (SymptomByEdition)e.Item.DataItem;

        _ds.Symptom.AddSymptomRow(symp.Symptom, symp.Page == null ? "" : symp.Page);

        Repeater rptProducts = (Repeater)e.Item.FindControl("rptProducts");
        rptProducts.DataSource = Querys.Instance.getProductsBySymptom(this._editionId, Convert.ToInt32(symp.SymptomId));
        rptProducts.DataBind();

        this._cont += 1;
    }
    protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProductByEdition prod = (ProductByEdition)e.Item.DataItem;

        _ds.Product.AddProductRow(prod.Brand, prod.Pages == null ? "" : prod.Pages, (IndSymptoms.SymptomRow)_ds.Symptom.Rows[this._cont]);

        
        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["Sintomas"] + "Sintomas.xml");

    }


    private int _cont;
    private IndSymptoms _ds = new IndSymptoms();
    private int _editionId;
}