using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CountriesDistributionBLC
    {

        #region Constructors

        private CountriesDistributionBLC() { }

        #endregion

        #region Public Methods

        public List<CountryInfo> getCountries()
        {
            return PLMClientsDataAccessComponent.CountriesDistributionDALC.Instance.getAll();
        }



        public List<CountryInfo> getCountryDistribution(string country)
        {


            return PLMClientsDataAccessComponent.CountriesDistributionDALC.Instance.getCountryAll(country);
        }


        #endregion

        public static readonly CountriesDistributionBLC Instance = new CountriesDistributionBLC();

    }
}
