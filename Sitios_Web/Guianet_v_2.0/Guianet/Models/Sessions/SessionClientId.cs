using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionClientId
    {
        #region Constructor
        public SessionClientId(int? ClientId)
        {
            Client = ClientId;
        }

        #endregion

        #region Member

        private int? Client;

        #endregion

        #region Properties

        public int? ClientId
        {
            get
            {
                return Client;
            }
            set
            {
                Client = value;
            }
        }
        #endregion
    }
}