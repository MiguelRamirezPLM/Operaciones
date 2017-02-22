using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace MedinetDataAccessComponent
{
    public class FilesDALC : MedinetDataAccessAdapter<FileInfo>
    {
        #region Constructors
        private FilesDALC() { }
        #endregion

        #region Public methods
        public void insertFileInfo(FileInfo dataFile,string prefixApplication) {
            DbCommand dbCmd = FilesDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spCRUDGenerateFiles");
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@VersionFileId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.VersionFileId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@FormatFileId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.FormatFileId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@FileName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.FileName);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@NickName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.NickName);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@FileDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.FileDate);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@BaseUrl", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.BaseUrl);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Version", DbType.AnsiString,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, dataFile.Version);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Active", DbType.Byte,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);

            DbCommand dbCmdUpdate = FilesDALC.MedInstanceDatabase.GetStoredProcCommand("plm_spSetVersionFile");
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmdUpdate, "@PrefixApplication", DbType.AnsiString,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixApplication);
            FilesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
            FilesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmdUpdate);
            

        
        }

        public int getFormatFileId(string formatFile) {
            DbCommand dbCmd = FilesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetFormatFileId");
            int formatFileId=0;
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@FormatFile", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, formatFile);
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = FilesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                if (dataReader.Read())
                {
                    formatFileId = int.Parse(dataReader["FormatFileId"].ToString());
                }
            }
            return formatFileId;
        }

        public VersionFileInfo getVersionFile(string prefixApplication,int editionId) {
            VersionFileInfo versFileIn = new VersionFileInfo();
            DbCommand dbCmd = FilesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetVersionFile");
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@PrefixApplication", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixApplication);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@EditionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = FilesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                if (dataReader.Read())
                {
                    versFileIn.ApplicationName = dataReader["ApplicationName"].ToString();
                    versFileIn.CurrentValue = int.Parse(dataReader["CurrentValue"].ToString());
                    versFileIn.Edition = int.Parse(dataReader["EditionId"].ToString());
                    versFileIn.PrefixApplication = dataReader["PrefixApplication"].ToString();
                    versFileIn.VersionFileId = int.Parse(dataReader["VersionFileId"].ToString());
                }
            }
            return versFileIn;
        
        }

        public string getUserAgentByDivisionByEditionByAction(int editionId,int divisionId, int categoryId,int pharmaFormId,int productId)
        {
            string AgentName = "";
            DbCommand dbCmd = FilesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgentUserByDivisionByEditionByAction");
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@actionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            FilesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@moduleId", DbType.Int32,
          ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = FilesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                if (dataReader.Read())
                {
                    AgentName = dataReader["UserName"].ToString();
                }
            }
            return AgentName;

        }


        public string getUserName(int userId)
        {
            string userName = "";
            Database UsersInstanceDatabase = DatabaseFactory.CreateDatabase("PLMConectorDB.Properties.Settings.PLMUsersConnectionString");
            DbCommand dbCmd = UsersInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUsers");
            UsersInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte, ParameterDirection.Input, string.Empty, DataRowVersion.Current, 1);
            UsersInstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32, ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            using (IDataReader dataReader = UsersInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                if (dataReader.Read())
                {
                    userName = dataReader["Name"].ToString() + " " + dataReader["LastName"].ToString() + " " + dataReader["SecondLastName"].ToString();
                }
            }
            return userName;
        }
        #endregion
        public static readonly FilesDALC Instance = new FilesDALC();
    }
}
