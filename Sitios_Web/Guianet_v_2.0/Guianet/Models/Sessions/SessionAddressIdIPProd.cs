using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionAddressIdIPProd
    {
        #region Constructor
        public SessionAddressIdIPProd(int? AddressId)
        {
            Address = AddressId;
        }

        #endregion

        #region Member

        private int? Address;

        #endregion

        #region Properties

        public int? AddressId
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }
        #endregion
    }
}