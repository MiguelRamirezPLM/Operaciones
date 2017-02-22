using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
namespace MedinetBusinessLogicComponent
{
    public class ProductsOtherInteractionsBLC
    {
        #region Contructors

        private ProductsOtherInteractionsBLC() { }
        
        #endregion

        public List<ProductOtherInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId)
        {
            return MedinetDataAccessComponent.ProductOtherInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId);
        }

        public List<ProductOtherInteractionsInfo> getInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductOtherInteractionsDALC.Instance.getInteractions(categoryId, pharmaFormId, productId, divisionId,activeSubstanceId);
        }

        public List<ProductOtherInteractionsInfo> getInteractionsElement(int categoryId, int pharmaFormId, int productId, int divisionId, int elementId,int activeSubstanceId)
        {
            return MedinetDataAccessComponent.ProductOtherInteractionsDALC.Instance.getInteractionsElements(categoryId, pharmaFormId, productId, divisionId, elementId,activeSubstanceId);
        }

        public int insertInteraction(ProductOtherInteractionsInfo bussinesEntity,int userId,string hash)
        {
           PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

           bussinesEntity.ProductId= MedinetDataAccessComponent.ProductOtherInteractionsDALC.Instance.insert(bussinesEntity);

           activityLog.UserId = userId;
           activityLog.HashKey = hash;
           activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductOtherInteractions);
           activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
           activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId+ ");(ActivesubstanceId,"
               + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(ElementId," + bussinesEntity.ElementId + ")";
           if (bussinesEntity.ProductId!=0)
           {
               PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);   
           }

            return bussinesEntity.ProductId;
        }
        public void deleteInteraction(ProductOtherInteractionsInfo bussinesEntity, int userId, string hash)
        {
            MedinetDataAccessComponent.ProductOtherInteractionsDALC.Instance.delete(bussinesEntity);
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ProductOtherInteractions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + bussinesEntity.ProductId + ");(DivisionId," + bussinesEntity.DivisionId + ");(CategoryId," + bussinesEntity.CategoryId + ");(ActivesubstanceId,"
                + bussinesEntity.ActiveSubstanceId + ");(PharmaFormId, " + bussinesEntity.PharmaFormId + ");(ElementId," + bussinesEntity.ElementId + ")";
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }
        public static readonly ProductsOtherInteractionsBLC Instance = new ProductsOtherInteractionsBLC();
    }
}
