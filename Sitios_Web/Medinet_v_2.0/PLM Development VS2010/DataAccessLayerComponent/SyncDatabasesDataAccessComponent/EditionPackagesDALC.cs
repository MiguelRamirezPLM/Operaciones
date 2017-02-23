using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesDataAccessComponent
{
    public class EditionPackagesDALC : SyncDatabasesDataAccessAdapter<EditionPackageInfo>
    {
        #region Constructors

        private EditionPackagesDALC(){}

        #endregion

        #region Public Methods

        public List<SQLPackageInfo> getEditionPacks(string code, int editionId, int dbId)
        {
            DbCommand dbCmd = EditionPackSqlTextsDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spGetEditionPacks");

            EditionPackagesDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionPackagesDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@dbId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, dbId);
            EditionPackagesDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@code", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);


            List<SQLPackageInfo> BECollection = new List<SQLPackageInfo>();

            using (IDataReader dataReader = EditionPackagesDALC.SyncDatabaseInstance.ExecuteReader(dbCmd))
            {
                SQLPackageInfo sqlPack;

                while (dataReader.Read())
                {
                    sqlPack = new SQLPackageInfo();

                    sqlPack.PackUpdId = Convert.ToInt32(dataReader["PackUpdId"]);
                    sqlPack.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    sqlPack.DbId = Convert.ToInt32(dataReader["DbId"]);
                    sqlPack.TableId = Convert.ToByte(dataReader["TableId"]);
                    sqlPack.Active = Convert.ToBoolean(dataReader["Active"]);

                    sqlPack.SqlPacks = EditionPackSqlTextsDALC.Instance.getUpdatePacks(code, sqlPack.PackUpdId);

                    BECollection.Add(sqlPack);

                }

            }

            return BECollection;


        }


        #endregion

        #region Protected Methods

        protected override EditionPackageInfo getFromDataReader(IDataReader current)
        {
            EditionPackageInfo record = new EditionPackageInfo();

            record.PackUpdId = Convert.ToInt32(current["PackUpdId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DbId = Convert.ToInt32(current["DbId"]);
            record.TableId = Convert.ToByte(current["TableId"]);
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }


        #endregion


        public static readonly EditionPackagesDALC Instance = new EditionPackagesDALC();
    }
}
