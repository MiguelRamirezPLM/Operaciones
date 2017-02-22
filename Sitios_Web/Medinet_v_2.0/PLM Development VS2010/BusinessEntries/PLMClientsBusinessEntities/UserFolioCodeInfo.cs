using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserFolioCodeInfo
    {
        #region Constructors

        public UserFolioCodeInfo() { }

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
        public int FolioId
        {
            get { return this._folioId; }
            set { this._folioId = value; }
        }

        #endregion

        private int _userId;
        private int _codeId;
        private int _folioId;

    }
}
