using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVBusinessLogicComponent
{
    public class ProductsBLC
    {
        #region Constructor

        private ProductsBLC() { }

        #endregion

        #region Public Methods

        #region New Products


        //Retrieves all new product by productId 

        public PEVBusinessEntries.ROC.ProductInfo rocGetNewProduct(string code, int editionId, byte countryId, int productId)
        {
            if (editionId <= 0 || countryId == 0 || productId <= 0)
                throw new ArgumentException("The book edition, country or product does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNewProduct(editionId, countryId, productId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Retrieves all new product by page and numerpage
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNewProductsByPage(string code, int editionId, byte countryId, int page, int numberByPage)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNewProducts(editionId, countryId, page, numberByPage);
                       
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }


        //Retrieves all new product by letter
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNewProductsByLetter(string code, int editionId, byte countryId, string letter, int page, int numberByPage)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNewProductsByLetter(editionId, countryId, letter, page, numberByPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

            }

        }

        //Retrieves all new product by text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNewProductsByText(string code, int editionId, byte countryId, string text, int page, int numberByPage)
        { 
            if (editionId <= 0 || page < 0 || numberByPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNewProductsByText(editionId, countryId, text, page, numberByPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

            }
        }

        //Retrieves all new product by full text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNewProductByFullText(string code, int editionId, byte countryId, string text, int page, int numberByPage)
        {
            if (editionId <= 0 || page < 0 || numberByPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNewProductsByFullText(editionId, countryId, text, page, numberByPage);
           
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");

            }
        }

        #endregion

        #region Nutritional Products

        //Retrieves all nutritional products 
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNutritionalProducts(string code, int editionId, byte countryId, int page, int numberPage)
        { 
          if (editionId <= 0 || page < 0 || numberPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does no exist");
            else
          {
              PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
              valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

              if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                  return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNutritionalProducts(editionId, countryId, page, numberPage);

              else
                  throw new ApplicationException("El código no es válido o se encuentra inactivo.");
          }
        }

        //Retrieves all nutritional products by letter
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNutritionalProductsByLetter(string code, int editionId, byte countryId, string letter, int page, int numberPage)
        {
            if (editionId <= 0 || page < 0 || numberPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNutritionalProductsByLetter(editionId, countryId, letter, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Retrieves all nutritional products by text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNutritionalProductsByText(string code, int editionId, byte countryId, string text, int page, int numberPage)
        {
            if (editionId <= 0 || page < 0 || numberPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does no exist");
            else
            { 
               PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNutritionalProductsByText(editionId, countryId, text, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }
    
        //Retrieves all nutritional products by full text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetNutritionalProductsByFullText(string code, int editionId, byte countryId, string text, int page, int numberPage)
        { 
         if (editionId <= 0 || page < 0 || numberPage < 0 || countryId == 0)
                throw new ArgumentException("The book edition, country, page or number page does no exist");
            else
            { 
               PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetNutritionalProductsByFullText(editionId, countryId, text, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion 

        #region Products 

        //Retrieves all product
        public PEVBusinessEntries.ROC.ProductContentInfo rocGetProuct(string code, int editionId, byte countryId, int productId)
        {
            if (editionId <= 0 || countryId == 0 || productId <=0)
                throw new ArgumentException("The book edition, country or product does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    PEVBusinessEntries.ROC.ProductContentInfo product = PEVDataAccessComponent.ProductsDALC.Instance.rocGetProdut(editionId, countryId, productId);

                    if(product != null)
                        product.HTMLContent = product.HTMLContent.Replace("src=\"img/", "src=\"" +
                            PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "img/");

                    return product;
                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }


        //Retrieves all products by page
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetProducts(string code, byte countryId, int editionId, int page, int numberByPage)
        {
            if (editionId <= 0 || countryId == 0 || page < 0 || numberByPage < 0 )
                throw new ArgumentException("The book edition, country, page or number by page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

        
                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProducts(countryId, editionId, page, numberByPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

        //Retrieves all products by letter
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetProductsByLetter(string code,  int editionId, byte countryId, string letter, int page, int numberPage)
        {
            if (editionId <= 0 || countryId == 0 ||  page < 0 || numberPage < 0)
                throw new ArgumentException("The book edition, country, page or number by page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductsByLetter(editionId, countryId, letter, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

        //Retrieves all products by Text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetProductsByText(string code,  int editionId, byte countryId, string text, int page, int numberPage)
        { 
        if (editionId <= 0 || countryId == 0 ||  page < 0 || numberPage < 0)
                throw new ArgumentException("The book edition, country, page or number by page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductsByText(editionId, countryId, text, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Retrieves all products by full text
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetProdutsByFullText(string code, int editionId, byte countryId, string text, int page, int numberPage)
        {

            if (editionId <= 0 || countryId == 0 || page < 0 || numberPage < 0)
                throw new ArgumentException("The book edition, country, page or number by page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProdutsByFullText(editionId, countryId, text, page, numberPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

        //Retrieves all products by specie
        public List<PEVBusinessEntries.ROC.ProductInfo> rocGetProductsBySpecie(string code,  byte countryId, int editionId, int specieId, int page, int numberByPage)
        {
            if (editionId <= 0 || countryId == 0 || specieId <= 0 || page < 0 || numberByPage < 0)
                throw new ArgumentException("The book edition, country, page or number by page does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductsBySpecie(countryId, editionId, specieId, page, numberByPage);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

        //Retrieves all products by category and division
        public List<PEVBusinessEntries.ROC.ProductByCategoryAndDivisionInfo> rocGetProductsByCategoryAndDivision(string code, int categoryId, int divisionId, int editionId, byte countryId)
        { 
          if (categoryId <=0 || divisionId <= 0 || editionId <= 0 || countryId == 0 )
                throw new ArgumentException("The book edition, country, category or division does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductsByCategoryAndDivision(categoryId, divisionId, editionId, countryId);


                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Retrieves all products by active substance
        public List<PEVBusinessEntries.ROC.ProductByActiveSubstanceInfo> rocProductsByActiveSubstance(string code, int activesubstanceId, byte countryId, int editionId)
        {
            if (activesubstanceId <= 0 || editionId <= 0 || countryId == 0)
                throw new ArgumentException("The book edition, country,  or active substance does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocProductsByActiveSubstance(activesubstanceId, countryId, editionId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }


        //Retrieves all products by combined active substance
        public List<PEVBusinessEntries.ROC.ProductByActiveSubstanceInfo> rocProductsByCombinesSubs(string code,  int activesubstanceId, byte countryId, int editionId)
        {
            if (activesubstanceId <= 0 || editionId <= 0 || countryId == 0)
                throw new ArgumentException("The book edition, country,  or active substance does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocProductsByCombinesSubs(activesubstanceId, countryId, editionId);


                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        }

        //Retrieves all products by therapeutic use
        public List<PEVBusinessEntries.ROC.ProductByTheraUseInfo> rocGetProductByTheraUse(string code,  int therapeuticUseId, int editionId)
        {
            if (therapeuticUseId <= 0 || editionId <= 0)
                throw new ArgumentException("The book edition or therapeutic use does no exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo valCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                valCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (valCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)


                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductByTheraUse(therapeuticUseId, editionId);

                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        
        
        }

        //Retrieves all Products by Equipment.
        public List<PEVBusinessEntries.ROC.ProductByEquipmentInfo> rocGetProductsByEquipment(string code, int editionId, int equipmentId, int countryId, int page, int numberByPage)
        {
            if (editionId < 1 || equipmentId < 1 || countryId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Equipment or Country or Page does not Exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductsByEquipment(editionId, equipmentId, countryId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all products by Id
        public PEVBusinessEntries.ROC.ProductByIdInfo rocGetProductById(string code, int productId)
        {

            if (productId <= 0)
            {
                throw new ArgumentException("The Product does not Exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ProductsDALC.Instance.rocGetProductById(productId);
                       
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