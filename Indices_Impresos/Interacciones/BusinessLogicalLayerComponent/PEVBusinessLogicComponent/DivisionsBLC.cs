using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class DivisionsBLC
    {

        #region Constructor

        private DivisionsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves information about an Division by DivisionId
        public List<PEVBusinessEntries.ROC.DivInformationByDivisionInfo> rocGetDivisionInformation(string code, int divisionId)
        {
            if (divisionId < 1)
            {
                throw new ArgumentException("The Division does not exist.");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PEVBusinessEntries.ROC.DivInformationByDivisionInfo> divisions = PEVDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionInformation(divisionId);

                    foreach (PEVBusinessEntries.ROC.DivInformationByDivisionInfo div in divisions)
                    {
                        div.Image = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + "logos/" + div.Image;
                    }

                    return divisions;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Divisions by EditionId and CountryId
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByEdition(string code, int editionId, int countryId, int page, int numberByPage)
        {
            if (editionId < 1 || countryId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page or Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByEditionId(editionId, countryId, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Divisions by EditionId and CountryId and Letter
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByLetter(string code, int editionId, int countryId, string letter, int page, int numberByPage)
        {
            if (editionId < 1 || countryId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page or Country does not exist.");
            }
            else 
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByLetter(editionId, countryId, letter, page, numberByPage);
                }

                else 
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
 
        }

        //Retrieves all Divisions by EditionId and CountryId and Text
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByText(string code, int editionId, int countryId, string text, int page, int numberByPage)
        {
            if (editionId < 1 || countryId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Page or Country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if(validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByText(editionId, countryId, text, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Divisions by EditionId and CountryId and FullText
        public List<PEVBusinessEntries.ROC.DivisionsInfo> rocGetDivisionsByFullText(string code, int editionId, int countryId, string fullText, int page, int numberByPage)
        {
            if (editionId < 1 || countryId < 1 || page < 0 || numberByPage < 0)
            {
                throw new ArgumentException("The Edition or Country or Page does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return PEVDataAccessComponent.DivisionsDALC.Instance.rocGetDivisionsByFullText(editionId, countryId, fullText, page, numberByPage);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly DivisionsBLC Instance = new DivisionsBLC();

    }
}
