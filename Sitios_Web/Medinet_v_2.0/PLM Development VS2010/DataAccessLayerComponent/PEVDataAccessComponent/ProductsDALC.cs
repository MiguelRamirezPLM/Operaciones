using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries.ROC;

namespace PEVDataAccessComponent
{
    public class ProductsDALC
    {
        #region Constructors

        private ProductsDALC() { }

        #endregion

        #region New Products

        //Retrieves All New Products By ProductId

        public PEVBusinessEntries.ROC.ProductInfo rocGetNewProduct(int editionId, byte countryId, int productId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productRow = from prodRow in db.roc_spGetNewProduct(productId, editionId, countryId)
                             select new ProductInfo
                             {
                                 ProductId = prodRow.ProductId,
                                 ProductName = prodRow.ProductName,
                                 Participant = prodRow.Participant.ToString(),
                                 DivisionId = prodRow.DivisionId,
                                 DivisionName = prodRow.Divisionname,
                                 DivisionShortName = prodRow.ShortName,
                                 //Description = prodRow.des
                                 RowNumber = (int)prodRow.RowNumber
                             };

            List<ProductInfo> newProducts = productRow.ToList();

            return newProducts.Count() > 0 ? newProducts[0] : null; 
        }

        //Retrieves All New Products By Page 
        public List<ProductInfo> rocGetNewProducts(int editionId, byte countryId, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productNewRow = from prodNewRow in db.roc_spGetNewProducts(editionId, countryId, page, numberByPage)
                                select new ProductInfo
                                {
                                    ProductId = prodNewRow.ProductId,
                                    ProductName = prodNewRow.ProductName,
                                    DivisionId = prodNewRow.DivisionId,
                                    DivisionName = prodNewRow.Divisionname,
                                    DivisionShortName = prodNewRow.ShortName,
                                    Description = prodNewRow.Description,
                                    Participant = prodNewRow.Participant,
                                    RowNumber = (int)prodNewRow.RowNumber,
                                    Total = (int)prodNewRow.TOTAL
                                };

            List<ProductInfo> newproduct = productNewRow.ToList();

            
            return newproduct.Count() > 0 ? newproduct : null;

        }

        //Retrieves All New Products By Letter
        public List<ProductInfo> rocGetNewProductsByLetter(int editionId, byte countryId, string letter, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productTextNewRow = from prodTextNewRow in db.roc_spGetNewProductsByLetter(editionId, countryId, letter, page, numberByPage)
                                       
                                        
                                      
                                    select new ProductInfo
                                    {

                                        ProductId = prodTextNewRow.ProductId,
                                        ProductName = prodTextNewRow.ProductName,
                                        DivisionId = prodTextNewRow.DivisionId,
                                        DivisionName = prodTextNewRow.Divisionname,
                                        DivisionShortName = prodTextNewRow.ShortName,
                                        Description = prodTextNewRow.Description,
                                        Participant = prodTextNewRow.Participant,
                                        RowNumber = (int)prodTextNewRow.RowNumber,
                                        Total = (int)prodTextNewRow.TOTAL
                                    };
            
            List<ProductInfo> newproducttext = productTextNewRow.ToList();

            return newproducttext.Count() > 0 ? newproducttext : null;
                

        }

        //Retrieves All New Products By Text
        public List<ProductInfo> rocGetNewProductsByText(int editionId, byte countryId, string text, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productNewRow = from prodNewRow in db.roc_spGetNewProductsByText(editionId, countryId, text, page, numberByPage)
                                    
                                select new ProductInfo
                                {

                                    ProductId = prodNewRow.ProductId,
                                    ProductName = prodNewRow.ProductName,
                                    DivisionId = prodNewRow.DivisionId,
                                    DivisionName = prodNewRow.Divisionname,
                                    DivisionShortName = prodNewRow.ShortName,
                                    Description = prodNewRow.Description,
                                    Participant = prodNewRow.Participant,
                                    RowNumber = (int)prodNewRow.RowNumber,
                                    Total = (int)prodNewRow.TOTAL

                                };

            List<ProductInfo> producttext = productNewRow.ToList();

            return producttext.Count() > 0 ? producttext : null;
        }

