using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class FamilyProductsBLC
    {

        #region Constructors

        private FamilyProductsBLC() { }

        #endregion

        #region Public Methods

        public void addFamilyToProduct(FamilyProductInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.FamilyProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(FamilyId," + BEntity.FamilyId + ");(DivisionId,"
                + BEntity.DivisionId + ");(CategoryId, " + BEntity.CategoryId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ")";

            MedinetDataAccessComponent.FamilyProductsDALC.Instance.insert(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeFamilyToProduct(FamilyProductInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.FamilyProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(FamilyId," + BEntity.FamilyId + ");(DivisionId,"
                + BEntity.DivisionId + ");(CategoryId, " + BEntity.CategoryId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ")";

            MedinetDataAccessComponent.FamilyProductsDALC.Instance.delete(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public FamilyProductInfo getFamilyProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The edition or division or category or product or pharmaform does not exist.");

            return MedinetDataAccessComponent.FamilyProductsDALC.Instance.getFamilyProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly FamilyProductsBLC Instance = new FamilyProductsBLC();

    }
}
