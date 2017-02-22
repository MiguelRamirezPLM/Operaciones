using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
namespace AgroBusinessLogicComponent
{
   public class CropsBLC
    {
           #region Contructors

       private CropsBLC() { }

        #endregion

        #region Methods


       public void addCropsForProduct(int productId, int agrochemicalUseId, int userId, string hash)
       {
           PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

           AgroDataAccessComponent.CropsDALC.Instance.insertForProduct(productId, agrochemicalUseId);

           activityLog.UserId = userId;
           activityLog.HashKey = hash;
           activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
           activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
           activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
           activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

           // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

       }
       public void deleteCropsForProduct(int productId, int agrochemicalUseId, int userId, string hash)
       {
           PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

           AgroDataAccessComponent.CropsDALC.Instance.deleteForProduct(productId, agrochemicalUseId);

           activityLog.UserId = userId;
           activityLog.HashKey = hash;
           activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
           activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
           activityLog.PrimaryKeyAffected = "(ProductId," + productId + ")";
           activityLog.FieldsAffected = "(AgrochemicalUseId," + agrochemicalUseId + ");";

           // PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

       }

       public List<AgroBusinessEntries.CropDetailInfo> getCropsNotAssociated(int editionId, int divisionId, int bookId, int productId, int pharmaformId)
       {
           if (editionId <= 0 || divisionId <= 0)
               throw new ArgumentException("The Edition or Country does not exist.");

           return AgroDataAccessComponent.CropsDALC.Instance.getCropsNotAssociated(editionId, divisionId, bookId, productId, pharmaformId);
       }

       public List<AgroBusinessEntries.CropDetailInfo> getCropsForProduct(int editionId, int divisionId, int bookId, int productId, int pharmaFormId)
       {
           if (editionId <= 0 || divisionId <= 0)
               throw new ArgumentException("The Edition or division does not exist.");

           return AgroDataAccessComponent.CropsDALC.Instance.getCropsForProduct(editionId, divisionId, bookId, productId, pharmaFormId);
       }

       public AgroEntityFramework.Crops getCropById(int cropId)
        {
           return AgroDataAccessComponent.CropsDALC.Instance.getCropById(cropId);

        }
       public CropDetailInfo getById(string isbn, int cropId, string code)
       {
           if (isbn == "" || cropId == 0)
           {
               throw new ArgumentException("The isbn or cropId does not exist.");
           }
           else
           {
               PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
               validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

               if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
               {
                   return DEAQDataAccessComponent.CropsDALC.Instace.getById(isbn, cropId);
               }
               else
               {
                   throw new ApplicationException("The code is not valid or is inactive.");
               }
           }
       }

       public List<AgroEntityFramework.Crops> getCropsByStateId(int stateId)
        {
          return AgroDataAccessComponent.CropsDALC.Instance.getCropsByStateId(stateId);
        }

       public List<AgroEntityFramework.Crops> getCrops()
       {
           return AgroDataAccessComponent.CropsDALC.Instance.getCrops();
       }

        #endregion

       #region update
       public void updateCrop(AgroEntityFramework.Crops crop)
       {
           crop.CropName = crop.CropName.ToUpper();
           crop.ScientificName = crop.ScientificName.ToUpper();
           AgroDataAccessComponent.CropsDALC.Instance.updateCrop(crop);
       }


       #endregion

       public static readonly CropsBLC Instance = new CropsBLC(); 

    }
}
