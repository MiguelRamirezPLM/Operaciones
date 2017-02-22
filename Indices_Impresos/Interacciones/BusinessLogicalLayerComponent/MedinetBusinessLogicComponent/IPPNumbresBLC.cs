 using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class IPPNumbresBLC 
    {
        #region Constructors

        private IPPNumbresBLC() { }

        #endregion

        #region Public Methods

        public List<IPPNumbersInfo> getIppNumberByProduct(int productId)
        {
            return MedinetDataAccessComponent.IPPNumbersDALC.Instance.getAll(productId);
        }

        public void addIppNumberToProduct(IPPNumbersInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            
            BEntity.IppId = MedinetDataAccessComponent.IPPNumbersDALC.Instance.insert(BEntity);
            
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.IPPNumbers);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(IppId," + BEntity.IppId + ")";
            activityLog.FieldsAffected = "(CountryId," + BEntity.CountryId +");(ProductId," + BEntity.ProductId + ");(Description," 
                + BEntity.Description + ");(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void updateIppNumber(IPPNumbersInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();


            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.IPPNumbers);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(IppId," + BEntity.IppId + ")";
            activityLog.FieldsAffected = "(CountryId," + BEntity.CountryId + ");(ProductId," + BEntity.ProductId + ");(Description,"
                + BEntity.Description + ");(Active," + BEntity.Active + ")";

            MedinetDataAccessComponent.IPPNumbersDALC.Instance.update(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeIppNumberFromProduct(int ippId, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.IPPNumbers);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(IppId," + ippId + ")";


            MedinetDataAccessComponent.IPPNumbersDALC.Instance.delete(ippId);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly IPPNumbresBLC Instance = new IPPNumbresBLC();

        
    }
}
