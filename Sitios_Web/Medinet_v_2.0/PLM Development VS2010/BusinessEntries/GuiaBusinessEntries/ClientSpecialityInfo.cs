using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GuiaBusinessEntries
{
    [DataContract]
    public class ClientSpecialityInfo
    {

        #region Constructors

        public ClientSpecialityInfo() { }

        #endregion

        #region Properties

        [DataMember]
        public int ClientId
        {
            get
            {
                return this._clientId;
            }
            set
            {
                this._clientId = value;
            }
        }

        [DataMember]
        public string CompanyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                this._companyName = value;
            }
        }

        [DataMember]
        public int ClientTypeId
        {
            get
            {
                return this._clientTypeId;
            }
            set
            {
                this._clientTypeId = value;
            }
        }

        [DataMember]
        public int SpecialityId
        {
            get
            {
                return this._specialityId;
            }
            set
            {
                this._specialityId = value;
            }
        }

        [DataMember]
        public string SpecialityDescription
        {
            get
            {
                return this._specialityDescription;
            }
            set
            {
                this._specialityDescription = value;
            }
        }

       

        #endregion

        private int _clientId;
        private string _companyName;
        private int _clientTypeId;
        private int _specialityId;
        private string _specialityDescription;
      

    }
}
