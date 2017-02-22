using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using VetBusinessEntries;

namespace VetBusinessLogicComponenet
{
    public sealed class ProductsBLC
    {
        #region Constructors

        private ProductsBLC() { }

        #endregion



        #region Public Methods

            #region Alphabetic

               public List<ProductByEditionInfo> getDrugs(string code, int editionId, string searchText)
                {
                  if (editionId <= 0)
                  throw new ArgumentException("The edition does not exist");
               else
                 {
                 PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                  if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                 {

                    List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getProductsByText(editionId, searchText);


                    return productList;
                }

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

              } 

            }

          #endregion

            #region Result

               public SearchResultsInfo getResults(string code, int editionId, string searchText)
               {
                   PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                   if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   {
                       SearchResultsInfo record = new SearchResultsInfo();

                       //Product Results

                       List<ProductByEditionInfo> productList = new List<ProductByEditionInfo>();
                       productList = getDrugs(code, editionId, searchText);
                       if (productList.Count > 0)
                           record.Products = productList;

                       //Substance Results

                       List<ActiveSubstanceInfo> substanceList = new List<ActiveSubstanceInfo>();
                       substanceList = SubstancesBLC.Instance.getSubstances(code,editionId,searchText);
                           if(substanceList.Count > 0)
                               record.Substances= substanceList;

                       //Therapeutic Results

                       List<TherapeuticInfo>therapeuticList = new List<TherapeuticInfo>();
                       therapeuticList = TherapeuticBLC.Instance.getTherapeutics(code, editionId, searchText);
                       if (therapeuticList.Count > 0)
                           record.Therapeutics = therapeuticList;

                       //Laboratory Results

                       List<DivisionInfo> divisionList = new List<DivisionInfo>();
                       divisionList = LaboratoriesBLC.Instance.getLaboratories(code,editionId,searchText);
                           if(divisionList.Count > 0)
                               record.Labs = divisionList;

                       //Specie Results

                       List<SpecieInfo> specieList = new List<SpecieInfo>();
                       specieList = SpeciesBLC.Instance.getSpecies(code, editionId, searchText);
                           if(specieList.Count > 0 )
                               record.Species= specieList;


                        return record;

                   }

                   else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");


               }

               public ProductDetailInfo getAttributesByProduct(string code, int editionId, int divisionId, int categoryId, int productId, int pharmaFormId,string applicationKey)
               {

                   PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                   if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                   {
                       ProductDetailInfo product = VetDataAccessComponent.ProductsDALC.Instance.getProductAttributes(editionId, divisionId, categoryId, productId, pharmaFormId);


                       if (product != null)
                       {
                           product.Attributes = VetDataAccessComponent.AttributesDALC.Instance.getCompleteAttributes(editionId, divisionId, categoryId, productId, pharmaFormId);
                           product.ActiveSubstances = VetDataAccessComponent.ActiveSubstancesDALC.Instance.getProductBySubstances(editionId, productId);
                           product.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +  product.BaseUrl;

                       }
                       return product;
                   }
                   else
                       throw new ApplicationException("El código no es válido o se encuentra inactivo.");
               }





            #endregion


              public List<ProductByEditionInfo> getDrugsBySubstance(string code, int editionId, int activeSubstanceId)
               {
                   if (editionId <= 0 || activeSubstanceId <= 0)
                       throw new ArgumentException("The book edition or substance does not exist");

                   else
                   {
                       PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                       if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                       {

                           List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getByActiveSubstance(editionId, activeSubstanceId);


                           return productList;
                       }


                       else
                           throw new ApplicationException("El código no es válido o se encuentra inactivo.");

                   }


               }


              public List<ProductByEditionInfo> getDrugsByTherapeutic(string code, int editionId, int therapeuticId)
              {
                  if (editionId <= 0 || therapeuticId <= 0)
                      throw new ArgumentException("The book edition or therapeutic does not exist");

                  else
                  {
                      PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                      if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                      {

                          List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getByTherapeutic(editionId,therapeuticId);


                          return productList;
                      }


                      else
                          throw new ApplicationException("El código no es válido o se encuentra inactivo.");

                  }


              }


              public List<ProductByEditionInfo> getDrugsByLab(string code, int editionId, int divisionId)
              {
                  if (editionId <= 0 || divisionId <= 0)
                      throw new ArgumentException("Th book edition or laboratory does not exist");

                  else
                  {
                      PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                      if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                      {

                          List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getByDivision(editionId,divisionId);


                          return productList;
                      }


                      else
                          throw new ApplicationException("El código no es válido o se encuentra inactivo.");

                  }


              }

              public List<ProductByEditionInfo> getDrugsBySpecie(string code, int editionId, int specieId)
              {
                  if (editionId <= 0 || specieId <= 0)
                      throw new ArgumentException("The country, book edition or substance does not exist");

                  else
                  {
                      PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                      if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                      {

                          List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getBySpecie(editionId, specieId);


                          return productList;
                      }


                      else
                          throw new ApplicationException("El código no es válido o se encuentra inactivo.");

                  }


              }

              public List<ProductByEditionInfo> getDrugsByLabByCategory(string code, int editionId, int divisionId, int categoryId, VetBusinessEntries.Types.ProductTypes productTypeId)
              {


                  if (editionId <= 0 || categoryId <= 0)
                      throw new ArgumentException("The  book edition or category does not exist");

                  else
                  {
                      PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
                      if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                      {

                          List<ProductByEditionInfo> productList = VetDataAccessComponent.ProductsDALC.Instance.getByDivisionByCategory(editionId, divisionId, categoryId, (byte)productTypeId);


                          return productList;
                      }


                      else
                          throw new ApplicationException("El código no es válido o se encuentra inactivo.");

                  }

              }






           
            





        #endregion
               
        #region Private Methods


               #endregion

               public static readonly ProductsBLC Instance = new ProductsBLC();


    }




}
