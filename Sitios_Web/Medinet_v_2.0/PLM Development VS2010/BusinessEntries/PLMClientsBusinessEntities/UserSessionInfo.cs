using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserSessionInfo
    {
        #region Constructors

        public UserSessionInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int UserSessionId
        {
            get { return this._userSessionId; }
            set { this._userSessionId = value; }
        }

        [DataMember]
        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value; }
        }

        [DataMember]
        public int SessionId
        {
            get { return this._sessionId; }
            set { this._sessionId = value; }
        }

        [DataMember]
        public DateTime SessionDate
        {
            get { return this._sessionDate; }
            set { this._sessionDate = value; }
        }

        #endregion

        private int _userSessionId;
        private int _userId;
        private int _sessionId;
        private DateTime _sessionDate;

    }
}
