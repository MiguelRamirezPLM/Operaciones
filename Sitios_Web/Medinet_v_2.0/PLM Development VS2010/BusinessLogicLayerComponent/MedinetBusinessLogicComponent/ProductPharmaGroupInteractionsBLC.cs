using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
namespace MedinetBusinessLogicComponent
{
   public class ProductPharmaGroupInteractionsBLC
    {
        #region Contructors

       private ProductPharmaGroupInteractionsBLC() { }
        
        #endregion

       public List<ProductPharmaGroupsInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
       {
           return MedinetDataAccessComponent.ProductPharmaGroupInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId);
       }
       public List<ProductPharmaGroupsInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
       {
           return MedinetDataAccessComponent.ProductPharmaGroupInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId,activeSubstanceId);
       }

       public List<ProductPharmaGroupsInteractionsInfo> getInteractionsGroups(int categoryId, int pharmaFormId, int productId, int divisionId, int pharmaGroupId,int activeSubstanceId)
       {
           return MedinetDataAccessComponent.ProductPharmaGroupInteractionsDALC.Instance.getInteractionsGroups(categoryId, pharmaFormId, productId, divisionId, pharmaGroupId, activeSubstanceId);
       }

       public int insertInteraction(ProductPharmaGroupsInteractionsInfo bussinesEntity,int userId,string hash)
       {
           PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

           bussinesEntity.ProductId = MedinetDataAccessComponent.ProductPharmaGroupInteractionsDALC.Instance.insert(bussinesEntity);

           activityLog.UserId = userId;
           activityLog.HashKey = hash;
           activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmaGroupInteractions);
           activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
           activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId + ");(ActivesubstanceId,"
               + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(PharmaGroupId," + bussinesEntity.PharmaGroupId + ")";
           if (bussinesEntity.ProductId != 0)
           {
               PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
           }

           return bussinesEntity.ProductId;
           
       }
       public void deleteInteraction(ProductPharmaGroupsInteractionsInfo bussinesEntity,int userId,string hash)
       {
           MedinetDataAccessComponent.ProductPharmaGroupInteractionsDALC.Instance.delete(bussinesEntity);
           PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

           activityLog.UserId = userId;
           activityLog.HashKey = hash;
           activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductPharmaGroupInteractions);
           activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
           activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId + ");(ActivesubstanceId,"
               + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(PharmaGroupId," + bussinesEntity.PharmaGroupId + ")";
           PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
       }

       public static readonly ProductPharmaGroupInteractionsBLC Instance = new ProductPharmaGroupInteractionsBLC();
    }
}
