using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
   public class CompanyClientPharmaBLC
    {
        #region Constructors

        private CompanyClientPharmaBLC() { }

        #endregion

        #region Public Methods

        public List<CompanyClientInfo> getAllCompanyTypes(int country, int prefixId, int distributionId, int targetId)
        {
            return PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.getAllCompanyPharma(country, prefixId, distributionId, targetId);
        }


        public List<PLMClientsBusinessEntities.CompanyClientInfo> getCompany(byte countryId)
        {
            return PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.getCompany(countryId);
        }

        public List<CompanyClientInfo> getAllCompanyPharma(int country, int prefixId, int distributionId, int targetId)
        {
            return PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.getAllCompanyPharma(country, prefixId, distributionId, targetId);
        }

        public PLMClientsBusinessEntities.CompanyClientInfo getCompanyClient(int companyClientId)
        {
            return PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.getOne(companyClientId);
        }

        public int insert(CompanyClientInfo BEntity, int userId, string hash)
        {

            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            BEntity.CompanyClientId = PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(CompanyClientId," + BEntity.CompanyClientId + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            return BEntity.CompanyClientId;
        }

        public void update(PLMClientsBusinessEntities.CompanyClientInfo businessEntity, int userId, string hash)
        {

            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(CompanyClientId," + businessEntity.CompanyClientId + ")";
            activityLog.FieldsAffected = "(CompanyName," + businessEntity.CompanyName + ");(ShortName," + businessEntity.ShortName + ")";
            
            PLMClientsDataAccessComponent.CompanyClientPharmaDALC.Instance.update(businessEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
        #endregion
        public static readonly CompanyClientPharmaBLC Instance = new CompanyClientPharmaBLC();
    }
}