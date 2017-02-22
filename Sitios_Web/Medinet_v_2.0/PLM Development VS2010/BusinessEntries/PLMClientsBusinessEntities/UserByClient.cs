using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserByClient
    {
        #region Constructors

        public UserByClient() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
        }

        [DataMember]
        public int CodeId
        {
            get { return this._codeId; }
            set { this._codeId = value; }
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
        public string CodeString
        {
            get { return this._codeString; }
            set { this._codeString = value; }
        }

        [DataMember]
        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        [DataMember]
        public bool UserActive
        {
            get { return this._userActive; }
            set { this._userActive = value; }
        }

        #endregion

        private int _userId;
        private int _clientId;
        private int _codeId;
        private string _nickName;
        private string _password;
        private string _codeString;
        private string _email;
        private bool _userActive;
    }
}
