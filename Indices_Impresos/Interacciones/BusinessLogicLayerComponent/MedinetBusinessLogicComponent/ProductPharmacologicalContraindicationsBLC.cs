using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductPharmacologicalContraindicationsBLC
    {
        #region Constructors

        private ProductPharmacologicalContraindicationsBLC() { }

        #endregion

        public void deleteProductPharmaContraindication(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity,userId,hash,PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteProductPharmaContraindicationAll(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public int addProductPharmaContraintication(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo bEntity, int userId, string hash)
        {
           
            bEntity.Returns =  MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.insert(bEntity);
            this.ActivityLogs(bEntity, userId, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            return bEntity.Returns;
        }

        public List<MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo> getPharmacologicalContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int activeSubstanceId)
        {

            return MedinetDataAccessComponent.ProductPharmacologicalContraindicationDALC.Instance.getProductPharmacologicalContraindications(productId,
            pharmaFormId, categoryId, divisionId, activeSubstanceId);

        }

        public void ActivityLogs(MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmacologicalContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(PharmaContraindicationId," + GenericBEntity.PharmaContraindicationId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public static readonly ProductPharmacologicalContraindicationsBLC Instance = new ProductPharmacologicalContraindicationsBLC();
    }
}
