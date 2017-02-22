using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public sealed class CountryProductsBLC
    {

        #region Constructors

        private CountryProductsBLC() { }

        #endregion 

        #region Public Methods

        public bool participate(byte countryCodeId, int pharmaFormId, int productId)
        {
            return MedinetDataAccessComponent.CountryProductsDALC.Instance.participate(countryCodeId, pharmaFormId, productId);
        }

        public void addToCountry(CountryProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CountryProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(CountryCodeId," + BEntity.CountryCodeId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId,"
                + BEntity.ProductId + ")";

            MedinetDataAccessComponent.CountryProductsDALC.Instance.insert(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public void removeToCountry(CountryProductInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.CountryProducts);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(CountryCodeId," + BEntity.CountryCodeId + ");(PharmaFormId," + BEntity.PharmaFormId + ");(ProductId,"
                + BEntity.ProductId + ")";

            MedinetDataAccessComponent.CountryProductsDALC.Instance.delete(BEntity);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly CountryProductsBLC Instance = new CountryProductsBLC();
    }
}
