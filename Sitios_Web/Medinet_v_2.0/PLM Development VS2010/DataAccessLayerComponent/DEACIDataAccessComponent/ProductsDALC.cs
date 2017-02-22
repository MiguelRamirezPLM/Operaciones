using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEACIBusinessEntries;

namespace DEACIDataAccessComponent
{
    public class ProductsDALC
    {

        #region Constructors

        private ProductsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves all Products By Section
        public List<DEACIBusinessEntries.ROC.ProductBySectionInfo> rocGetIndexProductsBySection(int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetIndexProductsBySection(sectionId)
                          select new DEACIBusinessEntries.ROC.ProductBySectionInfo
                          {
                              ProductId = productRow.ProductId,
                              ProductName = productRow.ProductName,
                              Description = productRow.Description,
                              CompanyId = productRow.CompanyId,
                              CompanyShortName = productRow.CompanyShortName
                          };

            List<DEACIBusinessEntries.ROC.ProductBySectionInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;

        }

        

        //Retrieves all Brands By IndexId
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrands(int page, int numberByPage, byte indexId, int editionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetBrands(page, numberByPage, indexId, editionId)
                          select new DEACIBusinessEntries.ROC.BrandInfo
                          {
                             
                              BrandId = productRow.BrandId,
                              BrandName = productRow.BrandName,
                              RowNumber = (int)productRow.RowNumber,
                              Total = (int)productRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.BrandInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;
        }

        //Retrieves all Products By ProductType and FullText
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByFulltext(int page, int numberByPage, int editionId, byte indexId, string fullText)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetBrandsByFulltext(page, numberByPage, indexId, editionId, fullText)
                          select new DEACIBusinessEntries.ROC.BrandInfo
                          {
                              BrandId = productRow.BrandId,
                              BrandName = productRow.BrandName,
                              RowNumber = (int)productRow.RowNumber,
                              Total = (int)productRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.BrandInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;

        }

        //Retrieves all Brands By IndexId and Letter
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByLetter(int page, int numberByPage, int editionId, byte indexId, string letter)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetBrandsByLetter(page, numberByPage, indexId, editionId, letter) 
                          select new DEACIBusinessEntries.ROC.BrandInfo
                          {
                                                            
                              BrandId = productRow.BrandId,
                              BrandName = productRow.BrandName,
                              RowNumber = (int)productRow.RowNumber,
                              Total = (int)productRow.TOTAL
                          };
         
            List<DEACIBusinessEntries.ROC.BrandInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;
        }

        //Retrieves all Products By ProductType and Text
        public List<DEACIBusinessEntries.ROC.BrandInfo> rocGetBrandsByText(int page, int numberByPage, int editionId, byte indexId, string text)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetBrandsByText(page, numberByPage, indexId, editionId, text)
                          select new DEACIBusinessEntries.ROC.BrandInfo
                          {
                              BrandId = productRow.BrandId,
                              BrandName = productRow.BrandName,
                              RowNumber = (int)productRow.RowNumber,
                              Total = (int)productRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.BrandInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;

        }

        //Retrieves all Products By ProductTypeId, EditionId, and ProductId
        public DEACIBusinessEntries.ROC.BrandByCompanyInfo rocGetBrand(int brandId, int editionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var brand = from brandRow in db.roc_spGetBrand(brandId, editionId)
                        select new DEACIBusinessEntries.ROC.BrandByCompanyInfo
                        {
                            BrandId = brandRow.BrandId,
                            BrandName = brandRow.BrandName
                
                        };

            List<DEACIBusinessEntries.ROC.BrandByCompanyInfo> brandInfo = brand.ToList();

            return brandInfo.Count() > 0 ? brandInfo[0] : null;

        }

        //Rerieves all Brands By BrandId and EditionId
        public DEACIBusinessEntries.ROC.BrandByCompanyInfo rocGetBrandByCompanyId(int editionId, int companyId, int brandId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var brandCompany = from brandCompanyRow in db.roc_spGetBrandByCompanyId(editionId, companyId, brandId)
                               select new DEACIBusinessEntries.ROC.BrandByCompanyInfo
                               {
                                   BrandId = brandCompanyRow.BrandId,
                                   BrandName = brandCompanyRow.BrandName
                               };

            List<DEACIBusinessEntries.ROC.BrandByCompanyInfo> brandComInfo = brandCompany.ToList();

            return brandComInfo.Count() > 0 ? brandComInfo[0] : null;
        }

        //Retrieves information about and Product
        public DEACIBusinessEntries.ROC.ProductByEditionInfo rocGetProduct(int editionId, int productId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetProduct(editionId, productId)
                          select new DEACIBusinessEntries.ROC.ProductByEditionInfo
                          {
                              ProductId = productRow.ProductId,
                              ProductIdParent = productRow.ProductIdParent,
                              ProductTypeId = (byte)productRow.ProductTypeId,
                              ProductName = productRow.ProductName,
                              Description = productRow.Description,
                              CompanyId = productRow.CompanyId,
                              ProdId = productRow.ProdId,
                              Active = productRow.Active,
                              HtmlFile = productRow.HTMLFile,
                              SectionIdParent = productRow.SectionIdParent
                          };

            List<DEACIBusinessEntries.ROC.ProductByEditionInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation[0] : null;
        }

