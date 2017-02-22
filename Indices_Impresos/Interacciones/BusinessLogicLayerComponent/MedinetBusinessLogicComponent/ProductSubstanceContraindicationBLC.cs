using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductSubstanceContraindicationBLC
    {
          #region Constructors

        private ProductSubstanceContraindicationBLC() { }

        #endregion

        public void deleteProductSubstanceContraindication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteProductSubstanceContraindicationAll(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity,int userId, string hash)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }
        public void addProductSubstanceContraintication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bEntity,int userId, string hash)
        {
            MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
        }

        public List<MedinetBusinessEntries.ProductSubstanceContraindicationInfo> getContraindicationSubstances(int productId,int categoryId,int divisionId,int pharmaFormId,int activeSubstanceId) {
        
        return MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.getContraindicationSubstances(productId,
        pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

        public int insertContraindication(MedinetBusinessEntries.ProductSubstanceContraindicationInfo bussinesEntity,int userId, string hash)
        {
            this.ActivityLogs(bussinesEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            bussinesEntity.Returns = MedinetDataAccessComponent.ProductSubstanceContraindicationDALC.Instance.insert(bussinesEntity);
            return bussinesEntity.Returns;

        }

        public void ActivityLogs(MedinetBusinessEntries.ProductSubstanceContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductHypersensibilityContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(PharmaGroupId," + GenericBEntity.SubstanceContraindicationId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }


        public static readonly ProductSubstanceContraindicationBLC Instance = new ProductSubstanceContraindicationBLC();
    }
}
