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

public partial class Carpetas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetter.DataSource = Catalogs.Instance.getCompaniesCarpetas(_editionId);
            this.rptLetter.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
           
        }

        this.Response.Redirect("general.aspx"); 

    }

    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow labRow = rowView.Row;

        _ds2.Lab.AddLabRow(labRow.ItemArray[2].ToString(),labRow.ItemArray[0].ToString());       

                
        Repeater rptMarcas = (Repeater)e.Item.FindControl("rptMarcas");
        rptMarcas.DataSource = Catalogs.Instance.getBrandsByCompany(Convert.ToInt32(labRow.ItemArray[1]), _editionId);
        rptMarcas.DataBind();
        _cont += 1;
        //_cont2 += 1;



    }
    protected void rptMarcas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow marcRow = rowView.Row;
 
        switch (marcRow.ItemArray[5].ToString())
        {
            case "1":
                if (!marcRow.ItemArray[4].ToString().Contains("Sin_Logo"))
                {
                    _ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);

                    _ds2.Marca1.AddMarca1Row(marcRow.ItemArray[6].ToString(), marcRow.ItemArray[3].ToString(), 
                        (IndCarpetas.MarcaRow)_ds2.Marca.Rows[_cont2]);
                    
                    _ds2.ImgTip1.AddImgTip1Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[4].ToString()),
                        (IndCarpetas.Marca1Row)_ds2.Marca1.Rows[_ds2.Marca1.Rows.Count - 1]);
                    
                    _ds2.Vigencia1.AddVigencia1Row(marcRow.ItemArray[7].ToString(), (IndCarpetas.Marca1Row)_ds2.Marca1.Rows[_ds2.Marca1.Rows.Count - 1]);
                    
                    _cont2 += 1;
                }
                break;

            case "2":
                if (!marcRow.ItemArray[4].ToString().Contains("Sin_Logo"))
                {
                    _ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);
                    
                    _ds2.Marca2.AddMarca2Row(marcRow.ItemArray[6].ToString(), marcRow.ItemArray[3].ToString(),
                        (IndCarpetas.MarcaRow)_ds2.Marca.Rows[_cont2]);

                    _ds2.ImgTip2.AddImgTip2Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[4].ToString())
                        , (IndCarpetas.Marca2Row)_ds2.Marca2.Rows[_ds2.Marca2.Rows.Count - 1]);
                    
                    _ds2.Vigencia2.AddVigencia2Row(marcRow.ItemArray[7].ToString(), (IndCarpetas.Marca2Row)_ds2.Marca2.Rows[_ds2.Marca2.Rows.Count - 1]);
                    
                    _cont2 += 1;
                }
                break;
            case "3":
                if (!marcRow.ItemArray[4].ToString().Contains("Sin_Logo"))
                {
                    _ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);
                    
                    _ds2.Marca3.AddMarca3Row(marcRow.ItemArray[6].ToString(), marcRow.ItemArray[3].ToString(), 
                        (IndCarpetas.MarcaRow)_ds2.Marca.Rows[_cont2]);

                    _ds2.ImgTip3.AddImgTip3Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[4].ToString()),
                       (IndCarpetas.Marca3Row)_ds2.Marca3.Rows[_ds2.Marca3.Rows.Count - 1]);
                    
                    _ds2.Vigencia3.AddVigencia3Row(marcRow.ItemArray[7].ToString(), (IndCarpetas.Marca3Row)_ds2.Marca3.Rows[_ds2.Marca3.Rows.Count - 1]);
                    
                    _cont2 += 1;
                }
                break;
            case "4":
                if (!marcRow.ItemArray[4].ToString().Contains("Sin_Logo"))
                {
                    _ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);
                    
                    _ds2.Marca4.AddMarca4Row(marcRow.ItemArray[6].ToString(), marcRow.ItemArray[3].ToString(), 
                        (IndCarpetas.MarcaRow)_ds2.Marca.Rows[_cont2]);

                    _ds2.ImgTip4.AddImgTip4Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[4].ToString()),
                       (IndCarpetas.Marca4Row)_ds2.Marca4.Rows[_ds2.Marca4.Rows.Count -1]);

                    _ds2.Vigencia4.AddVigencia4Row(marcRow.ItemArray[7].ToString(), (IndCarpetas.Marca4Row)_ds2.Marca4.Rows[_ds2.Marca4.Rows.Count - 1]);
                    
                    _cont2 += 1;
                }
                break;

            case "S":
                if (marcRow.ItemArray[4].ToString().Contains("Sin_Logo") || (marcRow.ItemArray[4].ToString().Contains("Sin_logo")))
                {
                    _ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);

                    _ds2.Marca4.AddMarca4Row(marcRow.ItemArray[6].ToString(), marcRow.ItemArray[3].ToString(),
                        (IndCarpetas.MarcaRow)_ds2.Marca.Rows[_cont2]);

                    _ds2.ImgTip4.AddImgTip4Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[4].ToString()),
                       (IndCarpetas.Marca4Row)_ds2.Marca4.Rows[_ds2.Marca4.Rows.Count - 1]);

                    _ds2.Vigencia4.AddVigencia4Row(marcRow.ItemArray[7].ToString(), (IndCarpetas.Marca4Row)_ds2.Marca4.Rows[_ds2.Marca4.Rows.Count - 1]);

                    _cont2 += 1;
                }
                break;
        }


        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "carpetas.xml");

    }


    private int _editionId;
    private int _cont;
    private int _cont2;
    private int _cont3;
    private IndCarpetas _ds2 = new IndCarpetas();
    private string _space = "  ";
}
 