using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ClientTypeInfo
    {
        #region Constructors

        public ClientTypeInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public byte ClientTypeId
        {
            get
            {
                return this._ClientTypeId;
            }
            set
            {
                this._ClientTypeId = value;
            }
        }

        [DataMember]
        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                this._TypeName = value;
            }
        }

        [DataMember]
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this._Active = value;
            }
        }

        #endregion

        private byte _ClientTypeId;

        private string _TypeName;

        private bool _Active;

    }
}
