using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [DataContract]
    public class UserDeviceInfo
    {
        #region Constructors

        public UserDeviceInfo()
        {
            this._addedDate = null;
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
        public byte DeviceId
        {
            get { return this._deviceId; }
            set { this._deviceId = value; }
        }

        [DataMember]
        public string MacAddress
        {
            get { return this._macAddress; }
            set { this._macAddress = value; }
        }

        [DataMember]
        public DateTime? AddedDate
        {
            get { return this._addedDate; }
            set { this._addedDate = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _userId;
        private int _codeId;
        private byte _deviceId;
        private string _macAddress;
        private DateTime? _addedDate;
        private bool _active;

    }
}
