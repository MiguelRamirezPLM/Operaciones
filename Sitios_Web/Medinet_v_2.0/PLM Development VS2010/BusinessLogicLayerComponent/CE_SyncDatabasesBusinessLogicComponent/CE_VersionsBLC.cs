using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesBusinessLogicComponent
{
    public sealed class CE_VersionsBLC
    {
        #region Constructors

        private CE_VersionsBLC() { }

        #endregion

        #region Public Methods

        public VersionInfo getVersion(string code)
        {
            return CE_SyncDatabasesDataAccessComponent.CE_VersionsDALC.Instance.getByCode(code);
        }

        public void updateVersion(string code, VersionInfo businessEntity)
        {
            CE_SyncDatabasesDataAccessComponent.CE_VersionsDALC.Instance.update(businessEntity);
        }

        #endregion

        public static readonly CE_VersionsBLC Instance = new CE_VersionsBLC();
    }
}
