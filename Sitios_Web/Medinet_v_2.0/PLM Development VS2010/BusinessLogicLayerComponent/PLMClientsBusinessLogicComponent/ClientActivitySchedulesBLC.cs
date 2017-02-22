using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientActivitySchedulesBLC
    {

        #region Constructors

        private ClientActivitySchedulesBLC() { }

        #endregion

        #region Public Methods

        public void addClientActivitySchedules(string code, byte activityTypeId, string activitySchedule)
        {
            if (activityTypeId <= 0 || activitySchedule.Trim() == "" || activitySchedule == null)
                throw new ArgumentException("The Activity Type or Activity Schedule is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientActivitySchedulesInfo clientActivitySchedule = new ClientActivitySchedulesInfo();
                clientActivitySchedule.ClientId = targetClient.ClientId;
                clientActivitySchedule.CodeId = targetClient.CodeId;
                clientActivitySchedule.DeviceId = targetClient.DeviceId;
                clientActivitySchedule.TargetId = targetClient.TargetId;
                clientActivitySchedule.ActivityTypeId = activityTypeId;
                clientActivitySchedule.ActivitySchedule = activitySchedule;

                PLMClientsDataAccessComponent.ClientActivitySchedulesDALC.Instance.insert(clientActivitySchedule);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        public void updateClientActivitySchedules(string code, byte activityTypeId, string activitySchedule)
        {
            if (activityTypeId <= 0 || activitySchedule.Trim() == "" || activitySchedule == null)
                throw new ArgumentException("The Activity Type or Activity Schedule is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientActivitySchedulesInfo clientActivitySchedule = new ClientActivitySchedulesInfo();
                clientActivitySchedule.ClientId = targetClient.ClientId;
                clientActivitySchedule.CodeId = targetClient.CodeId;
                clientActivitySchedule.DeviceId = targetClient.DeviceId;
                clientActivitySchedule.TargetId = targetClient.TargetId;
                clientActivitySchedule.ActivityTypeId = activityTypeId;
                clientActivitySchedule.ActivitySchedule = activitySchedule;

                PLMClientsDataAccessComponent.ClientActivitySchedulesDALC.Instance.update(clientActivitySchedule);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        public void deleteClientActivitySchedules(string code, byte activityTypeId, string activitySchedule)
        {
            if (activityTypeId <= 0 || activitySchedule.Trim() == "" || activitySchedule == null)
                throw new ArgumentException("The Activity Type or Activity Schedule is not valid.");

            PLMClientsBusinessEntities.TargetClientCodesInfo targetClient =
                PLMClientsBusinessLogicComponent.TargetClientCodesBLC.Instance.getTargetClientByCode(code);

            if (targetClient != null)
            {
                PLMClientsBusinessEntities.ClientActivitySchedulesInfo clientActivitySchedule = new ClientActivitySchedulesInfo();
                clientActivitySchedule.ClientId = targetClient.ClientId;
                clientActivitySchedule.CodeId = targetClient.CodeId;
                clientActivitySchedule.DeviceId = targetClient.DeviceId;
                clientActivitySchedule.TargetId = targetClient.TargetId;
                clientActivitySchedule.ActivityTypeId = activityTypeId;

                PLMClientsDataAccessComponent.ClientActivitySchedulesDALC.Instance.delete(clientActivitySchedule);
            }
            else
                throw new ArgumentException("The code is not valid.");
        }

        #endregion

        public static readonly ClientActivitySchedulesBLC Instance = new ClientActivitySchedulesBLC();

    }
}
