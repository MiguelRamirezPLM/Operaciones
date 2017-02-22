using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public sealed class DistributionsPharmaBLC
    {
        #region Constructors

        private DistributionsPharmaBLC() { }

        #endregion

        #region Public Methods


        public void addDistributionPharma(DistributionsPharmaInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DistributionPharmacies);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DistributionId," + BEntity.DistributionId + ");(PrefixId," + BEntity.PrefixId + ");(TargetId," +
                BEntity.TargetId + ");(CompanyClientId," + BEntity.CompanyClientId + ")";

            PLMClientsDataAccessComponent.DistributionsPharmaDALC.Instance.insert(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<DistributionCodePrefixesPharmaInfo> getCompanyDistribution(byte countryId, int prefixId, int distributionId, int targetId)
        {
            return PLMClientsDataAccessComponent.DistributionCodePrefixesPharmaDALC.Instance.getCompanyDistribution(countryId, distributionId, prefixId, targetId);
        }


        public void deleteDistribution( DistributionsPharmaInfo bussinesEntity, int userId, string hash)
        {
            PLMClientsDataAccessComponent.DistributionsPharmaDALC.Instance.delete(bussinesEntity);
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DistributionPharmacies);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(DistributionId," + bussinesEntity.DistributionId + ");(PrefixId," + bussinesEntity.PrefixId + ");(TargetId," +
             bussinesEntity.TargetId + ");(CompanyClientId," + bussinesEntity.CompanyClientId + ")";

           
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }

       
        //public AddressInfo getProjectActivity(int activityId)
        //{
        //    if (activityId <= 0)
        //        throw new ArgumentException("The activity does not exist.");

        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getOne(activityId);
        //}

        #endregion

        public static readonly DistributionsPharmaBLC Instance = new DistributionsPharmaBLC();
    }
}
