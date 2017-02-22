using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

/// <summary>
/// Descripción breve de Catalogs
/// </summary>
public class Catalogs : PLMDataAccessAdapter<object>
{
	private Catalogs()
	{ }
    #region Productos

    ////////////////////// Anunciantes

    public DataTable getLetterAnun(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        /////////// Indice Anunciantes
        sb.Append("\n Select Distinct SUBSTRING(c.companyname COLLATE SQL_Latin1_General_CP1_CI_AI , 1, 1) as Letter  ");
        sb.Append("\n from Clients c   ");
        sb.Append("\n INNER JOIN EditionClients ec on c.ClientId = ec.ClientId  ");
        sb.Append("\n where ec.EditionId = "+ editionId + " and ec.ClientTypeId = 2   ");
        sb.Append("\n order by 1  ");

        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getCompaniesAnun(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct c.ClientId, c.CompanyName , ad.Address, ad.Suburb, ad.ZipCode, ad.City, s.StateName, '('+  cf.Lada + ') ' + cf.Number as PHONE, ");
        sb.Append("\n  '(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX,  '(' + cfax.Lada + ') ' + cfax.Number as FAX ,  clada.Number as LADA , cox.Number as Lads  ,ad.Email, ad.Web, ec.Page ");
        sb.Append("\n from EditionClients ec ");
        sb.Append("\n left join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n left join States s on ad.StateId = s.StateId ");
        sb.Append("\n left join ClientPhones cf on ec.ClientId = cf.ClientId ");
        sb.Append("\n 	                       and 1 = cf.PhoneTypeId ");
        sb.Append("\n 	                       and cf.Active = 1 ");
        sb.Append("\n                          and cf.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
        sb.Append("\n 		                   and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n                          and ctf.AddressId = ca.AddressId ");
        sb.Append("\n  left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n                          and 3 = cfax.PhoneTypeId	 ");
        sb.Append("\n                           and cfax.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n 	                       and 4 = clada.PhoneTypeId	 ");
        sb.Append("\n                          and clada.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones cox on ec.ClientId = cox.ClientId ");
        sb.Append("\n 			               and 5 = cox.PhoneTypeId		");
        sb.Append("\n                          and cox.AddressId = ca.AddressId ");
        sb.Append("\n left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n where ec.EditionId = " + editionId + " and ec.ClientTypeId = 2 and ad.Active = 1 and ad.CountryId = 1  and c.CompanyName  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') ");
        sb.Append("\n ORDER BY c.CompanyName  ");
        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    ////////////////////// Marcas

    public DataTable getLetterMarcas(int editionId)
    {
        StringBuilder sb = new StringBuilder();
    
        sb.Append("\n SELECT  Distinct   SUBSTRING(b.BrandName, 1, 1) as Letter ");
        sb.Append("\n FROM ClientBrands cb ");
        sb.Append("\n inner join Brands b on cb.BrandId =b.BrandId ");
        sb.Append("\n where EditionId=" + editionId );
        sb.Append("\n order by 1   ");

        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getMarcas(string letra, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct cb.EditionId, b.BrandName, b.Logo,  substring (logo,1,1)as Typelogo,b.[brandId]");
        sb.Append("\n from ClientBrands cb ");
        sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId ");
        sb.Append("\n where cb.Active = 1 and cb.EditionId = " + edition +" and b.BrandName like ('" + letra + "%') order by 2");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getEmpresas(int brandId, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select cb.ClientBrandTypeId,  c.ShortName, ec.Page, c.CompanyName ");
        sb.Append("\n from EditionClients ec ");
        sb.Append("\n inner join ClientBrands cb on ec.ClientId = cb.ClientId ");
        sb.Append("\n and 2 = ec.ClientTypeId ");
        sb.Append("\n and ec.EditionId = cb.EditionId ");
        sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId ");
        sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
        sb.Append("\n where b.Active = 1 and ec.EditionId = " + edition +" and b.BrandId = " + brandId.ToString() + " order by 2");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }


    ////////////////////// Productos y Servicios

    public DataTable getLetterPyS(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct SUBSTRING(hc.Description COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) as  Letter ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n where echc.EditionId = " + editionId +"  and hc.ParentId is null --and hc.Active = 1");
        sb.Append("\n union ");
        sb.Append("\n select distinct SUBSTRING(hc1.Description COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) as  Letter ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n                             		 and hc.ParentId is not null ");
        sb.Append("\n inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	--and hc1.Active = 1");
        sb.Append("\n where echc.EditionId = " + editionId );
        sb.Append("\n order by 1 ");
        
        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductsPyS(string letra, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct  hc.Description ,  hc.HeterogeneousCategoryId");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId ");
        sb.Append("\n 				and echc.EditionId = ec.EditionId ");
        sb.Append("\n where echc.EditionId = " + edition + " and hc.ParentId is null and  hc.Description  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("\n union ");
        sb.Append("\n select distinct  hc1.Description , hc1.HeterogeneousCategoryId ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n     			 and hc.ParentId is not null ");
        sb.Append("\n inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId ");
        sb.Append("\n 				and echc.EditionId = ec.EditionId ");
        sb.Append("\n where echc.EditionId = " + edition + " and hc1.Description  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("\n order by 1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }
    
    public DataTable getBrandsByProdId(int prodid, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct  c.ShortName Companyname ,ec.Page, c.CountryId");
        sb.Append("\n from EditionClientHeterogeneousCategories echc");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId");
        sb.Append("\n 						     and echc.EditionId = ec.EditionId	");
        sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId");
        sb.Append("\n where echc.EditionId =" + edition + " and echc.HeterogeneousCategoryId =" + prodid.ToString());
        sb.Append("\n order by 1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    
    public DataTable getSubProducts(string prod, int edition)//(int productId)
    {
        StringBuilder sb = new StringBuilder(); 
 
        sb.Append(" select distinct hc.Description ,  hc.HeterogeneousCategoryId  ");
        sb.Append("\n  from EditionClientHeterogeneousCategories echc  ");
        sb.Append("\n  inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId  ");
        sb.Append("\n                              		 and hc.ParentId is not null  ");
        sb.Append("\n  inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	 ");
        sb.Append("\n  inner join EditionClients ec on echc.ClientId = ec.ClientId ");
        sb.Append("\n  							and echc.EditionId = ec.EditionId	 ");
        sb.Append("\n  where echc.EditionId =" + edition + "   and hc1.HeterogeneousCategoryId =" + prod.ToString());
        sb.Append("\n  order by 1 ");
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }
      
    public DataTable getSBrandsByProdId(int prodid,int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct  c.ShortName Companyname ,ec.Page, c.CountryId");
        sb.Append("\n from EditionClientHeterogeneousCategories echc");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId");
        sb.Append("\n 						     and echc.EditionId = ec.EditionId	");
        sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId");
        sb.Append("\n where echc.EditionId =" + edition + "and echc.HeterogeneousCategoryId =" + prodid.ToString());
        sb.Append("\n order by 1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ////////////////////// Especialidades

    public DataTable getState(int edition, int speciality)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct s.* ");
       sb.Append("\n from EditionClientSpecialities ecs");
       sb.Append("\n inner join Clients c on ecs.ClientId = c.ClientId");
       sb.Append("\n inner join ClientAddresses ca on c.ClientId = ca.ClientId");
       sb.Append("\n inner join Addresses a on ca.AddressId = a.AddressId");
       sb.Append("\n left join States s on a.StateId = s.StateId");
       sb.Append("\n inner join Specialities sp on ecs.SpecialityId = sp.SpecialityId");
       sb.Append("\n where ecs.EditionId =" + edition + " and s.CountryId = 1 and ecs.SpecialityId = " + speciality);
       sb.Append("\n order by 2");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getCities(int state, int speci, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct a.City,a.StateId,ecs.SpecialityId, sp.Description as Speciality");
        sb.Append("\n from EditionClientSpecialities ecs");
        sb.Append("\n inner join Clients c on ecs.ClientId = c.ClientId");
        sb.Append("\n inner join ClientAddresses ca on c.ClientId = ca.ClientId");
        sb.Append("\n inner join Addresses a on ca.AddressId = a.AddressId");
        sb.Append("\n left join States s on a.StateId = s.StateId");
        sb.Append("\n inner join Specialities sp on ecs.SpecialityId = sp.SpecialityId");
        sb.Append("\n where ecs.EditionId =" + edition + " and s.CountryId = 1 and a.StateId =" + state.ToString() + " and ecs.SpecialityId =" + speci.ToString());
        sb.Append("\n order by 1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getCompany(int state, int speci, string city, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct c.ClientId ,ec.SpecialityId , c.CompanyName ,ad.Address, ad.Suburb, ad.ZipCode, ec.ClientTypeId, '('+  cf.Lada + ') ' + cf.Number as PHONE,");
        sb.Append("\n '(' + cfax.Lada + ') ' + cfax.Number as FAX ,'(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX, clada.Number as LADA , ad.Email, ad.Web , cox.Number as Lads,  ecs.ClientTypeId as ClientTypeIdParent");
        sb.Append("\n from EditionClientSpecialities ec ");
        sb.Append("\n left join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n left join EditionClientSpecialities ecs on c.ClientIdParent = ecs.ClientId");
        sb.Append("\n           				and ec.EditionId = ecs.EditionId	");
        sb.Append("\n left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n left join States s on ad.StateId = s.StateId ");
        sb.Append("\n left join ClientPhones cf on ec.ClientId = cf.ClientId ");
        sb.Append("\n 						  and 1 = cf.PhoneTypeId ");
        sb.Append("\n 						  and cf.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
        sb.Append("\n 						   and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n 						   and ctf.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n							and 3 = cfax.PhoneTypeId	");
        sb.Append("\n							and cfax.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n 							and 4 = clada.PhoneTypeId");
        sb.Append("\n 							and clada.AddressId = ca.AddressId	 ");
        sb.Append("\n left join ClientPhones cox on ec.ClientId = cox.ClientId ");
        sb.Append("\n 							and 5 = cox.PhoneTypeId		");
        sb.Append("\n 							and cox.AddressId=ca.AddressId");
        sb.Append("\n left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n where ec.EditionId = " + edition + " and ad.StateId =" + state + " and ec.SpecialityId =" + speci + " and ad.City ='" + city.ToString() + "'");  
        sb.Append("\n  order by 3  ");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }


    ////////////////////// Consulta Rapida

    public DataTable getLetterCR(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct SUBSTRING(hc.Description COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) as  Letter ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n where echc.EditionId = " + editionId + "  and hc.ParentId is null ");
        sb.Append("\n union ");
        sb.Append("\n select distinct SUBSTRING(hc1.Description COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) as  Letter ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n                             		 and hc.ParentId is not null ");
        sb.Append("\n inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	");
        sb.Append("\n where echc.EditionId = " + editionId);
        sb.Append("\n order by 1 ");

        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    
    public DataTable getProductsCR(string letra, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct  hc.Description , null as Page, hc.HeterogeneousCategoryId");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId ");
        sb.Append("\n 				and echc.EditionId = ec.EditionId ");
        sb.Append("\n where echc.EditionId = " + edition + " and hc.ParentId is null and hc.Description  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("\n union ");
        sb.Append("\n select distinct  hc1.Description , null as Page, hc1.HeterogeneousCategoryId ");
        sb.Append("\n from EditionClientHeterogeneousCategories echc ");
        sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
        sb.Append("\n     			 and hc.ParentId is not null ");
        sb.Append("\n inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	");
        sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId ");
        sb.Append("\n 				and echc.EditionId = ec.EditionId ");
        sb.Append("\n where echc.EditionId = " + edition + "  and hc1.Description  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("\n order by 1");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }


    ////////////////////// Medicamentos Hospitalarios

    public DataTable getLaboratories(int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n  select distinct c.ClientId , c.CompanyName ,ad.Address, ad.Suburb, ad.ZipCode, ad.City,s.StateName ,'('+  cf.Lada + ') ' + cf.Number as PHONE,  ");
        sb.Append("\n  '(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX,'(' + cfax.Lada + ') ' + cfax.Number as FAX ,");
        sb.Append("\n   clada.Number as LADA , cox.Number as Lads, ad.Email, ad.Web ");
        sb.Append("\n   from EditionClientSpecialities ec ");
        sb.Append("\n   inner join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n   inner join EditionClientMedicalProducts ecmp on ec.ClientId = ecmp.ClientId");
        sb.Append("\n  											 and ec.EditionId = ecmp.EditionId");
        sb.Append("\n   left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n   left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n   left join States s on ad.StateId = s.StateId ");
        sb.Append("\n   left join ClientPhones cf on ec.ClientId = cf.ClientId ");
        sb.Append("\n   						  and 1 = cf.PhoneTypeId ");
        sb.Append("\n   						  and cf.AddressId = ca.AddressId");
        sb.Append("\n   left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
        sb.Append("\n   						   and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n   						   and ctf.AddressId = ca.AddressId");
        sb.Append("\n   left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n  							and 3 = cfax.PhoneTypeId	");
        sb.Append("\n  							and cfax.AddressId = ca.AddressId ");
        sb.Append("\n   left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n   							and 4 = clada.PhoneTypeId");
        sb.Append("\n   							and clada.AddressId = ca.AddressId");	 
        sb.Append("\n   left join ClientPhones cox on ec.ClientId = cox.ClientId ");
        sb.Append("\n   							and 5 = cox.PhoneTypeId	");	
        sb.Append("\n   							and cox.AddressId=ca.AddressId");
        sb.Append("\n   left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n   where ec.EditionId = "+ edition +" and    ec.SpecialityId = 28  ");
        sb.Append("\n   order by 2  ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSucursales(int clientId, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n  select distinct c.ClientId , c.CompanyName ,ad.Address, ad.Suburb, ad.ZipCode, ad.City,s.StateName ,'('+  cf.Lada + ') ' + cf.Number as PHONE,  ");
        sb.Append("\n  '(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX,'(' + cfax.Lada + ') ' + cfax.Number as FAX ,");
        sb.Append("\n   clada.Number as LADA , cox.Number as Lads, ad.Email, ad.Web ");
        sb.Append("\n   from EditionClientSpecialities ec ");
        sb.Append("\n   inner join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n   inner join EditionClientMedicalProducts ecmp on ec.ClientId = ecmp.ClientId");
        sb.Append("\n  											 and ec.EditionId = ecmp.EditionId");
        sb.Append("\n   left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n   left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n   left join States s on ad.StateId = s.StateId ");
        sb.Append("\n   left join ClientPhones cf on ec.ClientId = cf.ClientId ");
        sb.Append("\n   						  and 1 = cf.PhoneTypeId ");
        sb.Append("\n   						  and cf.AddressId = ca.AddressId");
        sb.Append("\n   left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
        sb.Append("\n   						   and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n   						   and ctf.AddressId = ca.AddressId");
        sb.Append("\n   left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n  							and 3 = cfax.PhoneTypeId	");
        sb.Append("\n  							and cfax.AddressId = ca.AddressId ");
        sb.Append("\n   left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n   							and 4 = clada.PhoneTypeId");
        sb.Append("\n   							and clada.AddressId = ca.AddressId");
        sb.Append("\n   left join ClientPhones cox on ec.ClientId = cox.ClientId ");
        sb.Append("\n   							and 5 = cox.PhoneTypeId	");
        sb.Append("\n   							and cox.AddressId=ca.AddressId");
        sb.Append("\n   left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n   where ec.EditionId = " + edition + " and    ec.SpecialityId = 28 and c.ClientIdParent = " + clientId);
        sb.Append("\n   order by 2  ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getMedicines(int clientId, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct p.ProductName DRUGNAME, acs.ActiveSubstance, pf.PharmaForm, pr.Description  PRESENTATION");
        sb.Append("\n from EditionClientMedicalProducts ecmp");
        sb.Append("\n inner join Products p on ecmp.ProductId = p.ProductId");
        sb.Append("\n left join PharmaceuticalForms pf on ecmp.PharmaFormId = pf.PharmaFormId");
        sb.Append("\n left join ActiveSubstances acs on ecmp.ActiveSubstanceId = acs.ActiveSubstanceId");
        sb.Append("\n left join Presentations pr on ecmp.PresentationId = pr.PresentationId ");
        sb.Append("\n where ecmp.EditionId = " + edition + " and ecmp.ClientId =" + clientId);
        sb.Append("\n order by 1");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    
    ////////////////////// Clientes internacionales

    public DataTable getLetterAnunI(int editionId)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n select distinct SUBSTRING(c.CompanyName COLLATE SQL_Latin1_General_CP1_CI_AI, 1, 1) as Letter ");
        sb.Append("\n from EditionClients ec ");
        sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
        sb.Append("\n and 4 = ec.ClientTypeId ");
        sb.Append("\n where ec.EditionId = " + editionId);
        sb.Append("\n order by 1 ");
        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    
    public DataTable getCompaniesInt(string letter, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct c.ClientId, c.CompanyName , ad.Address,  ad.ZipCode, ad.City, s.StateName, co.CountryName,     cf.Number as PHONE, ");
        sb.Append("\n    ctf.Number as PHONEFAX,    cfax.Number as FAX ,  clada.Number as LADA ,ad.Email, ad.Web, ec.Page ");
        sb.Append("\n from EditionClients ec ");
        sb.Append("\n left join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n left join States s on ad.StateId = s.StateId ");
        sb.Append("\n  left join ClientPhones cf on ec.ClientId = cf.ClientId ");
        sb.Append("\n 						  and 1 = cf.PhoneTypeId ");
        sb.Append("\n 						  and ca.AddressId = cf.AddressId 	");					 
        sb.Append("\n left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
        sb.Append("\n       				  and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n 						  and ca.AddressId = ctf.AddressId");
        sb.Append("\n left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n                         and 3 = cfax.PhoneTypeId	");
        sb.Append("\n and ca.AddressId =  cfax.AddressId");
        sb.Append("\n left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n and 4 = clada.PhoneTypeId	 ");
        sb.Append("\n and ca.AddressId = clada.AddressId");
        sb.Append("\n left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n left join Countries  co on ad.CountryId = co.CountryId  ");
        sb.Append("\n where ec.EditionId =" + edition + " and ec.ClientTypeId = 4 and ad.CountryId not in (1) and c.CompanyName  COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') ");
        sb.Append("\n ORDER BY c.CompanyName  ");
        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductByClient(int clientId, int edition)
    {
        StringBuilder sb = new StringBuilder(); 

        sb.Append("\n select p.ProductId, p.ProductName");
        sb.Append("\n from EditionClientProducts ecp");
        sb.Append("\n inner join Products p on ecp.ProductId = p.ProductId");
        sb.Append("\n 					   and 3 = p.TypeId");
        sb.Append("\n 					   and p.ParentId is null");
        sb.Append("\n inner join EditionClients ec on ecp.ClientId = ec.ClientId");
        sb.Append("\n 						    and ecp.EditionId = ec.EditionId");
        sb.Append("\n 						    and 4 = ec.ClientTypeId");					 
        sb.Append("\n where ecp.EditionId = " +  edition +"  and ecp.ClientId =" + clientId.ToString() );
        

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProductByIdByClientId(int productId, int clientId, int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n Select p.ProductId, p.ProductName");
        sb.Append("\n from EditionClientProducts ecp");
        sb.Append("\n inner join Products p on ecp.ProductId = p.ProductId");
        sb.Append("\n					 and 3 = p.TypeId ");
        sb.Append("\n inner join EditionClients ec on ecp.ClientId = ec.ClientId");
        sb.Append("\n						    and ecp.EditionId = ec.EditionId");
        sb.Append("\n						    and 4 = ec.ClientTypeId ");			 
        sb.Append("\n where ecp.EditionId = " + edition + "  and ecp.ClientId =" + clientId +" and p.ParentId =" + productId);
        sb.Append("\n   order by p.ProductName");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    ////////////////////// Clientes Carpetas


    public DataTable getCompaniesCarpetas(int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select	Distinct count(*) as brands,");
        sb.Append("\n c.ClientId, ");
        sb.Append("\n c.CompanyName , substring(CompanyName, 1, 1) ");
        sb.Append("\n From clients c ");
        sb.Append("\n inner join editionclients ec on c.clientid= ec.clientid ");
        sb.Append("\n inner join clientbrands cb on c.clientid = cb.clientid ");
        sb.Append("\n        			       and ec.EditionId = cb.EditionId");
        sb.Append("\n						   and ec.ClientTypeId = cb.ClientTypeId");
        sb.Append("\n where ec.editionid=" + edition + "and ec.clienttypeid= 2 ");
        sb.Append("\n group by c.ClientId, c.CompanyName ");
        sb.Append("\n Order by c.CompanyName "); 

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getBrandsByCompany(int clientid, int edition)
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
        sb.Append("\nwhere editionid = " + edition +  " and c.clientid = " + clientid.ToString());
        sb.Append("\norder by b.brandName ");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];

    }


    ////////////////////// Clientes Carpetas Nuevas

    //**************************** Informacion Cliente ****************************//

    public DataTable getCompaniesAnunCarpetas(int editionId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append(" select distinct c.ClientId, c.CompanyName , c.ShortName, ad.Address, ad.Suburb, ad.ZipCode, ad.City, s.StateName, '('+  cf.Lada + ') ' + cf.Number as PHONE,");
        sb.Append("\n '(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX,  '(' + cfax.Lada + ') ' + cfax.Number as FAX ,  clada.Number as LADA , cox.Number as Lads  ,ad.Email, ad.Web,");
        sb.Append("\n ct.TypeName, ");
        sb.Append("\n (select distinct  COUNT (HeterogeneousCategoryId) PyS from EditionClientHeterogeneousCategories  where EditionId = " + editionId + " and ClientId = C.ClientId) NumPyS ,");
        sb.Append("\n (select distinct  COUNT (BrandId) Brands from ClientBrands  where EditionId = " + editionId + " and ClientId = c.ClientId) Brands, ");
        sb.Append("\n (select distinct  COUNT (AdvertId) Advers from EditionSpecialityClientAdvers  where EditionId = " + editionId + " and ClientId = c.ClientId) Advers, ");
        sb.Append("\n (select COUNT (cs.ClientIdParent) Sucursal from EditionClients ecs ");
		sb.Append("\n 				inner join Clients cs on ecs.ClientId = cs.ClientId ");
        sb.Append("\n 				where ecs.EditionId = " + editionId + " and ecs.ClientTypeId = 1 and cs.ClientIdParent = c.ClientId) Sucursales, ");
        sb.Append("\n (select Distinct COUNT (SpecialityId) Speciality from EditionClientSpecialities  where EditionId = " + editionId + " and ClientId = c.ClientId) Specialities, 0 as Textos ");
        sb.Append("\n from EditionClients ec");
        sb.Append("\n left join Clients  c on ec.ClientId = c.ClientId");
        sb.Append("\n left join ClientAddresses ca on ec.ClientId = ca.ClientId");
        sb.Append("\n left join Addresses ad on ca.AddressId = ad.AddressId");
        sb.Append("\n left join States s on ad.StateId = s.StateId");
        sb.Append("\n left join ClientPhones cf on ec.ClientId = cf.ClientId");
        sb.Append("\n 	                       and 1 = cf.PhoneTypeId");
        sb.Append("\n  	                       and cf.Active = 1");
        sb.Append("\n                         and cf.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones ctf on ec.ClientId = ctf.ClientId");
        sb.Append("\n 		                   and 2 = ctf.PhoneTypeId	");
        sb.Append("\n                         and ctf.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones cfax on ec.ClientId = cfax.ClientId");
        sb.Append("\n                         and 3 = cfax.PhoneTypeId	");
        sb.Append("\n                          and cfax.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones clada on ec.ClientId = clada.ClientId");
        sb.Append("\n 	                       and 4 = clada.PhoneTypeId	");
        sb.Append("\n                         and clada.AddressId = ca.AddressId");
        sb.Append("\n left join ClientPhones cox on ec.ClientId = cox.ClientId");
        sb.Append("\n 			               and 5 = cox.PhoneTypeId		"); 
        sb.Append("\n                         and cox.AddressId = ca.AddressId");
        sb.Append("\n left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId");
        sb.Append("\n left join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
       sb.Append("\n  where ec.EditionId = " + editionId + " and ec.ClientTypeId = 2 and ad.Active = 1 and ad.CountryId = 1 ");
       sb.Append("\n  ORDER BY c.CompanyName ");
 
        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    //**************************** Informacion Especialidades ****************************//

  public DataTable getSpecialities(int editionId, int clientId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append(" Select distinct s.SpecialityId, s.Description, ecs.AdversDescription, ecs.Quantity ");
       sb.Append("\n from  EditionClientSpecialities ecs ");
       sb.Append("\n inner join Specialities s on ecs.SpecialityId = s.SpecialityId");
       sb.Append("\n where ecs.EditionId = " + editionId + " and ecs.ClientId = " + clientId );
       sb.Append("\n ORDER BY s.Description ");

       DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

       return ds.Tables[0];
    }

  //**************************** Informacion Sucursales ****************************//

  public DataTable getDirSucursales(int editionId, int clientId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append(" select distinct c.ClientId , c.CompanyName ,ad.Address, ad.Suburb, ad.ZipCode, ad.City,s.StateName ,'('+  cf.Lada + ') ' + cf.Number as PHONE,  ");
       sb.Append("\n '(' + ctf.Lada + ') ' + ctf.Number as PHONEFAX,'(' + cfax.Lada + ') ' + cfax.Number as FAX , ");
        sb.Append("\n clada.Number as LADA , cox.Number as Lads, ad.Email, ad.Web ");
        sb.Append("\n from EditionClients ec ");
        sb.Append("\n inner join Clients  c on ec.ClientId = c.ClientId ");
        sb.Append("\n left join ClientAddresses ca on ec.ClientId = ca.ClientId ");
        sb.Append("\n left join Addresses ad on ca.AddressId = ad.AddressId ");
        sb.Append("\n left join States s on ad.StateId = s.StateId ");
        sb.Append("\n left join ClientPhones cf on ec.ClientId = cf.ClientId ");
 	    sb.Append("\n 				  and 1 = cf.PhoneTypeId ");
        sb.Append("\n                 and cf.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones ctf on ec.ClientId = ctf.ClientId ");
 		sb.Append("\n 				  and 2 = ctf.PhoneTypeId	 ");
        sb.Append("\n 				  and ctf.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones cfax on ec.ClientId = cfax.ClientId ");
        sb.Append("\n 				  and 3 = cfax.PhoneTypeId	 ");
        sb.Append("\n 				  and cfax.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones clada on ec.ClientId = clada.ClientId ");
        sb.Append("\n 				  and 4 = clada.PhoneTypeId ");
        sb.Append("\n 				  and clada.AddressId = ca.AddressId ");
        sb.Append("\n left join ClientPhones cox on ec.ClientId = cox.ClientId ");
        sb.Append("\n 				  and 5 = cox.PhoneTypeId	 ");
        sb.Append("\n 				  and cox.AddressId=ca.AddressId ");
        sb.Append("\n left join PhoneTypes pt on cf.ClientPhoneId = pt.PhoneTypeId ");
        sb.Append("\n where ec.EditionId = " + editionId + " and ec.ClientTypeId=1  and  c.ClientIdParent = " + clientId + " ");
        sb.Append("\n order by 2  ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

  
 //**************************** Informacion Productos y Servicios ****************************//

      public DataTable getDetailPySClients(int editionId, int clientId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append(" select distinct c.ClientId, c.CompanyName, c.ShortName, ");
        sb.Append("\n (select distinct  COUNT (HeterogeneousCategoryId) PyS from EditionClientHeterogeneousCategories  where EditionId = " + editionId + " and ClientId = C.ClientId) NumPyS ");
       sb.Append("\n from EditionClients ec ");
       sb.Append("\n left join EditionClientSpecialities ecs on ec.ClientId = ecs.ClientId ");
       sb.Append("\n								   and ec.ClientTypeId = ecs.ClientTypeId ");
       sb.Append("\n left join Clients  c on ecs.ClientId = c.ClientId ");
       sb.Append("\n where ec.EditionId = " + editionId + " and ec.ClientTypeId = 2  and ec.ClientId = " + clientId);

       DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

       return ds.Tables[0];
    }

  public DataTable getDetailPyS(int editionId, int clientId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append(" select distinct  hc.Description ,  hc.HeterogeneousCategoryId, ec.ClientId ");
       sb.Append("\n from EditionClientHeterogeneousCategories echc ");
       sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
       sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId  ");
 		sb.Append("\n		and echc.EditionId = ec.EditionId ");
       sb.Append("\n  where echc.EditionId = " + editionId + " and hc.ParentId is null  and ec.ClientId = " + clientId );
       sb.Append("\n  union  ");
       sb.Append("\n  select distinct  hc1.Description , hc1.HeterogeneousCategoryId, ec.ClientId");
       sb.Append("\n  from EditionClientHeterogeneousCategories echc ");
       sb.Append("\n  inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId ");
     	sb.Append("\n		 and hc.ParentId is not null");
       sb.Append("\n  inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	");
       sb.Append("\n  inner join EditionClients ec on echc.ClientId = ec.ClientId ");
		sb.Append("\n         and echc.EditionId = ec.EditionId ");
       sb.Append("\n  where echc.EditionId = " + editionId + " and ec.ClientId = " + clientId );
       sb.Append("\n order by 1 ");

       DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

       return ds.Tables[0];
    }

  public DataTable getDetailPySH(int editionId, int clientId, int CatId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append(" select distinct hc.Description ,  hc.HeterogeneousCategoryId "); 
      sb.Append("\n from EditionClientHeterogeneousCategories echc ");  
      sb.Append("\n inner join HeterogeneousCategories hc on echc.HeterogeneousCategoryId = hc.HeterogeneousCategoryId "); 
      sb.Append("\n                        		 and hc.ParentId is not null  ");
      sb.Append("\n inner join HeterogeneousCategories hc1 on hc.ParentId = hc1.HeterogeneousCategoryId	");
      sb.Append("\n inner join EditionClients ec on echc.ClientId = ec.ClientId ");
  	  sb.Append("\n								and echc.EditionId = ec.EditionId	");
      sb.Append("\n where echc.EditionId = " + editionId + "  and hc1.HeterogeneousCategoryId = " + CatId + " and ec.ClientId = " + clientId);
      sb.Append("\n order by 1   ");
 
      DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }


  //**************************** Informacion Detalle Marcas ****************************//   


  public DataTable getDetailBrands(int editionId, int clientId)
  {
      StringBuilder sb = new StringBuilder();

       sb.Append(" select distinct c.ClientId, c.CompanyName, ");
       sb.Append("\n (select distinct  COUNT (BrandId) Brands from ClientBrands  where EditionId = " + editionId + " and ClientId = c.ClientId) Brands ");
       sb.Append("\n from EditionClients ec ");
       sb.Append("\n left join ClientBrands cb on ec.ClientId = cb.ClientId ");
		sb.Append("\n			 and ec.ClientTypeId = cb.ClientTypeId ");
       sb.Append("\n left join Clients c on ec.ClientId = c.ClientId ");
       sb.Append("\n where ec.EditionId = " + editionId + " and ec.ClientTypeId =2 and ec.ClientId = " + clientId);
       sb.Append("\norder by 2 ");

      DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getBrandsDetail(int editionId, int clientId)
  {
      StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct b.BrandId, b.BrandName,CASE cb.clientbrandtypeid  ");
       sb.Append("\nWHEN 1	THEN '*'  ");
       sb.Append("\nWHEN 2	THEN '**'  ");
       sb.Append("\nELSE '' END	as ClientBrandTypeId,  cb.ExpireDescription, SUBSTRING(b.Logo,1,1) TypeBrand, b.Logo, cb.ClientId ");
       sb.Append("\n from ClientBrands cb ");
       sb.Append("\n inner join Brands b on cb.BrandId = b.BrandId ");
       sb.Append("\n where cb.EditionId = " + editionId + "  and cb.ClientId = " + clientId);
       sb.Append("\norder by 2 ");

      DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  //****************************//**************************** INDICES NUEVA GUIA ****************************//****************************//   

  ////////////////////// Secciones Productos y Servicios

  public DataTable getLettersPyS(int editionId)
  {
      StringBuilder sb = new StringBuilder();
       
      sb.Append("\n Select distinct SUBSTRING (cat.CategoryThree COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1)  as L   ");
      sb.Append("\n from Clients c ");
      sb.Append("\n inner join EditionClients ec on c.ClientId = ec.ClientId ");
      sb.Append("\n inner join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
      sb.Append("\n inner join EditionClientProducts ecp on ec.ClientId = ecp.ClientId ");
      sb.Append("\n                                     and ec.EditionId = ecp.EditionId ");
      sb.Append("\n inner join ParticipantProducts pp on ecp.ClientId = pp.ClientId ");
      sb.Append("\n                                  and ecp.EditionId = pp.EditionId ");
      sb.Append("\n                                  and ecp.ProductId = pp.ProductId ");
      sb.Append("\n inner join Products p on pp.ProductId = p.ProductId ");
      sb.Append("\n inner join ClientProducts cp on ec.ClientId = cp.ClientId ");
      sb.Append("\n                             and p.ProductId = cp.ProductId ");
      sb.Append("\n inner join ClientProductLeafCategories cplc on cp.ClientId = cplc.ClientId ");
      sb.Append("\n                             and cp.ProductId = cplc.ProductId   ");
      sb.Append("\n inner join LeafHomogeneousCategories lhc on cplc.LeafCategoryId = lhc.LeafCategoryId ");
      sb.Append("\n                             and cplc.CategoryThreeId = lhc.CategoryThreeId ");
      sb.Append("\n inner join LeafCategories lc on lhc.LeafCategoryId = lc.LeafCategoryId ");
      sb.Append("\n inner join CategoryThree cat on lhc.CategoryThreeId = cat.CategoryThreeId ");
      sb.Append("\n where ec.EditionId = " + editionId);
      sb.Append("\n and ec.ClientTypeId = 2  ");
      sb.Append("\n order by 1  "); 

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getCatN3(string letter, int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n Select distinct cat.CategoryThreeId,cat.CategoryThree  ");
      sb.Append("\n from Clients c ");
      sb.Append("\n inner join EditionClients ec on c.ClientId = ec.ClientId ");
      sb.Append("\n inner join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
      sb.Append("\n inner join EditionClientProducts ecp on ec.ClientId = ecp.ClientId ");
      sb.Append("\n                                     and ec.EditionId = ecp.EditionId ");
      sb.Append("\n inner join ParticipantProducts pp on ecp.ClientId = pp.ClientId ");
      sb.Append("\n                                  and ecp.EditionId = pp.EditionId ");
      sb.Append("\n                                  and ecp.ProductId = pp.ProductId ");
      sb.Append("\n inner join Products p on pp.ProductId = p.ProductId ");
      sb.Append("\n inner join ClientProducts cp on ec.ClientId = cp.ClientId ");
      sb.Append("\n                             and p.ProductId = cp.ProductId ");
      sb.Append("\n inner join ClientProductLeafCategories cplc on cp.ClientId = cplc.ClientId ");
      sb.Append("\n                             and cp.ProductId = cplc.ProductId   ");
      sb.Append("\n inner join LeafHomogeneousCategories lhc on cplc.LeafCategoryId = lhc.LeafCategoryId ");
      sb.Append("\n                             and cplc.CategoryThreeId = lhc.CategoryThreeId ");
      sb.Append("\n inner join LeafCategories lc on lhc.LeafCategoryId = lc.LeafCategoryId ");
      sb.Append("\n inner join CategoryThree cat on lhc.CategoryThreeId = cat.CategoryThreeId ");
      sb.Append("\n where ec.EditionId = " + editionId + " and cat.CategoryThree COLLATE SQL_Latin1_General_CP1_CI_AI like ('" + letter + "%') ");
      sb.Append("\n and ec.ClientTypeId = 2  ");
      sb.Append("\n order by 2  ");

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getCatN4(int cat3Id, int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n Select distinct lc.LeafCategoryId,lc.LeafCategory   ");
      sb.Append("\n from Clients c ");
      sb.Append("\n inner join EditionClients ec on c.ClientId = ec.ClientId ");
      sb.Append("\n inner join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
      sb.Append("\n inner join EditionClientProducts ecp on ec.ClientId = ecp.ClientId ");
      sb.Append("\n                                     and ec.EditionId = ecp.EditionId ");
      sb.Append("\n inner join ParticipantProducts pp on ecp.ClientId = pp.ClientId ");
      sb.Append("\n                                  and ecp.EditionId = pp.EditionId ");
      sb.Append("\n                                  and ecp.ProductId = pp.ProductId ");
      sb.Append("\n inner join Products p on pp.ProductId = p.ProductId ");
      sb.Append("\n inner join ClientProducts cp on ec.ClientId = cp.ClientId ");
      sb.Append("\n                             and p.ProductId = cp.ProductId ");
      sb.Append("\n inner join ClientProductLeafCategories cplc on cp.ClientId = cplc.ClientId ");
      sb.Append("\n                             and cp.ProductId = cplc.ProductId   ");
      sb.Append("\n inner join LeafHomogeneousCategories lhc on cplc.LeafCategoryId = lhc.LeafCategoryId ");
      sb.Append("\n                             and cplc.CategoryThreeId = lhc.CategoryThreeId ");
      sb.Append("\n inner join LeafCategories lc on lhc.LeafCategoryId = lc.LeafCategoryId ");
      sb.Append("\n inner join CategoryThree cat on lhc.CategoryThreeId = cat.CategoryThreeId ");
      sb.Append("\n where ec.EditionId = " + editionId + " and lhc.CategoryThreeId = " + cat3Id );
      sb.Append("\n and ec.ClientTypeId = 2  ");
      sb.Append("\n order by 2  ");

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }


  public DataTable getClientC(int clientCId, int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n Select distinct c.ClientId,c.CompanyName, ec.Page , cplc.LeafCategoryId  ");
      sb.Append("\n from Clients c ");
      sb.Append("\n inner join EditionClients ec on c.ClientId = ec.ClientId ");
      sb.Append("\n inner join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
      sb.Append("\n inner join EditionClientProducts ecp on ec.ClientId = ecp.ClientId ");
      sb.Append("\n                                     and ec.EditionId = ecp.EditionId ");
      sb.Append("\n inner join ParticipantProducts pp on ecp.ClientId = pp.ClientId ");
      sb.Append("\n                                  and ecp.EditionId = pp.EditionId ");
      sb.Append("\n                                  and ecp.ProductId = pp.ProductId ");
      sb.Append("\n inner join Products p on pp.ProductId = p.ProductId ");
      sb.Append("\n inner join ClientProducts cp on ec.ClientId = cp.ClientId ");
      sb.Append("\n                             and p.ProductId = cp.ProductId ");
      sb.Append("\n inner join ClientProductLeafCategories cplc on cp.ClientId = cplc.ClientId ");
      sb.Append("\n                             and cp.ProductId = cplc.ProductId   ");
      sb.Append("\n inner join LeafHomogeneousCategories lhc on cplc.LeafCategoryId = lhc.LeafCategoryId ");
      sb.Append("\n                             and cplc.CategoryThreeId = lhc.CategoryThreeId ");
      sb.Append("\n inner join LeafCategories lc on lhc.LeafCategoryId = lc.LeafCategoryId ");
      sb.Append("\n inner join CategoryThree cat on lhc.CategoryThreeId = cat.CategoryThreeId ");
      sb.Append("\n where ec.EditionId = " + editionId + " and cplc.LeafCategoryId = " + clientCId);
      sb.Append("\n and ec.ClientTypeId = 2  ");
      sb.Append("\n order by 2  ");

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getProductoC(int clientCId, int editionId, int catN4Id)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n Select distinct p.ProductId,p.ProductName, ebcp.Page  ");
      sb.Append("\n from Clients c ");
      sb.Append("\n inner join EditionClients ec on c.ClientId = ec.ClientId ");
      sb.Append("\n inner join ClientTypes ct on ec.ClientTypeId = ct.ClientTypeId ");
      sb.Append("\n inner join EditionClientProducts ecp on ec.ClientId = ecp.ClientId ");
      sb.Append("\n                                     and ec.EditionId = ecp.EditionId ");
      sb.Append("\n inner join ParticipantProducts pp on ecp.ClientId = pp.ClientId ");
      sb.Append("\n                                  and ecp.EditionId = pp.EditionId ");
      sb.Append("\n                                  and ecp.ProductId = pp.ProductId ");
      sb.Append("\n left join EditionBookClientProducts ebcp on ecp.ProductId = ebcp.ProductId ");
	  sb.Append("\n                                  		and ecp.ClientId = ebcp.ClientId ");
      sb.Append("\n                                  		and ecp.EditionId = ebcp.EditionId  ");
      sb.Append("\n inner join Products p on pp.ProductId = p.ProductId ");
      sb.Append("\n inner join ClientProducts cp on ec.ClientId = cp.ClientId ");
      sb.Append("\n                             and p.ProductId = cp.ProductId ");
      sb.Append("\n inner join ClientProductLeafCategories cplc on cp.ClientId = cplc.ClientId ");
      sb.Append("\n                             and cp.ProductId = cplc.ProductId   ");
      sb.Append("\n inner join LeafHomogeneousCategories lhc on cplc.LeafCategoryId = lhc.LeafCategoryId ");
      sb.Append("\n                             and cplc.CategoryThreeId = lhc.CategoryThreeId ");
      sb.Append("\n inner join LeafCategories lc on lhc.LeafCategoryId = lc.LeafCategoryId ");
      sb.Append("\n inner join CategoryThree cat on lhc.CategoryThreeId = cat.CategoryThreeId ");
      sb.Append("\n where ec.EditionId = " + editionId + " and cplc.LeafCategoryId = " + catN4Id + " and ecp.ClientId= " + clientCId );
      sb.Append("\n and ec.ClientTypeId = 2  ");
      sb.Append("\n order by 2  ");

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }


  ////////////////////// Clientes Anunciantes


  public DataTable getLetterAnunNew(int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n select distinct SUBSTRING(c.CompanyName COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) LetterCN ");
      sb.Append("\n from EditionClients ec ");
      sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
      sb.Append("\n where ec.ClientTypeId = 2 and ec.EditionId =  " + editionId);
      sb.Append("\n order by 1"); 
   
      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getCompaniesAnunN(string let, int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n select distinct c.ClientId, c.CompanyName, ec.Page ");
      sb.Append("\n from EditionClients ec ");
      sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
      sb.Append("\n where ec.ClientTypeId = 2 and EditionId = " + editionId + " and c.CompanyName COLLATE SQL_Latin1_General_CP1_CI_AI like ('" + let + "%') ");
      sb.Append("\n order by c.CompanyName "); 

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

  public DataTable getCompaniesAnunDir(int clientId, int editionId)
  {
      StringBuilder sb = new StringBuilder();

      sb.Append("\n select distinct c.ClientId, c.CompanyName, c.ShortName, c.Description, ec.ClientTypeId, ec.Page,a.Address, a.ZipCode, a.City, a.Email, a.Web,a.Suburb ");
      sb.Append("\n , coun.CountryId ,coun.CountryName,s.StateId, ");
       sb.Append("\n s.StateName, dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Teléfono(s)') Tel, ");
       sb.Append("\n dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Teléfono / Fax') TelFax, ");
       sb.Append("\n dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Fax') Fax , ");
       sb.Append("\n dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Lada sin costo') Lada , ");
       sb.Append("\n dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Oxidom') Oxidom , ");
       sb.Append("\n dbo.plm_dfGetPhonesByClientByAddress(ca.AddressId, c.ClientId, 'Celular') Celular ");
      sb.Append("\n from EditionClients ec ");
      sb.Append("\n inner join Clients c on ec.ClientId = c.ClientId ");
      sb.Append("\n left join ClientAddresses ca on c.ClientId = ca.ClientId ");
      sb.Append("\n left join Addresses a on ca.AddressId = a.AddressId ");
      sb.Append("\n left join Countries coun on a.CountryId = coun.CountryId ");
      sb.Append("\n left join States s on a.StateId = s.StateId ");
      sb.Append("\n where ec.ClientTypeId = 2 and ec.EditionId =  " + editionId + "  and ec.ClientId =  " + clientId );
      sb.Append("\n order by c.CompanyName ");
 

      DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

      return ds.Tables[0];
  }

    #endregion


    public static readonly Catalogs Instance = new Catalogs();

}
