using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEIABusinessEntries.ROC
{
    [DataContract]
    public class CompanyPhoneInfo
    {
        #region Constructors

        public CompanyPhoneInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public string TypeName
        {
            get { return this._typeName; }
            set { this._typeName = value; }
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

        
        private byte _phoneTypeId;
        private string _typeName;
        private string _phoneValue;
    }
}