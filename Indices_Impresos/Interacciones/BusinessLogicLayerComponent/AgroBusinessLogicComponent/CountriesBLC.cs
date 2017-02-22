using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroBusinessLogicComponent
{
  public class CountriesBLC
    {
          #region Contructors

      private CountriesBLC() { }

        #endregion

        #region Public Methods
      #region select
      public List<AgroEntityFramework.Countries> getCountries(string countries){
        return AgroDataAccessComponent.CountriesDALC.Instance.SelectCountries(countries);
      }

      public List<AgroEntityFramework.Countries> getCountriesV()
      {
          return AgroDataAccessComponent.CountriesDALC.Instance.getCountries();
      }

        #endregion
        #endregion

      public static readonly CountriesBLC Instance = new CountriesBLC(); 
    }
}
