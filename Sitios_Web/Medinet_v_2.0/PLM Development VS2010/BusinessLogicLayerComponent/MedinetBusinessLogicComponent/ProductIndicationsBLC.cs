using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductIndicationsBLC
    {

        #region Constructors

        private ProductIndicationsBLC() { }

        #endregion

        #region Public Methods

        public bool check(ProductIndicationInfo BEntity)
        {
            return (MedinetDataAccessComponent.ProductIndicationsDALC.Instance.check(BEntity) == null ? true : false);
        }
                
        public void addIndication(ProductIndicationInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductIndications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(IndicationId," + BEntity.IndicationId + ")";

            MedinetDataAccessComponent.ProductIndicationsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeIndication(ProductIndicationInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductIndications);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(IndicationId," + BEntity.IndicationId + ")";

            MedinetDataAccessComponent.ProductIndicationsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public List<MedinetBusinessEntries.ProductIndicationInfo> getProductIndications(int indicationId)
        {
            return MedinetDataAccessComponent.ProductIndicationsDALC.Instance.getProductIndications(indicationId);
        }

        public MedinetBusinessEntries.ProductIndicationInfo getProductIndication(int productId, int indicationId)
        {
            return MedinetDataAccessComponent.ProductIndicationsDALC.Instance.getProductIndication(productId, indicationId);
        }

        #endregion

        public static readonly ProductIndicationsBLC Instance = new ProductIndicationsBLC();

    }
}
