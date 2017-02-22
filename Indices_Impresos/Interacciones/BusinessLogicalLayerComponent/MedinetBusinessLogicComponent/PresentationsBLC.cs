using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class PresentationsBLC
    {

        #region Constructors

        private PresentationsBLC() { }

        #endregion

        #region Public Methods

        public int addPresentationToProduct(MedinetBusinessEntries.PresentationsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Presentations);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(PresentationId," + BEntity.PresentationId + ")";
            activityLog.FieldsAffected = "(DivisionId," + BEntity.DivisionId + "); (CategoryId," + BEntity.CategoryId + "); (ProductId,"
                + BEntity.ProductId + "); (PharmaFormId, " + BEntity.PharmaFormId + "); (QtyExternalPack," + BEntity.QtyExternalPack + "); (ExternalPackId,"
                + BEntity.ExternalPackId + "); (QtyInternalPack, " + BEntity.QtyInternalPack + "); (InternalPackId," + BEntity.InternalPackId + "); (QtyContentUnit,"
                + BEntity.QtyContentUnit + "); (ContentUnitId, " + BEntity.ContentUnitId + "); (QtyWeightUnit," + BEntity.QtyWeightUnit + "); (WeightUnitId,"
                + BEntity.WeightUnitId + "); (Presentation, " + BEntity.Presentation + "); (Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return BEntity.PresentationId;
        }

        public void updatePresentationToProduct(MedinetBusinessEntries.PresentationsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Presentations);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(PresentationId," + BEntity.PresentationId + ")";
            activityLog.FieldsAffected = "(DivisionId," + BEntity.DivisionId + "); (CategoryId," + BEntity.CategoryId + "); (ProductId,"
                + BEntity.ProductId + "); (PharmaFormId, " + BEntity.PharmaFormId + "); (QtyExternalPack," + BEntity.QtyExternalPack + "); (ExternalPackId,"
                + BEntity.ExternalPackId + "); (QtyInternalPack, " + BEntity.QtyInternalPack + "); (InternalPackId," + BEntity.InternalPackId + "); (QtyContentUnit,"
                + BEntity.QtyContentUnit + "); (ContentUnitId, " + BEntity.ContentUnitId + "); (QtyWeightUnit," + BEntity.QtyWeightUnit + "); (WeightUnitId,"
                + BEntity.WeightUnitId + "); (Presentation, " + BEntity.Presentation + "); (Active," + BEntity.Active + ")";

            MedinetDataAccessComponent.PresentationsDALC.Instance.update(BEntity);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public PresentationsInfo getPresentation(int presentationId)
        {
            if (presentationId <= 0)
                throw new ArgumentException("The presentation does not exist.");

            return MedinetDataAccessComponent.PresentationsDALC.Instance.getOne(presentationId);
        }

        public List<PresentationDetailInfo> getPresentationsByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The division or category or product or pharmaform does not exist.");

            return MedinetDataAccessComponent.PresentationsDALC.Instance.getPresentationsByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        public List<PresentationDetailInfo> getPresentationsByEditionByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            if (divisionId <= 0 || categoryId <= 0 || productId <= 0 || pharmaFormId <= 0)
                throw new ArgumentException("The division or category or product or pharmaform does not exist.");

            return MedinetDataAccessComponent.PresentationsDALC.Instance.getPresentationsByEditionByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        }

        #endregion

        public static readonly PresentationsBLC Instance = new PresentationsBLC();

    }
}
