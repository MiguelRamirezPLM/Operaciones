using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class BannersBLC
    {
        #region Constructors

        private BannersBLC() { }

        #endregion

        #region Public Methods

        public List<BannersByTargetInfo> getBannersByCode(string codeString, string country, Catalogs.TargetOutputs targetId)
        {
            ValidCodeInfo validCode = new ValidCodeInfo();
            validCode = CodesBLC.Instance.validCode(codeString);

            //Valid user code
            if (validCode.CodeStatusId == Catalogs.CodeStatus.ACTIVO)
            {
                //Get the prefixId
                int prefixId = CodesBLC.Instance.getCode(codeString).PrefixId;

                //Get the country
                CountryInfo BECountry = CatalogsBLC.Instance.getCountry(country);

                List<BannersByTargetInfo> banners = PLMClientsDataAccessComponent.BannersDALC.Instance.getByTargetByPrefix(BECountry.CountryId, (byte)targetId, prefixId);

                if (banners != null)

                    //Set the root url to note and the banners
                    foreach (BannersByTargetInfo banner in banners)
                    {
                        banner.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + BECountry.CountryName + "/" + banner.BaseUrl;
                        banner.FileName = banner.BaseUrl + banner.FileName;
                    }
                return banners;
            }
            else
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
        }

        public List<BannerInfo> getBannerByTarget(string isbn, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId, string applicationKey)
        {
            if (isbn == null || (byte)targetId == 0)
                throw new ArgumentException("The ISBN or TargetOutput does not exist.");

            List<BannerInfo> banners = PLMClientsDataAccessComponent.BannersDALC.Instance.getBannerByTarget(isbn, targetId);

            if (banners != null)
                foreach (PLMClientsBusinessEntities.BannerInfo banner in banners)
                {
                    banner.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).CountryId).CountryName +
                        "/" + banner.BaseUrl;
                }
            return banners;
        }

        #endregion

        public static readonly BannersBLC Instance = new BannersBLC();

    }
}
