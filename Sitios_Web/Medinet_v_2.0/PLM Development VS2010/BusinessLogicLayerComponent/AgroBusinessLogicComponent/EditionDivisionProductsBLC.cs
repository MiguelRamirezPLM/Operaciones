using System;
using System.Collections.Generic;
using System.Text;
using AgroBusinessEntries;

namespace AgroBusinessLogicComponent
{
    public sealed class EditionDivisionProductsBLC
    {
        #region Constructors

        private EditionDivisionProductsBLC() { }

        #endregion

        #region Public Methods

        public bool checkDivision(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            return AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.checkEdiDivProd(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        public EditionDivisionProductInfo getDivision(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            return AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.getOne(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        public void addDivision(EditionDivisionProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionDivisionProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId 
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId +");(EditionId," + BEntity.EditionId + ");";

            AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeDivision(EditionDivisionProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionDivisionProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";

            AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void modifyDivision(EditionDivisionProductInfo BEntity, int divisionId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.delete(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionDivisionProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            BEntity.DivisionId = divisionId;
            AgroDataAccessComponent.EditionDivisionProductsDALC.Instance.insert(BEntity);

            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        
        }

        #endregion

        public static readonly EditionDivisionProductsBLC Instance = new EditionDivisionProductsBLC();

    }
}
