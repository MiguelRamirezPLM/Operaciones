using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;
using PLMUsersBusinessLogicComponent;

namespace MedinetBusinessLogicComponent
{
    public sealed class NewProductsBLC
    {

        #region Constructors

        private NewProductsBLC() { }

        #endregion

        #region Public Methods

        public bool newProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.NewProductsDALC.Instance.participate(productId, editionId, pharmaFormId, divisionId, categoryId);
        }

        public NewProductInfo getParticipantProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.NewProductsDALC.Instance.getOne(productId,editionId,pharmaFormId, divisionId, categoryId);
        }

        public void addNew(NewProductInfo BEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.NewProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            MedinetDataAccessComponent.NewProductsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
       
        public void removeNew(NewProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.NewProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            MedinetDataAccessComponent.NewProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly NewProductsBLC Instance = new NewProductsBLC();
    }
}
