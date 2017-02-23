using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncDatabaseFactory
{
    public class Factory
    {
        #region Public Methods

        public static CE_SyncDatabasesDataAccessComponent.ISyncDatabases getInstance(string instance)
        {
            switch (instance)
            {
                case "MEDINET":
                    return new CE_PharmaSearchEngineDataAccessComponent.CE_UpdatesDALC();
                default:
                    return null;
            }

        }

        #endregion

    }
}
