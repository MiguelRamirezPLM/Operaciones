using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class PhonesByClientInfo
    {
        #region Constructors

        public PhonesByClientInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public byte PhoneTypeId
        {
            get
            {
                return this._PhoneTypeId;
            }
            set
            {
                this._PhoneTypeId = value;
            }
        }

        [DataMember]
        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        [DataMember]
        public string Lada
        {
            get
            {
                return this._Lada;
            }
            set
            {
                this._Lada = value;
            }
        }

        [DataMember]
        public string PhoneType
        {
            get
            {
                return this._PhoneType;
            }
            set
            {
                this._PhoneType = value;
            }
        }

        #endregion

        private byte _PhoneTypeId;

        private string _Number;

        private string _Lada;

        private string _PhoneType;

    }
}
