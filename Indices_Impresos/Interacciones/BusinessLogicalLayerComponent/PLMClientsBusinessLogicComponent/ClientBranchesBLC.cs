using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public sealed class ClientBranchesBLC
    {
        #region Constructors

        private ClientBranchesBLC() { }

        #endregion

        #region Public Methods

        //public List<ProjectActivitiesInfo> getProjectActivities()
        //{
        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getProjects();
        //}

        public int insert(PLMClientsBusinessEntities.CompanyClientBranchesInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(BranchId," + BEntity.BranchId + ");(AddressId," + BEntity.AddressId + ")";
            activityLog.FieldsAffected = "(BranchKey," + BEntity.BranchKey + ");(BranchName," + BEntity.BranchName + ");(WebPage," + BEntity.WebPage +
                                            ");(Email," + BEntity.Email + ");(Logo," + BEntity.Logo + ");(CompanyClientId," + BEntity.CompanyClientId +
                                            ");(AddressId," + BEntity.AddressId + ");(AttentionSchedules," + BEntity.AttentionSchedules +
                                            ");(HomeService," + BEntity.HomeService + ")";
            return PLMClientsDataAccessComponent.ClientBranchesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

     
        public PLMClientsBusinessEntities.CompanyClientBranchesInfo getCompanyBranches(int branchId)
        {
            return PLMClientsDataAccessComponent.ClientBranchesDALC.Instance.getOne(branchId);
        }

        public void update(PLMClientsBusinessEntities.CompanyClientBranchesInfo businessEntity, int userId, string hash)
        {

            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(BranchId," + businessEntity.BranchId + ");(AddressId," + businessEntity.AddressId + ")";   
            activityLog.FieldsAffected = "(BranchKey," + businessEntity.BranchKey + ");(BranchName," + businessEntity.BranchName + ");(WebPage," + businessEntity.WebPage + 
                                            ");(Email," + businessEntity.Email + ");(Logo," + businessEntity.Logo + ");(CompanyClientId," + businessEntity.CompanyClientId + 
                                            ");(AddressId," + businessEntity.AddressId + ");(AttentionSchedules," + businessEntity.AttentionSchedules + 
                                            ");(HomeService," + businessEntity.HomeService + ")";
            PLMClientsDataAccessComponent.ClientBranchesDALC.Instance.update(businessEntity);


            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
         
   
    
    //public void update(AddressInfo businessEntity)
        //{
        //    PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.update(businessEntity);
        //}

        //public AddressInfo getProjectActivity(int activityId)
        //{
        //    if (activityId <= 0)
        //        throw new ArgumentException("The activity does not exist.");

        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getOne(activityId);
        //}

        #endregion

        public static readonly ClientBranchesBLC Instance = new ClientBranchesBLC();
    }
}
