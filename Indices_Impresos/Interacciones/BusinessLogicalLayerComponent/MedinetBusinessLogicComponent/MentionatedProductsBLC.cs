using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;
using PLMUsersBusinessLogicComponent;

namespace MedinetBusinessLogicComponent
{
    public sealed class MentionatedProductsBLC
    {

        #region Constructors

        private MentionatedProductsBLC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.MentionatedProductsDALC.Instance.participate(productId, editionId, pharmaFormId, divisionId, categoryId);
        }

        public MentionatedProductInfo getParticipantProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return MedinetDataAccessComponent.MentionatedProductsDALC.Instance.getOne(productId, editionId, pharmaFormId, divisionId, categoryId);
        }

        public void addToBook(MentionatedProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.MentionatedProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";

            MedinetDataAccessComponent.MentionatedProductsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeToBook(MentionatedProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.MentionatedProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";

            MedinetDataAccessComponent.MentionatedProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly MentionatedProductsBLC Instance = new MentionatedProductsBLC();

    }
}
