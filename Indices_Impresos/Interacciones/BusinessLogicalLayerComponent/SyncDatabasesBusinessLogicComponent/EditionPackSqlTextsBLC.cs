using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesBusinessLogicComponent
{
    public sealed class EditionPackSqlTextsBLC
    {
        #region Constructors

        private EditionPackSqlTextsBLC() { }

        #endregion

        #region Public Methods

        public List<EditionPackSQLTextInfo> getUpdates(string code, int editionId, int dbId)
        {
            return SyncDatabasesDataAccessComponent.EditionPackSqlTextsDALC.Instance.getUpdatePacks(editionId, dbId, code);
        }

        #endregion

        public static readonly EditionPackSqlTextsBLC Instance = new EditionPackSqlTextsBLC();
    }
}
