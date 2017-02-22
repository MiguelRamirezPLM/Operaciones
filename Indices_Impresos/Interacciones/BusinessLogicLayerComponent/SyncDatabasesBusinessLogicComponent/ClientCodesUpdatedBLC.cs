using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesBusinessLogicComponent
{
    public sealed class ClientCodesUpdatedBLC
    {
        #region Constructors

        private ClientCodesUpdatedBLC() { }

        #endregion

        #region Public Methods

        public List<ClientCodesUpdatedInfo> saveUpdates(string code, ClientCodesUpdatedInfo[] updates)
        {
            return SyncDatabasesDataAccessComponent.ClientCodesUpdatedDALC.Instance.saveUpdates(updates);
        }

        #endregion

        public static readonly ClientCodesUpdatedBLC Instance = new ClientCodesUpdatedBLC();
    }
}
