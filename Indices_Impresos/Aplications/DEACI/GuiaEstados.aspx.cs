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

public partial class GuiaEstados : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (this.Request.QueryString["index"] != null)
        {
            this._index = Convert.ToInt32(this.Request.QueryString["index"].ToString());

            DEACITableAdapters.StatesTableAdapter state = new DEACITableAdapters.StatesTableAdapter();

            DEACI.StatesDataTable stateTab;

            switch (this._index)
            {
                case 1:

                    this.lblTitulo.InnerText = "GUÍA NACIONAL DE FABRICANTES Y DISTRIBUIDORES";

                    stateTab = state.getStates(this._editionId);

                    this.rptStates.DataSource = stateTab;
                    this.rptStates.DataBind();
                    break;
                case 2:
                    this.lblTitulo.InnerText = "LABORATORIOS Y EMPRESAS DE DIAGNÓSTICOS POR IMAGEN";

                    stateTab = state.getLabStates(this._editionId);

                    this.rptStates.DataSource = stateTab;
                    this.rptStates.DataBind();

                    break;
            }
        }
    }

    #endregion

    private int _editionId = 4;
    private int _index;
    

}
