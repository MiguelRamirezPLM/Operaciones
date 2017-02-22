using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace PLMUserBusinessEntries
{
    [DataContract]
    public sealed class UserInfo
    {
        private int _userId;
        private string _name;
        private string _lastName;
        private string _secondLastName;
        private string _nickName;
        private string _password;
        private string _eMail;
        private DateTime _addedDate;
        private DateTime _expirationDate;
        private DateTime _lastUpdate;
        private bool _active;
        private int _countryId;

        #region Constructors

        public UserInfo() 
        {
            _addedDate = Convert.ToDateTime("01/01/1900");
            _expirationDate = Convert.ToDateTime("01/01/1900");
            _lastUpdate = Convert.ToDateTime("01/01/1900");
        }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        [DataMember]
        public string SecondLastName
        {
            get { return this._secondLastName; }
            set { this._secondLastName = value; }
        }

        [DataMember]
        public string NickName
        {
            get { return this._nickName; }
            set { this._nickName = value; }
        }

        [DataMember]
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
        }

        [DataMember]
        public string Email
        {
            get { return this._eMail; }
            set { this._eMail = value; }
        }

        [DataMember]
        public DateTime AddedDate
        {
            get { return this._addedDate; }
            set { this._addedDate = value; }
        }

        [DataMember]
        public DateTime ExpirationDate
        {
            get { return this._expirationDate; }
            set { this._expirationDate = value; }
        }

        [DataMember]
        public DateTime LastUpdate
        {
            get { return this._lastUpdate; }
            set { this._lastUpdate = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        [DataMember]
        public int CountryId
        {
            get { return this._countryId; }
            set { this._countryId = value; }
        }

        #endregion


    }
}
