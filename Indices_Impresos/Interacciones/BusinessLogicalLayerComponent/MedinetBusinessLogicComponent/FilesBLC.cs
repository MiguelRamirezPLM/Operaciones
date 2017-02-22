using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Web;
using System.IO;
using Excel=Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace MedinetBusinessLogicComponent
{
   public class FilesBLC
    {
        #region Contructors

        private FilesBLC() { }
        
        #endregion
        #region public methods

        public void addFile(MedinetBusinessEntries.FileInfo dataFile,string prefixApplication) {
           // PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            MedinetDataAccessComponent.FilesDALC.Instance.insertFileInfo(dataFile,prefixApplication);

            //activityLog.UserId = userId;
            //activityLog.HashKey = hash;
            //activityLog.TableId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Tables.Products);
            //activityLog.OperationId = Convert.ToInt32(PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.Actions.Agregar);
            //activityLog.PrimaryKeyAffected = "(ProductId," + BEntity.ProductId + ")";
            //activityLog.FieldsAffected = "(LaboratoryId," + BEntity.LaboratoryId + ");(AlphabetId," + BEntity.AlphabetId + ");(Brand,"
            //    + BEntity.Brand + ");(SanitaryRegistry, " + BEntity.SanitaryRegistry + ");(Description," + BEntity.Description + ");(Active," + BEntity.Active + ")";

            //PLMUsersBusinessLogicComponent.ActivityLogsBLC.Instance.addActivity(activityLog);

            //return BEntity.ProductId;
        
        }
        public int getFormatFileId(string formatFile) {
            return MedinetDataAccessComponent.FilesDALC.Instance.getFormatFileId(formatFile);
        }

        public VersionFileInfo getVersionFile(string prefixApplication,int editionId) {
            return MedinetDataAccessComponent.FilesDALC.Instance.getVersionFile(prefixApplication,editionId);

        }
        public string getUserName(int userId) {
            return MedinetDataAccessComponent.FilesDALC.Instance.getUserName(userId);
        
        }

        public string getUserAgentByDivisionByEditionByAction(int editionId,int divisionId,int categoryId,int pharmaFormId,int productId) {
            return MedinetDataAccessComponent.FilesDALC.Instance.getUserAgentByDivisionByEditionByAction(editionId,divisionId,categoryId,pharmaFormId,productId);
        }


        #endregion

        public static readonly FilesBLC Instance = new FilesBLC();
    }
}
