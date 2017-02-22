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



public class IndiceGuia : PLMDataAccessAdapter<object>
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

        //sb.Append("\n select distinct e.[numberedition],b.[brandname],b.[logo],substring (logo,1,1)as Typelogo,b.[brandId]");
        //sb.Append("\nfrom brands b");
        //sb.Append("\ninner join clientBrands cb on(b.brandId = cb.brandid )");
        //sb.Append("\ninner join clients c on (cb.clientId =c.clientId )");
        //sb.Append("\ninner join editions e on (c.editionId = e.editionId)");
        //sb.Append("\nwhere e.numberEdition= 60 and b.active=1 and cb.active=1 and c.active=1 and e.active=1  and b.Brandname like ('" + letra + "%') order by 2");

        sb.Append("\n select distinct cb.EditionId, b.BrandName, b.Logo,  substring (logo,1,1)as Typelogo,b.[brandId]");
       sb.Append("\n from ClientBrands cb ");
       sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId ");
       sb.Append("\n where cb.Active =1 and cb.EditionId = 2 and b.BrandName like ('" + letra + "%') order by 2");


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
        //sb.Append("\n WHERE TABLA1.MARCASID_ =" + marca_id.ToString() + " and edición ='59' ) TMP ");
        //sb.Append("\n INNER JOIN CARTA ON (TMP.CARTA = CARTA.ID) order by 2 ");


        //sb.Append("\nselect cbt.clientbrandtypeId,tmp.companyname,c.page");
        //sb.Append("\nfrom clients c");
        //sb.Append("\ninner	 join (");
        //sb.Append("\nselect distinct   [nombre corto de la empresa]as companyname, [código - cliente]");
        //sb.Append("\nfrom tmpguia59");
        //sb.Append("\nwhere [categoría] like 'anunc%' and [nombre corto de la empresa] is not null)tmp on(tmp.[código - cliente]	=c.clientCode )");
        //sb.Append("\ninner join ClientBrands cb on (c.clientId =cb.clientId)");
        //sb.Append("\ninner join brands b on(cb.brandId = b.brandid)");
        //sb.Append("\nleft join clientbrandtypes cbt on (cb.clientbrandtypeId = cbt.clientbrandtypeId)");
        //sb.Append("\ninner join editions e on (c.editionId = e.editionId)");
        //sb.Append("\nwhere  b.brandid="+marca_id.ToString()+" and  e.numberEdition=59 and  b.active=1 and cb.active=1 and c.active=1 and e.active=1 order by 2");

        
