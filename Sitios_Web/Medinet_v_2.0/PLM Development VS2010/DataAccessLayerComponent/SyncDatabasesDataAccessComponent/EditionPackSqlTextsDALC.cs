using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesDataAccessComponent
{
    public class EditionPackSqlTextsDALC : SyncDatabasesDataAccessAdapter<EditionPackSQLTextInfo>
    {
        #region Constructors

        private EditionPackSqlTextsDALC() { }

        #endregion

        #region Public Methods

        public List<EditionPackSQLTextInfo> getUpdatePacks(string code, int packUdpId)
        {
            DbCommand dbCmd = EditionPackSqlTextsDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spGetUpdatePacks");

            EditionPackSqlTextsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@packUdpId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, packUdpId);
            EditionPackSqlTextsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@code", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            List<EditionPackSQLTextInfo> BECollection = new List<EditionPackSQLTextInfo>();

            using (IDataReader dataReader = EditionPackSqlTextsDALC.SyncDatabaseInstance.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
                
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override EditionPackSQLTextInfo getFromDataReader(IDataReader current)
        {
            EditionPackSQLTextInfo record = new EditionPackSQLTextInfo();

            record.PackSqlTextId = Convert.ToInt32(current["PackSqlTextId"]);
            record.PackUpdId = Convert.ToInt32(current["PackUpdId"]);
            record.SqlText = current["SqlText"].ToString();
            record.AddedDate = Convert.ToDateTime(current["AddedDate"]);
            record.PackOrder = Convert.ToInt32(current["PackCount"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly EditionPackSqlTextsDALC Instance = new EditionPackSqlTextsDALC();

    }
}
