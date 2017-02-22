using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
   public class ProductCommentContraindicationBLC
    {
       #region Constructors

       private ProductCommentContraindicationBLC() { }

        #endregion

        public void deleteProductCommentContraindication(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity,int userId, string hash)
        {
            MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.delete(bEntity);
                this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);

        }

        public void deleteProductCommentContraindicationAll(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity,int userId, string hash)
        {
            MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }
        public int addProductCommentContraintication(MedinetBusinessEntries.ProductCommentContraindicationsInfo bEntity,int userId, string hash)
        {
            
            bEntity.Returns= MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            return bEntity.Returns;
        }

        public List<MedinetBusinessEntries.ProductCommentContraindicationsInfo> getCommentsContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductCommentContraindicationDALC.Instance.getCommentsContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public void ActivityLogs(MedinetBusinessEntries.ProductCommentContraindicationsInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductHypersensibilityContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(ElementId," + GenericBEntity.ProductCommentId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public static readonly ProductCommentContraindicationBLC Instance = new ProductCommentContraindicationBLC();
    }
}
 