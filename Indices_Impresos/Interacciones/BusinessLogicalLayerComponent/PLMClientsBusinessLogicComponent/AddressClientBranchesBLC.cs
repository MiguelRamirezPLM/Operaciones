using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
   public sealed class AddressClientBranchesBLC
    {
        #region Constructors

        private AddressClientBranchesBLC() { }

        #endregion

        #region Public Methods

        //public List<ProjectActivitiesInfo> getProjectActivities()
        //{
        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getProjects();
        //}

        public int insert(AddressInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(AddressId," + BEntity.AddressId + ")";
            activityLog.FieldsAffected = "(Street," + BEntity.Street + ");(Suburb," + BEntity.Suburb + ");(ZipCode," + BEntity.ZipCode +
                                            ");(Location," + BEntity.Location + ");(CountryId," + BEntity.CountryId + ");(StateId," + BEntity.StateId +
                                            ");(StateName," + BEntity.StateName + ");(PhoneOne," + BEntity.PhoneOne + ");(PhoneTwo," + BEntity.PhoneTwo +
                                            ");(Latitude," + BEntity.Latitude + ");(Longitude," + BEntity.Longitude + ")";

            return PLMClientsDataAccessComponent.AddressClientBranchesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void update(AddressInfo businessEntity, int userId, string hash)
        {

            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CompanyClients);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(AddressId," + businessEntity.AddressId + ")";
            activityLog.FieldsAffected = "(Street," + businessEntity.Street + ");(Suburb," + businessEntity.Suburb + ");(ZipCode," + businessEntity.ZipCode +
                                            ");(Location," + businessEntity.Location + ");(CountryId," + businessEntity.CountryId + ");(StateId," + businessEntity.StateId +
                                            ");(StateName," + businessEntity.StateName + ");(PhoneOne," + businessEntity.PhoneOne + ");(PhoneTwo," + businessEntity.PhoneTwo +
                                            ");(Latitude," + businessEntity.Latitude + ");(Longitude," + businessEntity.Longitude + ")";


            PLMClientsDataAccessComponent.AddressClientBranchesDALC.Instance.update(businessEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public PLMClientsBusinessEntities.AddressInfo getCompanyAddress(int AddressId)
        {
            return PLMClientsDataAccessComponent.AddressClientBranchesDALC.Instance.getOne( AddressId);
        }
       
       
       //public AddressInfo getProjectActivity(int activityId)
        //{
        //    if (activityId <= 0)
        //        throw new ArgumentException("The activity does not exist.");

        //    return PLMUsersDataAccessComponent.ProjectActivitiesDALC.Instance.getOne(activityId);
        //}

        #endregion

        public static readonly AddressClientBranchesBLC Instance = new AddressClientBranchesBLC();
    }
}
