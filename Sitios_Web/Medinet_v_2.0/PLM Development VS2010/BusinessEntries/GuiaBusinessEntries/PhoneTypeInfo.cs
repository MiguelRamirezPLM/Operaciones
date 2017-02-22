using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class PhoneTypeInfo
    {
        #region Constructors

        public PhoneTypeInfo() { }

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
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
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

        private byte _PhoneTypeId;

        private string _Description;

        private bool _Active;

    }
}
