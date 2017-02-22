using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIADataAccessComponent
{
   public class CatalogsDALC
   {

       #region Constructors
       
       private CatalogsDALC() { }

       #endregion

       #region Public Methods

       //Retrieves Information From Editions
       public DEIABusinessEntries.EditionInfo rocGetEdition()
       {

           DEIADataClassesDataContext db = new DEIADataClassesDataContext();
           
           var edition = from editionRow in db.roc_spGetEdition()
                         select new DEIABusinessEntries.EditionInfo
                         {
                             EditionId = editionRow.EditionId,
                             ParentId = editionRow.ParentId,
                             BookId = editionRow.BookId,
                             NumberEdition = editionRow.NumberEdition,
                             ISBN = editionRow.ISBN,
                             BarCode = editionRow.BarCode,
                             Active = editionRow.Active                       
                         };

           List<DEIABusinessEntries.EditionInfo> editionInfo = edition.ToList();

           return editionInfo.Count() > 0 ? editionInfo[0] : null;

       }
       
       //Retrieves all Phones by Company
       public List<DEIABusinessEntries.ROC.CompanyPhoneInfo> rocGetPhonesByCompany(int companyId)
       {
           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var phone = from phoneRow in db.roc_spGetPhonesByCompany(companyId)
                       select new DEIABusinessEntries.ROC.CompanyPhoneInfo
                       {   
                           PhoneTypeId = phoneRow.PhoneTypeId,
                           TypeName  = phoneRow.TypeName,
                           PhoneValue = phoneRow.PhoneValue
                       };

           List<DEIABusinessEntries.ROC.CompanyPhoneInfo> companyPhone = phone.ToList();

           return companyPhone.Count() > 0 ? companyPhone : null; 

        }

       #region States

       //Retrieves all States by stateId
       public DEIABusinessEntries.StateInfo rocGetState(int stateId)
       {
           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var state = from stateRow in db.roc_spGetState(stateId)
                       select new DEIABusinessEntries.StateInfo
                       {
                           StateId = stateRow.StateId,
                           CountryId = stateRow.CountryId,
                           StateName = stateRow.StateName,
                           Active = stateRow.Active

                       };

           List<DEIABusinessEntries.StateInfo> stateInfo = state.ToList();

           return stateInfo.Count() > 0 ? stateInfo[0] : null;
           
       }

       //Retrieves all States whith Companies By Sections
       public List<DEIABusinessEntries.StateInfo> rocGetStatesWithCompaniesBySection(int sectionId, int EditionId)
       {
           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var state = from stateRow in db.roc_spGetStatesWithCompaniesBySection(sectionId, EditionId)
                       select new DEIABusinessEntries.StateInfo
                       {
                           StateId = stateRow.StateId,
                           CountryId = stateRow.CountryId,
                           StateName = stateRow.StateName,
                           Active = stateRow.Active
                       };

           List<DEIABusinessEntries.StateInfo> stateInfo = state.ToList();

           return stateInfo.Count() > 0 ? stateInfo: null;
       }

       #endregion

       #region Cities

       //Retrieves all Information from City
       public DEIABusinessEntries.CityInfo rocGetCity(int cityId)
       {
           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var city = from cityRow in db.roc_spGetCity(cityId)
                      select new DEIABusinessEntries.CityInfo
                      {
                          CityId = cityRow.CityId,
                          StateId = cityRow.StateId,
                          CityName = cityRow.CityName,
                          Active = cityRow.Active
                      };

           List<DEIABusinessEntries.CityInfo> cityInfo = city.ToList();

           return cityInfo.Count() > 0 ? cityInfo[0] : null;

       }

       //Retrieves all Cities With Companies by State
       public List<DEIABusinessEntries.CityInfo> rocGetCitiesWithCompaniesByState(int stateId, int sectionId, int editionId)
       {
           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var city = from cityRow in db.roc_spGetCityWhitCompaniesByState(stateId, sectionId, editionId)
                      select new DEIABusinessEntries.CityInfo
                      {
                          CityId = cityRow.CityId,
                          StateId = cityRow.StateId,
                          CityName = cityRow.CityName,
                          Active = cityRow.Active
                      };

           List<DEIABusinessEntries.CityInfo> cityInfo = city.ToList();

           return cityInfo.Count() > 0 ? cityInfo : null; 
       }

       #endregion

       #region Advertisements

       //Retrieves all Advertisements by CompanyId
       public List<DEIABusinessEntries.AdvertisementInfo> rocGetAdvertisementsByCompanyId(int companyId)
       {

           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var adv = from advRow in db.roc_spGetAdvertisementsByCompanyId(companyId)
                     select new DEIABusinessEntries.AdvertisementInfo
                     {
                         AdvId = advRow.AdvId,
                         CompanyId = (int)advRow.CompanyId,
                         HiredSpace = advRow.HiredSpace,
                         AdvFile = advRow.AdvFile,
                         Active = advRow.Active
                     };

           List<DEIABusinessEntries.AdvertisementInfo> advInfo = adv.ToList();

           return advInfo.Count() > 0 ? advInfo : null;
       }

       //Retrieves all Advertisements by SectionId
       public List<DEIABusinessEntries.AdvertisementInfo> rocGetAdvertisementsBySectionId(int sectionId)
       {

           DEIADataClassesDataContext db = new DEIADataClassesDataContext();

           var adver = from adverRow in db.roc_spGetAdvertisementsBySectionId(sectionId)
                       select new DEIABusinessEntries.AdvertisementInfo
                       {
                           AdvId = adverRow.AdvId,
                           CompanyId = (int)adverRow.CompanyId,
                           HiredSpace = adverRow.HiredSpace,
                           AdvFile = adverRow.AdvFile,
                           Active = adverRow.Active
                       };

           List<DEIABusinessEntries.AdvertisementInfo> adverInfo = adver.ToList();

           return adverInfo.Count() > 0 ? adverInfo : null;
       }

       #endregion 

       #endregion

       public static readonly CatalogsDALC Instace = new CatalogsDALC();

   }
}
