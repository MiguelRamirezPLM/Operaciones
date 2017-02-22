using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductOMSTherapeuticsBLC
    {
        #region Constructors

        private ProductOMSTherapeuticsBLC() { }

        #endregion

        #region Public Methods

        public void addTherapeutic(ProductTherapeuticOMSInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductTherapeuticOMS);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId" + BEntity.PharmaFormId +
            ");(TherapeuticOMSId," + BEntity.TherapeuticOMSId + ")";
            
            MedinetDataAccessComponent.ProductOMSTherapeuticsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeTherapeutic(ProductTherapeuticOMSInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductTherapeuticOMS);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId" + BEntity.PharmaFormId +
            ");(TherapeuticOMSId," + BEntity.TherapeuticOMSId + ")";
            
            MedinetDataAccessComponent.ProductOMSTherapeuticsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public bool checkTherapeuticByProduct(int therapeuticOMSId, int productId, int pharmaFormId)
        {
            if (therapeuticOMSId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The Therapeutic or product or pharmaform does not exist.");

            return (MedinetDataAccessComponent.ProductOMSTherapeuticsDALC.Instance.getTherapeuticByProduct(therapeuticOMSId, productId, pharmaFormId) == null ? false : true);
        }

        #endregion

        public static readonly ProductOMSTherapeuticsBLC Instance = new ProductOMSTherapeuticsBLC();

    }
}
