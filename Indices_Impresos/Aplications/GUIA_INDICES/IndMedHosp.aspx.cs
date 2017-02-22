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

public partial class IndMedHosp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLabs.DataSource = Catalogs.Instance.getLaboratories(_editionId);
            this.rptLabs.DataBind();
            _cont = 0;
        }

        this.Response.Redirect("general.aspx"); 
    }

    protected void rptLabs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow labRow = rowView.Row;

        _ds2.Clients.AddClientsRow (labRow.ItemArray[1].ToString(),
            labRow.ItemArray[2].ToString(), 
            labRow.ItemArray[3].ToString(),
            labRow.ItemArray[4].ToString(), 
            (labRow.ItemArray[5].ToString() + "," + labRow.ItemArray[6].ToString()), 
            labRow.ItemArray[12].ToString(),
            labRow.ItemArray[13].ToString(),
            labRow.ItemArray[7].ToString(),
            labRow.ItemArray[9].ToString(),
            labRow.ItemArray[8].ToString(),
            labRow.ItemArray[10].ToString(),
            labRow.ItemArray[11].ToString());

        if (Catalogs.Instance.getSucursales(Convert.ToInt32(labRow.ItemArray[0]), _editionId).Rows.Count > 0)
        {
            Repeater rptSucs = (Repeater)e.Item.FindControl("rptSucs");
            rptSucs.DataSource = Catalogs.Instance.getSucursales(Convert.ToInt32(labRow.ItemArray[0]), _editionId);
            rptSucs.DataBind();
        }

        Repeater rptProds = (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = Catalogs.Instance.getMedicines(Convert.ToInt32(labRow.ItemArray[0]), _editionId);
        rptProds.DataBind();
             
        _cont += 1;
        //_cont2 = 0;

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds2.Drugs.AddDrugsRow(prodRow.ItemArray[0].ToString(),prodRow.ItemArray[1].ToString(),prodRow.ItemArray[2].ToString(),
            prodRow.ItemArray[3].ToString(), (IndMedHospitalarios.ClientsRow)_ds2.Clients.Rows[_cont]);

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "medHosp.xml");
       
    }

    protected void rptSucs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow sucRow = rowView.Row;

        _ds2.Sucursales.AddSucursalesRow(sucRow.ItemArray[1].ToString(), sucRow.ItemArray[2].ToString(), sucRow.ItemArray[3].ToString(),
            sucRow.ItemArray[4].ToString(), (sucRow.ItemArray[5].ToString() + "," + sucRow.ItemArray[6].ToString()), sucRow.ItemArray[7].ToString(),
            sucRow.ItemArray[8].ToString(), sucRow.ItemArray[9].ToString(), sucRow.ItemArray[10].ToString(), sucRow.ItemArray[11].ToString(),
            sucRow.ItemArray[12].ToString(), sucRow.ItemArray[13].ToString(), (IndMedHospitalarios.ClientsRow)_ds2.Clients.Rows[_cont]);

        
    }

    private int _editionId;
    private int _cont;
    private IndMedHospitalarios _ds2 = new IndMedHospitalarios();
}