        //Retrieves All New Products By FullText
        public List<ProductInfo> rocGetNewProductsByFullText(int editionId, byte countryId, string text, int page, int numberByPage)
        {
           PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productFullRow = from prodFullRow in db.roc_spGetNewProductsByFullText(editionId, countryId, text, page, numberByPage)
                                     
                                 select new ProductInfo
                                 {
                                   ProductId = prodFullRow.ProductId,
                                   ProductName = prodFullRow.ProductName,
                                   DivisionId = prodFullRow.DivisionId,
                                   DivisionName= prodFullRow.Divisionname,
                                   DivisionShortName = prodFullRow.ShortName,
                                   //Description = prodFullRow.Description,
                                   Participant = prodFullRow.Participant,
                                   RowNumber = (int)prodFullRow.RowNumber,
                                   Total = (int)prodFullRow.TOTAL
                                 };

            List<ProductInfo> productfull = productFullRow.ToList();

            return productfull.Count() > 0 ? productfull : null;

            
        }

        #endregion
        
        #region Nutritional Products

        //Retrieves all nutritional products by page
        public List<ProductInfo> rocGetNutritionalProducts(int editionId, byte countryId, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productoRow = from prodRow in db.roc_spGetNutritionalProducts(editionId, countryId, page, numberPage)
                              select new ProductInfo
                              {
                                  ProductId = prodRow.ProductId,
                                  ProductName = prodRow.ProductName,
                                  DivisionId = prodRow.DivisionId,
                                  DivisionName = prodRow.DivisionName,
                                  DivisionShortName = string.Empty,
                                  Description = prodRow.Description,
                                  Participant = prodRow.Participant,
                                  RowNumber = (int)prodRow.RowNumber,
                                  Total = (int)prodRow.TOTAL
                              };

            List<ProductInfo> prodNutritional = productoRow.ToList();
            
            return prodNutritional.Count() > 0 ? prodNutritional : null;
            
        }

        //Retrieves all nutritional products by letter
        public List<ProductInfo> rocGetNutritionalProductsByLetter(int editionId, byte countryId, string letter, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productNutRow = from prodNutRow in db.roc_spGetNutritionalProductsByLetter(editionId,countryId,letter, page, numberPage)
                                   
                                select new ProductInfo
                                {
                                    ProductId = prodNutRow.ProductId,
                                    ProductName = prodNutRow.ProductName,
                                    DivisionId = prodNutRow.DivisionId,
                                    DivisionName = prodNutRow.DivisionName,
                                    DivisionShortName = string.Empty,
                                    Description = prodNutRow.Description,
                                    Participant = prodNutRow.Participant,
                                    RowNumber = (int)prodNutRow.RowNumber,
                                    Total = (int)prodNutRow.TOTAL
                                };

            List<ProductInfo> productNutritional = productNutRow.ToList();

            return productNutritional.Count() > 0 ? productNutritional : null;

        }
       
        //Retrieves all nutritional products by text
        public List<ProductInfo> rocGetNutritionalProductsByText(int editionId, byte countryId, string text, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productNutTextRow = from prodNutTextRow in db.roc_spGetNutritionalProductsByText(editionId, countryId, text, page, numberPage)
                                        
                                    select new ProductInfo
                                    {
                                        ProductId = prodNutTextRow.ProductId,
                                        ProductName = prodNutTextRow.ProductName,
                                        DivisionId = prodNutTextRow.DivisionId,
                                        DivisionName = prodNutTextRow.DivisionName,
                                        DivisionShortName = string.Empty,
                                        Description = prodNutTextRow.Description,
                                        Participant = prodNutTextRow.Participant,
                                        RowNumber = (int)prodNutTextRow.RowNumber,
                                        Total = (int)prodNutTextRow.TOTAL
                                    };

            List<ProductInfo> prodNutText = productNutTextRow.ToList();

            return prodNutText.Count() > 0 ? prodNutText : null;
         
        }
         
