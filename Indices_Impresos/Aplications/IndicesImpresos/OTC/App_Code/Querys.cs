using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Querys
/// </summary>
public class Querys
{
    #region Constructors
    
    private Querys()
	{
    }

    #endregion

    #region Public Methods

    #region Índice General

    public List<ProductByEdition> getProducts(int editionId, string letter)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var products = (from rows1 in db.plm_vwProductsByEdition
                       where rows1.EditionId == editionId && rows1.TypeInEdition.Equals("P") && rows1.Brand.StartsWith(letter)
                       orderby rows1.Brand
                       select new ProductByEdition(){
                           Brand = rows1.Brand,
                           Substances = db.plm_dfGetActiveSubsByProduct(rows1.ProductId),
                           Description = rows1.ProductDescription,
                           PharmaForm = db.plm_dfGetPharmaFormsByProductName(rows1.EditionId, rows1.Brand),
                           Division = rows1.DivisionShortName,
                           Pages = db.plm_dfGetPagesByProduct(rows1.ProductId,editionId, rows1.DivisionId)
                       }).Distinct();

        

        List<ProductByEdition> prods = products.ToList();

        return prods;
    }

    public List<string> getAlphabet(int editionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var letter = (from rows1 in db.plm_vwProductsByEdition
                     where rows1.EditionId == editionId
                     select rows1.Brand.Substring(0, 1)).Distinct();


        List<string> letters = letter.ToList();

        return letters;

    }

    #endregion

    #region Índice de Síntomas

    public List<string> getAlphabetSymtoms(int editionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var letter = (from rows1 in db.plm_vwProductsByEdition
                      join rows2 in db.ProductSymptoms on rows1.ProductId equals rows2.ProductId
                      join rows3 in db.Symptoms on rows2.SymptomId equals rows3.SymptomId
                      where rows1.EditionId == editionId  
                      select rows3.SymptomName.Substring(0, 1)).Distinct();


        List<string> letters = letter.ToList();

        return letters;
    }

    public List<SymptomByEdition> getSymptoms(int editionId, string letter)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var symps = (from rows1 in db.EditionSymptoms
                     join rows2 in db.Symptoms on rows1.SymptomId equals rows2.SymptomId
                     where rows1.EditionId == editionId && rows2.SymptomName.StartsWith(letter)
                     orderby rows2.SymptomName
                     select new SymptomByEdition
                     {
                         Symptom = rows2.SymptomName,
                         Page = rows1.Page,
                         SymptomId = rows2.SymptomId.ToString()
                     }).Distinct();


        List<SymptomByEdition> symptoms = symps.ToList();

        return symptoms;
    }

    public List<ProductByEdition> getProductsBySymptom(int editionId, int symptomId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var products = (from rows1 in db.plm_vwProductsByEdition
                        join rows2 in db.ProductSymptoms on rows1.ProductId equals rows2.ProductId
                        where rows1.EditionId == editionId && rows1.TypeInEdition.Equals("P") && rows2.SymptomId == symptomId
                        orderby rows1.Brand
                        select new ProductByEdition()
                        {
                            Brand = rows1.Brand,//db.plm_dfProductsByPage(editionId, rows1.Page),
                            Substances = "",
                            Description = "",
                            PharmaForm = "",
                            Division = "",
                            Pages = rows1.Page 
                         }).Distinct();

        List<ProductByEdition> prods = products.ToList();

        return prods;
    }

    #endregion

    #region Índice de Sustancias

    public List<string> getAlphabetSubstances(int editionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var letter = (from rows1 in db.plm_vwProductsByEdition
                      join rows2 in db.ProductSubstances on rows1.ProductId equals rows2.ProductId
                      join rows3 in db.ActiveSubstances on rows2.ActiveSubstanceId equals rows3.ActiveSubstanceId
                      where rows1.EditionId == editionId
                      select rows3.Description.Substring(0, 1)).Distinct();


        List<string> letters = letter.ToList();

        return letters;
    }

    public List<SubstanceByEdition> getSubstances(int editionId, string letter)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var subs = (from rows1 in db.plm_vwProductsByEdition
                     join rows2 in db.ProductSubstances on rows1.ProductId equals rows2.ProductId
                     join rows3 in db.ActiveSubstances on rows2.ActiveSubstanceId equals rows3.ActiveSubstanceId
                     where rows1.EditionId == editionId && rows3.Description.StartsWith(letter)
                     orderby rows3.Description
                     select new SubstanceByEdition
                     {
                         Substance = rows3.Description,
                         SubstanceId = rows3.ActiveSubstanceId.ToString()
                     }).Distinct();


        List<SubstanceByEdition> substances = subs.ToList();

        return substances; 


    }

    public List<ProductByEdition> getAloneProductsBySubstance(int editionId, int substanceId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var products = (from rows1 in db.plm_vwProductsByEdition
                        join rows2 in db.ProductSubstances on rows1.ProductId equals rows2.ProductId
                        where rows1.EditionId == editionId && rows1.TypeInEdition.Equals("P") && rows2.ActiveSubstanceId == substanceId &&
                        rows1.NumberOfActiveSubstances == 1
                        orderby rows1.Brand
                        select new ProductByEdition()
                        {
                            Brand = rows1.Brand,
                            Substances = "",
                            Description = "",
                            PharmaForm = db.plm_dfGetPharmaFormsByProductName(rows1.ProductId, rows1.Brand),
                            Division = rows1.DivisionShortName,
                            Pages = db.plm_dfGetPagesByProduct(rows1.ProductId,editionId, rows1.DivisionId)
                        }).Distinct();

        List<ProductByEdition> prods = products.ToList();

        return prods;
    }

    public List<ProductByEdition> getCombinedProductsBySubstance(int editionId, int substanceId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var products = (from rows1 in db.plm_vwProductsByEdition
                        join rows2 in db.ProductSubstances on rows1.ProductId equals rows2.ProductId
                        where rows1.EditionId == editionId && rows1.TypeInEdition.Equals("P") && rows2.ActiveSubstanceId == substanceId &&
                        rows1.NumberOfActiveSubstances > 1
                        orderby rows1.Brand
                        select new ProductByEdition()
                        {
                            Brand = rows1.Brand,
                            Substances = db.plm_dfGetActiveSubstancesByProductBySubstance(rows1.ProductId, substanceId),
                            Description = "",
                            PharmaForm = db.plm_dfGetPharmaFormsByProductName(rows1.ProductId, rows1.Brand),
                            Division = rows1.DivisionShortName,
                            Pages = db.plm_dfGetPagesByProduct(rows1.ProductId, editionId, rows1.DivisionId)
                        }).Distinct();

        List<ProductByEdition> prods = products.ToList();

        return prods;
    }


    #endregion

    #region Índice de Anuncios

    public List<Divisions> getDivisionAds(int editionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var divs = (from rows1 in db.EditionDivisionAds
                    join rows2 in db.Divisions on rows1.DivisionId equals rows2.DivisionId
                    where rows1.EditionId == editionId
                    orderby rows2.Description
                    select rows2).Distinct();

        List<Divisions> divisions = divs.ToList();

        return divisions;

    }

    public List<AdsByEdition> getAdvertisements(int editionId, int divisionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var ads = (from rows1 in db.EditionDivisionAds
                   join rows2 in db.Advertisements on rows1.AdId equals rows2.AdId
                   where rows1.EditionId == editionId && rows1.DivisionId == divisionId
                   orderby rows2.AdName
                   select new AdsByEdition
                   {
                       AdName= rows2.AdName,
                       Page = rows1.Page
                   }).Distinct();

        List<AdsByEdition> advertisements = ads.ToList();

        return advertisements;

    }

    #endregion

    #region Índice de Laboratorios

    public List<Divisions> getLaboratories(int editionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var labs = (from rows1 in db.plm_vwProductsByEdition
                    join rows2 in db.Divisions on rows1.DivisionId equals rows2.DivisionId
                    where rows1.EditionId == editionId
                    orderby rows2.Description
                    select rows2).Distinct();

        List<Divisions> laboratories = labs.ToList();

        return laboratories;
    
    }

    public DivisionInformation getLabInformation(int divisionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var divInfo = from rows1 in db.DivisionInformation
                      where rows1.DivisionId == divisionId
                      select rows1;

        DivisionInformation divisionInfo = (DivisionInformation)divInfo;

        return divisionInfo;

    }

    public List<ProductByEdition> getProductsByLab(int editionId, int divisionId)
    {
        OTCDataClassesDataContext db = new OTCDataClassesDataContext();

        var prods = (from rows1 in db.plm_vwProductsByEdition
                     where rows1.EditionId == editionId && rows1.DivisionId == divisionId
                     orderby rows1.Brand
                     select new ProductByEdition
                     {
                         Brand = rows1.Brand,
                         Description = rows1.ProductDescription,
                         PharmaForm = db.plm_dfGetPharmaFormsByProductName(rows1.ProductId, rows1.Brand),
                         Pages = db.plm_dfGetPagesByProduct(rows1.ProductId, editionId, rows1.DivisionId)
                     }).Distinct();


        List<ProductByEdition> products = prods.ToList();

        return products;
    }


    #endregion

    #endregion

    public static readonly Querys Instance = new Querys();

}
