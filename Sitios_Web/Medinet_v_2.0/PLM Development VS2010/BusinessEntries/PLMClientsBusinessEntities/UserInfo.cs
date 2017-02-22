using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserInfo
    {
        #region Constructors

        public UserInfo() { }

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
        public int NickPrefixId
        {
            get { return this._nickPrefixId; }
            set { this._nickPrefixId = value; }
        }

        [DataMember]
        public string Password
        {
            get { return this._password; }
            set { this._password = value; }
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
        private int _nickPrefixId;
        private string _password;
        private bool _active;

    }
}