        //Retrieves all nutritional products by full text
        public List<ProductInfo> rocGetNutritionalProductsByFullText(int editionId, byte countryId, string text, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var productNutFree = from prodNutFree in db.roc_spGetNutritionalProductsByFullText(editionId, countryId, text, page, numberPage)
                                     
                                 select new ProductInfo
                                 {
                                     ProductId = prodNutFree.ProductId,
                                     ProductName = prodNutFree.ProductName,
                                     DivisionId = prodNutFree.DivisionId,
                                     DivisionName = prodNutFree.DivisionName,
                                     DivisionShortName = string.Empty,
                                     Description = prodNutFree.Description,
                                     Participant = prodNutFree.Participant,
                                     RowNumber = (int)prodNutFree.RowNumber,
                                     Total = (int)prodNutFree.TOTAL

                                 };
            List<ProductInfo> productNutriFree = productNutFree.ToList();

            return productNutriFree.Count() > 0 ? productNutriFree : null;
        }


        #endregion 

        #region Products

        //Retrieves all products
        public PEVBusinessEntries.ROC.ProductContentInfo rocGetProdut(int editionId, byte countryId, int productId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var product = from productRow in db.roc_spGetProduct(editionId, countryId, productId)
                          select new ProductContentInfo
                          {
                              ProductId = productRow.ProductId,
                              PharmaFormsId = productRow.PharmaFormId,
                              EditionId = productRow.EditionId,
                              DivisionId = productRow.DivisionId,
                              CategoryId = productRow.CategoryId,
                              HTMLContent = productRow.HTMLContent,
                              Page = productRow.Page,
                              ProductName = productRow.ProductName,
                              CountryId = productRow.CountryId,
                              LaboratoryId = productRow.LaboratoryId,
                              Description = productRow.Description,
                              Active = productRow.Active
                              
                          };

            List<ProductContentInfo> products = product.ToList();

            return products.Count() > 0 ? products[0] : null;
        }

        //Retrieves all products by page
        public List<ProductInfo> rocGetProducts(byte countryId, int editionId, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var products = from productsRow in db.roc_spGetProducts(countryId, editionId, page, numberByPage)
                           select new ProductInfo
                           {
                               ProductId = productsRow.ProductId,
                               ProductName = productsRow.ProductName,
                               Description = productsRow.Description,
                               DivisionId = productsRow.DivisionId,
                               DivisionName =string.Empty, 
                               DivisionShortName = productsRow.ShortName,
                               Participant = productsRow.Participant,
                               RowNumber = (int)productsRow.RowNumber,
                               Total = (int)productsRow.TOTAL

                           };

            List<ProductInfo> productsp = products.ToList();

            return productsp.Count() > 0 ? productsp : null;
        
        }

        //Retrieves all products by letter
        public List<ProductInfo> rocGetProductsByLetter(int editionId, byte countryId, string letter, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var product = from prodRow in db.roc_spGetProductsByLetter(editionId, countryId, letter, page, numberPage)
                          select new ProductInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              Description = prodRow.Description,
                              DivisionId = prodRow.DivisionId,
                              DivisionShortName = prodRow.ShortName,
                              Participant = prodRow.Participant,
                              RowNumber = (int)prodRow.RowNumber,
                              Total = (int)prodRow.TOTAL

                          };
            List<ProductInfo> productos = product.ToList();

            return productos.Count() > 0 ? productos : null;
        }

        //Retrieves all products by Text
        public List<ProductInfo> rocGetProductsByText(int editionId, byte countryId, string text, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var producttxt = from prodtRow in db.roc_spGetProductsByText(editionId, countryId, text, page, numberPage)
                             select new ProductInfo
                             {
                                 ProductId = prodtRow.ProductId,
                                 ProductName = prodtRow.ProductName,
                                 Description = prodtRow.Description,
                                 DivisionId = prodtRow.DivisionId,
                                 DivisionShortName = prodtRow.ShortName,
                                 Participant = prodtRow.Participant,
                                 RowNumber = (int)prodtRow.RowNumber,
                                 Total = (int)prodtRow.TOTAL
                             };

            List<ProductInfo> prodtext = producttxt.ToList();

            return prodtext.Count() > 0 ? prodtext : null;

        }

