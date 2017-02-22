using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models.Sessions
{
    public class sessionCountryId
    {
        #region Constructor
        public sessionCountryId(int CountryId,int DivisionId)
        {
            country = CountryId;
            division = DivisionId;
        }

        #endregion

        #region Member

        public int country;
        public int division;

        #endregion

        #region Properties

        public int CountryId
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        public int DivisionId
        {
            get
            {
                return division;
            }
            set
            {
                division = value;
            }
        }
   
        #endregion
    }
}