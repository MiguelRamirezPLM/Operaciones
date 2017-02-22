using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIABusinessLogicComponent
{
    public class ProductsBLC
    {

        #region Constructor

        private ProductsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves information from a product
        public DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo rocGetProduct(string code, int productId, int editionId)
        {
            if (productId < 1 || editionId < 1)
            {
                throw new ArgumentException("The Product or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo product = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProduct(productId, editionId);

                    if (product.HtmlContent != null)
                    {
                        product.HtmlContent = product.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                            PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                    }
                    return product;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves information from all products by SectionId and EditionId.
        public List<DEIABusinessEntries.ROC.ProductBySectionInfo> rocGetProductsBySection(string code, int sectionId, int editionId)
        {
            if (sectionId < 1 || editionId < 1)
            {
                throw new ArgumentException("The Section or the Edition does not exist.");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductBySectionInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsBySection(sectionId, editionId);

                    foreach (DEIABusinessEntries.ROC.ProductBySectionInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
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

        //Retrieves information from all products by SectionId and ProductType
        public List<DEIABusinessEntries.ROC.ProductBySectionInfo> rocGetProductsBySectionAndType(string code, int productTypeId, int sectionId, int editionId)
        {
            if (productTypeId < 1 || sectionId < 1 || editionId < 1)
            {
                throw new ArgumentException("The ProductType or Section or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductBySectionInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsBySectionAndType(productTypeId, sectionId, editionId);

                    foreach (DEIABusinessEntries.ROC.ProductBySectionInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all products by CompanyId 
        public List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> rocGetProductsByCompanyId(string code, int companyId, int editionId)
        {
            if (companyId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The Company or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsByCompanyId(companyId, editionId);

                    foreach (DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves Product information by Edition
        public DEIABusinessEntries.ROC.ProductByEditionInfo rocGetEditionProduct(string code, int productId, int editionId)
        {
            if (productId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The Product or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    DEIABusinessEntries.ROC.ProductByEditionInfo product = DEIADataAccessComponent.ProductsDALC.Instance.rocGetEditionProduct(productId, editionId);

                    if (product.HtmlContent != null)
                    {
                        product.HtmlContent = product.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                    }
                    return product;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves Product information by Edition and Section
        public List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> rocGetIndexProductsBySection(string code, int editionId, int sectionId)
        {
            if (editionId <= 0 || sectionId <= 0)
            {
                throw new ArgumentException("The Edition or Section does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetIndexProductsBySection(editionId, sectionId);

                    foreach (DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #region ProductsIndex

        //Retrieves information from all products index  
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndex(string code, byte indexId, int editionId, int page, int numberbypage)
        {
            if (indexId <= 0 || editionId <= 0 || page < 0 || numberbypage < 0)
            {
                throw new ArgumentException("The Index, page or numberbypage does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductIndexInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsIndex(indexId, editionId, page, numberbypage);

                    foreach (DEIABusinessEntries.ROC.ProductIndexInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Products Index by Letter
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByLetter(string code, byte indexId, int editionId, int page, int numberbypage, string letter)
        {
            if (indexId <= 0 || editionId <= 0 || page < 0 || numberbypage < 0)
            {
                throw new ArgumentException("The Index, page or numberbypage does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductIndexInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsIndexByLetter(indexId, editionId, page, numberbypage, letter);

                    foreach (DEIABusinessEntries.ROC.ProductIndexInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Products Index by Text
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByText(string code, byte indexId, int editionId, int page, int numberbypage, string text)
        {
            if (indexId <= 0 || editionId <= 0 || page < 0 || numberbypage < 0)
            {
                throw new ArgumentException("The Index, page or numberbypage does not exist.");
            }
            else
            { 
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductIndexInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsIndexByText(indexId, editionId, page, numberbypage, text);

                    foreach (DEIABusinessEntries.ROC.ProductIndexInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        //Retrieves information from all Products Index by Full Text
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByFullText(string code, byte indexId, int editionId, int page, int numberbypage, string text)
        {
            if (indexId <= 0 || editionId <= 0 || page < 0 || numberbypage < 0)
            {
                throw new ArgumentException("The Index, page or numberbypage does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.ROC.ProductIndexInfo> products = DEIADataAccessComponent.ProductsDALC.Instance.rocGetProductsIndexByFullText(indexId, editionId, page, numberbypage, text);

                    foreach (DEIABusinessEntries.ROC.ProductIndexInfo productItem in products)
                    {
                        if (productItem.HtmlContent != null)
                        {
                            productItem.HtmlContent = productItem.HtmlContent.Replace("src=\"imagenes/", "src=\"" +
                                PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "imagenes/");
                        }
                    }
                    return products;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo");
                }
            }
        }

        #endregion

        #endregion

        public static readonly ProductsBLC Instance = new ProductsBLC();

    }
}
