using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
   public class IntClientByEditionInfo
    {
        #region Constructor

        public IntClientByEditionInfo() { }

        #endregion

        #region Properties

        

        [DataMember]
        public int IntClientId
        {
            get { return this._intClientId; }
            set { this._intClientId = value; }
        }

        [DataMember]
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }

       
        #endregion

        
        private int _intClientId;
        private string _companyName;
        

    }
}
