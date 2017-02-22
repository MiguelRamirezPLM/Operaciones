using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductOtherContraindicationsBLC
    {
         #region Constructors

        private ProductOtherContraindicationsBLC() { }

        #endregion

        public void deleteProductOtherContraindication(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteProductOtherContraindicationAll(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public int addProductOtherContraintication(MedinetBusinessEntries.ProductOtherContraindicationInfo bEntity, int userId, string hash)
        {
            bEntity.Returns = MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            return bEntity.Returns;
        }

        public List<MedinetBusinessEntries.ProductOtherContraindicationInfo> getOtherContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductOtherContraindicationDALC.Instance.getProductOtherContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public void ActivityLogs(MedinetBusinessEntries.ProductOtherContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductHypersensibilityContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(ElementId," + GenericBEntity.ElementId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
        public static readonly ProductOtherContraindicationsBLC Instance = new ProductOtherContraindicationsBLC();
    }
}
