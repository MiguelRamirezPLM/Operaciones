using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEAQBusinessEntries;
using DEAQBusinessEntries.ROC;
using AgroBusinessEntries;
namespace DEAQBusinessLogicComponent
{
    public  class SeedsBLC
    {
        #region Constructor

        private SeedsBLC() { } 

        #endregion

        #region Methods
        #region ROC Methods
        //Retrieves Information From Seed
        public SeedInfo rocGetSeed(string code, int seedId)
        {
            if (seedId <= 0)
                throw new ArgumentException("The seed does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.SeedsDALC.Instance.rocGetSeed(seedId);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
      
        
        }


        //Retrieves All Information The Seeds By Page
        public List<SeedByPageInfo> rocGetSeeds(string code, int numberByPage, int page)
        {

            if (numberByPage < 0 || page < 0)
                throw new ArgumentException("The page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.SeedsDALC.Instance.rocGetSeeds(numberByPage, page);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

         //Retrieves All Information The Seeds By FullText
        public List<SeedByPageInfo> rocGetSeedsByFullText(string code, int numberByPage, int page, string text)
        {

            if (numberByPage < 0 || page < 0)
                throw new ArgumentException("The page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.SeedsDALC.Instance.rocGetSeedsByFullText(numberByPage, page, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }

        }

        //Retrieves All Information The Seeds By Letter
        public List<SeedByPageInfo> rocGetSeedsByLetter(string code, int numberByPage, int page, string letter)
        {
            if (numberByPage < 0 || page < 0)
                throw new ArgumentException("The page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.SeedsDALC.Instance.rocGetSeedsByLetter(numberByPage, page, letter);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        //Retrieves All Information The Seeds By Text
        public List<SeedByPageInfo> rocGetSeedsByText(string code, int numberByPage, int page, string text)
        {

            if (numberByPage < 0 || page < 0)
                throw new ArgumentException("The page does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)

                    return DEAQDataAccessComponent.SeedsDALC.Instance.rocGetSeedsByText(numberByPage, page, text);

                else
                    throw new ApplicationException("The code is not valid or is inactive.");
            }
        }

        #endregion
        #region PLM Methods
        public List<SeedDetailInfo> getAll(string isbn, string code)
        {
            if (isbn == "")
            {
                throw new ArgumentException("The isbn or page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.SeedsDALC.Instance.getAll(isbn);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public SeedDetailInfo getById(string isbn, int seedId, string code)
        {
            if (isbn == "" || seedId == 0)
            {
                throw new ArgumentException("The isbn or seedId does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.SeedsDALC.Instance.getById(isbn, seedId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<SeedDetailInfo> getByProduct(string isbn, int productId, int pharmaFormId, int divisionId, int categoryId, string code)
        {
            if (isbn == "" || categoryId == 0 || pharmaFormId == 0 || categoryId == 0 || divisionId == 0)
                throw new ArgumentException("The isbn or divisionId or categoryId  or pharmaFormId or categoryId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.SeedsDALC.Instance.getByProduct(isbn, productId, pharmaFormId, divisionId, categoryId);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }

        public List<SeedDetailInfo> getByText(string isbn, string text, string code)
        {
            if (isbn == "" || text == "")
                throw new ArgumentException("The isbn or divisionId does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEAQDataAccessComponent.SeedsDALC.Instance.getByText(isbn, text);
                }
                else
                {
                    throw new ApplicationException("The code is not valid or is inactive.");
                }
            }
        }
        #endregion

        #endregion
        public static readonly SeedsBLC Instance = new SeedsBLC();
    }
}
