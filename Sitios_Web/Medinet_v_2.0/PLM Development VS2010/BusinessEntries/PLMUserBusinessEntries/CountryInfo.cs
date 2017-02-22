using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class CountryInfo
    {
        private int _countryId;
        private string _countryName;
        private bool _active;
        private string _iD;
        private int _regionId;

        #region Constructors

        public CountryInfo() 
        {

        }

        #endregion


        #region Properties

        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public string CountryName
        {
            get { return this._countryName; }
            set { this._countryName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public int RegionId
        {
            get { return this._regionId; }
            set { this._regionId = value; }
        }

        [DataMember]
        public string ID
        {
            get { return this._iD; }
            set { this._iD = value; }
        }

        #endregion

    }
}
