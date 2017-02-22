using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetBusinessLogicComponent
{
   public class IppaProductInteractionBLC
    {
        #region Contructors

       private IppaProductInteractionBLC() { }
        
        #endregion

        public Boolean checkProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstance)
        {
            if (MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.checkProductInteractions(categoryId, pharmaFormId, productId, divisionId, activeSubstance) > 0)
                return true;
            else
                return false;
        }
        
        public List<MedinetBusinessEntries.IppaProductInteractionsInfo> getProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId, int activeSubstance)
        {
            return MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.getIppaProductsInteractions(categoryId, pharmaFormId, productId, divisionId, activeSubstance);
        }
        public List<MedinetBusinessEntries.IppaProductInteractionsInfo> getProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            return MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.getIppaProductsInteractions(categoryId, pharmaFormId, productId, divisionId);
        }

        public String getHtmlContenrProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            return MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.getHtmlContentInteractionByProduct(categoryId, pharmaFormId, productId, divisionId);
        }

        public void addProductInteraction(MedinetBusinessEntries.IppaProductInteractionsInfo BEntity) {
            MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.insert(BEntity);
        }
        public void updateProductInteraction(MedinetBusinessEntries.IppaProductInteractionsInfo BEntity)
        {
            MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.update(BEntity);
        }

        public void checkStatusIMProduct(int categoryId, int pharmaFormId, int productId, int divisionId) 
        {
            MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.checkStatusIMProductChanges(categoryId, pharmaFormId, productId, divisionId);
        }
        public void deleteProductInteraction(MedinetBusinessEntries.IppaProductInteractionsInfo BEntity,int userId,string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.IPPAProductInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(DivisionId," + BEntity.DivisionId
                + ");(ActiveSubstanceId," + BEntity.ActiveSubstanceId + ");(CategoryId," + BEntity.CategoryId + ");(PharmaFormId," + BEntity.PharmaFormId + ")";

            MedinetDataAccessComponent.IppaProductInteractionsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            
        }

        public static readonly IppaProductInteractionBLC Instance = new IppaProductInteractionBLC();
    }
}
