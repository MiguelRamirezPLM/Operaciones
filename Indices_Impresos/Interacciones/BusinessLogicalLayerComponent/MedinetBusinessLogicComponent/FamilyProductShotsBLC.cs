using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class FamilyProductShotsBLC
    {

        #region Constructors

        private FamilyProductShotsBLC() { }

        #endregion

        #region Public Methods

        public void addFamilyToProductShot(FamilyProductShotInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.FamilyProductShots);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(FamilyId," + BEntity.FamilyId + ");(EditionProductShotId," + BEntity.EditionProductShotId + ")";

            MedinetDataAccessComponent.FamilyProductShotsDALC.Instance.insert(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeFamilyToProductShot(FamilyProductShotInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.FamilyProductShots);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(FamilyId," + BEntity.FamilyId + ");(EditionProductShotId," + BEntity.EditionProductShotId + ")";

            MedinetDataAccessComponent.FamilyProductShotsDALC.Instance.delete(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public FamilyProductShotInfo getFamilyProductShot(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (editionId <= 0 || divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The edition or division or category or product or pharmaform does not exist.");

            return MedinetDataAccessComponent.FamilyProductShotsDALC.Instance.getFamilyProductShot(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly FamilyProductShotsBLC Instance = new FamilyProductShotsBLC();

    }
}
