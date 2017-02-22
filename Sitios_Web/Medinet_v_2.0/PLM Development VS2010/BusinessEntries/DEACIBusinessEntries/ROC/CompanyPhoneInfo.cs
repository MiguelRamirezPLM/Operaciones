using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DEACIBusinessEntries.ROC
{
    [DataContract]
    public  class CompanyPhoneInfo
    {

        #region Constructor

        public CompanyPhoneInfo() { }

        #endregion 

        #region Porperties

        [DataMember]
        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        
        }

        [DataMember]
        public string PhoneValue
        {
            get { return this._phoneValue; }
            set { this._phoneValue = value; }

        }


        #endregion


        private string _description;
        private string _phoneValue;


    }
}
