using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class EditionProductShotsBLC
    {

        #region Constructors

        private EditionProductShotsBLC() { }

        #endregion

        #region Public Methods

        public EditionProductShotsInfo getEditionProductShot(int editionProductShotId)
        {
            if (editionProductShotId <= 0)
                throw new ArgumentException("The Product Shot does not exist.");

            return MedinetDataAccessComponent.EditionProductShotsDALC.Instance.getOne(editionProductShotId);
        }

        public int addProductShot(EditionProductShotsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.EditionProductShotId = MedinetDataAccessComponent.EditionProductShotsDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionProductShots);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(EditionProductShotId," + BEntity.EditionProductShotId + ")";
            activityLog.FieldsAffected = "(EditionId," + BEntity.EditionId + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId,"
                + BEntity.CategoryId + ");(PharmaFormId, " + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId + ");(PSTypeId,"
                + BEntity.PSTypeId + ");(ProductShot, " + BEntity.ProductShot + ");(QtyCells, " + BEntity.QtyCells + ");(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return BEntity.EditionProductShotId;
        }

        public void removeProductShot(int editionProductShotId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionProductShots);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(EditionProductShotId," + editionProductShotId + ")";

            MedinetDataAccessComponent.EditionProductShotsDALC.Instance.delete(editionProductShotId);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductShotsByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog;
            
            List<EditionProductShotsInfo> editionProductShots = 
                MedinetDataAccessComponent.EditionProductShotsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);

            foreach (EditionProductShotsInfo productShot in editionProductShots)
            {
                activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
                activityLog.UserId = userId;
                activityLog.HashKey = hash;
                activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.EditionProductShots);
                activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
                activityLog.PrimaryKeyAffected = "(EditionProductShotId," + productShot.EditionProductShotId + ")";

                MedinetDataAccessComponent.EditionProductShotsDALC.Instance.delete(productShot.EditionProductShotId);
                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            }
        }

        public List<EditionProductShotsInfo> getProductShots(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.EditionProductShotsDALC.Instance.getByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly EditionProductShotsBLC Instance = new EditionProductShotsBLC();

    }
}
