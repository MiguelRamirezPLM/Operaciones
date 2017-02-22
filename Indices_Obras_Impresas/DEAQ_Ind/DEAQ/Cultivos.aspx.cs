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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

public partial class DEAQ_Cultivos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        this.rptLet.DataSource = InformacionCultivos.Instance.getAlphabet();
        this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
      }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cropsRow = rowView.Row;

        Repeater rptCultivo = (Repeater)e.Item.FindControl("rptCultivo");
        rptCultivo.DataSource = InformacionCultivos.Instance.getCultivos(cropsRow.ItemArray[0].ToString());
        rptCultivo.DataBind();
      //  _cont += 1;
    }

    protected void rptCultivo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cropsRow = rowView.Row;

      _ds.Cultivos.AddCultivosRow(cropsRow.ItemArray[0].ToString(), cropsRow.ItemArray[1].ToString());

        Repeater rptPlaga = (Repeater)e.Item.FindControl("rptPlaga");
        rptPlaga.DataSource = InformacionCultivos.Instance.getPlagasCultivos(Convert.ToInt32(cropsRow.ItemArray[2].ToString()));
        rptPlaga.DataBind();

        Repeater rptEnfermedad = (Repeater)e.Item.FindControl("rptEnfermedad");
        rptEnfermedad.DataSource = InformacionCultivos.Instance.getEnfermedadesCultivos(Convert.ToInt32(cropsRow.ItemArray[2].ToString()));
        rptEnfermedad.DataBind();

        Repeater rptMalezas = (Repeater)e.Item.FindControl("rptMalezas");
        rptMalezas.DataSource = InformacionCultivos.Instance.getMalezasCultivos(Convert.ToInt32(cropsRow.ItemArray[2].ToString()));
        rptMalezas.DataBind();

        _cont += 1;
    
    }

    protected void rptPlaga_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow PlagaRow = rowView.Row;
 
        _ds.Plagas.AddPlagasRow((IndCultivosInfo.CultivosRow)_ds.Cultivos.Rows[_cont]);
        _ds.TablaInfoPlaga.AddTablaInfoPlagaRow((IndCultivosInfo.PlagasRow)_ds.Plagas.Rows[_cont2]);
        _ds.CeldaImgPlaga.AddCeldaImgPlagaRow((IndCultivosInfo.TablaInfoPlagaRow)_ds.TablaInfoPlaga.Rows[_cont2]);

        _ds.ImagenPlaga.AddImagenPlagaRow("file:///C:/img/" + PlagaRow.ItemArray[4].ToString().ToLower() + "", (IndCultivosInfo.CeldaImgPlagaRow)_ds.CeldaImgPlaga.Rows[_cont2]);

        _ds.CeldaInfoPlaga.AddCeldaInfoPlagaRow(PlagaRow.ItemArray[0].ToString(), PlagaRow.ItemArray[1].ToString(),
            (IndCultivosInfo.TablaInfoPlagaRow)_ds.TablaInfoPlaga.Rows[_cont2]);

        _ds.TablaPlagas.AddTablaPlagasRow((IndCultivosInfo.PlagasRow)_ds.Plagas.Rows[_cont2]);

        

        Repeater rptInformacionPlagas = (Repeater)e.Item.FindControl("rptInformacionPlagas");
        rptInformacionPlagas.DataSource = InformacionCultivos.Instance.getPlagasCultivosInfo(Convert.ToInt32(PlagaRow.ItemArray[3].ToString()),
                                                                                                Convert.ToInt32(PlagaRow.ItemArray[2].ToString()));

        rptInformacionPlagas.DataBind();

        _cont2 += 1;
    }

    protected void rptInformacionPlagas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow PlagaRow = rowView.Row;

        _ds.PlagasInfo.AddPlagasInfoRow(PlagaRow.ItemArray[0].ToString(), PlagaRow.ItemArray[1].ToString(), PlagaRow.ItemArray[2].ToString() ,
            (IndCultivosInfo.TablaPlagasRow)_ds.TablaPlagas.Rows[_cont2]);

      //  _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["InfoCultivos"] + "InfoCultivos2.xml");

       //   remplazo();
       
    }

    protected void rptEnfermedad_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cropsRow = rowView.Row;

        _ds.Enfermedades.AddEnfermedadesRow((IndCultivosInfo.CultivosRow)_ds.Cultivos[_cont]);
        _ds.TablaInfoEnfermedades.AddTablaInfoEnfermedadesRow((IndCultivosInfo.EnfermedadesRow)_ds.Enfermedades.Rows[_cont3]);
        _ds.CeldaImgEnfermedades.AddCeldaImgEnfermedadesRow((IndCultivosInfo.TablaInfoEnfermedadesRow)_ds.TablaInfoEnfermedades.Rows[_cont3]);
        _ds.ImagenEnfermedades.AddImagenEnfermedadesRow("file:///C:/img/" + cropsRow.ItemArray[4].ToString().ToLower() + "" ,
                                                         (IndCultivosInfo.CeldaImgEnfermedadesRow)_ds.CeldaImgEnfermedades.Rows[_cont3]);

        _ds.CeldaInfoEnfermedades.AddCeldaInfoEnfermedadesRow(cropsRow.ItemArray[0].ToString(), cropsRow.ItemArray[1].ToString(), 
                                                                 (IndCultivosInfo.TablaInfoEnfermedadesRow)_ds.TablaInfoEnfermedades.Rows[_cont3]);

        _ds.TablaEnfermedades.AddTablaEnfermedadesRow((IndCultivosInfo.EnfermedadesRow)_ds.Enfermedades.Rows[_cont3]);

        Repeater rptInformacionEnfermedades = (Repeater)e.Item.FindControl("rptInformacionEnfermedades");
        rptInformacionEnfermedades.DataSource = InformacionCultivos.Instance.getEnfermedadesCultivosInfo(Convert.ToInt32(cropsRow.ItemArray[3].ToString()),
                                                                                                Convert.ToInt32(cropsRow.ItemArray[2].ToString()));

        rptInformacionEnfermedades.DataBind();
        _cont3 += 1;

    }

    protected void rptInformacionEnfermedades_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow EnfRow = rowView.Row;

      //  _ds.PlagasInfo.AddPlagasInfoRow(PlagaRow.ItemArray[0].ToString(), PlagaRow.ItemArray[1].ToString(), PlagaRow.ItemArray[2].ToString(),
            //(IndCultivosInfo.TablaPlagasRow)_ds.TablaPlagas.Rows[_cont3]);

        _ds.EnfermedadesInfo.AddEnfermedadesInfoRow(EnfRow.ItemArray[0].ToString(), EnfRow.ItemArray[1].ToString(), EnfRow.ItemArray[2].ToString(),
                (IndCultivosInfo.TablaEnfermedadesRow)_ds.TablaEnfermedades.Rows[_cont3]);
         
    }

    protected void rptMalezas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cropsRow = rowView.Row;

        _ds.Malezas.AddMalezasRow((IndCultivosInfo.CultivosRow)_ds.Cultivos[_cont]);
        _ds.TablaInfoMalezas.AddTablaInfoMalezasRow((IndCultivosInfo.MalezasRow)_ds.Malezas.Rows[_cont4]);
        _ds.CeldaImgMalezas.AddCeldaImgMalezasRow((IndCultivosInfo.TablaInfoMalezasRow)_ds.TablaInfoMalezas.Rows[_cont4]);
        _ds.ImagenMaleza.AddImagenMalezaRow("file:///C:/img/" + cropsRow.ItemArray[4].ToString().ToLower() + "",
                                                         (IndCultivosInfo.CeldaImgMalezasRow)_ds.CeldaImgMalezas.Rows[_cont4]);

        _ds.CeldaInfoMalezas.AddCeldaInfoMalezasRow(cropsRow.ItemArray[0].ToString(), cropsRow.ItemArray[1].ToString(),
                                                                 (IndCultivosInfo.TablaInfoMalezasRow)_ds.TablaInfoMalezas.Rows[_cont4]);

        _ds.TablaMalezas.AddTablaMalezasRow((IndCultivosInfo.MalezasRow)_ds.Malezas.Rows[_cont4]);

        Repeater rptInformacionMalezas = (Repeater)e.Item.FindControl("rptInformacionMalezas");
        rptInformacionMalezas.DataSource = InformacionCultivos.Instance.getMalezasCultivosInfo(Convert.ToInt32(cropsRow.ItemArray[3].ToString()),
                                                                                                Convert.ToInt32(cropsRow.ItemArray[2].ToString()));

        rptInformacionMalezas.DataBind();
        _cont4 += 1;

    }

    protected void rptInformacionMalezas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow EnfRow = rowView.Row;


        _ds.MalezasInfo.AddMalezasInfoRow(EnfRow.ItemArray[0].ToString(), EnfRow.ItemArray[1].ToString(), EnfRow.ItemArray[2].ToString(),
                (IndCultivosInfo.TablaMalezasRow)_ds.TablaMalezas.Rows[_cont4]);

       _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["InfoCultivos"] + "InfoCultivos.xml");

        remplazo();

    }


    public static void remplazo()
    {
         StreamReader SR;
        StreamWriter SW;
        string cont = "";
        string archivo = "";

        SR = File.OpenText("C:\\Indices/DEAQ/InfoCultivos2.xml");
        cont = SR.ReadToEnd();
        archivo = cont;

        archivo = archivo.Replace("        <PlagasInfo>\r\n", "");
        archivo = archivo.Replace("        </PlagasInfo>\r\n", "");
        archivo = archivo.Replace(" <TablaPlagas>\r\n        ", "<TablaPlagas>\r\n        <Encabezado1/>\r\n        <Encabezado2/>\r\n        <Encabezado3/>\r\n       ");

        archivo = archivo.Replace("        <EnfermedadesInfo>\r\n", "");
        archivo = archivo.Replace("        </EnfermedadesInfo>\r\n", "");
        archivo = archivo.Replace(" <TablaEnfermedades>\r\n        ", "<TablaEnfermedades>\r\n        <Encabezado1/>\r\n        <Encabezado2/>\r\n        <Encabezado3/>\r\n       ");

        archivo = archivo.Replace("        <MalezasInfo>\r\n", "");
        archivo = archivo.Replace("        </MalezasInfo>\r\n", "");
        archivo = archivo.Replace(" <TablaMalezas>\r\n        ", "<TablaMalezas>\r\n        <Encabezado1/>\r\n        <Encabezado2/>\r\n        <Encabezado3/>\r\n       ");

        //archivo = archivo.Replace("..jpg", ".jpg");

        SR.Close();

        SW = File.CreateText("C:\\Indices/DEAQ/InfoCultivos2.xml");
        SW.Write(archivo);
        SW.Close();
    }
   


    private int _cont, _cont2, _cont3, _cont4;
    private IndCultivosInfo _ds = new IndCultivosInfo();
 
    
    
}