        //Retrieves all Products By Section
        public List<DEACIBusinessEntries.ROC.ProductByEditionInfo> rocGetProductBySectionId(int editionId, int sectionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetProductBySectionId(editionId, sectionId)
                          select new DEACIBusinessEntries.ROC.ProductByEditionInfo
                          {
                              ProductId = productRow.ProductId,
                              ProductIdParent = productRow.ProductIdParent,
                              ProductTypeId = (byte)productRow.ProductTypeId,
                              ProductName = productRow.ProductName,
                              Description = productRow.Description,
                              CompanyId = productRow.CompanyId,
                              ProdId = productRow.ProdId,
                              Active = productRow.Active,
                              HtmlFile = productRow.HTMLFile,
                              SectionIdParent = productRow.SectionIdParent
                          };

            List<DEACIBusinessEntries.ROC.ProductByEditionInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;

        }

        //Retrieves all Products By Section and ProductType
        public List<DEACIBusinessEntries.ROC.ProductByEditionInfo> rocGetProducts(int editionId, int sectionId, int productTypeId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from productRow in db.roc_spGetProducts(editionId, sectionId, productTypeId)
                          select new DEACIBusinessEntries.ROC.ProductByEditionInfo
                          {
                              ProductId = productRow.ProductId,
                              ProductIdParent = productRow.ProductIdParent,
                              ProductTypeId = (byte)productRow.ProductTypeId,
                              ProductName = productRow.ProductName,
                              Description = productRow.Description,
                              CompanyId = productRow.CompanyId,
                              ProdId = productRow.ProdId,
                              Active = productRow.Active,
                              HtmlFile = productRow.HtmlFile,
                              SectionIdParent = productRow.ProductIdParent
                          };

            List<DEACIBusinessEntries.ROC.ProductByEditionInfo> productInformation = product.ToList();

            return productInformation.Count() > 0 ? productInformation : null;

        }

        #region ComercialNameProduts

        //Retrieves all Products By Comercial Name
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProducts(int editionId, int countryId, int page, int numberByPage)
        {

            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from prodRow in db.roc_spGetComercialNameProducts(editionId, countryId, page, numberByPage)
                          select new DEACIBusinessEntries.ROC.ProductByProductTypeInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              ProductTypeId = (byte)prodRow.ProductTypeId,
                              Description = prodRow.Description,
                              CompanyId = prodRow.CompanyId,
                              CompanyShortName = prodRow.CompanyShortName,
                              RowNumber = (int)prodRow.RowNumber,
                              Total = (int)prodRow.TOTAL
                          };
            List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> productComercial = product.ToList();

            return productComercial.Count() > 0 ? productComercial : null;
        
        }

        //Retrieves all Products Comercial Name by Letter
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByLetter(int page, int numberByPage, int editionId, int countryId, string letter)
        {

            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from prodRow in db.roc_spGetComercialNameProductsByLetter(page, numberByPage, editionId, countryId, letter)
                          select new DEACIBusinessEntries.ROC.ProductByProductTypeInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              ProductTypeId = (byte)prodRow.ProductTypeId,
                              Description = prodRow.Description,
                              CompanyId = prodRow.CompanyId,
                              CompanyShortName = prodRow.CompanyShortName,
                              RowNumber = (int)prodRow.RowNumber,
                              Total = (int)prodRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> productComercial = product.ToList();

            return productComercial.Count() > 0 ? productComercial : null;

        }
      
        //Retrieves all Products Comercial Name by Text
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByText(int page, int numberByPage, int editionId, int countryId, string text)
        {

            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from prodRow in db.roc_spGetComercialNameProductsByText(page, numberByPage, editionId, countryId, text)
                          select new DEACIBusinessEntries.ROC.ProductByProductTypeInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              ProductTypeId = (byte)prodRow.ProductTypeId,
                              Description = prodRow.Description,
                              CompanyId = prodRow.CompanyId,
                              CompanyShortName = prodRow.CompanyShortName,
                              RowNumber = (int)prodRow.RowNumber,
                              Total = (int)prodRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> productComercial = product.ToList();

            return productComercial.Count() > 0 ? productComercial : null;

        }

        //Retrives all Products Comercial Name by Fulltext
        public List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> rocGetComercialNameProductsByFullText(int page, int numberByPage, int editionId, int countryId, string text)
        {

            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var product = from prodRow in db.roc_spGetComercialNameProductsByFulltext(page, numberByPage, editionId, countryId, text)
                          select new DEACIBusinessEntries.ROC.ProductByProductTypeInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              ProductTypeId = (byte)prodRow.ProductTypeId,
                              Description = prodRow.Description,
                              CompanyId = prodRow.CompanyId,
                              CompanyShortName = prodRow.CompanyShortName,
                              RowNumber = (int)prodRow.RowNumber,
                              Total = (int)prodRow.TOTAL
                          };

            List<DEACIBusinessEntries.ROC.ProductByProductTypeInfo> productComercial = product.ToList();

            return productComercial.Count() > 0 ? productComercial : null;
        }

        #endregion

        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();

    }
}
