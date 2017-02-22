using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesBusinessLogicComponent
{
    public sealed class VersionsBLC
    {
        #region Constructors

        private VersionsBLC() { }

        #endregion

        #region Public Methods

        public VersionInfo getVersion(string code, int editionId, int dbId)
        {
            return SyncDatabasesDataAccessComponent.VersionsDALC.Instance.getByEditionByDb(editionId, dbId);
        }

        #endregion

        public static readonly VersionsBLC Instance = new VersionsBLC();
    }
}
