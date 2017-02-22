using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ProductInteractionsBLC
    {

        #region Constructors

        private ProductInteractionsBLC() { }

        #endregion

        #region Public Methods

        public void addProductInteraction(ProductInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(SubstanceInteractId," + BEntity.SubstanceInteractId + ")";

            MedinetDataAccessComponent.ProductInteractionsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductInteraction(ProductInteractionsInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(SubstanceInteractId," + BEntity.SubstanceInteractId + ")";

            MedinetDataAccessComponent.ProductInteractionsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<ProductInteractionsInfo> getProductInteractions(int productId, int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductInteractionsDALC.Instance.getProductInteractions(productId, activeSubstanceId);
        }

        public List<ProductInteractionsInfo> getProductInteractions(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductInteractionsDALC.Instance.getProductInteractions(activeSubstanceId);
        }

        #endregion

        public static readonly ProductInteractionsBLC Instance = new ProductInteractionsBLC();

    }
}
