using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionProductId
    {
        #region Constructor
        public SessionProductId(int? ClientId, int? ProductId)
        {
            Client = ClientId;
            Product = ProductId;
        }

        #endregion

        #region Member

        private int? Client;
        public int? Product;

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

        public int? ProductId
        {
            get
            {
                return Product;
            }
            set
            {
                Product = value;
            }
        }
        #endregion
    }
}