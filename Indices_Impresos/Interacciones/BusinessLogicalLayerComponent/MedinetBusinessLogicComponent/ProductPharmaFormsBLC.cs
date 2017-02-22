using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductPharmaFormsBLC
    {

        #region Constructors

        private ProductPharmaFormsBLC() { }

        #endregion

        #region Public Methods

        public void addPharmaForm(ProductPharmaFormInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmaForms);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ")";
            
            MedinetDataAccessComponent.ProductPharmaFormsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removePharmaForm(ProductPharmaFormInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmaForms);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ")";

            MedinetDataAccessComponent.ProductPharmaFormsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void modifyPharmaForm(ProductPharmaFormInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            
            MedinetDataAccessComponent.ProductPharmaFormsDALC.Instance.update(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmaForms);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ")";
            activityLog.FieldsAffected = "(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public ProductPharmaFormInfo getProductPF(int productId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.ProductPharmaFormsDALC.Instance.getOne(productId,pharmaFormId);
        }

        #endregion

        public static readonly ProductPharmaFormsBLC Instance = new ProductPharmaFormsBLC();

    }
}
