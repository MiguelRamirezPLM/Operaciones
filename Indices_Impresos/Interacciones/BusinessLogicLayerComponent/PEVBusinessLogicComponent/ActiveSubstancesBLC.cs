using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class ActiveSubstancesBLC
    {

        #region Constructor

        private ActiveSubstancesBLC() { }
        
        #endregion

        #region Public Methods

        //Retrieves an ActiveSubstance by an ActiveSubstanceId
        public PEVBusinessEntries.ActiveSubstanceInfo rocGetActiveSubstanceBySubstanceId(string code, int substanceId)
        {
            if (substanceId < 1)
            {
                throw new ArgumentException("The substance does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstanceBySubstanceId(substanceId);
                }
                else 
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all ActiveSubstances by an ProductId
        public List<PEVBusinessEntries.ActiveSubstanceInfo> rocGetActiveSubstancesByProduct(string code, int productId)
        {
            if (productId < 1)
            {
                throw new ArgumentException("The Product does not exist");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByProductId(productId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all ActiveSubstances by an EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByEdition(string code, int editionId, int page, int numberByPage)
        {
            if (editionId < 1 || numberByPage < 0 || page < 0)
            {
                throw new ArgumentException("The Edition or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByEditionId(editionId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all ActiveSubstances by letter and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByLetter(string code, int editionId, string letter, int page, int numberByPage)
        {
            if (editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByLetter(editionId, letter, page, numberByPage);
                }
                else 
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all ActiveSubstances by an Text and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByText(string code, int editionId, string text, int page, int numberByPage)
        {
            if (editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByText(editionId, text, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all ActiveSubstances by an FullText and EditionId
        public List<PEVBusinessEntries.ROC.ActiveSubstanceByEditionInfo> rocGetActiveSubstancesByFullText(string code, int editionId, string fullText, int page, int numberByPage)
        {
            if (editionId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.ActiveSubstancesDALC.Instance.rocGetActiveSubstancesByFullText(editionId, fullText, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        
        #endregion

        public static readonly ActiveSubstancesBLC Instance = new ActiveSubstancesBLC();

    }
}
