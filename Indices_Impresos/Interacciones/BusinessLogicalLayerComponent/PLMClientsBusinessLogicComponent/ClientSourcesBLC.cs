using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public sealed class ClientSourcesBLC
    {
        #region Constructors

        private ClientSourcesBLC() { }

        #endregion

        #region Public methods

        public void addClientSource(ClientSourceInfo entry)
        {
            PLMClientsDataAccessComponent.ClientSourcesDALC.Instance.insert(entry);
        }

        public void removeClientSource(ClientSourceInfo entry)
        {
            PLMClientsDataAccessComponent.ClientSourcesDALC.Instance.delete(entry);
        }

        #endregion

        public static readonly ClientSourcesBLC Instance = new ClientSourcesBLC();
    }
}
