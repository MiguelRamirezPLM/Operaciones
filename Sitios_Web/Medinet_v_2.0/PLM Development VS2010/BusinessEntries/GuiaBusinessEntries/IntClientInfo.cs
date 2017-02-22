using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class IntClientInfo
    {
        #region Constructors

        public IntClientInfo() { }

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

        [DataMember]
        public string ShortName
        {
            get { return this._shortName; }
            set { this._shortName = value; }
        }

        [DataMember]
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        #endregion

        private int _intClientId;
        private string _companyName;
        private string _shortName;
        private bool _active;

    }
}
