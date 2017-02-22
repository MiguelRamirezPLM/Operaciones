using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using PLMClientsBusinessLogicComponent;
using PLMClientsBusinessEntities;

namespace DEAQBusinessLogicComponent
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
      #endregion
      public static readonly CatalogsBLC Instance = new CatalogsBLC();

  }
}
