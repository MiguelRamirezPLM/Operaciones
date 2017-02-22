using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
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

            MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.insert(BEntity);

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

            MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public MedinetBusinessEntries.ModifiedAttributeGroupsInfo getModifiedAttributeGroup(ModifiedAttributeGroupsInfo businessEntity)
        {
            return MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getOne(businessEntity);
        }

        public List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> getModifiedAttributeGroupsByProduct(int editionId, int divisionId, int categoryId, int pharmaFormId, int productId)
        {
            return MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getModifiedAttributeGroupsByProduct(editionId, divisionId, categoryId, pharmaFormId, productId);
        }

        public List<MedinetBusinessEntries.AttributeGroupDetailInfo> getAttributeGroupsByProduct(int bookId, int countryId, int editionId,
            int divisionId, int categoryId, int pharmaFormId, int productId) 
        {
            return MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getAttributeGroupsByProduct(bookId, countryId, editionId, divisionId, categoryId, pharmaFormId, productId);
        }

        public List<MedinetBusinessEntries.AttributeGroupInfo> getModifiedAttributesByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            return MedinetDataAccessComponent.ModifiedAttributeGroupsDALC.Instance.getModifiedAttributesByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly ModifiedAttributeGroupsBLC Instance = new ModifiedAttributeGroupsBLC();

    }
}