        //Retrieves all products by full text
        public List<ProductInfo> rocGetProdutsByFullText(int editionId, byte countryId, string text, int page, int numberPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var prodFull = from prodfRow in db.roc_spGetProductsByFullText(editionId, countryId, text, page, numberPage)
                           select new ProductInfo
                           {
                               ProductId = prodfRow.ProductId,
                               ProductName = prodfRow.ProductName,
                               Description = prodfRow.Description,
                               DivisionId = prodfRow.DivisionId,
                               DivisionName = prodfRow.ProductName,
                               DivisionShortName = prodfRow.ShortName,
                               Participant = prodfRow.Participant,
                               RowNumber = (int)prodfRow.RowNumber,
                               Total = (int)prodfRow.TOTAL
                           };

            List<ProductInfo> productof = prodFull.ToList();

            return productof.Count() > 0 ? productof : null;
        }
     
        //Retrieves all products by specie
        public List<ProductInfo> rocGetProductsBySpecie(byte countryId, int editionId, int specieId, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var prodspecie = from prodRow in db.roc_spGetProductsBySpecie(countryId, editionId, specieId, page, numberByPage)
                             select new ProductInfo
                             {
                                 ProductId = prodRow.ProductId,
                                 ProductName = prodRow.ProductName,
                                 Description = string.Empty,
                                 DivisionId = prodRow.DivisionId,
                                 DivisionName = string.Empty,
                                 DivisionShortName = prodRow.ShortName,
                                 Participant = prodRow.Participant,
                                 RowNumber = (int)prodRow.RowNumber,
                                 Total = (int)prodRow.TOTAL

                             };

            List<ProductInfo> productospecie = prodspecie.ToList();

            return productospecie.Count() > 0 ? productospecie : null;
        }

        //Retrieves all products by category and division
        public List<ProductByCategoryAndDivisionInfo> rocGetProductsByCategoryAndDivision(int categoryId, int divisionId, int editionId, byte countryId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var proddiv = from proddrow in db.roc_spGetProductsByCategoryAndDivision(categoryId, divisionId, editionId, countryId)
                          select new ProductByCategoryAndDivisionInfo
                          {
                              ProductId = proddrow.ProductId,
                              ProductName = proddrow.ProductName,
                              CountryId = proddrow.CountryId,
                              LaboratoryId = proddrow.LaboratoryId,
                              Description = proddrow.Description,
                              Active = proddrow.Active,
                              Participant = proddrow.Participant,
                              CategoryId = proddrow.CategoryId
                          };

            List<ProductByCategoryAndDivisionInfo> products = proddiv.ToList();

            return products.Count() > 0 ? products : null;
        }

        //Retrieves all products by active substance
        public List<ProductByActiveSubstanceInfo> rocProductsByActiveSubstance(int activesubstanceId, byte countryId, int editionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var prodsus = from prodRow in db.roc_spGetProductByActiveSubstance(activesubstanceId, countryId, editionId)
                          select new ProductByActiveSubstanceInfo
                          {
                              ProductId = prodRow.ProductId,
                              ProductName = prodRow.ProductName,
                              Description = prodRow.Description,
                              CountryId = prodRow.CountryId,
                              Active = prodRow.Active,
                              Participant = prodRow.Participant,
                              LaboratoryName = prodRow.LaboratoryName,
                              LaboratoryId = prodRow.LaboratoryId,
                              ImageName = prodRow.ImageName,
                              CourtesImage = prodRow.CourtesyImage

                          };
            List<ProductByActiveSubstanceInfo> prod = prodsus.ToList();

            return prod.Count() > 0 ? prod : null;
        }

