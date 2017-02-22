using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public  class IntPhonesByClientInfo
    {
        #region Constructor

        public IntPhonesByClientInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int IntClientPhoneId
        {
            get { return this._intClientPhoneId; }
            set { this._intClientPhoneId = value; }
        }

        [DataMember]
        public byte PhoneTypeId
        {
            get { return this._phoneTypeId; }
            set { this._phoneTypeId = value; }
        }

        [DataMember]
        public int IntClientId
        {
            get { return this._intclientId; }
            set { this._intclientId = value; }
        }

        [DataMember]
        public string Number
        {
            get { return this._number; }
            set { this._number = value; }
        }

        [DataMember]
        public string Lada
        {
            get { return this._lada; }
            set { this._lada = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._acive; }
            set { this._acive = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        #endregion

        private int _intClientPhoneId;
        private byte _phoneTypeId;
        private int _intclientId;
        private string _number;
        private string _lada;
        private bool _acive;
        private string _description;

    }
}
