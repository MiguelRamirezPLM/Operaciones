using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class PharmaGroupInteractionsBLC
    {

        #region Constructors

        private PharmaGroupInteractionsBLC() { }

        #endregion

        #region Public Methods

        public void addPharmaGroupInteraction(PharmaGroupInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.PharmaGroupInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(PharmaGroupId," + BEntity.PharmaGroupId + ")";

            MedinetDataAccessComponent.PharmaGroupInteractionsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removePharmaGroupInteraction(PharmaGroupInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.PharmaGroupInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(PharmaGroupId," + BEntity.PharmaGroupId + ")";

            MedinetDataAccessComponent.PharmaGroupInteractionsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<PharmaGroupInteractionsInfo> getPharmaGroupInteractions(int productId, int activeSubstanceId)
        {
            return MedinetDataAccessComponent.PharmaGroupInteractionsDALC.Instance.getPharmaGroupInteractions(productId, activeSubstanceId);
        }

        public List<PharmaGroupInteractionsInfo> getPharmaGroupInteractions(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.PharmaGroupInteractionsDALC.Instance.getPharmaGroupInteractions(activeSubstanceId);
        }

        #endregion

        public static readonly PharmaGroupInteractionsBLC Instance = new PharmaGroupInteractionsBLC();

    }
}
