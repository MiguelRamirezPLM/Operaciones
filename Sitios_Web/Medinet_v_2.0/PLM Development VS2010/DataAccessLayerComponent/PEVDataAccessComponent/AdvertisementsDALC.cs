using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEVBusinessEntries;

namespace PEVDataAccessComponent
{
    public class AdvertisementsDALC
    {

        #region Constructor

        private AdvertisementsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves all Advertisements by Division
        public List<PEVBusinessEntries.ROC.AdvertisementByDivisionInfo> rocGetAdvertisementsByDivision(int divisionId, int editionId)
        {
            PEVDataClassesDataContext db = new PEVDataClassesDataContext();

            var AdvertisementInformation = from AdvertisementInfo in db.roc_spGetAdvertisementsByDivision(divisionId, editionId)
                                           select new PEVBusinessEntries.ROC.AdvertisementByDivisionInfo
                                           {
                                               AdId = AdvertisementInfo.AdId,
                                               AdFile = AdvertisementInfo.AdFile,
                                               Active = AdvertisementInfo.Active,
                                               BaseUrl = AdvertisementInfo.BaseUrl
                                           };

            List<PEVBusinessEntries.ROC.AdvertisementByDivisionInfo> advertisements = AdvertisementInformation.ToList();

            return advertisements.Count() > 0 ? advertisements : null;

        }

        #endregion

        public static readonly AdvertisementsDALC Instance = new AdvertisementsDALC();

    }
}
