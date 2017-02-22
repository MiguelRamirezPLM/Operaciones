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
        this.rptLetter.DataSource = IndiceGuia.Instance.getLetter( );
        
        //this.rptLetter.DataSource = IndiceGuia.Instance.getLetter("trete");
        this.rptLetter.DataBind();
        _cont = 0;
        _cont2 = 0;
        
    }

    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letRow = rowView.Row;

        _ds2.Letter.AddLetterRow(letRow.ItemArray[0].ToString());
        _ds2.ImgLetter.AddImgLetterRow((System.Configuration.ConfigurationManager.AppSettings["letras"] + letRow.ItemArray[1].ToString()), (XMLSchema.LetterRow)_ds2.Letter.Rows[_cont]);
       
        Repeater rptMarcas = (Repeater)e.Item.FindControl("rptMarcas");
        rptMarcas.DataSource = IndiceGuia.Instance.getMarcas(letRow.ItemArray[2].ToString());
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
        rptEmpresas.DataSource = IndiceGuia.Instance.getEmpresas(Convert.ToInt32(marcRow.ItemArray[4].ToString()));
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
        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "prue1.xml");


       
    }

    private int _cont;
    private int _cont2;
    private int _cont3;
    private XMLSchema _ds2 = new XMLSchema();
    private string _space = "  ";
}
