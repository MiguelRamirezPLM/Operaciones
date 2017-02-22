using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class IndicationsBLC
    {

        #region Constructors

        private IndicationsBLC() { }

        #endregion

        #region Public Methods

        public int addIndication(MedinetBusinessEntries.IndicationInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.IndicationId = MedinetDataAccessComponent.IndicationsDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Indications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(IndicationId," + BEntity.IndicationId + ")";
            activityLog.FieldsAffected = "(ParentId," + BEntity.ParentId + ");(Description," + BEntity.Description + ");(Active,"
                + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return BEntity.IndicationId;
        }

        public void updateIndication(MedinetBusinessEntries.IndicationInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Indications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(IndicationId," + BEntity.IndicationId + ")";
            activityLog.FieldsAffected = "(ParentId," + BEntity.ParentId + ");(Description," + BEntity.Description + ");(Active,"
                + BEntity.Active + ")";

            MedinetDataAccessComponent.IndicationsDALC.Instance.update(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public MedinetBusinessEntries.IndicationInfo getIndication(int indicationId)
        {
            return MedinetDataAccessComponent.IndicationsDALC.Instance.getOne(indicationId);
        }

        public List<IndicationInfo> getAllIndicationsByFilter(int indicationId, string description)
        {
            return MedinetDataAccessComponent.IndicationsDALC.Instance.getAllByFilter(indicationId, description);
        }

        #endregion

        public static readonly IndicationsBLC Instance = new IndicationsBLC();

    }
}
