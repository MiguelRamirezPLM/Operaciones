using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class ProductCategoriesBLC 
    {

        #region Constructors

        private ProductCategoriesBLC() { }

        #endregion

        #region Public Methods

        public ProductCategoriesInfo getCategory(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductCategoriesDALC.Instance.getOne(productId, pharmaFormId, divisionId, categoryId);
        }

        public bool assigned(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.ProductCategoriesDALC.Instance.assigned(productId, pharmaFormId, divisionId, categoryId);
        }

        public void addProductToCategory(ProductCategoriesInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductCategories);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            MedinetDataAccessComponent.ProductCategoriesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductFromCategory(ProductCategoriesInfo BEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductCategories);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            MedinetDataAccessComponent.ProductCategoriesDALC.Instance.delete(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void assignedProductToCategory(ProductCategoriesInfo BEntity, int categoryId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductCategories);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            MedinetDataAccessComponent.ProductCategoriesDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            
            BEntity.CategoryId = categoryId;
            MedinetDataAccessComponent.ProductCategoriesDALC.Instance.insert(BEntity);

            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void updateProductToCategory(ProductCategoriesInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductCategories);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            MedinetDataAccessComponent.ProductCategoriesDALC.Instance.update(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly ProductCategoriesBLC Instance = new ProductCategoriesBLC();

    }
}