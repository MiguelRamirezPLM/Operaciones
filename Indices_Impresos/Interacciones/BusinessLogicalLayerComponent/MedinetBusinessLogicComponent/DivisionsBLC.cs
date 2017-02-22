using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class DivisionsBLC
    {

        #region Constructors

        private DivisionsBLC() { }

        #endregion

        #region Public Methods

        public DivisionsInfo getDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");

            return MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId);
        }

        public void updateDivision(DivisionsInfo division, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Divisions);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(DivisionId," + division.DivisionId + ")";
            activityLog.FieldsAffected = "(LaboratoryId," + division.LaboratoryId + ");(CountryId,"
                + division.CountryId + ");(Description, " + division.Description + ");(ShortName," + division.ShortName + ");(Active," + division.Active + ")";

            MedinetDataAccessComponent.DivisionsDALC.Instance.update(division);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        #endregion

        public static readonly DivisionsBLC Instance = new DivisionsBLC();

    }
}
