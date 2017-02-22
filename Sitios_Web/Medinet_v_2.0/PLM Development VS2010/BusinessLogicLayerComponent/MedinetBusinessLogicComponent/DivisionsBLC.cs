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
        public string getSizes()
        {
            return MedinetDataAccessComponent.DivisionsDALC.Instance.getSizes("Logos");
        }

        public DivisionsInfo getDivision(int divisionId)
        {
            if (divisionId <= 0)
                throw new ArgumentException("The division does not exist.");

            return MedinetDataAccessComponent.DivisionsDALC.Instance.getOne(divisionId);
        }
        public void addLogoToDivision(MedinetBusinessEntries.LogoInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            //BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insertImage(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionImages);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionImageId," + BEntity.LogoId + ")";
            activityLog.FieldsAffected = "(ProductShot," + BEntity.divisionShot+ "); (Active," + BEntity.Active + "); (PresentationId,"
                + BEntity.DivisionId + ");";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            MedinetDataAccessComponent.DivisionsDALC.Instance.insertLogo(BEntity);

        }

        public void deleteLogoToDivision(MedinetBusinessEntries.LogoInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            //BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insertImage(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionImages);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionImageId," + BEntity.LogoId + ")";
            activityLog.FieldsAffected = "(ProductShot," + BEntity.divisionShot + "); (Active," + BEntity.Active + "); (PresentationId,"
                + BEntity.DivisionId + ");";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            MedinetDataAccessComponent.DivisionsDALC.Instance.deleteLogo(BEntity);

        }
        public void updateLogoToDivision(MedinetBusinessEntries.LogoInfo BEntity, int userId, string hash)
        {
            PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            //BEntity.PresentationId = MedinetDataAccessComponent.PresentationsDALC.Instance.insertImage(BEntity);

            activityLog.UserId = userId;
            activityLog.HashKey = hash;
            activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.DivisionImages);
            activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            activityLog.PrimaryKeyAffected = "(DivisionImageId," + BEntity.LogoId + ")";
            activityLog.FieldsAffected = "(ProductShot," + BEntity.divisionShot + "); (Active," + BEntity.Active + "); (PresentationId,"
                + BEntity.DivisionId+ ");";

            PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);
            MedinetDataAccessComponent.DivisionsDALC.Instance.updateLogo(BEntity);

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
        public List<DivisionsInfo> getParticipantDivisions(int countryId, int editionId)
        {
            if (countryId <= 0 )
                throw new ArgumentException("The country does not exist.");
            
            List<DivisionsInfo> divisions = MedinetDataAccessComponent.DivisionsDALC.Instance.getLogosByDivision(countryId,editionId);
            return divisions;
        }
        public List<DivisionsInfo> getDivisionLogos(int editionId, int divisionId, int countryId,int bookId)
        {
            if (divisionId <= 0 )
                throw new ArgumentException("The division does not exist.");
            List<DivisionsInfo> divisions = MedinetDataAccessComponent.DivisionsDALC.Instance.getDivisionLogos(editionId,divisionId,countryId,bookId);
            return divisions;
        }


        #endregion

        public static readonly DivisionsBLC Instance = new DivisionsBLC();

    }
}
