using System;
using System.Collections.Generic;
using System.Text;
using MedinetBusinessEntries;

namespace MedinetBusinessLogicComponent
{
    public class DivisionInformationBLC
    {

        #region Constructors

        private DivisionInformationBLC() { }

        #endregion

        #region Public Methods

        public DivisionInformationInfo getInformationByDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");
            return MedinetDataAccessComponent.DivisionInformationDALC.Instance.getInformationByDivId(divisionId);
        }
        public List<DivisionInformationInfo> getListInformationByDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");
            return MedinetDataAccessComponent.DivisionInformationDALC.Instance.getListInformationByDivId(divisionId);
        }

        public void updateDivisionInformation(DivisionInformationInfo divisionInformation, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformation);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(DivisionInfId," + divisionInformation.DivisionInfId + ")";
            activityLog.FieldsAffected = "(DivisionId," + divisionInformation.DivisionId + ");(Address,"
                + divisionInformation.Address + ");(Image, " + divisionInformation.Image + ");(Suburb," + divisionInformation.Suburb + ");(ZipCode,"
                + divisionInformation.ZipCode + ");(Telephone, " + divisionInformation.Telephone + ");(Fax,"
                + divisionInformation.Fax + ");(Web, " + divisionInformation.Web + ");(Email," + divisionInformation.Email + ");(City, "
                + divisionInformation.City + ");(State," + divisionInformation.State + ");(Contact, " + divisionInformation.Contact + ");(Active," + divisionInformation.Active + ")";

            MedinetDataAccessComponent.DivisionInformationDALC.Instance.update(divisionInformation);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public int addDivisionInformation(DivisionInformationInfo divisionInformation, int userId, string hash)
        { 
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            divisionInformation.DivisionInfId= MedinetDataAccessComponent.DivisionInformationDALC.Instance.insert(divisionInformation);
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformation);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionInfId," + divisionInformation.DivisionInfId + ")";
            activityLog.FieldsAffected = "(DivisionId," + divisionInformation.DivisionId + ");(Address,"
                + divisionInformation.Address + ");(Image, " + divisionInformation.Image + ");(Suburb," + divisionInformation.Suburb + ");(ZipCode,"
                + divisionInformation.ZipCode + ");(Telephone, " + divisionInformation.Telephone + ");(Fax,"
                + divisionInformation.Fax + ");(Web, " + divisionInformation.Web + ");(Email," + divisionInformation.Email + ");(City, "
                + divisionInformation.City + ");(State," + divisionInformation.State + ");(Contact, " + divisionInformation.Contact + ");(Active," + divisionInformation.Active + ")";

            
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return divisionInformation.DivisionInfId;
        }

        public void deleteDivisionInformation(DivisionInformationInfo divisionInformation, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            MedinetDataAccessComponent.DivisionInformationDALC.Instance.delete(divisionInformation);
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformation);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(DivisionInfId," + divisionInformation.DivisionInfId + ")";
            activityLog.FieldsAffected = "(DivisionId," + divisionInformation.DivisionId + ");(Address,"
                + divisionInformation.Address + ");(Image, " + divisionInformation.Image + ");(Suburb," + divisionInformation.Suburb + ");(ZipCode,"
                + divisionInformation.ZipCode + ");(Telephone, " + divisionInformation.Telephone + ");(Fax,"
                + divisionInformation.Fax + ");(Web, " + divisionInformation.Web + ");(Email," + divisionInformation.Email + ");(City, "
                + divisionInformation.City + ");(State," + divisionInformation.State + ");(Contact, " + divisionInformation.Contact + ");(Active," + divisionInformation.Active + ")";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
         
        }

        #endregion

        public static readonly DivisionInformationBLC Instance = new DivisionInformationBLC();

    }
}
