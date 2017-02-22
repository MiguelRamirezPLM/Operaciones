using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [DataContract]
    public class StateOSMobileUserCodeInfo
    {
        #region Constructors

        public StateOSMobileUserCodeInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int StateId
        {
            get { return this._stateId; }
            set { this._stateId = value; }
        }

        [DataMember]
        public byte OSMobileId
        {
            get { return this._OSMobileId; }
            set { this._OSMobileId = value; }
        }

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

        #endregion

        private int _stateId;
        private byte _OSMobileId;
        private int _userId;
        private int _codeId;

    }
}
