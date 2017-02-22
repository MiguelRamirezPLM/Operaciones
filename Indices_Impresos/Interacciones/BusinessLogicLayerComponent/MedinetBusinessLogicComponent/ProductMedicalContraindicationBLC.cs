using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
    public class ProductMedicalContraindicationBLC
    {
        #region Constructors

        private ProductMedicalContraindicationBLC() { }

        #endregion

        public void deleteProductMedicalContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity,int Userid, string hash){

            MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.delete(bEntity);
            this.ActivityLogs(bEntity,Userid,hash,PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }

        public void deleteAllProductMedicalContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity, int Userid, string hash)
        {
            MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.deleteAll(bEntity);
            this.ActivityLogs(bEntity, Userid, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
        }
       
        public int insertContraindication(MedinetBusinessEntries.ProductMedicalContraindicationInfo bEntity,int Userid, string hash)
        {

            bEntity.Return = MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.insert(bEntity);
             this.ActivityLogs(bEntity, Userid, hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);

             return bEntity.Return;
        }
       
        public List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> getMedicalContraindications(int productId, int categoryId, int divisionId, int pharmaFormId, int ICDId)
        {

            return MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.getProductMedicalContraindications(productId,
            pharmaFormId, categoryId, divisionId, ICDId);

        }

        public List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> getMedicalICDContraindications(int productId, int categoryId, int divisionId, int pharmaFormId)
        {

            return MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.getProductMedicalContraindications(productId,
            pharmaFormId, categoryId, divisionId);

        }

        public string InsertICD(List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> icds, int Userid, string hash)
        {
            foreach (MedinetBusinessEntries.ProductMedicalContraindicationInfo item in icds)
            {

                this.insertContraindication(item, Userid, hash);
              
            }
            return "";
        }

        public List<MedinetBusinessEntries.ProductMedicalContraindicationInfo> getDifferenceICDContra(int productId,int divisionId,int pharmaformId,int categoryId,int activesubstancesId,int icdId)
        {
            return MedinetDataAccessComponent.ProductMedicalContraindicationsDALC.Instance.getDifferenceICDContra(productId, divisionId, pharmaformId, categoryId, activesubstancesId, icdId);
        }

        public void ActivityLogs(MedinetBusinessEntries.ProductMedicalContraindicationInfo GenericBEntity, int userId, string hash, PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions action)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductICDContraindications);
            activityLog.OperationId = Convert.ToInt32(action);
            activityLog.PrimaryKeyAffected = "(ProductId," + GenericBEntity.ProductId + ");(PharmaFormId," + GenericBEntity.PharmaFormId +
            ");(ICDId," + GenericBEntity.ICDId + ");(ActiveSubstanceId," + GenericBEntity.ActiveSubstanceId + ");(DivisionId," + GenericBEntity.DivisionId +
            ");(CategoryId," + GenericBEntity.CategoryId + ");";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public static readonly ProductMedicalContraindicationBLC Instance = new ProductMedicalContraindicationBLC();
    }
}
