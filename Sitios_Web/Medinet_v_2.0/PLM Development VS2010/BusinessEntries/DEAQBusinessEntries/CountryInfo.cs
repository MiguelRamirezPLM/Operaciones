using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEAQBusinessEntries
{ 
    [DataContract]
    public class CountryInfo
    {
        #region Constructor

        public CountryInfo() {}

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
        public byte? CountryCode
        {
            get { return this._countryCode; }
            set { this._countryCode = value; }
        }

        [DataMember]
        public string ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _countryId;
        private string _countryName;
        private byte? _countryCode;
        private string _ID;
        private bool _active;

    }
}
