using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class CountryBLC
    {
        #region Constructors

        private CountryBLC() { }

        #endregion

        #region Public Methods

        public List<CountryInfo> getCountries()
        {
            return PLMUsersDataAccessComponent.CountryDALC.Instance.getCountries();
        }

        #endregion

        public static readonly CountryBLC Instance = new CountryBLC();
    }
}
