using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIADataAccessComponent
{
    public class ProductsDALC
    {

        #region Constructor

        private ProductsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves information from a product
        public DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo rocGetProduct(int productId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetProduct(productId, editionId)
                             select new DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo
                             {
                                 ProductId = productRow.ProductId,
                                 ParentId = productRow.ParentId,
                                 ProductTypeId = productRow.ProductTypeId,
                                 CompanyId = productRow.CompanyId,
                                 ProductName = productRow.ProductName,
                                 ProductDescription = productRow.ProductDescription,
                                 EditionId = productRow.EditionId,
                                 SectionId = productRow.SectionId,
                                 HtmlFile = productRow.HtmlFile,
                                 HtmlContent = productRow.HtmlContent,
                                 EPSDescription = productRow.EPSDescription
                             };
            List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> productInfo = products.ToList();

            return productInfo.Count() > 0 ? productInfo[0] : null;

        }

        //Retrieves information from all products by SectionId and EditionId.
        public List<DEIABusinessEntries.ROC.ProductBySectionInfo> rocGetProductsBySection(int sectionId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetProductsBySection(sectionId, editionId)
                             select new DEIABusinessEntries.ROC.ProductBySectionInfo
                             {
                                 ProductId = productRow.ProductId,
                                 ParentId = productRow.ParentId,
                                 ProductTypeId = productRow.ProductTypeId,
                                 ProductName = productRow.ProductName,
                                 ProductDescription = productRow.Description,
                                 CompanyId = productRow.CompanyId,
                                 CompanyName = productRow.CompanyName,
                                 HtmlFile = productRow.HtmlFile,
                                 HtmlContent = productRow.HtmlContent
                             };

            List<DEIABusinessEntries.ROC.ProductBySectionInfo> productsInfo = products.ToList();

            return productsInfo.Count() > 0 ? productsInfo : null;

        }

        //Retrieves information from all products by SectionId and ProductType
        public List<DEIABusinessEntries.ROC.ProductBySectionInfo> rocGetProductsBySectionAndType(int productTypeId, int sectionId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetProductsBySectionAndType(productTypeId, sectionId, editionId)
                             select new DEIABusinessEntries.ROC.ProductBySectionInfo
                             {
                                 ProductId = productRow.ProductId,
                                 ParentId = productRow.ParentId,
                                 ProductTypeId = productRow.ProductTypeId,
                                 ProductName = productRow.ProductName,
                                 ProductDescription = productRow.Description,
                                 CompanyId = productRow.CompanyId,
                                 CompanyName = productRow.CompanyName,
                                 HtmlFile = productRow.HtmlFile,
                                 HtmlContent = productRow.HtmlContent
                             };

            List<DEIABusinessEntries.ROC.ProductBySectionInfo> productsInfo = products.ToList();

            return productsInfo.Count() > 0 ? productsInfo : null;

        }

        //Retrieves information from all Products by CompanyId
        public List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> rocGetProductsByCompanyId(int companyId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetProductsByCompanyId(companyId, editionId)
                           select new DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo
                           {
                               ProductId = productRow.ProductId,
                               ParentId = productRow.ParentId,
                               ProductTypeId = productRow.ProductTypeId,
                               CompanyId = productRow.CompanyId,
                               ProductName = productRow.ProductName,
                               ProductDescription = productRow.ProductDescription,
                               EditionId = productRow.EditionId,
                               SectionId = productRow.SectionId,
                               HtmlFile = productRow.HtmlFile,
                               HtmlContent = productRow.HtmlContent,
                               EPSDescription = productRow.EPSDescription
                           };

            List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> productsInfo = products.ToList();

            return productsInfo.Count() > 0 ? productsInfo : null;

        }

        //Retrieves Product information by Edition
        public DEIABusinessEntries.ROC.ProductByEditionInfo rocGetEditionProduct(int productId, int editionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetEditionProduct(productId, editionId)
                           select new DEIABusinessEntries.ROC.ProductByEditionInfo
                           {
                               ProductId = productRow.ProductId,
                               ProductName = productRow.ProductName,
                               Description = productRow.Description,
                               SectionId = productRow.SectionId,
                               HtmlFile = productRow.HtmlFile,
                               HtmlContent = productRow.HtmlContent
                           };

            List<DEIABusinessEntries.ROC.ProductByEditionInfo> productInfo = products.ToList();

            return productInfo.Count() > 0 ? productInfo[0] : null;

        }

        //Retrieves Product information by Edition and Section
        public List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> rocGetIndexProductsBySection(int editionId, int sectionId)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var products = from productRow in db.roc_spGetIndexProductsBySection(editionId, sectionId)
                           select new DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo
                           {
                               ProductId = productRow.ProductId,
                               ParentId = productRow.ParentId,
                               ProductTypeId = productRow.ProductTypeId,
                               CompanyId = productRow.CompanyId,
                               ProductName = productRow.ProductName,
                               ProductDescription = productRow.ProductDescription,
                               EditionId = productRow.EditionId,
                               SectionId = productRow.SectionId,
                               HtmlFile = productRow.HtmlFile,
                               HtmlContent = productRow.HtmlContent,
                               EPSDescription = productRow.EPSDescription
                           };

            List<DEIABusinessEntries.ROC.ProductsByEditionBySectionInfo> productsInfo = products.ToList();

            return productsInfo.Count() > 0 ? productsInfo : null;

        }

        #region ProductsIndex

        //Retrieves information from all Products Index
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndex(byte indexId, int editionId, int page, int numberbypage)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var productsIndex = from productRow in db.roc_spGetProductsIndex(indexId, editionId, page, numberbypage)
                            select new DEIABusinessEntries.ROC.ProductIndexInfo
                            {
                                EdProdId = productRow.EdProdId,
                                ProductId = productRow.ProductId,
                                ProductTypeId = productRow.ProductTypeId,
                                ProductName = productRow.ProductName,
                                Description = productRow.Description,
                                CompanyId = productRow.CompanyId,
                                HtmlFile = productRow.HtmlFile,
                                HtmlContent = productRow.HtmlContent,
                                RowNumber = (int)productRow.RowNumber,
                                Total = (int)productRow.TOTAL
                            };

            List<DEIABusinessEntries.ROC.ProductIndexInfo> productsIndexInfo = productsIndex.ToList();

            return productsIndexInfo.Count() > 0 ? productsIndexInfo : null;

        }

        //Retrieves information from all Products Index by Letter
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByLetter(byte indexId, int edition, int page, int numberbypage, string letter)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var productsIndex = from productRow in db.roc_spGetProductsIndexByLetter(indexId, edition, page, numberbypage, letter)
                            select new DEIABusinessEntries.ROC.ProductIndexInfo
                            {
                                EdProdId = productRow.EdProdId,
                                ProductId = productRow.ProductId,
                                ProductTypeId = productRow.ProductTypeId,
                                ProductName = productRow.ProductName,
                                Description = productRow.Description,
                                CompanyId = productRow.CompanyId,
                                HtmlFile = productRow.HtmlFile,
                                HtmlContent = productRow.HtmlContent,
                                RowNumber = (int)productRow.RowNumber,
                                Total = (int)productRow.TOTAL
                            };
            List<DEIABusinessEntries.ROC.ProductIndexInfo> productsIndexInfo = productsIndex.ToList();

            return productsIndexInfo.Count() > 0 ? productsIndexInfo : null;

        }

        //Retrieves information from all Products Index by Text
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByText(byte indexId, int editionId, int page, int numberbypage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var productsIndex = from productRow in db.roc_spGetProductsIndexByText(indexId, editionId, page, numberbypage, text)
                            select new DEIABusinessEntries.ROC.ProductIndexInfo
                            {
                                EdProdId = productRow.EdProdId,
                                ProductId = productRow.ProductId,
                                ProductTypeId = productRow.ProductTypeId,
                                ProductName = productRow.ProductName,
                                Description = productRow.Description,
                                CompanyId = productRow.CompanyId,
                                HtmlFile = productRow.HtmlFile,
                                HtmlContent = productRow.HtmlContent,
                                RowNumber = (int)productRow.RowNumber,
                                Total = (int)productRow.TOTAL
                            };

            List<DEIABusinessEntries.ROC.ProductIndexInfo> productsIndexInfo = productsIndex.ToList();

            return productsIndexInfo.Count() > 0 ? productsIndexInfo : null;

        }

        //Retrieves information from all Products Index by Full Text
        public List<DEIABusinessEntries.ROC.ProductIndexInfo> rocGetProductsIndexByFullText(byte indexId, int editionId, int page, int numberbypage, string text)
        {
            DEIADataClassesDataContext db = new DEIADataClassesDataContext();

            var productsIndex = from productRow in db.roc_spGetProductsIndexByFullText(indexId, editionId, page, numberbypage, text)
                            select new DEIABusinessEntries.ROC.ProductIndexInfo
                            {
                                EdProdId = productRow.EdProdId,
                                ProductId = productRow.ProductId,
                                ProductTypeId = productRow.ProductTypeId,
                                ProductName = productRow.ProductName,
                                Description = productRow.Description,
                                CompanyId = productRow.CompanyId,
                                HtmlFile = productRow.HtmlFile,
                                HtmlContent = productRow.HtmlContent,
                                RowNumber = (int)productRow.RowNumber,
                                Total = (int)productRow.TOTAL
                            };

            List<DEIABusinessEntries.ROC.ProductIndexInfo> productsIndexInfo = productsIndex.ToList();

            return productsIndexInfo.Count() > 0 ? productsIndexInfo : null;

        }

        #endregion

        #endregion

        public static readonly ProductsDALC Instance = new ProductsDALC();

    }
}
