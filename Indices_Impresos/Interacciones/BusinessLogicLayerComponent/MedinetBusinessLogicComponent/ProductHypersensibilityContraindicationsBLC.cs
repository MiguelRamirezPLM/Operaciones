using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductHypersensibilityContraindicationsBLC
    {
         #region Constructors

        private ProductHypersensibilityContraindicationsBLC() { }

        #endregion

        public void deleteProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteProductHypersensibilityAll(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public int addProductHypersensibility(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo bEntity, int userId, string hash)
        {
            bEntity.Returns= MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            return bEntity.Returns;
        }

        public List<MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo> getHypersensibilityContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductHypersensibilityContraindicationDALC.Instance.getProductHypersensibilities(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public void ActivityLogs(MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductHypersensibilityContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(HypersensibilityId," + GenericBEntity.HypersensibilityId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public static readonly ProductHypersensibilityContraindicationsBLC Instance = new ProductHypersensibilityContraindicationsBLC();
    }
}
