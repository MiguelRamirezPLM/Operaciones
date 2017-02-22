using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductIcdBLC
    {
        #region Constructors

        private ProductIcdBLC() { }

        #endregion


        #region Public Methods

        public int addICD(ProductICDInfo BEntity, int Userid, string hash)
        {
            
            BEntity.Returns =   MedinetDataAccessComponent.ProductIcdDALC.Instance.insert(BEntity);
            this.ActivityLogs(BEntity, Userid, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);

            return BEntity.Returns;
          
             
        }

        public void removeICD(ProductICDInfo BEntity, int Userid, string hash)
        {
            MedinetDataAccessComponent.ProductIcdDALC.Instance.delete(BEntity);
            this.ActivityLogs(BEntity, Userid, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public bool checkICDByProduct(int ICDId, int productId, int pharmaFormId)
        {
            if (productId <= 0 || ICDId <= 0)
                throw new ArgumentException("The Therapeutic or product or pharmaform does not exist.");

            return (MedinetDataAccessComponent.ProductIcdDALC.Instance.getICDByIDByProduct(ICDId, productId,pharmaFormId) == null ? false : true);
        }

        //public string InsertICD(List<ProductICDInfo> icds) {
        //    foreach (ProductICDInfo item in icds)
        //    {
        //        if (!this.checkICDByProduct(item.ICDId,item.ProductId, item.PharmaFormId))
        //        {
        //            this.addICD(item);
        //        }    
        //    }
        //    return "";
        //}

        public MedinetBusinessEntries.ProductICDInfo checkIcd(int ICDId, int productId, int pharmaFormId)
        {
           

            return MedinetDataAccessComponent.ProductIcdDALC.Instance.getICDByIDByProduct(ICDId, productId, pharmaFormId);
        }

        public void ActivityLogs(MedinetBusinessEntries.ProductICDInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductICD);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(ICDId," + GenericBEntity.ICDId + ")";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
   
        #endregion
        public static readonly ProductIcdBLC Instance = new ProductIcdBLC();
    }
}
