using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
namespace MedinetBusinessLogicComponent
{
    public class ProductSubstanceInteractionsBLC
    {
        #region Contructors

        private ProductSubstanceInteractionsBLC() { }
        
        #endregion

        public List<ProductSubstanceInteractionsInfo> getInteractions(int categoryId,int pharmaFormId,int productId,int divisionId) 
        {
            return MedinetDataAccessComponent.ProductSubstanceInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId);
        }
        public List<ProductSubstanceInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductSubstanceInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId,activeSubstanceId);
        }

        public List<ProductSubstanceInteractionsInfo> getInteractionsSubstances(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductSubstanceInteractionsDALC.Instance.getInteractionsSubstances(categoryId, pharmaFormId, productId, divisionId, activeSubstanceId);
        }
        public int insertInteraction(ProductSubstanceInteractionsInfo bussinesEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            bussinesEntity.ProductId = MedinetDataAccessComponent.ProductSubstanceInteractionsDALC.Instance.insert(bussinesEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductSubstanceInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId + ");(ActivesubstanceId,"
                + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(SubstanceInteractionId," + bussinesEntity.SubstanceInteractId+ ")";
            if (bussinesEntity.ProductId != 0)
            {
                PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            }

            return bussinesEntity.ProductId;

        }
        public void deleteInteraction(ProductSubstanceInteractionsInfo bussinesEntity,int userId,string hash)
        {
            MedinetDataAccessComponent.ProductSubstanceInteractionsDALC.Instance.delete(bussinesEntity);
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductSubstanceInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId + ");(ActivesubstanceId,"
                + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(SubstanceInteractionId," + bussinesEntity.SubstanceInteractId + ")";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
        public static readonly ProductSubstanceInteractionsBLC Instance = new ProductSubstanceInteractionsBLC();
    }
}
