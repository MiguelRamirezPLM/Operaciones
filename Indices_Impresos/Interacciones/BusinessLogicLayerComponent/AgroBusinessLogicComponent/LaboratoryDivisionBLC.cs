using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using AgroDataAccessComponent;

namespace AgroBusinessLogicComponent
{
    public class LaboratoryDivisionBLC
    {
      #region Constructor

        private LaboratoryDivisionBLC() { }

      #endregion

        #region Public Methods

        //public List<LaboratoriesInfo> getAll()
        //{
        //   // return AgroPLMBusinessLogicComponent.LaboratoryDALC.Instance.getAll();
        //    return AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.getAll();
        //}


        //public void AddLaboratory(LaboratoriesInfo businessEntity)
        //{
        //    AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.insert(businessEntity);
        //}

        //public void update(LaboratoriesInfo businessEntity)
        //{
        //    AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.updateLab(businessEntity);

             
        //}


        //public AgroBusinessEntries.LaboratoriesInfo  getLaboratories(int laboratoryId)
        //{
        //      return AgroDataAccessComponent.LaboratoryDivisionDALC.Instance.getOne(laboratoryId);
        //}


        //public List<DivisionAddressInfo> getDivisionLab(int laboratoryId, int countryId)
        //{
        //    return AgroDataAccessComponent.DivisionAddresDALC.Instance.getDivisionLab(laboratoryId, countryId);
        //}

        public DivisionAddressInfo getDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");

            return AgroDataAccessComponent.DivisionInfoDALC.Instance.getOne(divisionId);
        }

        public List<DivisionAddressInfo> getListInformationByDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");
            return AgroDataAccessComponent.DivisionAddresDALC.Instance.getListInformationByDivId(divisionId);
        }

        public DivisionAddressInfo getInformationByDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");
            return AgroDataAccessComponent.DivisionAddresDALC.Instance.getInformationByDivId(divisionId);
        }

        public void updateDivision(DivisionAddressInfo division, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionsDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(DivisionId," + division.DivisionId + ")";
            activityLog.FieldsAffected = "(DivisionName, " + division.DivisionName + ");(ShortName," + division.ShortName + ")";

            AgroDataAccessComponent.DivisionInfoDALC.Instance.update(division);

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }


        public void updateDivisionInformation(DivisionAddressInfo divisionInformation, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformationDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Actualizar);
            activityLog.PrimaryKeyAffected = "(DivisionInformationId," + divisionInformation.DivisionInformationId + ")";
            activityLog.FieldsAffected = "(DivisionId," + divisionInformation.DivisionId + ");(Address,"
                + divisionInformation.Address + ");(Suburb," + divisionInformation.Suburb + ");(ZipCode,"
                + divisionInformation.ZipCode + ");(Telephone, " + divisionInformation.Telephone + ");(Fax,"
                + divisionInformation.Fax + ");(Web, " + divisionInformation.Web + ");(Email," + divisionInformation.Email + ");(City, "
                + divisionInformation.City + ");(State," + divisionInformation.State + ");(Lada, " + divisionInformation.Lada + ");(Active," + divisionInformation.Active + ")";

            AgroDataAccessComponent.DivisionAddresDALC.Instance.update(divisionInformation);
            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
        }

        public int addDivisionInformation(DivisionAddressInfo divisionInformation, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            divisionInformation.DivisionInformationId = AgroDataAccessComponent.DivisionAddresDALC.Instance.insert(divisionInformation);
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformationDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionInformationId," + divisionInformation.DivisionInformationId + ")";
            activityLog.FieldsAffected = "(DivisionId," + divisionInformation.DivisionId + ");(Address,"
                + divisionInformation.Address + ");(Suburb," + divisionInformation.Suburb + ");(ZipCode,"
                + divisionInformation.ZipCode + ");(Telephone, " + divisionInformation.Telephone + ");(Fax,"
                + divisionInformation.Fax + ");(Web, " + divisionInformation.Web + ");(Email," + divisionInformation.Email + ");(City, "
                + divisionInformation.City + ");(State," + divisionInformation.State + ");(Lada, " + divisionInformation.Lada + ");(Active," + divisionInformation.Active + ")";


            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            return divisionInformation.DivisionInformationId;
        }

        public void deleteDivisionInformation(DivisionAddressInfo bussinesEntity, int userId, string hash)
        {
            AgroDataAccessComponent.DivisionAddresDALC.Instance.delete(bussinesEntity);

            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();
            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionInformationDEAQ);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Eliminar);
            activityLog.PrimaryKeyAffected = "(DivisionInformationId," + bussinesEntity.DivisionInformationId + ")";


            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

        }

        #endregion

        public static readonly LaboratoryDivisionBLC Instance = new LaboratoryDivisionBLC();
    }
}
