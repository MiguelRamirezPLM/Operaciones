using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ClientByBrandInfo
    {
        #region Constructors

        public ClientByBrandInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ClientId
        {
            get { return this._clientId; }
            set { this._clientId = value; }
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
        public byte ClientTypeId
        {
            get { return this._clientTypeId; }
            set { this._clientTypeId = value; }
        }

        #endregion

        private int _clientId;
        private string _companyName;
        private string _shortName;
        private byte _clientTypeId;

    }
}
