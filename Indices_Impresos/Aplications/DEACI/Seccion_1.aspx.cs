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

public partial class Seccion_1 : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._sectionId = this.Request.QueryString["SectionId"] == null ? -1 :
            Convert.ToInt32(this.Request.QueryString["SectionId"]);

        if (this._sectionId != -1)
        {
            DEACITableAdapters.SectionsTableAdapter secAdp = new DEACITableAdapters.SectionsTableAdapter();

            DEACI.SectionsDataTable section = secAdp.getOne(this._sectionId);
            this.lblSectionName.Text = section.Rows[0]["Description"].ToString();

            DEACI.SectionsDataTable secTable = secAdp.getSectionsByParentId(this._editionId, this._sectionId);

            this.rptSubSections.DataSource = secTable;
            this.rptSubSections.DataBind();
        }
    }

    #endregion

    #region Control Events

    protected void rptSubSections_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DEACI.SectionsRow secRow = (DEACI.SectionsRow)rowView.Row;

        DEACITableAdapters.SectionsTableAdapter subsubsecAdp = new DEACITableAdapters.SectionsTableAdapter();
        DEACI.SectionsDataTable subsubsecTable = subsubsecAdp.getSectionsByParentId(this._editionId, secRow.SectionId);

        Repeater rptSusSubSec = (Repeater)e.Item.FindControl("rptSubSubSection");
        rptSusSubSec.DataSource = subsubsecTable;
        rptSusSubSec.DataBind();
    }

    #endregion

    #region Public Methods

    public string getLink(string subSubSection, string description, string sectionId)
    {
        string cadena = "";

        if (!string.IsNullOrEmpty(description))
        {
            if (subSubSection.Equals("1"))
            {
                //cadena = "<b> <a class='linksSecciones' href='" + sectionId + ".htm'>" + description + "</a></b>";
                cadena = "<span class='linksSecciones'> " + description + "</span>";
            }
            else
            {
                cadena = "<a class='linksSecciones' href='" + sectionId + ".htm'>" + description + "</a>";
            }
        }
        else
            cadena = "";

        return cadena;
    }

    #endregion

    private int _sectionId;
    private int _editionId = 4;
}
