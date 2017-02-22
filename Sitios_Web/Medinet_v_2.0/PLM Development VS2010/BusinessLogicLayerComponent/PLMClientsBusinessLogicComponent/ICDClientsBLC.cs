using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class ICDClientsBLC
    {
        #region Constructors

        private ICDClientsBLC() { }

        #endregion


         #region Public Methods

        public List<PLMClientsBusinessEntities.ICDGuidesInfo> getGuidesByICD(string prefix, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId, PLMClientsBusinessEntities.Catalogs.ElectronicInformationTypes informationTypeId, string country, string icdKey, string applicationKey)
        {

            List<PLMClientsBusinessEntities.ICDGuidesInfo> icdInfo = PLMClientsDataAccessComponent.ICDDALC.Instance.getByGuides(prefix, icdKey);

            foreach (PLMClientsBusinessEntities.ICDGuidesInfo item in icdInfo)
            {
                item.MedicalGuides = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByTypeByICD(prefix, (byte)targetId, (byte)informationTypeId, country, item.ICDId);
                
                foreach(PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronicItem in item.MedicalGuides)
                {
                    electronicItem.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                                             PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                                             "/" + electronicItem.BaseUrl;
                }
                
            }
            return icdInfo;
        }

         #endregion

         public static readonly ICDClientsBLC Instance = new ICDClientsBLC();
    }
}
