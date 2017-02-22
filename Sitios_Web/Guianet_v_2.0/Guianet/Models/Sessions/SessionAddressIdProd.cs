using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models.Sessions
{
    public class SessionAddressIdProd
    {
        #region Constructor
        public SessionAddressIdProd(int? AddressId, int? CountryId)
        {
            Address = AddressId;
            Country = CountryId;
        }

        #endregion

        #region Member

        private int? Address;
        private int? Country;

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

        public int? CountryId
        {
            get
            {
                return Country;
            }
            set
            {
                Country = value;
            }
        }
        #endregion
    }
}