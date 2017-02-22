using System;
using System.Collections.Generic;
using System.Text;
using AgroBusinessEntries;
using PLMUsersBusinessLogicComponent;

namespace AgroBusinessLogicComponent
{
    public sealed class ParticipantProductsBLC 
    {
        #region Constructors

        private ParticipantProductsBLC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return AgroDataAccessComponent.ParticipantProductsDALC.Instance.participate(productId, editionId, pharmaFormId, divisionId, categoryId);
        }
         
        public ParticipantProductsInfo getParticipantProduct(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            return AgroDataAccessComponent.ParticipantProductsDALC.Instance.getOne(productId, editionId, pharmaFormId, divisionId, categoryId);
        }

        public List<PagedProductInfo> getProductPaged(int editionId, string brand)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist.");

            return AgroDataAccessComponent.ParticipantProductsDALC.Instance.getPagedProducts(editionId, brand, (byte)CatalogsBLC.TypeInEdition.Participante);
        }

        public List<PagedProductInfo> getProductPagedSymptom(int editionId, string brand)
        {
            if (editionId <= 0)
                throw new ArgumentException("The edition does not exist.");

            return AgroDataAccessComponent.ParticipantProductsDALC.Instance.getPagedProductsSymptoms(editionId, brand, (byte)CatalogsBLC.TypeInEdition.Participante);
        }
        public void addToBook(ParticipantProductsInfo BEntity,int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            AgroDataAccessComponent.ParticipantProductsDALC.Instance.insert(BEntity);
            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void addPage(ParticipantProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";
            activityLog.FieldsAffected = "(Page," + BEntity.Page + ")";

            AgroDataAccessComponent.ParticipantProductsDALC.Instance.update(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }
               
        public void removeToBook(ParticipantProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ")";

            AgroDataAccessComponent.ParticipantProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void modifiedContent(ParticipantProductsInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            if (BEntity.ModifiedContent == null)
                BEntity.ModifiedContent = false;
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.ParticipantProductsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ");(PharmaFormId," + BEntity.PharmaFormId
                + ");(DivisionId," + BEntity.DivisionId + ");(CategoryId," + BEntity.CategoryId + ");(EditionId," + BEntity.EditionId + ");";
            activityLog.FieldsAffected = "(ModifiedContent," + BEntity.ModifiedContent + ")";

            AgroDataAccessComponent.ParticipantProductsDALC.Instance.update(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly ParticipantProductsBLC Instance = new ParticipantProductsBLC();

    }
}
