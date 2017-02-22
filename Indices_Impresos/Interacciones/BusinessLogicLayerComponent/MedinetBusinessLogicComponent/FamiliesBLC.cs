using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class FamiliesBLC
    {

        #region Constructors

        private FamiliesBLC() { }

        #endregion

        #region Public Methods

        public FamilyInfo getFamily(int familyId)
        {
            if (familyId <= 0)
                throw new ArgumentException("The family does not exist.");

            return MedinetDataAccessComponent.FamiliesDALC.Instance.getOne(familyId);
        }

        public int addFamily(FamilyInfo BEntity, int userId, string hashkey)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            BEntity.FamilyId = MedinetDataAccessComponent.FamiliesDALC.Instance.insert(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hashkey;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Families);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(FamilyId," + BEntity.FamilyId + ")";
            activityLog.FieldsAffected = "(PrefixId," + BEntity.PrefixId + ");(FamilyString," + BEntity.FamilyString + ");(Active," + BEntity.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return BEntity.FamilyId;
        }

        public List<FamilyInfo> getContentFamiliesByDivision(int editionId, int divisionId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The edition or division does not exist.");

            return MedinetDataAccessComponent.FamiliesDALC.Instance.getContentFamiliesByDivision(editionId, divisionId);
        }

        public List<FamilyInfo> getPSFamiliesByDivision(int editionId, int divisionId)
        {
            if (editionId <= 0 || divisionId <= 0)
                throw new ArgumentException("The edition or division does not exist.");

            return MedinetDataAccessComponent.FamiliesDALC.Instance.getPSFamiliesByDivision(editionId, divisionId);
        }

        #endregion

        public static readonly FamiliesBLC Instance = new FamiliesBLC();

    }
}
