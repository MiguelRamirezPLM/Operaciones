using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEACIBusinessLogicComponent
{
    public class ProductsBLC
    {

        #region Constructors

        private ProductsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves all Products By Section
        public List<DEACIBusinessEntries.ROC.ProductBySectionInfo> rocGetIndexProductsBySection(string code, int sectionId)
        {
            if (sectionId < 1)
            {
                throw new ArgumentException("The Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetIndexProductsBySection(sectionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By ProductType
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrands(string code, int page, int numberByPage, byte indexId, int editionId)
        {
            if (page < 0 || numberByPage < 0 || indexId <= 0 || editionId < 1)
            {
                throw new ArgumentException("The Page or IndexId or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrands(page, numberByPage, indexId, editionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By ProductType and FullText
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByFulltext(string code, int page, int numberByPage, int editionId, byte indexId, string fullText)
        {
            if (page < 0 || numberByPage < 0 || indexId <= 0  || editionId < 1)
            {
                throw new ArgumentException("The Page or IndexId or Edition does not exist.");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrandsByFulltext(page, numberByPage, editionId, indexId, fullText);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By ProductType and Letter
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByLetter(string code, int page, int numberByPage, int editionId, byte indexId, string letter)
        {
            if (page < 0 || numberByPage < 0 || indexId <= 0 || editionId < 1)
            {
                throw new ArgumentException("The Page or IndexId or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrandsByLetter(page, numberByPage, editionId, indexId, letter);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By ProductType and Text
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByText(string code, int page, int numberByPage, int editionId, byte indexId, string text)
        {
            if (page < 0 || numberByPage < 0 || indexId <= 0 || editionId < 1)
            {
                throw new ArgumentException("The Page or IndexId or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrandsByText(page, numberByPage, editionId, indexId, text);
                }
                else 
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

         //Retrieves all Products By ProductTypeId, EditionId, and ProductId
        public DEACIBusinessEntries.ROC.BrandByCompanyInfo rocGetBrand(string code, int brandId, int editionId)
        {
            if (brandId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The brand or edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DEACIBusinessEntries.ROC.BrandByCompanyInfo product = DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrand(brandId, editionId);

                   

                    return product;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        //Retrieves all Brand By CompanyId
        public DEACIBusinessEntries.ROC.BrandByCompanyInfo rocGetBrandByCompanyId(string code, int editioId, int companyId, int brandId)
        { 
        if (editioId <= 0 || companyId <= 0 || brandId <= 0)
        {
            throw new ArgumentException("The brand, edition or company does not exist.");
        }
        else
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                DEACIBusinessEntries.ROC.BrandByCompanyInfo product = DEACIDataAccessComponent.ProductsDALC.Instance.rocGetBrandByCompanyId(editioId, companyId, brandId);

                return product;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        }


        //Retrieves information about an Product
        public DEACIBusinessEntries.ROC.ProductByEditionInfo rocGetProduct(string code, int editionId, int productId)
        {
            if (editionId < 1 || productId < 1)
            {
                throw new ArgumentException("The Product or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DEACIBusinessEntries.ROC.ProductByEditionInfo product = DEACIDataAccessComponent.ProductsDALC.Instance.rocGetProduct(editionId, productId);

                    if (product.HtmlFile != null)
                    {
                        product.HtmlFile = product.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                            PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "img/");
                    }

                    return product;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By Section
        public List<DEACIBusinessEntries.ROC.ProductByEditionInfo> rocGetProductBySectionId(string code, int editionId, int sectionId)
        {
            if (editionId < 1 || sectionId < 1)
            {
                throw new ArgumentException("The Section or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ProductByEditionInfo> products = DEACIDataAccessComponent.ProductsDALC.Instance.rocGetProductBySectionId(editionId, sectionId);

                    if (products != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ProductByEditionInfo product in products)
                        {
                            if (product.HtmlFile != null)
                            {
                                product.HtmlFile = product.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "img/");
                            }
                        }
                    }

                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products By Section and ProductType
        public List<DEACIBusinessEntries.ROC.ProductByEditionInfo> rocGetProducts(string code, int editionId, int sectionId, int productTypeId)
        {
            if (editionId < 1 || sectionId < 1 || productTypeId < 1)
            {
                throw new ArgumentException("The Section or Edition or ProductType does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.ProductByEditionInfo> products = DEACIDataAccessComponent.ProductsDALC.Instance.rocGetProducts(editionId, sectionId, productTypeId);

                    if (products != null)
                    {
                        foreach (DEACIBusinessEntries.ROC.ProductByEditionInfo product in products)
                        {
                            if (product.HtmlFile != null)
                            {
                                product.HtmlFile = product.HtmlFile.Replace("src=\"imagenes/", "src=\"" +
                                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "img/");
                            }
                        }
                    }

                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        #region ComercialNameProduts

        //Retrieves all Products By Comercial Name
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProducts( string code, int editionId, int countryId, int page, int numberByPage)
        {
            if (editionId <= 0 || countryId <= 0 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Country or Page or ProductType  does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetComercialNameProducts(editionId, countryId, page, numberByPage);
                        
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        //Retrieves all Products Comercial Name by Letter
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByLetter(string code, int page, int numberByPage, int editionId, int countryId, string letter)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || countryId <= 0)
            {
                throw new ArgumentException("The Page or Edition or Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetComercialNameProductsByLetter(page, numberByPage, editionId, countryId, letter);
                       
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Products Comercial Name by Text
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByText(string code, int page, int numberByPage, int editionId, int countryId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || countryId <= 0)
            {
                throw new ArgumentException("The Page or Edition or Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetComercialNameProductsByText(page, numberByPage, editionId, countryId, text);
                    

                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrives all Products Comercial Name by Fulltext
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByFullText(string code, int page, int numberByPage, int editionId, int countryId, string text)
        {
            if (page < 0 || numberByPage < 0 || editionId <= 0 || countryId <= 0)
            {
                throw new ArgumentException("The Page or Edition or Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.ProductsDALC.Instance.rocGetComercialNameProductsByFullText(page, numberByPage, editionId, countryId, text);
                        

                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }


        #endregion

        #endregion

        public static readonly ProductsBLC Instance = new ProductsBLC();

    }
}
