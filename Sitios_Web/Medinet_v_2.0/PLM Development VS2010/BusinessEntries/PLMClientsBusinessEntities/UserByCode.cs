using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserByCode
    {
        #region Constructors

        public UserByCode() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
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
        public byte CodeStatusId
        {
            get { return this._codeStatusId; }
            set { this._codeStatusId = value; }
        }

        #endregion

        private int _userId;
        private int _codeId;
        private string _nickName;
        private string _password;
        private string _codeString;
        private byte _codeStatusId;
        

    }
}
