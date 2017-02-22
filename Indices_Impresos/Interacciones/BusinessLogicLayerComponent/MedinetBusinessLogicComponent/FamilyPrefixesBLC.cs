using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class FamilyPrefixesBLC
    {

        #region Constructors

        private FamilyPrefixesBLC() { }

        #endregion

        #region Public Methods

        public FamilyPrefixInfo getFamily(int prefixId)
        {
            if (prefixId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return MedinetDataAccessComponent.FamilyPrefixesDALC.Instance.getOne(prefixId);
        }

        public void updateFamilyPrefix(FamilyPrefixInfo familyPrefix, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.FamilyPrefixes);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(PrefixId," + familyPrefix.PrefixId + ")";
            activityLog.FieldsAffected = "(EditionId," + familyPrefix.EditionId + ");(PrefixName," + familyPrefix.PrefixName + ");(CurrentValue, "
                + familyPrefix.CurrentValue + ");(PrefixDescription," + familyPrefix.PrefixDescription + ");(Active," + familyPrefix.Active + ")";

            MedinetDataAccessComponent.FamilyPrefixesDALC.Instance.update(familyPrefix);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public FamilyPrefixInfo getFamilyPrefixByEdition(int editionId, byte prefixTypeId)
        {
            if (editionId <= 0 || prefixTypeId <= 0)
                throw new ArgumentException("The edition or prefix does not exist.");

            return MedinetDataAccessComponent.FamilyPrefixesDALC.Instance.getByEdition(editionId, prefixTypeId);
        }

        #endregion

        public static readonly FamilyPrefixesBLC Instance = new FamilyPrefixesBLC();

    }
}
