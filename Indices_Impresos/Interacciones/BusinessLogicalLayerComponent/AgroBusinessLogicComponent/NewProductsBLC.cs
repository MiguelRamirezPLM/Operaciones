using System;
using System.Collections.Generic;
using System.Text;
using AgroBusinessEntries;
using PLMUsersBusinessLogicComponent;

namespace AgroBusinessLogicComponent
{
    public sealed class NewProductsBLC
    {

        #region Constructors

        private NewProductsBLC() { }

        #endregion

        #region Public Methods

        public bool newProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return AgroDataAccessComponent.NewProductsDALC.Instance.participate(productId, editionId, pharmaFormId, divisionId, categoryId);
        }

        public NewProductInfo getParticipantProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return AgroDataAccessComponent.NewProductsDALC.Instance.getOne(productId,editionId,pharmaFormId, divisionId, categoryId);
        }

        public void addNew(NewProductInfo BEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            AgroDataAccessComponent.NewProductsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
       
        public void removeNew(NewProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            AgroDataAccessComponent.NewProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly NewProductsBLC Instance = new NewProductsBLC();
    }
}
