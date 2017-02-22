using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesBusinessLogicComponent
{
    public sealed class ClientCodesErrorsBLC
    {
        #region Constructors

        private ClientCodesErrorsBLC() { }

        #endregion

        #region Public Methods

        public List<ClientCodesErrorInfo> saveErrors(string code, ClientCodesErrorInfo[] errors)
        {
            return SyncDatabasesDataAccessComponent.ClientCodesErrorsDALC.Instance.saveErrors(errors);
        }

        #endregion

        public static readonly ClientCodesErrorsBLC Instance = new ClientCodesErrorsBLC();
    }
}
