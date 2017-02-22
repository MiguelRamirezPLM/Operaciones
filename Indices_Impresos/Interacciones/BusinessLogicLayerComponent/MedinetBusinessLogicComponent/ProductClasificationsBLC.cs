using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;
using PLMUsersBusinessLogicComponent;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductClasificationsBLC
    {

        #region Constructors

        private ProductClasificationsBLC() { }

        #endregion

        #region Public Methods

        public bool productClasf(int productId, int editionId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.ProductClasificationsDALC.Instance.participate(productId, editionId, pharmaFormId);
        }

        public ProductClasificationInfo getParticipantProduct(int productId, int editionId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.ProductClasificationsDALC.Instance.getOne(productId,editionId,pharmaFormId);
        }

        public void addProductClasf(ProductClasificationInfo BEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductClasifications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(EditionId," + BEntity.EditionId + ");(PharmaFormId,"
                + BEntity.PharmaFormId + ")";

            MedinetDataAccessComponent.ProductClasificationsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductClasf(ProductClasificationInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductClasifications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(EditionId," + BEntity.EditionId + ");(PharmaFormId,"
                + BEntity.PharmaFormId + ")";

            MedinetDataAccessComponent.ProductClasificationsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly ProductClasificationsBLC Instance = new ProductClasificationsBLC();

    }
}
