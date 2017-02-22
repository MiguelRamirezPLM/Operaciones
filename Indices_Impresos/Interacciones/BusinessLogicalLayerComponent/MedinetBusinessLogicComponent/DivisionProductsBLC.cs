using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class DivisionProductsBLC 
    {
        #region Constructors

        private DivisionProductsBLC() { }

        #endregion

        #region Public Methods

        public DivisionProductInfo assigned(int productId, int pharmaFormId, int countryId, int laboratoryId)
        {
            return MedinetDataAccessComponent.DivisionProductsDALC.Instance.assigned(productId, pharmaFormId, countryId, laboratoryId);
        }

        public void addProductToDivision(DivisionProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(DivisionId," + BEntity.DivisionId + ")";
            
            MedinetDataAccessComponent.DivisionProductsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductFromDivision(DivisionProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(DivisionId," + BEntity.DivisionId + ")";

            MedinetDataAccessComponent.DivisionProductsDALC.Instance.delete(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void assignedProductToDivision(DivisionProductInfo BEntity, int divisionId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            MedinetDataAccessComponent.DivisionProductsDALC.Instance.delete(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(DivisionId," + BEntity.DivisionId + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            BEntity.DivisionId = divisionId;
            MedinetDataAccessComponent.DivisionProductsDALC.Instance.insert(BEntity);

            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(DivisionId," + BEntity.DivisionId + ")";
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public DivisionProductInfo getDivision(int productId, int pharmaFormId, int divisionId)
        {
            return MedinetDataAccessComponent.DivisionProductsDALC.Instance.getOne(productId, pharmaFormId, divisionId);
        }

        #endregion

        public static readonly DivisionProductsBLC Instance = new DivisionProductsBLC();

        
    }
}
