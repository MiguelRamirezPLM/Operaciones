using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class DivisionsbyCountry
    {
          #region Constructor
        public DivisionsbyCountry(int Country)
        {
            CountryId = Country;
        }

        #endregion

        #region Member

        private int CountryId;

        #endregion

        #region Properties

        public int Country
        {
            get
            {
                return CountryId;
            }
            set
            {
                CountryId = value;
            }
        }       
        #endregion
    }
}