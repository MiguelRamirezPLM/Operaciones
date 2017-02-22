using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class ProductContentSubstancesBLC
    {

        #region Constructors

        private ProductContentSubstancesBLC() { }

        #endregion

        #region Public Methods

        public void addProductContentSubstance(ProductContentSubstancesInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductContentSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";

            MedinetDataAccessComponent.ProductContentSubstancesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductContentSubstance(ProductContentSubstancesInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductContentSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductContentId," + BEntity.ProductContentId + ");(ProductId," + BEntity.ProductId + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";

            MedinetDataAccessComponent.ProductContentSubstancesDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<ProductContentSubstancesInfo> getProductContentSubstances(int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductContentSubstancesDALC.Instance.getProductContentSubstances(activeSubstanceId);
        }

        public List<ProductContentSubstancesInfo> getProductContentSubstances(int productId, int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductContentSubstancesDALC.Instance.getProductContentSubstances(productId, activeSubstanceId);
        }

        #endregion

        public static readonly ProductContentSubstancesBLC Instance = new ProductContentSubstancesBLC();

    }
}
