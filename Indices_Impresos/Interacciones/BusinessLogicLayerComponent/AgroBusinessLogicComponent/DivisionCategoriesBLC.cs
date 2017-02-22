using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;

namespace AgroBusinessLogicComponent
{
    public class DivisionCategoriesBLC
    {

        #region Constructors

        private DivisionCategoriesBLC() { }

        #endregion

        #region Public Methods

        public void addDivisionCategory(DivisionCategoryInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionCategories);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ")";

            AgroDataAccessComponent.DivisionCategoriesDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public DivisionCategoryInfo getDivisionCategory(int divisionId, int categoryId)
        {
            if (divisionId <= 0 || categoryId <= 0)
                throw new ArgumentException("The division or category does not exist.");

            return AgroDataAccessComponent.DivisionCategoriesDALC.Instance.getDivisionCategory(divisionId, categoryId);
        }

        #endregion

        public static readonly DivisionCategoriesBLC Instance = new DivisionCategoriesBLC();

    }
}
