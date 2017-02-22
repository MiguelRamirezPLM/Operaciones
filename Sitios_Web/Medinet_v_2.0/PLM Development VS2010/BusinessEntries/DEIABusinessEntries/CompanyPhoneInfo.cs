using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries
{
    [DataContract]
    public class CompanyPhoneInfo
    {
        #region Constructors

        public CompanyPhoneInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int CompanyId
        {
            get { return this._companyId; }
            set { this._companyId = value; }
        }

        [DataMember]
        public byte PhoneTypeId
        {
            get { return this._phoneTypeId; }
            set { this._phoneTypeId = value; }
        }

        [DataMember]
        public string PhoneValue
        {
            get { return this._phoneValue; }
            set { this._phoneValue = value; }
        }

        #endregion

        private int _companyId;
        private byte _phoneTypeId;
        private string _phoneValue;
    }
}