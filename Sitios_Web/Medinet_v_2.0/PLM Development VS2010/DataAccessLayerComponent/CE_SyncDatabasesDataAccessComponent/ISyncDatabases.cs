using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public interface ISyncDatabases
    {
        UpdateMessageInfo executeQuery(string slqText);


    }
}
