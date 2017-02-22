using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CompanyClientBranchesBLC
    {

        #region Constructors

        private CompanyClientBranchesBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.BranchDetailInfo getBranch(int branchId)
        {
            if (branchId <= 0)
                throw new ArgumentException("The Branch does not exist.");

            PLMClientsBusinessEntities.BranchDetailInfo branchInfo = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranch(branchId);

            if (branchInfo.Logo != null)
            {
                branchInfo.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branchInfo.BaseUrl + "logoDetail/" + branchInfo.Logo;
                branchInfo.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branchInfo.BaseUrl;
            }

            return branchInfo;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranches()
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranches();
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByCompanyClient(int companyClientId)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByCompanyClient(companyClientId);
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByType(PLMClientsBusinessEntities.Catalogs.CompanyClientTypes typeId)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByType(Convert.ToByte(typeId));
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByState(string state)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByState(this.getStateByKey(state));
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByAgent(int agentId)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByAgent(agentId);
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByZone(byte zoneId)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByZone(zoneId);
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByPrefix(string codePrefix)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByPrefix(codePrefix);
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByAgentByType(byte zoneId, int agentId, byte typeId)
        {
            return PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByAgentByType(zoneId, agentId, typeId);
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getCloseBranches(decimal latitude, decimal longitude)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            BECollection = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranches();

            foreach (PLMClientsBusinessEntities.BranchDetailInfo branch in BECollection)
            {
                branch.BranchDistance = Convert.ToDecimal(this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), 
                    Convert.ToDouble(branch.Latitude), Convert.ToDouble(branch.Longitude)));

                if (branch.Logo != null)
                {
                    branch.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl + "logoDetail/" + branch.Logo;
                    branch.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl;
                }

                branch.CompanyPromotions = PLMClientsBusinessLogicComponent.PromotionsBLC.Instance.getByCompanyClient(branch.CompanyClientId);
            }

            var branchList = from branch in BECollection
                             orderby branch.BranchDistance
                             select new PLMClientsBusinessEntities.BranchDetailInfo
                             {
                                 BranchId = branch.BranchId,
                                 CompanyClientId = branch.CompanyClientId,
                                 CompanyName = branch.CompanyName,
                                 BranchKey = branch.BranchKey,
                                 BranchName = branch.BranchName,
                                 WebPage = branch.WebPage,
                                 Email = branch.Email,
                                 BaseUrl = branch.BaseUrl,
                                 Logo = branch.Logo,
                                 LogoDetail = branch.LogoDetail,
                                 BranchActive = branch.BranchActive,
                                 AddressId = branch.AddressId,
                                 Street = branch.Street,
                                 InternalNumber = branch.InternalNumber,
                                 Suburb = branch.Suburb,
                                 ZipCode = branch.ZipCode,
                                 Location = branch.Location,
                                 CountryId = branch.CountryId,
                                 StateId = branch.StateId,
                                 StateName = branch.StateName,
                                 Lada = branch.Lada,
                                 PhoneOne = branch.PhoneOne,
                                 PhoneTwo = branch.PhoneTwo,
                                 Ext = branch.Ext,
                                 AttentionSchedules = branch.AttentionSchedules,
                                 HomeService = branch.HomeService,
                                 ServiceType = branch.ServiceType,
                                 Latitude = branch.Latitude,
                                 Longitude = branch.Longitude,
                                 BranchDistance = branch.BranchDistance,
                                 CompanyPromotions = branch.CompanyPromotions
                             };

            IEnumerable<PLMClientsBusinessEntities.BranchDetailInfo> collect = branchList.Take(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PharmacyNumber"]));
            BECollection = collect.ToList();

            return BECollection.Count > 0 ? BECollection : null;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getCloseBranchesByCompany(int companyClientId, decimal latitude, decimal longitude)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            BECollection = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByCompanyClient(companyClientId);

            foreach (PLMClientsBusinessEntities.BranchDetailInfo branch in BECollection)
            {
                branch.BranchDistance = Convert.ToDecimal(this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude), 
                    Convert.ToDouble(branch.Latitude), Convert.ToDouble(branch.Longitude)));

                if (branch.Logo != null)
                {
                    branch.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl + "logoDetail/" + branch.Logo;
                    branch.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl;
                }

                branch.CompanyPromotions = PLMClientsBusinessLogicComponent.PromotionsBLC.Instance.getByCompanyClient(branch.CompanyClientId);
            }

            var branchList = from branch in BECollection
                         orderby branch.BranchDistance
                         select new  PLMClientsBusinessEntities.BranchDetailInfo
                         {
                             BranchId = branch.BranchId,
                             CompanyClientId = branch.CompanyClientId,
                             CompanyName = branch.CompanyName,
                             BranchKey = branch.BranchKey,
                             BranchName = branch.BranchName,
                             WebPage = branch.WebPage,
                             Email = branch.Email,
                             BaseUrl = branch.BaseUrl,
                             Logo = branch.Logo,
                             LogoDetail = branch.LogoDetail,
                             BranchActive = branch.BranchActive,
                             AddressId = branch.AddressId,
                             Street = branch.Street,
                             InternalNumber = branch.InternalNumber,
                             Suburb = branch.Suburb,
                             ZipCode = branch.ZipCode,
                             Location = branch.Location,
                             CountryId = branch.CountryId,
                             StateId = branch.StateId,
                             StateName = branch.StateName,
                             Lada = branch.Lada,
                             PhoneOne = branch.PhoneOne,
                             PhoneTwo = branch.PhoneTwo,
                             Ext = branch.Ext,
                             AttentionSchedules = branch.AttentionSchedules,
                             HomeService = branch.HomeService,
                             ServiceType = branch.ServiceType,
                             Latitude = branch.Latitude,
                             Longitude = branch.Longitude,
                             BranchDistance = branch.BranchDistance,
                             CompanyPromotions = branch.CompanyPromotions
                         };

            IEnumerable<PLMClientsBusinessEntities.BranchDetailInfo> collect = branchList.Take(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PharmacyNumber"]));
            BECollection = collect.ToList();

            return BECollection.Count > 0 ? BECollection : null;

        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getCloseBranchesByPrefix(string codePrefix, decimal latitude, decimal longitude)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = 
                BECollection = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByPrefix(codePrefix);

            if (BECollection.Count > 0)
            {
                foreach (PLMClientsBusinessEntities.BranchDetailInfo branch in BECollection)
                {
                    branch.BranchDistance = Convert.ToDecimal(this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude),
                        Convert.ToDouble(branch.Latitude), Convert.ToDouble(branch.Longitude)));

                    if (branch.Logo != null)
                    {
                        branch.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl + "logoDetail/" + branch.Logo;
                        branch.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl;
                    }

                    branch.CompanyPromotions = PLMClientsBusinessLogicComponent.PromotionsBLC.Instance.getByCompanyClient(branch.CompanyClientId);
                }
                var branchList = from branch in BECollection
                                 orderby branch.BranchDistance
                                 select new PLMClientsBusinessEntities.BranchDetailInfo
                                 {
                                     BranchId = branch.BranchId,
                                     CompanyClientId = branch.CompanyClientId,
                                     CompanyName = branch.CompanyName,
                                     BranchKey = branch.BranchKey,
                                     BranchName = branch.BranchName,
                                     WebPage = branch.WebPage,
                                     Email = branch.Email,
                                     BaseUrl = branch.BaseUrl,
                                     Logo = branch.Logo,
                                     LogoDetail = branch.LogoDetail,
                                     BranchActive = branch.BranchActive,
                                     AddressId = branch.AddressId,
                                     Street = branch.Street,
                                     InternalNumber = branch.InternalNumber,
                                     Suburb = branch.Suburb,
                                     ZipCode = branch.ZipCode,
                                     Location = branch.Location,
                                     CountryId = branch.CountryId,
                                     StateId = branch.StateId,
                                     StateName = branch.StateName,
                                     Lada = branch.Lada,
                                     PhoneOne = branch.PhoneOne,
                                     PhoneTwo = branch.PhoneTwo,
                                     Ext = branch.Ext,
                                     AttentionSchedules = branch.AttentionSchedules,
                                     HomeService = branch.HomeService,
                                     ServiceType = branch.ServiceType,
                                     Latitude = branch.Latitude,
                                     Longitude = branch.Longitude,
                                     BranchDistance = branch.BranchDistance,
                                     CompanyPromotions = branch.CompanyPromotions
                                 };

                IEnumerable<PLMClientsBusinessEntities.BranchDetailInfo> collect;

                if (BECollection[0].DisplayPharmacies != null)
                    collect = branchList.Take(Convert.ToInt32(BECollection[0].DisplayPharmacies));
                else
                    collect = branchList;

                BECollection = collect.ToList();
            }
            return BECollection.Count > 0 ? BECollection : null;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByPrefixByText(string codePrefix, int? stateId, string companyClients, string searchText)
        {
            if(codePrefix == null)
                throw new ArgumentException("The prefix does not exist.");

            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            BECollection = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByPrefixByText(codePrefix, stateId, companyClients, searchText);

            foreach (PLMClientsBusinessEntities.BranchDetailInfo branch in BECollection)
            {
                if (branch.Logo != null)
                {
                    branch.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl + "logoDetail/" + branch.Logo;
                    branch.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl;
                }

                branch.CompanyPromotions = PLMClientsBusinessLogicComponent.PromotionsBLC.Instance.getByCompanyClient(branch.CompanyClientId);
            }

            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getCloseBranchesByPrefixByCompanyType(string prefix, byte companyTypeId, decimal latitude, decimal longitude)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = 
                BECollection = PLMClientsDataAccessComponent.CompanyClientBranchesDALC.Instance.getBranchesByPrefixByCompanyType(prefix, companyTypeId);

            if (BECollection.Count > 0)
            {
                foreach (PLMClientsBusinessEntities.BranchDetailInfo branch in BECollection)
                {
                    branch.BranchDistance = Convert.ToDecimal(this.calculateDistance(Convert.ToDouble(latitude), Convert.ToDouble(longitude),
                        Convert.ToDouble(branch.Latitude), Convert.ToDouble(branch.Longitude)));

                    if (branch.Logo != null)
                    {
                        branch.LogoDetail = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl + "logoDetail/" + branch.Logo;
                        branch.BaseUrl = System.Configuration.ConfigurationManager.AppSettings["PharmacyBaseUrl"] + branch.BaseUrl;
                    }

                    branch.CompanyPromotions = PLMClientsBusinessLogicComponent.PromotionsBLC.Instance.getByCompanyClient(branch.CompanyClientId);

                }
                var branchList = from branch in BECollection
                                 orderby branch.BranchDistance
                                 select new PLMClientsBusinessEntities.BranchDetailInfo
                                 {
                                     BranchId = branch.BranchId,
                                     CompanyClientId = branch.CompanyClientId,
                                     CompanyName = branch.CompanyName,
                                     BranchKey = branch.BranchKey,
                                     BranchName = branch.BranchName,
                                     WebPage = branch.WebPage,
                                     Email = branch.Email,
                                     BaseUrl = branch.BaseUrl,
                                     Logo = branch.Logo,
                                     LogoDetail = branch.LogoDetail,
                                     BranchActive = branch.BranchActive,
                                     AddressId = branch.AddressId,
                                     Street = branch.Street,
                                     InternalNumber = branch.InternalNumber,
                                     Suburb = branch.Suburb,
                                     ZipCode = branch.ZipCode,
                                     Location = branch.Location,
                                     CountryId = branch.CountryId,
                                     StateId = branch.StateId,
                                     StateName = branch.StateName,
                                     Lada = branch.Lada,
                                     PhoneOne = branch.PhoneOne,
                                     PhoneTwo = branch.PhoneTwo,
                                     Ext = branch.Ext,
                                     AttentionSchedules = branch.AttentionSchedules,
                                     HomeService = branch.HomeService,
                                     ServiceType = branch.ServiceType,
                                     Latitude = branch.Latitude,
                                     Longitude = branch.Longitude,
                                     BranchDistance = branch.BranchDistance,
                                     CompanyPromotions = branch.CompanyPromotions
                                 };

                IEnumerable<PLMClientsBusinessEntities.BranchDetailInfo> collect;

                if (BECollection[0].DisplayPharmacies != null)
                    collect = branchList.Take(Convert.ToInt32(BECollection[0].DisplayPharmacies));
                else
                    collect = branchList;

                BECollection = collect.ToList();
            }
            return BECollection.Count > 0 ? BECollection : null;
        }

        #endregion

        #region Private Methods

        private int getStateByKey(string state)
        {
            PLMClientsBusinessEntities.StateInfo stateInfo = new PLMClientsBusinessEntities.StateInfo();

            stateInfo = PLMClientsDataAccessComponent.StatesDALC.Instance.getByShortName(state);
            return stateInfo.StateId;
        }

        private double calculateDistance(double clientLatitude, double clientLongitud, double branchLatitude, double branchLongitude)
        {
            double PI_OVER_180 = Math.PI / 180.0;
            double a = 6378137; 
            double b  = 6356752.3142;
            double f = 1/298.257223563;

            double L = (branchLongitude - clientLongitud) * PI_OVER_180;
            double U1 = Math.Atan((1 - f) * Math.Tan(clientLatitude * PI_OVER_180));
            double U2 = Math.Atan((1 - f) * Math.Tan(branchLatitude * PI_OVER_180));

            double sinU1 = Math.Sin(U1);
            double cosU1 = Math.Cos(U1);

            double sinU2 = Math.Sin(U2);
            double cosU2 = Math.Cos(U2);

            double lambda = L;
            double lambdaP = 2 * Math.PI;

            double cosSqAlpha = 0;
            double cos2SigmaM = 0;
            double cosSigma = 0;
            double sinSigma = 0;
            double sigma = 0;

            int iterLimit = 20;

            while (Math.Abs(lambda - lambdaP) > Math.Pow(1,-12) && --iterLimit > 0)
            {
                double sinLambda = Math.Sin(lambda);
                double cosLambda = Math.Cos(lambda);
                sinSigma = Math.Sqrt((cosU2*sinLambda) * (cosU2*sinLambda) + (cosU1*sinU2-sinU1*cosU2*cosLambda) * (cosU1*sinU2-sinU1*cosU2*cosLambda));
                
                if (sinSigma==0) 
                    return 0;

                cosSigma = sinU1*sinU2 + cosU1*cosU2*cosLambda;
                sigma = Math.Atan2(sinSigma, cosSigma);
                double sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
                cosSqAlpha = 1 - sinAlpha*sinAlpha;
                cos2SigmaM = cosSigma - 2*sinU1*sinU2/cosSqAlpha;
                if (Double.IsNaN(cos2SigmaM))
                    cos2SigmaM = 0;
                double C = f/16*cosSqAlpha*(4+f*(4-3*cosSqAlpha));
                lambdaP = lambda;
                lambda = L + (1-C) * f * sinAlpha * (sigma + C*sinSigma*(cos2SigmaM+C*cosSigma*(-1+2*cos2SigmaM*cos2SigmaM)));
            }
            
            if (iterLimit==0) 
                return 0;

            double uSq = cosSqAlpha * (a*a - b*b) / (b*b);
            double A = 1 + uSq/16384*(4096+uSq*(-768+uSq*(320-175*uSq)));
            double B = uSq/1024 * (256+uSq*(-128+uSq*(74-47*uSq)));
            double deltaSigma = B*sinSigma*(cos2SigmaM+B/4*(cosSigma*(-1+2*cos2SigmaM*cos2SigmaM)-B/6*cos2SigmaM*(-3+4*sinSigma*sinSigma)*(-3+4*cos2SigmaM*cos2SigmaM)));
            double s = b*A*(sigma-deltaSigma);
            
            return s/1000;
        }
        
        #endregion

        public static readonly CompanyClientBranchesBLC Instance = new CompanyClientBranchesBLC();

    }
}
