using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductSubstancesBLC
    {

        #region Constructors

        private ProductSubstancesBLC() { }

        #endregion

        #region Public Methods

        public void addSubstance(ProductSubstanceInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";
            
            MedinetDataAccessComponent.ProductSubstancesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeSubstance(ProductSubstanceInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductSubstances);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ")";
            
            MedinetDataAccessComponent.ProductSubstancesDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public ProductSubstanceInfo getProductSubstance(int productId, int activeSubstanceId)
        {
            return MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.getProductSubstance(productId, activeSubstanceId);
        }

        public List<ProductSubstanceInfo> getProductSubstances(int activeSubstanceId)
        {
            return MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.getProductSubstances(activeSubstanceId);
        }

        public bool checkSubstance(int activeSubstanceId)
        {
            if (activeSubstanceId <= 0)
                throw new ArgumentException("The substance does not exist.");

            return MedinetDataAccessComponent.ProductSubstancesDALC.Instance.checkSubstance(activeSubstanceId);
        }

        public bool checkSubstance(int productId, int activeSubstanceId)
        {
            if (productId <= 0 || activeSubstanceId <= 0)
                throw new ArgumentException("The product or substance does not exist.");

            return MedinetDataAccessComponent.ProductSubstancesDALC.Instance.checkSubstance(productId, activeSubstanceId);
        }

        #endregion

        public static readonly ProductSubstancesBLC Instance = new ProductSubstancesBLC();

    }
}
