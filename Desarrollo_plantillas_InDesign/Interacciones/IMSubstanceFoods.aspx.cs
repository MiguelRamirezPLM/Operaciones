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

public partial class IMSubstanceFoods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptSub.DataSource = GenIndice.Instance.getSubstances(this._editionId);
            this.rptSub.DataBind();
          
        }

    //    btnBack.Attributes.Add("onclick", "history.back(); return false;");

    }

    protected void rptSub_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow SubstancesRow = rowView.Row;
 
        _ds.IMSubstances.AddIMSubstancesRow(SubstancesRow.ItemArray[0].ToString(), SubstancesRow.ItemArray[2].ToString());

 
         
        Repeater rptIMSFoods = (Repeater)e.Item.FindControl("rptIMSFoods");
        rptIMSFoods.DataSource = GenIndice.Instance.getIMSubstancesF(Convert.ToInt32(SubstancesRow.ItemArray[1]));
        rptIMSFoods.DataBind();

        Repeater rptIMSReference = (Repeater)e.Item.FindControl("rptIMSReference");
        rptIMSReference.DataSource = GenIndice.Instance.getIMSubstancesR(Convert.ToInt32(SubstancesRow.ItemArray[1]));
        rptIMSReference.DataBind();

        _cont1 += 1;


    }

    protected void rptIMSFoods_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow IMSFoodsRow = rowView.Row;

 

        _ds.Foods.AddFoodsRow(IMSFoodsRow.ItemArray[0].ToString(),"["+ IMSFoodsRow.ItemArray[2].ToString()+".png]", IMSFoodsRow.ItemArray[1].ToString(), IMSFoodsRow.ItemArray[3].ToString(),
               (IMFoods.IMSubstancesRow)_ds.IMSubstances.Rows[_cont1]);

        

       // _ds.Severity.AddSeverityRow("file:///C:/img/" + IMSFoodsRow.ItemArray[2].ToString().ToLower() + ".png", (IMFoods.FoodsRow)_ds.Foods.Rows[_ds.Foods.Rows.Count - 1]);

 

 

        Repeater rptIMSFoods = (Repeater)e.Item.FindControl("rptIMSFoods");
  
        _cont2 += 1;


    }

    protected void rptIMSReference_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow IMSFoodsRRow = rowView.Row;

        _ds.ClinicalReference.AddClinicalReferenceRow(IMSFoodsRRow.ItemArray[0].ToString(), IMSFoodsRRow.ItemArray[1].ToString(), 
                    (IMFoods.IMSubstancesRow)_ds.IMSubstances.Rows[_cont1]);



        Repeater rptIMSReference = (Repeater)e.Item.FindControl("rptIMSReference");
     

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "IMSubstancesFoods.xml");

    }


    private int _cont;
    private int _cont1;
    private int _cont2;

    private IMFoods _ds = new IMFoods();

    private int _editionId;

    public int EditionIdI { get; set; }

}