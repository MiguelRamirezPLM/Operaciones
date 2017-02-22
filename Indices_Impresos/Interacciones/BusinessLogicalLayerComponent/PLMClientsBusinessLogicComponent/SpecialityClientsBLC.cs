using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class SpecialityClientsBLC
    {
        #region Constructors

        private SpecialityClientsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.SpecialityInfo> getAll()
        {
            return PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getAll();
        }

        public void addSpeciality(PLMClientsBusinessEntities.SpecialityClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.insert(BEntity);
        }

        public void removeSpeciality(PLMClientsBusinessEntities.SpecialityClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.delete(BEntity);
        }

        public void removeSpeciality(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The clientId does not exist.");
                        
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.delete(clientId);
        }

        public void updateSpeciality(PLMClientsBusinessEntities.SpecialityClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.update(BEntity);
        }

        public PLMClientsBusinessEntities.SpecialityClientInfo getSpecialityByClient(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            return PLMClientsDataAccessComponent.SpecialityClientsDALC.Instance.getByClient(clientId);
        }

        public PLMClientsBusinessEntities.LicenseClientInfo getSpecialitybyId(int speciality)
        {
            if (speciality == 0)
                throw new ArgumentException("The client does not exist.");

            return PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getSpecialityById(speciality); 
        }

        public List<PLMClientsBusinessEntities.SpecialityGuidesInfo> getGuidesBySpeciality(string prefix, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId, PLMClientsBusinessEntities.Catalogs.ElectronicInformationTypes informationTypeId, string country, string applicationKey)
        {
            List<PLMClientsBusinessEntities.SpecialityGuidesInfo> specialityInfo = getSpecialityByGuides(prefix);                        

            foreach (PLMClientsBusinessEntities.SpecialityGuidesInfo item in specialityInfo)
            {
                item.MedicalGuides = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByTypeBySpeciality(prefix, (byte)targetId, (byte)informationTypeId, country, item.SpecialityId);

                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronicItem in item.MedicalGuides)
                {
                    electronicItem.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                                             PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                                             "/" + electronicItem.BaseUrl;
                }
            }
    
            return specialityInfo;
        }


        public List<PLMClientsBusinessEntities.SpecialityGuidesInfo> getSpecialityByGuides(string prefix)
        {
            return PLMClientsDataAccessComponent.SpecialitiesDALC.Instance.getByGuides(prefix);
        }

       

 
        #endregion

        public static readonly SpecialityClientsBLC Instance = new SpecialityClientsBLC();
    }
}