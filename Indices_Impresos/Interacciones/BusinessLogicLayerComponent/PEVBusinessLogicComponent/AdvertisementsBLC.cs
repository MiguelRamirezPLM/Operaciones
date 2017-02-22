using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEVBusinessLogicComponent
{
    public class AdvertisementsBLC
    {

        #region Constructor

        private AdvertisementsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves all Advertisements by Division
        public List<PEVBusinessEntries.ROC.AdvertisementByDivisionInfo> rocGetAdvertisementsByDivision(string code, int divisionId, int editionId)
        {
            if (divisionId < 1 || editionId < 1)
            {
                throw new ArgumentException("The Division or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<PEVBusinessEntries.ROC.AdvertisementByDivisionInfo> ads = PEVDataAccessComponent.AdvertisementsDALC.Instance.rocGetAdvertisementsByDivision(divisionId, editionId);

                    if(ads != null)
                        foreach (PEVBusinessEntries.ROC.AdvertisementByDivisionInfo ad in ads)
                        {
                            ad.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl + ad.BaseUrl;
                        }


                    return ads;
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly AdvertisementsBLC Instance = new AdvertisementsBLC();

    }
}
