using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;

namespace AgroBusinessLogicComponent
{
    public class ModifiedAttributeGroupsBLC
    {

        #region Constructors

        private ModifiedAttributeGroupsBLC() { }

        #endregion

        #region Public Methods

        public void addProductToAttributeGroup(ModifiedAttributeGroupsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ModifiedAttributeGroups);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(DivisionId," + BEntity.DivisionId
                + ");(CategoryId," + BEntity.CategoryId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId
                + ");(AttributeGroupId," + BEntity.AttributeGroupId + ")";

            AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeProductToAttributeGroup(ModifiedAttributeGroupsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ModifiedAttributeGroups);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(EditionId," + BEntity.EditionId + ");(DivisionId," + BEntity.DivisionId
                + ");(CategoryId," + BEntity.CategoryId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId," + BEntity.ProductId
                + ");(AttributeGroupId," + BEntity.AttributeGroupId + ")";

            AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public AgroBusinessEntries.ModifiedAttributeGroupsInfo getModifiedAttributeGroup(ModifiedAttributeGroupsInfo businessEntity)
        {
            return AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getOne(businessEntity);
        }

        public List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> getModifiedAttributeGroupsByProduct(int editionId, int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            return AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getModifiedAttributeGroupsByProduct(editionId, divisionId, categoryId, pharmaFormId, productId);
        }

        public List<AgroBusinessEntries.AttributeGroupDetailInfo> getAttributeGroupsByProduct(int bookId, int countryId, int editionId,
            int divisionId, int categoryId, int pharmaFormId, int productId) 
        {
            return AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getAttributeGroupsByProduct(bookId, countryId, editionId, divisionId, categoryId, pharmaFormId, productId);
        }

        public List<AgroBusinessEntries.AttributeGroupInfo> getModifiedAttributesByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            return AgroDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getModifiedAttributesByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly ModifiedAttributeGroupsBLC Instance = new ModifiedAttributeGroupsBLC();

    }
}
