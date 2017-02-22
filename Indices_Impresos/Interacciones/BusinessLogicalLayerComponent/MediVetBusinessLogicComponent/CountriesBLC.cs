using System;
using System.Collections.Generic;
using System.Text;
using MediVetBusinessEntries;


namespace MediVetBusinessLogicComponent
{
    public class CountriesBLC
    {
        #region Constructor

        private CountriesBLC() { }

        #endregion

        #region Public Methods

     

        public List<CountriesInfo> getCountries()
        {
            //return MedinetDataAccessComponent.AlphabetDALC.Instance.getAll();
        }

        #endregion

        public static readonly CountriesBLC Instance = new CountriesBLC();
    }
}
