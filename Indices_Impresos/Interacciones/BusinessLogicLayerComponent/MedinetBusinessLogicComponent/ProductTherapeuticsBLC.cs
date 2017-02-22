using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductTherapeuticsBLC
    {

        #region Constructors

        private ProductTherapeuticsBLC() { }

        #endregion

        #region Public Methods

        public void addTherapeutic(ProductTherapeuticInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductTherapeutic);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId" + BEntity.PharmaFormId +
            ");(TherapeuticId," + BEntity.TherapeuticId + ")";
            
            MedinetDataAccessComponent.ProductTherapeuticsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeTherapeutic(ProductTherapeuticInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductTherapeutic);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId" + BEntity.PharmaFormId +
            ");(TherapeuticId," + BEntity.TherapeuticId + ")";
            
            MedinetDataAccessComponent.ProductTherapeuticsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public bool checkTherapeuticByProduct(int therapeuticId, int productId, int pharmaFormId)
        {
            if (therapeuticId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The Therapeutic or product or pharmaform does not exist.");

            return (MedinetDataAccessComponent.ProductTherapeuticsDALC.Instance.getTherapeuticByProduct(therapeuticId, productId, pharmaFormId) == null ? false : true);
        }

        #endregion

        public static readonly ProductTherapeuticsBLC Instance = new ProductTherapeuticsBLC();

    }
}
