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

public partial class CarpetasVentas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptCliente.DataSource = Catalogs.Instance.getCompaniesAnunCarpetas(_editionId);
            this.rptCliente.DataBind();
            this._cont = 0;
        }

       // this.Response.Redirect("general.aspx");
    }

    protected void rptCliente_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowClients = rowView.Row;

        // _ds2.Clients.AddLetterRow(rowClients.ItemArray[1].ToString());

        _ds3.Anunciantes.AddAnunciantesRow(rowClients.ItemArray[15].ToString(), rowClients.ItemArray[1].ToString(), rowClients.ItemArray[2].ToString(), rowClients.ItemArray[3].ToString(), rowClients.ItemArray[4].ToString(),
            rowClients.ItemArray[5].ToString(), rowClients.ItemArray[6].ToString(), rowClients.ItemArray[7].ToString(), rowClients.ItemArray[8].ToString(), rowClients.ItemArray[10].ToString(),
            rowClients.ItemArray[9].ToString(), rowClients.ItemArray[11].ToString(), rowClients.ItemArray[12].ToString(), rowClients.ItemArray[13].ToString(), rowClients.ItemArray[14].ToString(),
            rowClients.ItemArray[16].ToString(), rowClients.ItemArray[17].ToString(), rowClients.ItemArray[18].ToString(), rowClients.ItemArray[19].ToString(),
            rowClients.ItemArray[20].ToString(), rowClients.ItemArray[21].ToString());

         

        Repeater rptSpecialities = (Repeater)e.Item.FindControl("rptSpecialities");
        rptSpecialities.DataSource = Catalogs.Instance.getSpecialities(_editionId, Convert.ToInt32(rowClients.ItemArray[0]));
        rptSpecialities.DataBind();

        Repeater rptSucursalesD = (Repeater)e.Item.FindControl("rptSucursalesD");
        rptSucursalesD.DataSource = Catalogs.Instance.getDirSucursales(_editionId, Convert.ToInt32(rowClients.ItemArray[0]));
        rptSucursalesD.DataBind();

        Repeater rptPySClients = (Repeater)e.Item.FindControl("rptPySClients");
        rptPySClients.DataSource = Catalogs.Instance.getDetailPySClients(_editionId, Convert.ToInt32(rowClients.ItemArray[0]));
        rptPySClients.DataBind();

        //Repeater rptPySRaiz = (Repeater)e.Item.FindControl("rptPySRaiz");
        //rptPySRaiz.DataSource = Catalogs.Instance.getDetailPyS(_editionId, Convert.ToInt32(rowClients.ItemArray[0]));
        //rptPySRaiz.DataBind();

        Repeater rptClienteBrand = (Repeater)e.Item.FindControl("rptClienteBrand");
        rptClienteBrand.DataSource = Catalogs.Instance.getDetailBrands(_editionId, Convert.ToInt32(rowClients.ItemArray[0]));
        rptClienteBrand.DataBind();

        _cont1 += 1;
    }

    protected void rptSpecialities_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowEspe = rowView.Row;

        _ds3.Especialidades.AddEspecialidadesRow(rowEspe.ItemArray[1].ToString(), rowEspe.ItemArray[2].ToString(), rowEspe.ItemArray[3].ToString(), 
                (VentasC.AnunciantesRow)_ds3.Anunciantes.Rows[_cont1]);
 
       
    }

    protected void rptSucursalesD_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowSuc = rowView.Row;

        _ds3.Sucursales.AddSucursalesRow(rowSuc.ItemArray[1].ToString(), rowSuc.ItemArray[2].ToString(), rowSuc.ItemArray[3].ToString(), rowSuc.ItemArray[4].ToString(),
            rowSuc.ItemArray[5].ToString(), rowSuc.ItemArray[6].ToString(), rowSuc.ItemArray[7].ToString(), rowSuc.ItemArray[9].ToString(), rowSuc.ItemArray[8].ToString(),
            rowSuc.ItemArray[10].ToString(), rowSuc.ItemArray[11].ToString(), rowSuc.ItemArray[12].ToString(), rowSuc.ItemArray[13].ToString(), 
            (VentasC.AnunciantesRow)_ds3.Anunciantes.Rows[_cont1]);
        
    }


    protected void rptPySClients_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowPySClients = rowView.Row;

        _ds3.PyS.AddPySRow(rowPySClients.ItemArray[1].ToString(), rowPySClients.ItemArray[2].ToString(), rowPySClients.ItemArray[3].ToString(), 
            (VentasC.AnunciantesRow)_ds3.Anunciantes.Rows[_cont1]);
   
        Repeater rptPySRaiz = (Repeater)e.Item.FindControl("rptPySRaiz");
        rptPySRaiz.DataSource = Catalogs.Instance.getDetailPyS(_editionId, Convert.ToInt32(rowPySClients.ItemArray[0]));
        rptPySRaiz.DataBind();
        
        _cont2 += 1;
    }

    protected void rptPySRaiz_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowCate = rowView.Row;

        
        _ds3.Product.AddProductRow(rowCate.ItemArray[0].ToString(),(VentasC.PySRow)_ds3.PyS.Rows[_cont2]);
 
        Repeater rptPySRaizH = (Repeater)e.Item.FindControl("rptPySRaizH");
        rptPySRaizH.DataSource = Catalogs.Instance.getDetailPySH(_editionId, Convert.ToInt32(rowCate.ItemArray[2]), Convert.ToInt32(rowCate.ItemArray[1]));
        rptPySRaizH.DataBind();

        _cont3 += 1;
    }

    protected void rptPySRaizH_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowCateS = rowView.Row;

        _ds3.SubProd.AddSubProdRow(rowCateS.ItemArray[0].ToString(), (VentasC.ProductRow)_ds3.Product.Rows[_cont3]);
       
    }

    protected void rptClienteBrand_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowBrandD = rowView.Row;

        _ds3.Brands.AddBrandsRow(rowBrandD.ItemArray[1].ToString(), rowBrandD.ItemArray[2].ToString(), (VentasC.AnunciantesRow)_ds3.Anunciantes.Rows[_cont1]);

        Repeater rptClienteBrandDetail = (Repeater)e.Item.FindControl("rptClienteBrandDetail");
        rptClienteBrandDetail.DataSource = Catalogs.Instance.getBrandsDetail(_editionId, Convert.ToInt32(rowBrandD.ItemArray[0]));
        rptClienteBrandDetail.DataBind();
         
        _cont4 += 1;
    }

    protected void rptClienteBrandDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow marcRow = rowView.Row;

        switch (marcRow.ItemArray[4].ToString())
        {
            case "1":

                //_ds2.Marca.AddMarcaRow(marcRow.ItemArray[1].ToString(), (IndCarpetas.LabRow)_ds2.Lab.Rows[_cont]);

                _ds3.Marcas.AddMarcasRow(marcRow.ItemArray[6].ToString(),(VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

                _ds3.BrandD.AddBrandDRow(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.MarcasRow)_ds3.Marcas.Rows[_cont5]);

                _ds3.Image.AddImageRow((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[5].ToString()), 
                    (VentasC.BrandDRow)_ds3.BrandD.Rows[_ds3.BrandD.Rows.Count - 1]); //[_ds2.Marca4.Rows.Count - 1]

                _ds3.Expire.AddExpireRow(marcRow.ItemArray[3].ToString(), (VentasC.BrandDRow)_ds3.BrandD.Rows[_ds3.BrandD.Rows.Count - 1]);

                _cont5 += 1;

            break;


            case "2":

            _ds3.Marcas.AddMarcasRow(marcRow.ItemArray[6].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.BrandD2.AddBrandD2Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.MarcasRow)_ds3.Marcas.Rows[_cont5]);

            //_ds3.BrandD2.AddBrandD2Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.Image2.AddImage2Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[5].ToString()),
                (VentasC.BrandD2Row)_ds3.BrandD2.Rows[_ds3.BrandD2.Rows.Count - 1]);

            _ds3.Expire2.AddExpire2Row(marcRow.ItemArray[3].ToString(), (VentasC.BrandD2Row)_ds3.BrandD2.Rows[_ds3.BrandD2.Rows.Count - 1]);

            _cont5 += 1;

            break;

            case "3":

            _ds3.Marcas.AddMarcasRow(marcRow.ItemArray[6].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);
            _ds3.BrandD3.AddBrandD3Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.MarcasRow)_ds3.Marcas.Rows[_cont5]);

            //_ds3.BrandD3.AddBrandD3Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.Image3.AddImage3Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[5].ToString()), 
                (VentasC.BrandD3Row)_ds3.BrandD3.Rows[_ds3.BrandD3.Rows.Count - 1]);

            _ds3.Expire3.AddExpire3Row(marcRow.ItemArray[3].ToString(), (VentasC.BrandD3Row)_ds3.BrandD3.Rows[_ds3.BrandD3.Rows.Count - 1]);
            _cont5 += 1;

            break;

            case "4":

            _ds3.Marcas.AddMarcasRow(marcRow.ItemArray[6].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);
            _ds3.BrandD4.AddBrandD4Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.MarcasRow)_ds3.Marcas.Rows[_cont5]);
            //_ds3.BrandD4.AddBrandD4Row(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.Image4.AddImage4Row((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[5].ToString()), 
                (VentasC.BrandD4Row)_ds3.BrandD4.Rows[_ds3.BrandD4.Rows.Count - 1]);

            _ds3.Expire4.AddExpire4Row(marcRow.ItemArray[3].ToString(), (VentasC.BrandD4Row)_ds3.BrandD4.Rows[_ds3.BrandD4.Rows.Count - 1]);

            _cont5 += 1;

            break;


            case "S":

            _ds3.Marcas.AddMarcasRow(marcRow.ItemArray[6].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.BrandDS.AddBrandDSRow(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.MarcasRow)_ds3.Marcas.Rows[_cont5]);

            //_ds3.BrandDS.AddBrandDSRow(marcRow.ItemArray[2].ToString(), marcRow.ItemArray[1].ToString(), (VentasC.BrandsRow)_ds3.Brands.Rows[_cont4]);

            _ds3.ImageS.AddImageSRow((System.Configuration.ConfigurationManager.AppSettings["Ruta"] + marcRow.ItemArray[5].ToString()),
                (VentasC.BrandDSRow)_ds3.BrandDS.Rows[_ds3.BrandDS.Rows.Count - 1]);

            _ds3.ExpireS.AddExpireSRow(marcRow.ItemArray[3].ToString(), (VentasC.BrandDSRow)_ds3.BrandDS.Rows[_ds3.BrandDS.Rows.Count - 1]);

            _cont5 += 1;
            break;
        }
        


        _ds3.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "CarVentasMEDITEC.xml");

        

    }


    private int _editionId;
    private int _cont;
    private int _cont1;
    private int _cont2;
    private int _cont3;
    private int _cont4;
    private int _cont5;
    //private CarpetasVentas _ds2 = new CarpetasVentas();
   
    private VentasC _ds3 = new VentasC();
   
    
 
}