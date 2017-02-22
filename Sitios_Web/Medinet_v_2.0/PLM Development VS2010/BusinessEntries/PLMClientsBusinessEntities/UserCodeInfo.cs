using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [Serializable]
    [DataContract]
    public class UserCodeInfo
    {
        #region Constructors

        public UserCodeInfo() 
        {
            
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
        public int CodeId
        {
            get { return this._codeId; }
            set { this._codeId = value; }
        }

        [DataMember]
        public DateTime? InitialDate
        {
            get { return this._initialDate; }
            set { this._initialDate = value; }
        }

        [DataMember]
        public DateTime? FinalDate
        {
            get { return this._finalDate; }
            set { this._finalDate = value; }
        }

        #endregion

        private int _userId;
        private int _codeId;
        private DateTime? _initialDate;
        private DateTime? _finalDate;

    }
}
