using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using PLMClientsBusinessLogicComponent;
using PLMClientsBusinessEntities;

namespace AgroBusinessLogicComponent
{
  public class CatalogsBLC
  {

      #region Constructor

      private CatalogsBLC() { }

      #endregion

      #region Public Methods

      //Retrieves Information From Books
      public DEAQBusinessEntries.BookInfo rocGetBook(string code, int bookId)
      {

          if (bookId <= 0)
              throw new ArgumentException("The book does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetBook(bookId);                 

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      
      }

      //Retrieves Information From Categories
      public CategoryInfo rocGetCategory(string code, int categoryId)
      {
          if (categoryId <= 0)
              throw new ArgumentException("The category does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCategory(categoryId);

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      }

      //Retrieves Information From Countries By ID
      public DEAQBusinessEntries.CountryInfo rocGetCountry(string code, string country)
      {
          PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
          validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
          {
              return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountry(country);
              
          }
          else
          {
              throw new ApplicationException("The code is not valid or is inactive.");
          }
      
      }

      //Retrieves Information From Editions
      public DEAQBusinessEntries.EditionInfo rocGetEdition(string code, int countryId, int bookId)
      {

          if (countryId <= 0 || bookId <= 0)

              throw new ArgumentException("The country or book does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetEdition(countryId, bookId);

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      }

      //Retrieves Information From Laboratories
      public LaboratoryInfo rocGetLaboratory(string code, int laboratoryId)
      {
          if (laboratoryId <= 0)

              throw new ArgumentException("The laboratory does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetLaboratory(laboratoryId);

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      
      }

      //Retrieves Information From PharmaForms
      public PharmaFormInfo rocGetPharmaForm(string code, int pharmaFormId)
      {

          if (pharmaFormId <= 0)

              throw new ArgumentException("The pharmaform does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetPharmaForm(pharmaFormId);

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      }

      //Retrieves Information From PharmaForms By EditionId
      public List<PharmaFormInfo> rocGetPharmaFormsByProductId(string code, int editionId, int productId)
      {
          if (editionId <= 0 || productId <= 0)

              throw new ArgumentException("The edition or product does not exist");
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetPharmaFormsByProductId(editionId, productId);

              else
                  throw new ApplicationException("The code is not valid or is inactive.");
          }
      
      }

      //Retrieves information all Advertisements by Edition and Division
      public List<DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo> rocGetAdvertisementsByDivision(string code, int divisionId, int editionId)
      {
          if (divisionId < 1 || editionId < 1)
          {
              throw new ArgumentException("The Division or Edition does not exist.");
          }
          else
          {
              PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
              {

                  List<DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo> advs = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetAdvertisementsByDivision(divisionId, editionId);


                  if (advs != null)
                  {
                      foreach (DEAQBusinessEntries.ROC.AdvertisementByDivisionInfo adv in advs)
                      {
                          if (!string.IsNullOrEmpty(adv.AdFile))
                          {
                              PLMClientsBusinessEntities.BookInfo bookinfo = PLMClientsBusinessLogicComponent.BooksBLC.Instance.getBookByCode(code);

                              CountryByEditionInfo countryInfo = DEAQDataAccessComponent.CatalogsDALC.Instance.rocGetCountryByEdition(adv.EditionId);

                              adv.AdFile = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + countryInfo.CountryName + "/" + bookinfo.ShortName  +"/Anuncios/" + adv.AdFile;
                          }
                      }
 
                  }
                  return advs;                 
              }
              else
              {
                  throw new ApplicationException("The code is not valid or is inactive.");
              }
          }
      }

      #endregion

      #region PLM methods
      public List<AgroBusinessEntries.CategoriesInfo> getCategoriesByISBN(string code,string isbn, int divisionId) {

          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
          {
              return DEAQDataAccessComponent.CatalogsDALC.Instance.GetCategoriesByISBN(isbn, divisionId);

             

          }
          else
          {
              throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }
      }

      public List<AgroBusinessEntries.CategoriesInfo> getCategoriesByDivision(string code, int divisionId)
      {
          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
          {
              return DEAQDataAccessComponent.CatalogsDALC.Instance.GetCategoriesByDivision(divisionId);
          }
          else
          {
              throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }
          
      }
    
      public AgroBusinessEntries.CategoriesInfo getCategoryById(string code, int categoryId)
      {
          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
          {
              return DEAQDataAccessComponent.CatalogsDALC.Instance.GetCategoriesById(categoryId);
          }
          else
          {
              throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }

      }
      public AgroBusinessEntries.DivisionInfo getDivisionById(string code, int divisionId)
      {
          PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

          if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
          {
              return DEAQDataAccessComponent.CatalogsDALC.Instance.GetDivisionById(divisionId);
          }
          else
          {
              throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }

      }
      #region Countries

      public List<CountriesInfo> getCountries()
      {
          return AgroDataAccessComponent.CountriesDALC.Instance.getAll();
      }
      public List<CountriesInfo> getCountries(string countries)
      {
          return AgroDataAccessComponent.CountriesDALC.Instance.getCountries(countries);
      }
      public CountriesInfo getCountry(int countryId)
      {
          if (countryId <= 0)
              throw new ArgumentException("The country does not exist.");

          return AgroDataAccessComponent.CountriesDALC.Instance.getOne(countryId);
      }

    

      //public List<AgroBusinessEntries.CountryCodeInfo> getCountryCodes(int countryId)
      //{
      //    if (countryId <= 0)
      //        throw new ArgumentException("The country does not exist.");

      //    return AgroDataAccessComponent.CountriesDALC.Instance.getCountryCodes(countryId);
      //}

      public BooksInfo getBook(int bookId)
      {
          return AgroDataAccessComponent.BooksDALC.Instance.getOne(bookId);
      }

      public List<BooksInfo> getBooksByCountry(int countryId)
      {
          if (countryId <= 0)
              throw new ArgumentException("The country does not exist.");

          return AgroDataAccessComponent.BooksDALC.Instance.getAllByCountry(countryId);
      }

      public List<EditionsInfo> getEditionsByBook(int bookId, int countryId)
      {
          if (bookId <= 0 || countryId <= 0)
              throw new ArgumentException("The book or country does not exist.");

          return AgroDataAccessComponent.EditionsDALC.Instance.getAllByBook(bookId, countryId);
      }
      public List<BooksInfo> getEncyclopediaBooksByCountry(int countryId)
      {
          if (countryId <= 0)
              throw new ArgumentException("The country does not exist.");

          return AgroDataAccessComponent.BooksDALC.Instance.getEncyclopediaBooksByCountry(countryId);
      }

      public List<EditionsInfo> getEncyclopediaEditionsByBook(int bookId, int countryId)
      {
          if (bookId <= 0 || countryId <= 0)
              throw new ArgumentException("The book or country does not exist.");

          return AgroDataAccessComponent.EditionsDALC.Instance.getEncyclopediaEditionsByBook(bookId, countryId);
      }


      public EditionsInfo getEdition(int editionId)
      {
          if (editionId <= 0)
              throw new ArgumentException("The edition does not exist.");

          return AgroDataAccessComponent.EditionsDALC.Instance.getOne(editionId);
      }

      public EditionsInfo getEditionByISBN(string isbn)
      {
          return AgroDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn);
      }

      public List<LaboratoriesInfo> getLaboratories()
      {
          return AgroDataAccessComponent.LaboratoriesDALC.Instance.getAll();
      }

      public List<DivisionsInfo> getDivisionByCountryLab(int countryId, int laboratoryId)
      {
          if (countryId <= 0 || laboratoryId <= 0)
              throw new ArgumentException("The country or laboratory does not exist.");

          List<DivisionsInfo> collection = AgroDataAccessComponent.DivisionsDALC.Instance.getAllByCountryLab(countryId, laboratoryId);

          DivisionsInfo record = new DivisionsInfo();

          record.DivisionId = -1;
          record.ShortName = "SELECCIONAR";

          collection.Add(record);

          return collection;
      }


      //public List<AssignedProduct> getNoAssigned(int editionId)
      //{
      //    if (editionId <= 0)
      //        throw new ArgumentException("The edition does not exist.");

      //    return AgroDataAccessComponent.ProductsDALC.Instance.getNoAssignedProducts(editionId, (byte)CatalogsBLC.TypeInEdition.Participante);
      //}

      public List<DivisionsInfo> getDivisionsByCountry(int countryId)
      {
          if (countryId <= 0)
              throw new ArgumentException("The country does not exist.");

          return AgroDataAccessComponent.DivisionsDALC.Instance.getAllByCountry(countryId);
      }

      public DivisionsInfo getDivision(int divisionId)
      {
          if (divisionId <= 0)
              throw new ArgumentException("The division does not exist.");

          return AgroDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId);
      }
      public PharmaceuticalFormInfo getPharmaForm(int pharmaFormId)
      {
          if (pharmaFormId <= 0)
              throw new ArgumentException("The pharmaceutical form does not exist.");

          return AgroDataAccessComponent.PharmaceuticalFormsDALC.Instance.getOne(pharmaFormId);
      }
      public List<PharmaceuticalFormInfo> getPharmaFormsWithoutProduct(int productId)
      {
          if (productId <= 0)
              throw new ArgumentException("The product does not exist.");

          return AgroDataAccessComponent.PharmaceuticalFormsDALC.Instance.getAllWithoutProduct(productId);
      }
      public List<PharmaceuticalFormInfo> getPharmaForms(string description)
      {
          return AgroDataAccessComponent.PharmaceuticalFormsDALC.Instance.getAllByFilter(description);
      }
      public List<AssignedProduct> getNoAssigned(int editionId)
      {
          if (editionId <= 0)
              throw new ArgumentException("The edition does not exist.");

          return AgroDataAccessComponent.ProductsDALC.Instance.getNoAssignedProducts(editionId, (byte)CatalogsBLC.TypeInEdition.Participante);
      }


      #endregion
      #endregion

      public enum TypeInEdition : byte
      {
          Participante = 1,
          Mencionado = 2
      }
      public static readonly CatalogsBLC Instance = new CatalogsBLC();

  }
}
