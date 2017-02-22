using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class OtherInteractionsBLC
    {

        #region Constructors

        private OtherInteractionsBLC() { }

        #endregion

        #region Public Methods

        public void addOtherInteraction(OtherInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.OtherInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(ElementId," + BEntity.ElementId + ")";

            MedinetDataAccessComponent.OtherInteractionsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeOtherInteraction(OtherInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey; 
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.OtherInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(ElementId," + BEntity.ElementId + ")";

            MedinetDataAccessComponent.OtherInteractionsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<OtherInteractionsInfo> getOtherInteractions(int productId, int activeSubstanceId)
        {
            return MedinetDataAccessComponent.OtherInteractionsDALC.Instance.getOtherInteractions(productId, activeSubstanceId);
        }

        public List<OtherInteractionsInfo> getOtherInteractions(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.OtherInteractionsDALC.Instance.getOtherInteractions(activeSubstanceId);
        }

        #endregion

        public static readonly OtherInteractionsBLC Instance = new OtherInteractionsBLC();

    }
}
