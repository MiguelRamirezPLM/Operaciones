using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public class CityByCountryInfo
    {

        #region Constructor

        public CityByCountryInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CityId
        {
            get
            {
                return this._cityId;
            }
            set
            {
                this._cityId = value;
            }
        }

        [DataMember]
        public string CityName
        {
            get 
            {
                return this._cityName;
            }
            set
            {
                this._cityName = value;
            }
        }

        [DataMember]
        public int StateId
        {
            get
            {
                return this._stateId;
            }
            set
            {
                this._stateId = value;
            }
        }

        [DataMember]
        public string StateName
        {
            get
            {
                return this._stateName;
            }
            set
            {
                this._stateName = value;
            }
        }

        [DataMember]
        public string CountryName
        {
            get
            {
                return this._countryName;
            }
            set
            {
                this._countryName = value;
            }
        }

        #endregion

        private int _cityId;
        private string _cityName;
        private int _stateId;
        private string _stateName;
        private string _countryName;

    }
}
