using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMUserBusinessEntries
{
    [DataContract]
    public class CountriesByUserInfo
    {
        #region Constructors

        public CountriesByUserInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public string NickName
        {
            get { return this._nickName; }
            set { this._nickName = value; }
        }

        [DataMember]
        public string Countries
        {
            get { return this._countries; }
            set { this._countries = value; }
        }

        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _userId;
        private string _nickName;
        private string _countries;
        private int _countryId;
        private bool _active;


    }
}
