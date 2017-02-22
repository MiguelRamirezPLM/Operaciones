using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PLMClientsBusinessEntities
{
    [DataContract]
    public class OSMobileUserCodesInfo
    {
        #region Constructors

        public OSMobileUserCodesInfo() { }

        #endregion

        #region Public properties

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

        [DataMember]
        public string IMEI
        {
            get { return this._IMEI; }
            set { this._IMEI = value; }
        }

        #endregion

        private byte _OSMobileId;
        private int _userId;
        private int _codeId;
        private string _IMEI;
    }
}
