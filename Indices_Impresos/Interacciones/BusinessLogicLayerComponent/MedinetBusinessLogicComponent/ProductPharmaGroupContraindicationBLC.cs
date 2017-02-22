using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductPharmaGroupContraindicationBLC
    {
          #region Constructors

        private ProductPharmaGroupContraindicationBLC() { }

        #endregion

        public void deleteProductPharmaGroupContraindication(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteProductPharmaGroupContraindicationAll(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public int addProductPharmaGroupContraintication(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo bEntity, int userId, string hash)
        {
           
            bEntity.Returns= MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            return bEntity.Returns;
        }

        public List<MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo> getPharmaGroupsContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductPharmaGroupContraindicationsDALC.Instance.getProductPharmaGroupContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);


        }

        public void ActivityLogs(MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductHypersensibilityContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(PharmaGroupId," + GenericBEntity.PharmaGroupId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public static readonly ProductPharmaGroupContraindicationBLC Instance = new ProductPharmaGroupContraindicationBLC();
    } 
}
