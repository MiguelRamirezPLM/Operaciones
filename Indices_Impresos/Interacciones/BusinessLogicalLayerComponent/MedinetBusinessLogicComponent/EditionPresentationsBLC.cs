using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class EditionPresentationsBLC
    {

        #region Constructors

        private EditionPresentationsBLC() { }

        #endregion

        #region Public Methods

        public void addEditionPresentation(EditionPresentationsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionPresentations);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(PresentationId," + BEntity.PresentationId + ")";

            MedinetDataAccessComponent.EditionPresentationsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeEditionPresentation(EditionPresentationsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionPresentations);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(PresentationId," + BEntity.PresentationId + ")";

            MedinetDataAccessComponent.EditionPresentationsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public bool checkEditionPresentation(int editionId, int presentationId)
        {
            return MedinetDataAccessComponent.EditionPresentationsDALC.Instance.chechEditionPresentation(editionId, presentationId);
        }

        #endregion

        public static readonly EditionPresentationsBLC Instance = new EditionPresentationsBLC();

    }
}
