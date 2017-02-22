using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiaBusinessLogicComponent
{
    public class CatalogMethods
    {
        #region Constructors

        private CatalogMethods() { }

        #endregion

        #region Public Methods

        #region Countries

        public GuiaBusinessEntries.CountryInfo getCountry(string code, byte countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The countryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return GuiaDataAccessComponent.Queries.Instance.getCountryById(countryId);
                }

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        #endregion

        #region States

        public GuiaBusinessEntries.StateInfo getState(string code, int stateId)
        {
            if (stateId <= 0)
                throw new ArgumentException("The stateId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    return GuiaDataAccessComponent.Queries.Instance.getStateById(stateId);
                }

                else
                    throw new ApplicationException("The code is invalid or inactive");

            }
            
        }

        #endregion

        #region Editions

        public GuiaBusinessEntries.EditionInfo getEdition(string code)
        {

            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                PLMClientsBusinessEntities.EditionInfo edition = PLMClientsBusinessLogicComponent.CatalogsBLC.Instance.getEdition(code);

                if (edition != null)
                {
                    return GuiaDataAccessComponent.Queries.Instance.getEditionByISBN(edition.ISBN);
                }
                else
                    throw new ApplicationException("El código no tiene asociada ninguna edición.");
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }

            
        }

        #endregion

        #region Clients

        public int getNumberOfClients(string code, int editionId, byte clientTypeId)
        {
            if (editionId <= 0 || clientTypeId<=0)
                throw new ArgumentException("The clientTypeId or editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return GuiaDataAccessComponent.Queries.Instance.getNumberOfClientsByEditionByType(editionId, clientTypeId);
                }

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.ClientInfo getClient(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return GuiaDataAccessComponent.Queries.Instance.getClientById(clientId);
                }

                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientTypeInfo> getClientTypes(string code)
        {
            
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientTypes();
                else
                    throw new ApplicationException("The code is invalid or inactive");
            
            
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByType(string code, int editionId, byte clientTypeId)
        {
            if (editionId <= 0 || clientTypeId <=0)
                throw new ArgumentException("The editionId or clientTypeId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByEditionByType(editionId, clientTypeId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByLetter(string code, int editionId, byte clientTypeId, string letter)
        {
            if (editionId <= 0 || clientTypeId <= 0)
                throw new ArgumentException("The editionId or clientTypeId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByName(editionId, clientTypeId, letter);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByTextByType(string code, int editionId, byte clientTypeId, string text)
        {
            if (editionId <= 0 || clientTypeId <= 0)
                throw new ArgumentException("The editionId or clientTypeId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByTextByType(editionId, clientTypeId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByText(string code, int editionId, string text)
        {
            if (editionId <= 0 )
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }

        }

        public List<GuiaBusinessEntries.BrandDetailInfo> getBrandsByClient(string code, int editionId, int clientId)
        {
            if (editionId <= 0 || clientId <= 0)
                throw new ArgumentException("The editionId or clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandsByClient(editionId, clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.DrugDetailInfo> getDrugs(string code, int editionId, string text)
        {
            if (editionId <= 0 )
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getDrugs(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.PhoneTypeInfo> getPhoneTypes(string code)
        {
            
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getPhoneTypes();
                else
                    throw new ApplicationException("The code is invalid or inactive");
            
            
        }

        public List<GuiaBusinessEntries.PhonesByClientInfo> getPhonesByClient(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getPhonesByClient(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        #endregion

        #region Drugs

        public List<GuiaBusinessEntries.DrugInfo> getDrugsByClientId(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getDrugsByClientId(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        #endregion

        #region ROC

        
        public List<GuiaBusinessEntries.ProductsByClientInfo> getProductsByClient(string code, int clientId, int? top)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByClient(clientId, top);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductsByEditionInfo> getProductsByText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductsByEditionInfo> getProductsByTextByType(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }

        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandsByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.SpecialityInfo> getSpecialities(string code)
        {
           
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getSpecialities();
                else
                    throw new ApplicationException("The code is invalid or inactive");
           
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpeciality(string code, int editionId, int specialityId)
        {
            if (editionId <= 0||specialityId<=0)
                throw new ArgumentException("The editionId or specialityId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpeciality(editionId, specialityId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByStateByText(string code, int editionId, int specialityId, int stateId, string text)
        {
            if (editionId <= 0 || specialityId <= 0||stateId<=0)
                throw new ArgumentException("The editionId or specialityId or stateId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByStateByText(editionId, specialityId, stateId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByState(string code, int editionId, int specialityId, int stateId)
        {
            if (editionId <= 0 || specialityId <= 0 || stateId <= 0)
                throw new ArgumentException("The editionId or specialityId or stateId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByState(editionId, specialityId, stateId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByText(string code, int editionId, int specialityId, string text)
        {
            if (editionId <= 0 || specialityId <= 0 )
                throw new ArgumentException("The editionId or specialityId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByText(editionId, specialityId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.StateInfo> getStatesByCountry(string code, byte countryId)
        {
            if (countryId <= 0 )
                throw new ArgumentException("The countryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getStatesByCountry(countryId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrands(string code, int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrands(editionId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByLetter(string code, int editionId,  string letter)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandsByLetter(editionId, letter);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByEditionByText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandsByEditionByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductInfo> getProducts(string code, int editionId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProducts(editionId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductInfo> getProductsByParent(string code, int editionId,int parentId)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProducts(editionId, parentId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }

        }

        public List<GuiaBusinessEntries.ProductInfo> getProductsByLetter(string code, int editionId,  string letter)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByLetter(editionId, letter);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductInfo> getProductsByEditionByText(string code, int editionId,  string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByEditionByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientByBrandInfo> getClientsByBrand(string code, int editionId, int brandId, int? top)
        {
            if (editionId <= 0 || brandId<=0)
                throw new ArgumentException("The editionId or brandId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByBrand(editionId, brandId, top);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.SubProductByProductInfo> getSubProductByProduct(string code, int editionId, int productId)
        {
            if (editionId <= 0 || productId <= 0)
                throw new ArgumentException("The editionId or productId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getSubProductsByProduct(editionId, productId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientByProductInfo> getClientsWithoutSubProduct(string code, int editionId, int productId)
        {
            if (editionId <= 0 || productId <= 0)
                throw new ArgumentException("The editionId or productId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsWithoutSubProduct(editionId, productId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.SpecialityInfo getSpeciality(string code ,int specialityId)
        {
            if (specialityId <= 0 )
                throw new ArgumentException("The specialityId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getSpecialityById(specialityId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.ProductInfo getProduct(string code, int editionId, int productId)
        {
            if (editionId <= 0 || productId <= 0)
                throw new ArgumentException("The editionId or productId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductById(editionId, productId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.BrandsByEditionInfo getBrand(string code, int editionId, int brandId)
        {
            if (editionId <= 0 || brandId <= 0)
                throw new ArgumentException("The editionId or brandId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandById(editionId, brandId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.AdvertisementInfo> getAdvertisementsByClient(string code,int clientId,int editionId)
        {
            if (editionId <= 0 || clientId <= 0)
                throw new ArgumentException("The editionId or clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getAdvertByClient(clientId, editionId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsByFullText(string code, int editionId, byte clientTypeId, string text)
        {
            if (editionId <= 0 || clientTypeId <= 0)
                throw new ArgumentException("The editionId or clientTypeId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsByFullText(editionId, clientTypeId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientSpecialityInfo> getClientsAndSpecialityByText(string code, int editionId, string text)
        {
            if (editionId <= 0 )
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsAndSpecialityByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientSpecialityInfo> getClientsAndSpecialityByFullText(string code, int editionId, string fullText)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsAndSpecialityByFullText(editionId, fullText);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ProductsByEditionInfo> getProductsByFullText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getProductsByFullText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByFullText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getBrandsByFullText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByStateByFullText(string code, int editionId, int specialityId, int stateId, string text)
        {
            if (editionId <= 0||specialityId<=0||stateId<=0)
                throw new ArgumentException("The editionId or specialityId or stateId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByStateByFullText(editionId, specialityId, stateId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByFullText(string code, int editionId, int specialityId, string text)
        {
            if (editionId <= 0 || specialityId <= 0)
                throw new ArgumentException("The editionId or specialityId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByFullText(editionId, specialityId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
            
        }

        #endregion 

        #region International

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternational(string code, int editionId)
        {
            if (editionId <= 0 )
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternational(editionId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByLetter(string code, int editionId, string letter)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalByLetter(editionId, letter);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalByText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByFullText(string code, int editionId, string text)
        {
            if (editionId <= 0)
                throw new ArgumentException("The editionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalByFullText(editionId, text);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.IntAddressByClientInfo getAddressByInternationalId(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getAddressByInternationalId(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntPhonesByClientInfo> getPhonesByInternationalId(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getPhonesByInternationalId(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalCategories(string code)
        {
            
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalCategories();
                else
                    throw new ApplicationException("The code is invalid or inactive");
            
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalCategoriesByParentId(string code, int parentId)
        {
            if (parentId <= 0)
                throw new ArgumentException("The parentId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalCategoriesByParentId(parentId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.CategoryInfo getInternationalCategoriesByClientId(string code, int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalCategoriesByClientId(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntProductsByClientInfo> getInternationalProductsByClientAndCategory(string code, int clientId, int categoryId)
        {
            if (clientId <= 0||categoryId <=0)
                throw new ArgumentException("The clientId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalProductsByClientAndCategory(clientId, categoryId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalSubcategoriesByClientId(string code, int clientId)
        {
            if (clientId <= 0 )
                throw new ArgumentException("The clientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalSubcategoriesByClientId(clientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.IntProductsByClientInfo> getInternationalProductsByParentId(string code, int clientId, int categoryId, int productParentId)
        {
            if (clientId <= 0||categoryId <=0 || productParentId<=0)
                throw new ArgumentException("The clientId or categoryId or productParentId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getInternationalProductsByParentId(clientId, categoryId, productParentId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public List<GuiaBusinessEntries.CategoryInfo> getCategoryById(string code, int categoryId)
        {
            if ( categoryId <= 0)
                throw new ArgumentException("The categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getCategoryById(categoryId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        public GuiaBusinessEntries.IntClientInfo getInternationalById(string code, int intClientId)
        {
            if (intClientId <= 0)
                throw new ArgumentException("The intClientId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return GuiaDataAccessComponent.Queries.Instance.getIntClientById(intClientId);
                else
                    throw new ApplicationException("The code is invalid or inactive");
            }
            
        }

        #endregion

        #region Search

        public GuiaBusinessEntries.SearchResultsInfo getResults(string code,int editionId,string searchText) {
            GuiaBusinessEntries.SearchResultsInfo results = new GuiaBusinessEntries.SearchResultsInfo();
            results.Brands = getBrandsByText(code, editionId, searchText);
            results.Clients = getClientsByText(code, editionId, searchText);
            results.Products = getProductsByText(code, editionId, searchText);

            return results;
        
        }

        #endregion


        #endregion

        public static readonly CatalogMethods Instance = new CatalogMethods();
    }
}
