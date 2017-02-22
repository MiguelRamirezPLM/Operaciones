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

        public List<EditionPackSQLTextInfo> getUpdatePacks(string code, int dbId)
        {
            return SyncDatabasesDataAccessComponent.EditionPackSqlTextsDALC.Instance.getUpdatePacks( code,dbId);
        }

        #endregion

        public static readonly EditionPackSqlTextsBLC Instance = new EditionPackSqlTextsBLC();
    }
}
