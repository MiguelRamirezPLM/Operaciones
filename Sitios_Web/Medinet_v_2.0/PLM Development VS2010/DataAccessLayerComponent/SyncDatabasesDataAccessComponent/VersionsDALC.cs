using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesDataAccessComponent
{
    public class VersionsDALC : SyncDatabasesDataAccessAdapter<VersionInfo>
    {
        #region Constructors

        private VersionsDALC() { }

        #endregion

        #region Public Methods

        public VersionInfo getByEditionByDb(int editionId, int dbId)
        {
            DbCommand dbCmd = VersionsDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spGetVersionsByEdition");

            VersionsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            VersionsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@dbId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dbId);

            using (IDataReader dataReader = VersionsDALC.SyncDatabaseInstance.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        #endregion

        #region Protected Methods

        protected override VersionInfo getFromDataReader(IDataReader current)
        {
            VersionInfo record = new VersionInfo();

            record.VersionId = Convert.ToInt32(current["VersionId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DbId = Convert.ToInt32(current["DbId"]);
            record.PackCount = Convert.ToInt32(current["PackCount"]);
            record.VersionNumber = current["VersionNumber"].ToString();
            record.LastUpdate = Convert.ToDateTime(current["LastUpdate"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly VersionsDALC Instance = new VersionsDALC(); 
    }
}