        //Retrieves all products by combined active substance
        public List<ProductByActiveSubstanceInfo> rocProductsByCombinesSubs(int activesubstanceId, byte countryId, int editionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var prodc = from prodcRow in db.roc_spGetProductByCombinedActiveSubstance(activesubstanceId, countryId, editionId)
                        select new ProductByActiveSubstanceInfo
                        {
                            ProductId = prodcRow.ProductId,
                            ProductName = prodcRow.ProductName,
                            Description = prodcRow.Description,
                            CountryId = prodcRow.CountryId,
                            Active = prodcRow.Active,
                            Participant = prodcRow.Participant,
                            LaboratoryName = prodcRow.LaboratoryName,
                            LaboratoryId = prodcRow.LaboratoryId,
                            ImageName = prodcRow.ImageName,
                            CourtesImage = prodcRow.CourtesyImage

                        };

            List<ProductByActiveSubstanceInfo> prodcomb = prodc.ToList();

            return prodcomb.Count() > 0 ? prodcomb : null;
          
        }
           
        //Retrieves all products by therapeutic use
        public List<ProductByTheraUseInfo> rocGetProductByTheraUse(int therapeuticUseId, int editionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var producto = from prodRow in db.roc_spGetProductByTherapeuticUse(therapeuticUseId, editionId)
                           select new ProductByTheraUseInfo
                           {
                               ProductId = prodRow.ProductId,
                               ProductName = prodRow.ProductName,
                               DivisinId = prodRow.DivisionId,
                               ShortName = prodRow.ShortName,
                               Participant = prodRow.Participant

                           };

            List<ProductByTheraUseInfo> prodt = producto.ToList();

            return prodt.Count() > 0 ? prodt : null;
        }

        //Retrieves all Products by Equipment.
        public List<PEVBusinessEntries.ROC.ProductByEquipmentInfo> rocGetProductsByEquipment(int editionId, int equipmentId, int countryId, int page, int numberByPage)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var ProductInformation = from ProductInf in db.roc_spGetProductsByEquipment(editionId, equipmentId, countryId, page, numberByPage)
                                       select new PEVBusinessEntries.ROC.ProductByEquipmentInfo
                                       {
                                           ProductId = ProductInf.ProductId,
                                           ProductName = ProductInf.ProductName,
                                           CountryId = ProductInf.CountryId,
                                           LaboratoryId = ProductInf.LaboratoryId,
                                           Description = ProductInf.Description,
                                           Active = ProductInf.Active,
                                           IdAccess = ProductInf.IdAccess,
                                           Participant = ProductInf.Participant,
                                           DivisionId = ProductInf.DivisionId,
                                           DivisionName = ProductInf.DivisionName,
                                           ShortName = ProductInf.ShortName,
                                           RowNumber = (int)ProductInf.RowNumber,
                                           Total = (int)ProductInf.TOTAL
                                       };

            List<PEVBusinessEntries.ROC.ProductByEquipmentInfo> Products = ProductInformation.ToList();

            return Products.Count() > 0 ? Products : null;
        }

        //Retrieves all Products by Id
        public PEVBusinessEntries.ROC.ProductByIdInfo rocGetProductById(int ProductId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var Producto = from ProdRow in db.roc_spGetProductById(ProductId)
                           select new PEVBusinessEntries.ROC.ProductByIdInfo
                           {
                               ProductId = ProdRow.ProductId,
                               ProductName = ProdRow.ProductName,
                               CountryId = ProdRow.CountryId,
                               LaboratoryId = ProdRow.LaboratoryId,
                               Description = ProdRow.Description,
                               Active = ProdRow.Active,
                               Participant = ProdRow.Participant,
                               DivisionId = ProdRow.DivisionId,
                               DivisionName = ProdRow.DivisionName,
                               ShortName = ProdRow.ShortName


                           };

            List<ProductByIdInfo> products = Producto.ToList();

            return products.Count() > 0 ? products[0] : null;
           

        }

        #endregion
        

        public static readonly ProductsDALC Instance = new ProductsDALC();


    }
}
