using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    #region Page events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.getCompanies();
        }
    }

    #endregion

    #region Control events

    protected void grdCompanies_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.grdCompanies.CurrentPageIndex = e.NewPageIndex;
        this.getCompanies();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.getDataSource(Convert.ToInt16(this.ddlPageSize.SelectedValue));
    }

    #endregion

    #region Private methods

    private void getCompanies()
    {
        this.grdCompanies.DataSource = GuiaDataAccess.Instance.getCompanies();
        this.grdCompanies.DataBind();
    }

    private void getDataSource(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdCompanies.AllowPaging = true;

            this.grdCompanies.PageSize = pagesize;
            this.grdCompanies.CurrentPageIndex = 0;
        }
        else
            this.grdCompanies.AllowPaging = false;

        this.getCompanies();
    }

    #endregion
}
