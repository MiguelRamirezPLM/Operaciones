using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ActiveSubstancesBLC
    {

        #region Constructors

        private ActiveSubstancesBLC() { }

        #endregion

        #region Public Methods

        public int addActiveSubstance(MedinetBusinessEntries.ActiveSubstanceInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.ActiveSubstanceId = MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ActiveSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";
            activityLog.FieldsAffected = "(Description," + BEntity.Description + ");(EnglishDescription," + BEntity.EnglishDescription + ");(Enunciative,"
                + BEntity.Enunciative + ");(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return BEntity.ActiveSubstanceId;
        }

        public void updateActiveSubstance(MedinetBusinessEntries.ActiveSubstanceInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ActiveSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";
            activityLog.FieldsAffected = "(Description," + BEntity.Description + ");(EnglishDescription," + BEntity.EnglishDescription + ");(Enunciative,"
                + BEntity.Enunciative + ");(Active," + BEntity.Active + ")";

            MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.update(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public MedinetBusinessEntries.ActiveSubstanceInfo checkSubstanceByName(string description)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.checkByName(description);
        }

        public MedinetBusinessEntries.ActiveSubstanceInfo getActiveSubstance(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getOne(activeSubstanceId);
        }

        public List<ActiveSubstanceInfo> getAllSubstancesByFilter(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByFilter(activeSubstanceId);
        }

        public List<ActiveSubstanceInfo> getAllSubstancesByFilter(int activeSubstanceId, string description)
        {
            return MedinetDataAccessComponent.ActiveSubstancesDALC.Instance.getAllByFilter(activeSubstanceId, description);
        }

        #endregion

        public static readonly ActiveSubstancesBLC Instance = new ActiveSubstancesBLC();
         
    }
}
