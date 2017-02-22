using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.Common;



public class IndiceGuia : GUIADataAccessAdapter<object>
{
    #region Constructors

    private IndiceGuia()
    { }

    #endregion

    public DataTable getMarcas(string letra)
    {
        StringBuilder sb = new StringBuilder();


        //sb.Append("\nselect distinct e.[edición],e.[marca],e.[Hipervínculo marca], e.[nombre del archivo],e. [sin logo],e.[tipo de logo],m.[id] ");
        //sb.Append("\nfrom dbo.[Ed56 Marcas] e ");
        //sb.Append("\ninner join marcas m on ( e.marca=m.marca)");
        //sb.Append("\nwhere edición='56' and e.marca like  ('" + letra + "%') ORDER BY MARCA ");

        sb.Append("\n select distinct e.[numberedition],b.[brandname],b.[logo],substring (logo,1,1)as Typelogo,b.[brandId]");
        sb.Append("\nfrom brands b");
        sb.Append("\ninner join clientBrands cb on(b.brandId = cb.brandid )");
        sb.Append("\ninner join clients c on (cb.clientId =c.clientId )");
        sb.Append("\ninner join editions e on (c.editionId = e.editionId)");
        sb.Append("\nwhere e.numberEdition= 56 and b.active=1 and cb.active=1 and c.active=1 and e.active=1  and b.Brandname like ('" + letra + "%') order by 2");

       // DataSet ds = IndiceGuia.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public DataTable getEmpresas(int marca_id)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSELECT CARTA.CARTA, TMP.[Nombre CORTO de la Empresa - GUIA 53] ,paginas ");
        //sb.Append("\nFROM (  ");
        //sb.Append("\n SELECT [Nombre CORTO de la Empresa - GUIA 53],TABLA1.CARTA,marcasID_,paginas ");
        //sb.Append("\n FROM EMPRESAS INNER JOIN TABLA1 ON (EMPRESAS.ID=TABLA1.EMPRESASId_)");
        //sb.Append("\n WHERE TABLA1.MARCASID_ =" + marca_id.ToString() + " and edición ='56' ) TMP ");
        //sb.Append("\n INNER JOIN CARTA ON (TMP.CARTA = CARTA.ID) order by 2 ");


        //sb.Append("\nselect cbt.clientbrandtypeId,tmp.companyname,c.page");
        //sb.Append("\nfrom clients c");
        //sb.Append("\ninner	 join (");
        //sb.Append("\nselect distinct   [nombre corto de la empresa]as companyname, [código - cliente]");
        //sb.Append("\nfrom tmpguia56");
        //sb.Append("\nwhere [categoría] like 'anunc%' and [nombre corto de la empresa] is not null)tmp on(tmp.[código - cliente]	=c.clientCode )");
        //sb.Append("\ninner join ClientBrands cb on (c.clientId =cb.clientId)");
        //sb.Append("\ninner join brands b on(cb.brandId = b.brandid)");
        //sb.Append("\nleft join clientbrandtypes cbt on (cb.clientbrandtypeId = cbt.clientbrandtypeId)");
        //sb.Append("\ninner join editions e on (c.editionId = e.editionId)");
        //sb.Append("\nwhere  b.brandid="+marca_id.ToString()+" and  e.numberEdition=56 and  b.active=1 and cb.active=1 and c.active=1 and e.active=1 order by 2");

        
			sb.Append("\nselect cbt.clientbrandtypeId, ");
		sb.Append("\nc.shortname,");
		sb.Append("\nc.page");
sb.Append("\nfrom clients c   ");    
sb.Append("\ninner join ClientBrands cb on (c.clientId =cb.clientId)");
sb.Append("\ninner join brands b on(cb.brandId = b.brandid)");
sb.Append("\nleft join clientbrandtypes cbt on (cb.clientbrandtypeId = cbt.clientbrandtypeId)");
sb.Append("\n join editions e on (c.editionId = e.editionId)");
sb.Append("\nwhere  b.brandid=" + marca_id.ToString() + " and  e.numberEdition=56 and  b.active=1 and cb.active=1 and c.active=1 and e.active=1 order by 2");

		
		 
        
        //DataSet ds = IndiceGuia.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getLetter()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT *  ");
        sb.Append("\nFROM Alphabet   where  id>0 and id<13");
        sb.Append("\n ORDER BY 1 ");
        DataSet ds = IndiceGuia.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        
        return ds.Tables[0];
    }
    public DataTable getLetter(string letras)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT *  ");
        sb.Append("\nFROM Alphabet   ");
        sb.Append("\n  where letra in ('a')");
        sb.Append("\n ORDER BY 1 ");
        DataSet ds = IndiceGuia.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    
    
    
    public DataTable getState()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nselect * from States ");
        sb.Append("\n where countryId=1 order by 1");
        //sb.Append("\nwhere countryId=1  and stateId<=7  order by 1");
        //sb.Append("\n where countryId=1  and stateId>7 and stateId<=10  order by 1");
       // sb.Append("\n where countryId=1  and stateId>10 and stateId<=14  order by 1");
        //sb.Append("\n where countryId=1  and stateId>14 and stateId<=20  order by 1");
        //sb.Append("\n where countryId=1  and stateId>20 and stateId<=28  order by 1");
        //sb.Append("\n  where countryId=1  and stateId>28  order by 1");


        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }


    public DataTable getCities(int state,int speci)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct  c.[city],c.[stateId],cs.[specialityId]");
        sb.Append("\n from clientSpecialities cs ");
        sb.Append("\n inner join clients c on (cs.clientId=c.clientId)");
        sb.Append("\n inner join States s on (s.StateID = c.stateId)");
        sb.Append("\n where s.Active =1 and s.StateID="+ state.ToString()+" and cs.SpecialityId="+ speci.ToString() +" and cs.active=1 and c.Active=1  and editionId=5 order by city");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getCompany(int state,int speci,string city)
    {
        StringBuilder sb = new StringBuilder();


 //       sb.Append("\n select distinct  c.[clientId],cs.[specialityId],c.[companyName],c.[address],c.[suburb],c.[postalCode],c.[ClientTypeId],tmp.Phone,tmf.fax,tmtf.telfax,tml.ladS,c.[email],c.[web],tmo.oxidom");
 //sb.Append("\n from clientSpecialities cs");
 //sb.Append("\n inner join clients c on (cs.clientId=c.clientId)");
 //sb.Append("\n inner join States s on (s.StateID = c.stateId)");
 //sb.Append("\n left join (");
 //            sb.Append("\n SELECT 'Tel.: (' + LADA + ') ' + NUMBER as Phone,ClientId");
 //          sb.Append("\n FROM CLIENTPHONES  ");
 //          sb.Append("\n WHERE PHONETYPEID = 1	) tmp  on(c.clientId = tmp.clientId)");
 //sb.Append("\n left join (");
 //            sb.Append("\n SELECT 'Fax.: (' + LADA + ') ' + NUMBER as Fax,ClientId");
 //            sb.Append("\n FROM CLIENTPHONES "); 
 //          sb.Append("\n WHERE PHONETYPEID = 3	) tmf  on(c.clientId = tmf.clientId)");
 //sb.Append("\n left join (");
 //            sb.Append("\n SELECT 'Tel/Fax: (' + LADA + ') ' + NUMBER as TelFax,ClientId");
 //          sb.Append("\n FROM CLIENTPHONES  ");
 //          sb.Append("\n WHERE PHONETYPEID = 2	) tmtf  on(c.clientId = tmtf.clientId)");
 //sb.Append("\n left join (");
 //            sb.Append("\n SELECT 'Lada sin costo: '  + NUMBER as LadS,ClientId");
 //          sb.Append("\n FROM CLIENTPHONES  ");
 //          sb.Append("\n WHERE PHONETYPEID = 4	) tml  on(c.clientId = tml.clientId)");
 //sb.Append("\n left join (");
 //           sb.Append("\n SELECT 'Oxidom: '  + NUMBER as Oxidom,ClientId");
 //          sb.Append("\n FROM CLIENTPHONES  ");
 //          sb.Append("\n  WHERE PHONETYPEID = 5	) tmo  on(c.clientId = tml.clientId)");




        sb.Append("select distinct  c.[clientId],cs.[specialityId],c.[companyName],c.[address],c.[suburb],c.[postalCode],c.[ClientTypeId],tmp.Phone,tmf.fax,tmtf.PhoneFax,tml.lada,c.[email],c.[web],tmo.Lads");
  sb.Append("\n from clientSpecialities cs");
  sb.Append("\n inner join clients c on (cs.clientId=c.clientId)");
  sb.Append("\n inner join States s on (s.StateID = c.stateId)");
  sb.Append("\n left join Telefono tmp  on(c.clientId = tmp.clientId)");
  sb.Append("\n left join Fax tmf  on(c.clientId = tmf.clientId)");
  sb.Append("\n left join Telfax tmtf  on(c.clientId = tmtf.clientId)");
  sb.Append("\n left join lada tml  on(c.clientId = tml.clientId)");
  sb.Append("\n left join Oxidom tmo  on(c.clientId = tmo.clientId)");

           sb.Append("\n where s.Active =1 and s.StateID=" + state.ToString() + " and cs.SpecialityId=" + speci.ToString() + " and cs.active=1 and c.Active=1  and c.[city]='" + city + "' and editionId=5 order by 3  ");





        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getPhones(int id, int speci)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n  select distinct  cp.[specialityId],cp.[number],pt.[description],cp.[lada],ct.[clientTypeId],c.[ClientId]");
        sb.Append("\n from clients c");
        sb.Append("\n inner join clientPhones cp on (c.clientId=cp.clientId)");
        sb.Append("\n inner join Phonetypes pt on (cp.phonetypeId = pt.phonetypeId)");
        sb.Append("\n  inner join ClientTypes ct on (c.clientTypeId = ct.clientTypeId)");
        sb.Append("\n where cp.clientId="+ id.ToString()+" and pt.Active=1 and  cp.SpecialityId="+speci.ToString()+" and ct.Active=1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }


    public static readonly IndiceGuia Instance = new IndiceGuia();
}
