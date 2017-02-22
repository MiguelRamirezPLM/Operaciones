using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SyncDatabaseBusinessEntries
{
    [DataContract]
    public class CountryInfo
    {
        #region Constructors

        public CountryInfo() { }

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
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
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
        private string _id;
        private bool _active;

    }
}