//            sb.Append("\nselect cbt.clientbrandtypeId, ");
//        sb.Append("\nc.shortname,");
//        sb.Append("\nc.page,");
//        sb.Append("\nc.CompanyName");
//sb.Append("\nfrom clients c   ");    
//sb.Append("\ninner join ClientBrands cb on (c.clientId =cb.clientId)");
//sb.Append("\ninner join brands b on(cb.brandId = b.brandid)");
//sb.Append("\nleft join clientbrandtypes cbt on (cb.clientbrandtypeId = cbt.clientbrandtypeId)");
//sb.Append("\n join editions e on (c.editionId = e.editionId)");
//sb.Append("\nwhere  b.brandid=" + marca_id.ToString() + " and  e.numberEdition=60 and  b.active=1 and cb.active=1 and c.active=1 and e.active=1 order by 2");

            sb.Append("\n select cb.ClientBrandTypeId,  c.ShortName, ec.Page, c.CompanyName ");
            sb.Append("\n from EditionClients ec ");
            sb.Append("\n inner join ClientBrands cb on ec.ClientId = cb.ClientId ");
            sb.Append("\n and 2 = ec.ClientTypeId ");
            sb.Append("\n and ec.EditionId = cb.EditionId ");
            sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId ");
            sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
            sb.Append("\n where b.Active = 1 and ec.EditionId = 2  and b.BrandId = " + marca_id.ToString() + " order by 2");
		 
        
        //DataSet ds = IndiceGuia.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getLetter()
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSELECT *  ");
        //sb.Append("\nFROM Alphabet   ");
        //sb.Append("\n ORDER BY 1 ");

        
        //sb.Append("\n Select Distinct SUBSTRING(c.companyname, 1, 1)  as Letter from Clients c ");
        //sb.Append("\n where c.EditionId = 10 ");
        //sb.Append("\n and c.ClientTypeId = 2");
        //sb.Append("\n order by 1");

        //indice de marcas
        //sb.Append("\n select Distinct SUBSTRING(c.companyname, 1, 1) as Letter  ");
        //sb.Append("\n from brands b ");
        //sb.Append("\n inner join clientBrands cb on(b.brandId = cb.brandid ) ");
        //sb.Append("\n inner join clients c on (cb.clientId =c.clientId ) ");
        //sb.Append("\n inner join editions e on (c.editionId = e.editionId)");
        //sb.Append("\n where e.Editionid= 60 and b.active=1 and cb.active=1 and c.active=1 and e.active=1    order by 1");


        sb.Append("\n select Distinct SUBSTRING(b.BrandName, 1, 1) as Letter   ");  
        sb.Append("\n from ClientBrands cb   ");
        sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId   ");
        sb.Append("\n where EditionId = 2   ");
        sb.Append("\n order by 1   ");
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
        sb.Append("\n where s.Active =1 and s.StateID="+ state.ToString()+" and cs.SpecialityId="+ speci.ToString() +" and cs.active=1 and c.Active=1  and editionId=10 order by city");

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




        sb.Append("select   distinct max(c.[clientId]) as clientId ,cs.[specialityId],c.[companyName],c.[address],c.[suburb],c.[postalCode],c.[ClientTypeId],tmp.Phone,tmf.fax,tmtf.PhoneFax,tml.lada,c.[email],c.[web],tmo.Lads");
  sb.Append("\n from clientSpecialities cs");
  sb.Append("\n inner join clients c on (cs.clientId=c.clientId)");
  sb.Append("\n inner join States s on (s.StateID = c.stateId)");
  sb.Append("\n left join Telefono tmp  on(c.clientId = tmp.clientId)");
  sb.Append("\n left join Fax tmf  on(c.clientId = tmf.clientId)");
  sb.Append("\n left join Telfax tmtf  on(c.clientId = tmtf.clientId)");
  sb.Append("\n left join lada tml  on(c.clientId = tml.clientId)");
  sb.Append("\n left join Oxidom tmo  on(c.clientId = tmo.clientId)");
  sb.Append("\n where s.Active =1 and s.StateID=" + state.ToString() + " and cs.SpecialityId=" + speci.ToString() + " and cs.active=1 and c.Active=1  and c.[city]='" + city + "' and editionId=10 ");
  sb.Append("\n group by   cs.[specialityId],c.[companyName],c.[address],c.[suburb],c.[postalCode],c.[ClientTypeId],tmp.Phone,tmf.fax,tmtf.PhoneFax,tml.lada,c.[email],c.[web],tmo.Lads   order by 3  ");





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

    public DataTable getCompanies()
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append(" Select	Distinct ");
        //sb.Append("\nc.ClientId, ");
        //sb.Append("\nc.CompanyName, ");
        //sb.Append("\ncount(*) as brands ");
        //sb.Append("\nFrom clients c ");
        //sb.Append("\ninner join clientbrands cb on c.clientid = cb.clientid ");
        //sb.Append("\n where c.clientid = 36482");
        //sb.Append("\nWhere Editionid = 10 and ClientTypeid = 2 ");
        //sb.Append("\nand substring(companyname, 1, 1) ");
        //sb.Append("\ngroup by c.ClientId, c.CompanyName ");
        //sb.Append("\nOrder by c.CompanyName ");

        sb.Append("Select	Distinct count(*) as brands,");
        sb.Append("\n c.ClientId, ");
        sb.Append("\n c.CompanyName , substring(CompanyName, 1, 1) ");
        sb.Append("\n From clients c ");
        sb.Append("\n inner join editionclients ec on c.clientid= ec.clientid ");
        sb.Append("\n inner join clientbrands cb on c.clientid = cb.clientid ");
        sb.Append("\n where ec.editionid= 2 and ec.clienttypeid= 2 and cb.clienttypeid= 2 and cb.EditionId=2");
        sb.Append("\n group by c.ClientId, c.CompanyName ");
        sb.Append("\n Order by c.CompanyName ");

        

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getBrandsByCompany(int clientid)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select	Distinct ");
        sb.Append("\nc.clientid, ");
        sb.Append("\nc.companyname, ");
        sb.Append("\nb.brandId, ");
        sb.Append("\nb.brandName, ");
        sb.Append("\ncoalesce(b.Logo, 'Sin_Logo.jpg') as Logo, ");
        sb.Append("\ncoalesce(substring(logo, 1, 1), '2') as LogoType, ");
        sb.Append("\nCASE cb.clientbrandtypeid ");
        sb.Append("\nWHEN 1	THEN '*' ");
        sb.Append("\nWHEN 2	THEN '**' ");
        sb.Append("\nELSE '' END	as distribuidor, ");
        sb.Append("\ncoalesce(cb.expireDescription, '') as expire ");
        sb.Append("\nFrom Clients c ");
        sb.Append("\nInner join ClientBrands cb on c.clientid = cb.clientid ");
        sb.Append("\nInner join Brands b on cb.brandId = b.brandId ");
        sb.Append("\nLeft join ClientBrandTypes cbt on cb.clientbrandtypeid = cbt.clientbrandtypeid ");
        sb.Append("\nwhere editionid = 2 and c.clientid = " + clientid.ToString());
        //sb.Append("\nwhere editionid = 6 and c.clientid = 36482 "); 
        sb.Append("\norder by b.brandName ");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];

    }


    public static readonly IndiceGuia Instance = new IndiceGuia();
}
