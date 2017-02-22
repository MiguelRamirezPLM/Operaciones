using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class SpeciesBLC
    {
        #region Constructor

        private SpeciesBLC() { }
        
        #endregion

        #region Public Methods

        //Retrieves information about an Specie by SpecieId
        public PEVBusinessEntries.SpeciesBySpecieIdInfo rocGetSpecieBySpecieId(string code, int specieId)
        {
            if (specieId < 1)
            {
                throw new ArgumentException("The Specie does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.SpeciesDALC.Instance.rocGetSpecieBySpecieId(specieId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Species
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpecies(string code, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.SpeciesDALC.Instance.rocGetSpecies(page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Species by Letter
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByLetter(string code, string letter, int page, int numberByPage ,int editionid)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.SpeciesDALC.Instance.rocGetSpeciesByLetter(letter, page, numberByPage, editionid);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Species by Text
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByText(string code, string text, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.SpeciesDALC.Instance.rocGetSpeciesByText(text, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Species by FullText
        public List<PEVBusinessEntries.ROC.SpeciesInfo> rocGetSpeciesByFullText(string code, string fullText, int page, int numberByPage)
        {
            if (page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.SpeciesDALC.Instance.rocGetSpeciesByFullText(fullText, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly SpeciesBLC Instance = new SpeciesBLC();
    }
}
