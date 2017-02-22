using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CountriesPharmaBLC
    {

        #region Constructors

        private CountriesPharmaBLC() { }

        #endregion

        #region Public Methods

        public List<StateInfo> getCountriesPharma(int countryId)
        {
            return PLMClientsDataAccessComponent.CountriesPharmaDALC.Instance.getCountryAll(countryId);
        }

       

        #endregion

        public static readonly CountriesPharmaBLC Instance = new CountriesPharmaBLC();

    }
}
