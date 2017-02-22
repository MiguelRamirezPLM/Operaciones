using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroBusinessEntries;
using System.Data;
using System.Web;
using System.IO;
using Excel=Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace AgroBusinessLogicComponent
{
   public class FilesBLC
    {
        #region Contructors

        private FilesBLC() { }
        
        #endregion
        #region public methods

        public void addFile(AgroBusinessEntries.FileInfo dataFile,string prefixApplication) {
           // PLMUserBusinessEntries.ActivityLogInfo activityLog = new PLMUserBusinessEntries.ActivityLogInfo();

            AgroDataAccessComponent.FilesDALC.Instance.insertFileInfo(dataFile,prefixApplication);

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
            return AgroDataAccessComponent.FilesDALC.Instance.getFormatFileId(formatFile);
        }

        public VersionFileInfo getVersionFile(string prefixApplication,int editionId) {
            return AgroDataAccessComponent.FilesDALC.Instance.getVersionFile(prefixApplication,editionId);

        }
        public string getUserName(int userId) {
            return AgroDataAccessComponent.FilesDALC.Instance.getUserName(userId);
        
        }

        public string getUserAgentByDivisionByEditionByAction(int editionId,int divisionId,int categoryId,int pharmaFormId,int productId) {
            return AgroDataAccessComponent.FilesDALC.Instance.getUserAgentByDivisionByEditionByAction(editionId,divisionId,categoryId,pharmaFormId,productId);
        }


        #endregion

        public static readonly FilesBLC Instance = new FilesBLC();
    }
}
