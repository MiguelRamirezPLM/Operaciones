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

    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetter.DataSource = Catalogs.Instance.getLetterMarcas(_editionId);
            this.rptLetter.DataBind();
            _cont = 0;
            _cont2 = 0;
        }

        this.Response.Redirect("general.aspx"); 
        
    }

    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letRow = rowView.Row;

        _ds2.Letter.AddLetterRow(letRow.ItemArray[0].ToString());
        _ds2.ImgLetter.AddImgLetterRow((System.Configuration.ConfigurationManager.AppSettings["letras"] + letRow.ItemArray[0].ToString()), (XMLSchema.LetterRow)_ds2.Letter.Rows[_cont]);
       
        Repeater rptMarcas = (Repeater)e.Item.FindControl("rptMarcas");
        rptMarcas.DataSource = Catalogs.Instance.getMarcas(letRow.ItemArray[0].ToString(), _editionId);
        rptMarcas.DataBind();
        _cont += 1;
        
    }
    protected void rptMarcas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow marcRow = rowView.Row;
        
        _ds2.ProductsByLetter.AddProductsByLetterRow(marcRow.ItemArray[1].ToString(), (XMLSchema.LetterRow)_ds2.Letter.Rows[_cont]);

        _ds2.Name.AddNameRow(marcRow.ItemArray[1].ToString(), (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);
        
        switch (marcRow.ItemArray[3].ToString())
        {
            case "1":
                if (!marcRow.ItemArray[2].ToString().Contains("Sin_Logo_"))
                    
                    _ds2.ImgTip1.AddImgTip1Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[2].ToString())
                       , (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);
                break;

            case "2":
                if (!marcRow.ItemArray[2].ToString().Contains("Sin_Logo_"))
                    
                    _ds2.ImgTip2.AddImgTip2Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[2].ToString())
                       , (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);
                break;
            case "3":
                if (!marcRow.ItemArray[2].ToString().Contains("Sin_Logo_"))
                 
                    _ds2.ImgTip3.AddImgTip3Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[2].ToString())
                       , (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);
                break;
            case "4":
                if (!marcRow.ItemArray[2].ToString().Contains("Sin_Logo_"))
                   
                    _ds2.ImgTip4.AddImgTip4Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[2].ToString())
                       , (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);
                break;
        }
        
        
        //if (marcRow.ItemArray[3].ToString().StartsWith("Sin_Logo_"))
        //    _ds2.imgProd.AddimgProdRow("", (XMLSchema.productsByLetterRow)_ds2.productsByLetter.Rows[_cont2]);
        //else
        //    _ds2.imgProd.AddimgProdRow((System.Configuration.ConfigurationManager.AppSettings["Ruta"]+marcRow.ItemArray[3].ToString())
        //        , (XMLSchema.productsByLetterRow)_ds2.productsByLetter.Rows[_cont2]);
              

        Repeater rptEmpresas = (Repeater)e.Item.FindControl("rptEmpresas");
        rptEmpresas.DataSource = Catalogs.Instance.getEmpresas(Convert.ToInt32(marcRow.ItemArray[4].ToString()),_editionId);
        rptEmpresas.DataBind();
        _cont2 += 1;

    }

    protected void rptEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow empRow = rowView.Row;

        if (empRow.ItemArray[0].ToString() == "1")
            _space = "*";
        else if (empRow.ItemArray[0].ToString() == "2")
            _space = "**";


        _ds2.Lab.AddLabRow((empRow.ItemArray[0].ToString() == "" ? "  " : _space.ToString()), empRow.ItemArray[1].ToString(), empRow.ItemArray[2].ToString(),
            (XMLSchema.ProductsByLetterRow)_ds2.ProductsByLetter.Rows[_cont2]);

        //_ds2.lab.AddlabRow((empRow.ItemArray[0].ToString() == "" ? "  " : empRow.ItemArray[0].ToString()), empRow.ItemArray[1].ToString(),
        //    (XMLSchema.productsByLetterRow)_ds2.productsByLetter.Rows[_cont2]);

        //_ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + ddlLetra.SelectedItem.ToString() + ".xml");
        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "marcas62.xml");

        
    }

    private int _cont;
    private int _cont2;
    private int _cont3;
    private int _editionId;
    private XMLSchema _ds2 = new XMLSchema();
    private string _space = "  ";
}
