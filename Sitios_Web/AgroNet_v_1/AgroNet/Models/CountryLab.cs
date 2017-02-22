using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class CountryLab
    {
           #region Constructor
        public CountryLab(string CountryId)
        {
            Country = CountryId;
        }

        #endregion

        #region Member

        private string Country;

        #endregion

        #region Properties

        public string CountryId
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